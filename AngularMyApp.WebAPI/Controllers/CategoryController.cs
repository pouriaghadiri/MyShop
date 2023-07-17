using ShoppingSiteApi.Core.Services.Implementations;
using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.DataAccess.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingSiteApi.Core.DTOs.Products;

namespace ShoppingSiteApi.WebAPI.Controllers
{
    /// <summary>
    /// Controller that provides CRUD operations for categories.
    /// </summary>
    public class CategoryController : SiteBasicController
    {
        private readonly ICategoryService _productCategoryService;
        public CategoryController(ICategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        /// <summary>
        /// Retrieves a single category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category with the given ID.</returns>
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
        /// <summary>
        /// Retrieves all categories in the system.
        /// </summary>
        /// <returns>All categories in the system.</returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productCategoryService.GetAll());
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="entity">The data for the new category.</param>
        /// <returns>The new category.</returns>
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

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="entity">The data for the category to update.</param>
        /// <returns>The updated category.</returns>
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
        /// <summary>
        /// Deletes an existing category.
        /// </summary>
        /// <param name="entity">The category to delete.</param>
        /// <returns>An HTTP status code indicating whether the operation was successful.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Category entity)
        {
            await _productCategoryService.Delete(entity);
            return Ok();
        }

        /// <summary>
        /// Deletes an existing category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>An HTTP status code indicating whether the operation was successful.</returns>
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
