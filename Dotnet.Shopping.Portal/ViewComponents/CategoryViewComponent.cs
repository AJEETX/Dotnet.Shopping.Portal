using AutoMapper;
using Dotnet.Shopping.Portal.Models.Catalog;
using Dotnet.Shopping.Portal.Services.Catalog;
using Dotnet.Shopping.Portal.ViewModels.Catalog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Shopping.Portal.ViewComponents
{
    [ViewComponent(Name = "Category")]
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(
                _categoryService.GetAllCategoriesWithoutParent().Where(x => x.Published)
            );
        }
    }

    [ViewComponent(Name = "Page")]
    public class PageViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public PageViewComponent(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var pageListModel = new List<ProductCreateOrUpdateModel>();
            var pageList = _productService.GetAllProducts();
                var pp = pageList.Where(p=>p.Name.ToLower()=="about");
            foreach (var item in pp)
            {
                var model = _mapper.Map<Product, ProductCreateOrUpdateModel>(item);
                pageListModel.Add(model);
            }
            return View(pageListModel);
        }
    }
}
