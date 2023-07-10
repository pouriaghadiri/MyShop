using ShoppingSiteApi.Core.Services.Implementations;
using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.DataAccess.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingSiteApi.WebAPI.Controllers
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
