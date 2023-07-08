using AngularMyApp.Core.Services.Implementations;
using AngularMyApp.Core.Services.Interfaces;
using AngularMyApp.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularMyApp.WebAPI.Controllers
{

    public class ProductCategoryController : BaseCRUDController<ProductCategory, IProductCategoryService>
    {
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService) : base(productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

    }
}
