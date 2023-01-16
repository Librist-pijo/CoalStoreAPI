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
        [HttpPut("updateorder")]
        public IActionResult UpdateOrders([FromBody] UpdateOrdersDTO ordersDTO)
        {
            var value = _ordersService.UpdateOrders(ordersDTO);
            return Json(value);
        }
        [HttpDelete("deleteorder")]
        public IActionResult DeleteOrders(int orderId)
        {
            var value = _ordersService.DeleteOrders(orderId);
            return Json(value);
        }
        [HttpGet("getordersstates")]
        public IActionResult GetOrdersStates()
        {
            List<EnumDescriptionDTO<OrderState>> ordersStates = _ordersService.GetOrdersStates();
            return Json(ordersStates);
        }
        [HttpGet("getorders")]
        public IActionResult GetOrders()
        {
            var value = _ordersService.GetOrders();
            return Json(value);
        }
        [HttpGet("getordersbyid")]
        public IActionResult GetOrderById(int orderId)
        {
            var value = _ordersService.GetOrderById(orderId);
            return Json(value);
        }

        [HttpPost("createorder")]
        public IActionResult CreateOrders([FromBody] CreateOrdersDTO ordersDTO)
        {
            var value = _ordersService.CreateOrders(ordersDTO);
            return Json(value);
        }

    }
}
