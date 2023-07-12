using ShoppingSiteApi.Core.Services.Implementations;
using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.DataAccess.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingSiteApi.Core.DTOs.Products;

namespace ShoppingSiteApi.WebAPI.Controllers
{

    public class CategoryController : SiteBasicController
    {
        private readonly ICategoryService _productCategoryService;
        public CategoryController(ICategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Get(int id)
        {
            var entity = await _productCategoryService.GetByID(id);
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
            return Ok(await _productCategoryService.GetAll());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CategoryDTO entity)
        {
            try
            {
                var cat = await _productCategoryService.Create(entity);
                return CreatedAtAction(nameof(Get), new { id = cat.Id }, cat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Update(CategoryUpdateDTO entity)
        {
            var cat = await _productCategoryService.Update(entity);
            if (cat != null)
            {
                return Ok(cat);
            }
            return NotFound();

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Category entity)
        {
            await _productCategoryService.Delete(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productCategoryService.DeleteByID(id);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
