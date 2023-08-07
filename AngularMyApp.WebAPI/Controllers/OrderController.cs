using ShoppingSiteApi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingSiteApi.Core.Services.Implementations;
using ShoppingSiteApi.DataAccess.Entities.Products;
using ShoppingSiteApi.DataAccess.Entities.Orders;
using ShoppingSiteApi.Core.DTOs.Products;
using ShoppingSiteApi.Core.Utilities.Extentions.Identity;

namespace ShoppingSiteApi.WebAPI.Controllers
{
    public class OrderController : SiteBasicController
    {
        #region constructor

        private IOrderDetailService _detailService;

        public OrderController(IOrderDetailService detailService)
        {
            _detailService = detailService;
        }

        #endregion

        #region users list

        [HttpGet("Orders/{orderId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int orderId)
        {
            var entity = await _detailService.GetByID(orderId);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpGet("Orders")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var entity = await _detailService.GetAll();
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }



        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(OrderDetail entity)
        {
            await _detailService.Delete(entity);
            return Ok();
        }

        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int orderId)
        {
            var result = await _detailService.DeleteByID(orderId);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddProductToOrder(int productID, int count)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                var userID = User.GetUserID();
                    var orderDetail = await _detailService.AddProductToOrder(userID, productID, count);
                    if (orderDetail != null)
                    {
                        return CreatedAtAction(nameof(Get), new { orderId = orderDetail.Id }, orderDetail);
                    }
                    return BadRequest();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
