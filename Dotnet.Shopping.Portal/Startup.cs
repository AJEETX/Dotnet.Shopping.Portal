using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dotnet.Shopping.Portal.Data;
using Dotnet.Shopping.Portal.Models;
using Dotnet.Shopping.Portal.Services.Catalog;
using Dotnet.Shopping.Portal.Services.Messages;
using Dotnet.Shopping.Portal.Services.Sale;
using Dotnet.Shopping.Portal.Services.Statistics;
using Dotnet.Shopping.Portal.Services.User;
using AutoMapper;
using Dotnet.Shopping.Portal.Helpers;
using Microsoft.Extensions.Hosting.Internal;
using System;
using Dotnet.Shopping.Portal.Middleware;

namespace Dotnet.Shopping.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets("aspnet-aspCart.Web-b7b6c0c8-2794-41a1-ad6c-528772b97f8a");
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            WebHostEnvironment = env;
        }

        private IWebHostEnvironment WebHostEnvironment;
        public IConfigurationRoot Configuration { get; }
        public MapperConfiguration MapperConfiguration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI()
            .AddDefaultTokenProviders();

            services.Configure<AdminAccount>(Configuration.GetSection("AdminAccount"));
            services.Configure<UserAccount>(Configuration.GetSection("UserAccount"));
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            services.AddTransient<IBillingAddressService, BillingAddressService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IImageManagerService, ImageManagerService>();
            services.AddTransient<IManufacturerService, ManufacturerService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<ISpecificationService, SpecificationService>();

            services.AddTransient<IOrderCountService, OrderCountService>();
            services.AddTransient<IVisitorCountService, VisitorCountService>();

            services.AddTransient<IContactUsService, ContactUsService>();

            // singleton
            services.AddSingleton(sp => MapperConfiguration.CreateMapper());
            services.AddSingleton<ViewHelper>();
            services.AddSingleton<DataHelper>();
            services.AddSingleton(WebHostEnvironment.ContentRootFileProvider);
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseImageResize();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseSession();
            app.UseVisitorCounter();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            // apply migration
            SampleDataProvider.ApplyMigration(app.ApplicationServices);

            // seed default data
            SampleDataProvider.Seed(app.ApplicationServices, Configuration);
        }
    }
}
