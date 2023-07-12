using ShoppingSiteApi.Core.DTOs.Products;
using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.DataAccess.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingSiteApi.WebAPI.Controllers
{

    public class ProductController : SiteBasicController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productProductService)
        {
            _productService = productProductService;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Get(int id)
        {
            var entity = await _productService.GetByID(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(ProductDTO entity)
        {
            try
            {
                var cat = await _productService.Create(entity);
                if (cat != null)
                {
                    return CreatedAtAction(nameof(Get), new { id = cat.Id }, cat);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Update(ProductUpdateDTO entity)
        {
            var cat = await _productService.Update(entity);
            if (cat != null)
            {
                return Ok(cat);
            }
            return NotFound();

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Product entity)
        {
            await _productService.Delete(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteByID(id);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
