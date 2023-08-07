using ShoppingSiteApi.Core.DTOs.Products;
using ShoppingSiteApi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingSiteApi.Core.Utilities.Extentions.Identity;
using ShoppingSiteApi.DataAccess.Entities.Products;

namespace ShoppingSiteApi.WebAPI.Controllers
{
    /// <summary>
    /// Controller that provides CRUD operations for products and product categories.
    /// </summary>
    public class ProductController : SiteBasicController
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductGalleryService  _productGalleryService;
        private readonly ICommentService  _commentService;
        public ProductController(IProductService productProductService ,
                                    IProductCategoryService productCategoryService ,
                                    IProductGalleryService productGalleryService , 
                                    ICommentService commentService)
        {
            _productService = productProductService;
            _productCategoryService = productCategoryService;
            _productGalleryService = productGalleryService;
            _commentService = commentService;
        }

        #region Products 

        /// <summary>
        /// Retrieves a single product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the given ID.</returns>
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
        /// <summary>
        /// Retrieves all products in the system.
        /// </summary>
        /// <returns>All products in the system.</returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="entity">The data for the new product.</param>
        /// <returns>The new product.</returns>
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
        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="entity">The data for the product to update.</param>
        /// <returns>The updated product.</returns>
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
        /// <summary>
        /// Deletes an existing product.
        /// </summary>
        /// <param name="entity">The product to delete.</param>
        /// <returns>An HTTP status code indicating whether the operation was successful.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Product entity)
        {
            await _productService.Delete(entity);
            return Ok();
        }
        /// <summary>
        /// Deletes an existing product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>An HTTP status code indicating whether the operation was successful.</returns>
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
        /// <summary>
        /// Retrieves a single product category by its ID.
        /// </summary>
        /// <param name="id">The ID of the product category to retrieve.</param>
        /// <returns>The product category with the given ID.</returns>
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
        /// <summary>
        /// Creates a new product category.
        /// </summary>
        /// <param name="entity">The data for the new product category.</param>
        /// <returns>The new product category.</returns>
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
        /// <summary>
        /// Updates an existing product category.
        /// </summary>
        /// <param name="entity">The data for the product category to update.</param>
        /// <returns>The updated product category.</returns>
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
        /// <summary>
        /// Deletes an existing product category.
        /// </summary>
        /// <param name="id">The ID of the product category to delete.</param>
        /// <returns>An HTTP status code indicating whether the operation was successful.</returns>
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

        #region Product Gallery

        [HttpGet("GetActiveProductGallery/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveProductGallery(int productId)
        {
            var a = await _productGalleryService.GetActiveProductGallery(productId);
            if (a == null)
            {
                return NotFound();
            }
            return Ok(a);
        }
        #endregion

        #region Related Products

        [HttpGet("GetRelatedProducts/{productID}")]
        public async Task<IActionResult> GetRelatedProducts(int productID)
        {
            return Ok(await _productService.GetRelatedProducts(productID));
        }

        #endregion

        #region Comment

        [HttpGet("GetProductComments/{productId}")]
        public async Task<IActionResult> GetProductComment(int productId)
        {
            var comments = await _commentService.GetActiveProductComments(productId);
            if (comments != null)
            {
                return Ok(comments);
            }
            return NotFound();
        }


        [HttpGet("GetComment/{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comments = await _commentService.GetByID(id);
            if (comments != null)
            {
                return Ok(comments);
            }
            return NotFound();
        }

        [HttpPost("ProductComment")]
        public async Task<IActionResult> ProductCommetn (AddCommentDTO entity)
        {
            try
            {
                var userId = User.GetUserID();
                if (userId == 0)
                {
                    return Forbid();
                }
                var comment = await _commentService.Create(entity , userId);
                if (comment != null)
                {
                    return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

    }
}
