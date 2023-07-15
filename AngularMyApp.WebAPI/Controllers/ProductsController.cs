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
        private readonly IProductCategoryService _productCategoryService;
        public ProductController(IProductService productProductService , IProductCategoryService productCategoryService)
        {
            _productService = productProductService;
            _productCategoryService = productCategoryService;
        }

        #region Products 

        
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
        #endregion

        #region Product Category

        [HttpGet("ProductCategories/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductCat(int id)
        {
            var entity = await _productCategoryService.GetByID(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);

        }

        [HttpPost("ProductCategories")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(ProductCategoryDTO entity)
        {
            try
            {
                var cat = await _productCategoryService.Create(entity);
                if (cat != null)
                {
                    return CreatedAtAction(nameof(GetProductCat), new { id = cat.Id }, cat);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("ProductCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Update(ProductCategoryDTO entity)
        {
            var cat = await _productCategoryService.Update(entity);
            if (cat != null)
            {
                return Ok(cat);
            }
            return NotFound();

        }

        [HttpDelete("ProductCategories/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProductCat(int id)
        {
            var result = await _productCategoryService.DeleteByID(id);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok();
        }

        #endregion
    }
}
