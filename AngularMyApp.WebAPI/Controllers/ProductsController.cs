using AngularMyApp.Core.DTOs.Products;
using AngularMyApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularMyApp.WebAPI.Controllers
{

    public class ProductController : SiteBasicController
    {
        #region constructor

        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region products

        [HttpGet("FilterProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductsDTO filter)
        {
            var products = await _productService.FilterProductsDTO(filter);

            return Ok(products);
        }

        #endregion
    }
}
