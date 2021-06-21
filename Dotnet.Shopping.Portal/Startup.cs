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

namespace Dotnet.Shopping.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI()
            .AddDefaultTokenProviders();

            services.Configure<AdminAccount>(Configuration.GetSection("AdminAccount"));
            services.Configure<UserAccount>(Configuration.GetSection("UserAccount"));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageManagerService, ImageManagerService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            //services.AddScoped<OrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IBillingAddressService, BillingAddressService>();
            services.AddScoped<ISpecificationService, SpecificationService>();

            //services.AddScoped<OrderCountService, OrderCountService>();
            services.AddScoped<IVisitorCountService, VisitorCountService>();

            services.AddScoped<IContactUsService, ContactUsService>();

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
            app.UseStaticFiles();

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
        }
    }
}
