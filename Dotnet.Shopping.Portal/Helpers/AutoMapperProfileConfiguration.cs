using AutoMapper;
using Dotnet.Shopping.Portal.Models.Catalog;
using Dotnet.Shopping.Portal.Models.Messages;
using Dotnet.Shopping.Portal.Models.Sale;
using Dotnet.Shopping.Portal.Models.User;
using Dotnet.Shopping.Portal.ViewModels;
using Dotnet.Shopping.Portal.ViewModels.Catalog;
using Dotnet.Shopping.Portal.ViewModels.ManageViewModels;
using Dotnet.Shopping.Portal.ViewModels.Sale;
using Dotnet.Shopping.Portal.ViewModels.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Shopping.Portal.Helpers
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            // billing address mappings
            CreateMap<BillingAddress, BillingAddressModel>()
                .ReverseMap();
            CreateMap<BillingAddress, CheckoutModel>()
                .ReverseMap();

            // category mappings
            CreateMap<Category, CategoryListModel>();
            CreateMap<Category, CategoryCreateOrUpdateModel>()
                .ReverseMap();

            // manufacturer mappings
            CreateMap<Manufacturer, ManufacturerListModel>();
            CreateMap<Manufacturer, ManufacturerCreateOrUpdateModel>()
                .ReverseMap();

            // order mapping
            CreateMap<OrderManageModel, Order>();

            // product mappings
            CreateMap<Product, ProductListModel>();
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Manufacturers, opt => opt.Ignore())
                .ForMember(dest => dest.Specifications, opt => opt.Ignore());
            CreateMap<Product, ProductCreateOrUpdateModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Specifications, opt => opt.Ignore());
            CreateMap<ProductCreateOrUpdateModel, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Specifications, opt => opt.Ignore());

            // review
            CreateMap<Review, ReviewModel>()
                .ReverseMap();

            // specifications
            CreateMap<Specification, SpecificationListModel>();
            CreateMap<Specification, SpecificationCreateOrUpdateModel>()
                .ReverseMap();

            // suport
            CreateMap<ContactUsMessage, ContactUsMessageModel>();
        }
    }
}
