using API.Enums;
using API.ModelsDTO;
using API.ModelsDTO.Orders;
using API.ModelsDTO.ProductsDTO;
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

        public OrdersController(IConfiguration config, IOrdersService ordersService)
        {
            _config = config;
            _ordersService = ordersService;
        }

        [HttpGet("get-orders-states")]
        public IActionResult GetOrdersStates()
        {
            List<EnumDescriptionDTO<OrderState>> ordersStates = _ordersService.GetOrdersStates();
            return Json(ordersStates);
        }
        [HttpGet("get-orders")]
        public IActionResult GetOrders()
        {
            var value = _ordersService.GetOrders();
            return Json(value);
        }
        [HttpGet("get-orders-by-id")]
        public IActionResult GetProductByName(int orderId)
        {
            var value = _ordersService.GetOrderById(orderId);
            return Json(value);
        }

        [HttpPost("create-order")]
        public IActionResult CreateProducts([FromBody] CreateOrdersDTO ordersDTO)
        {
            throw new NotImplementedException();
        }
    }
}
