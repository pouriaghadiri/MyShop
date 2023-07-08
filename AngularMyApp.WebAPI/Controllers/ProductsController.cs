using AngularMyApp.Core.DTOs.Products;
using AngularMyApp.Core.Services.Interfaces;
using AngularMyApp.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularMyApp.WebAPI.Controllers
{

    public class ProductController : BaseCRUDController<Product , IProductService>
    {
        #region constructor

        private IProductService _productService;

        public ProductController(IProductService productService):base(productService)
        {
            _productService = productService;
        }

        #endregion

        #region filter products

        [HttpGet("FilterProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductsDTO filter)
        {
            var products = await _productService.FilterProductsDTO(filter);

            return Ok(products);
        }

        #endregion

    }
}
