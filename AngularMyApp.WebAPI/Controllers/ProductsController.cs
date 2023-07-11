using ShoppingSiteApi.Core.DTOs.Products;
using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.DataAccess.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingSiteApi.WebAPI.Controllers
{

    public class ProductController : BaseCRUDController<ProductDTO , IProductService , Product>
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
