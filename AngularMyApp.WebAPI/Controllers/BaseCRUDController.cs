using AngularMyApp.Core.Services.Interfaces;
using AngularMyApp.DataLayer.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularMyApp.WebAPI.Controllers
{
    /// <summary>
    /// Base CRUD controller for handling CRUD operations on entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity to perform CRUD operations on.</typeparam>
    /// <typeparam name="TService">The type of the service that implements the IBaseCRUD interface for the entity.</typeparam>

    public class BaseCRUDController<T, TService> : SiteBasicController where T : BasicEntity
                                                                       where TService : IBaseCRUD<T>
    {
        private readonly TService _service;

        /// <summary>
        /// Initializes a new instance of the BaseCRUDController class.
        /// </summary>
        /// <param name="service">The service that implements the IBaseCRUD interface for the entity.</param>
        public BaseCRUDController(TService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>A status code indicating whether the operation was successful.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(T entity)
        {
            await _service.Create(entity);
            return Ok();
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A status code indicating whether the operation was successful.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(T entity)
        {
            await _service.Update(entity);
            return Ok();
        }

        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A status code indicating whether the operation was successful.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(T entity)
        {
            await _service.Delete(entity);
            return Ok();
        }

        /// <summary>
        /// Deletes an existing entity by ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A status code indicating whether the operation was successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteByID(id);
            return Ok();
        }

        /// <summary>
        /// Gets an existing entity by ID.
        /// </summary>
        /// <param name="id">The ID of the entity to get.</param>
        /// <returns>The entity with the specified ID, or a status code indicating that the entity was not found.</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _service.GetByID(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
    }
}