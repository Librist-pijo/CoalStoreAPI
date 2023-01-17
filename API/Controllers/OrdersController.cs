using API.Enums;
using API.ModelsDTO;
using API.ModelsDTO.Orders;
using API.ModelsDTO.ProductsDTO;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IOrdersService _ordersService;

        public OrdersController(IConfiguration config,
                                IOrdersService ordersService)
        {
            _config = config;
            _ordersService = ordersService;
        }

        /// <summary>
        /// Creates new orders
        /// </summary>
        /// <param name="ordersDTO"></param>
        /// <returns></returns>
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrders([FromBody] CreateOrdersDTO ordersDTO)
        {
            var value = await _ordersService.CreateOrders(ordersDTO);
            return Json(value);
        }

        /// <summary>
        /// Updates orders
        /// </summary>
        /// <param name="ordersDTO"></param>
        /// <returns></returns>
        [HttpPut("update-order")]
        public async Task<IActionResult> UpdateOrders([FromBody] UpdateOrdersDTO ordersDTO)
        {
            var value = await _ordersService.UpdateOrders(ordersDTO);
            return Json(value);
        }
        /// <summary>
        /// Deletes orders by id of order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete("delete-order/{orderId}")]
        public async Task<IActionResult> DeleteOrders(int orderId)
        {
            var value = await _ordersService.DeleteOrders(orderId);
            return Json(value);
        }

        /// <summary>
        /// Gets possible states of orders
        /// </summary>
        /// <returns>Enum of order states</returns>
        [HttpGet("get-orders-states")]
        public IActionResult GetOrdersStates()
        {
            List<EnumDescriptionDTO<OrderState>> ordersStates = _ordersService.GetOrdersStates();
            return Json(ordersStates);
        }

        /// <summary>
        /// Gets all orders
        /// </summary>
        /// <returns>List of all orders</returns>
        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var value = await _ordersService.GetOrders();
            return Json(value);
        }

        /// <summary>
        /// Get orders by id of order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>One order that match order id</returns>
        [HttpGet("get-orders-by-id/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var value = await _ordersService.GetOrderById(orderId);
            return Json(value);
        }



    }
}
