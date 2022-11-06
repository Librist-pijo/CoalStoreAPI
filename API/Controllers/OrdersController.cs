using API.Enums;
using API.ModelsDTO;
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
        private readonly IOrdersService ordersService;

        public OrdersController(IConfiguration config)
        {
            _config = config;
            ordersService = new OrdersService();
        }

        [HttpGet("get-orders-states")]
        public IActionResult GetOrdersStates()
        {
            List<EnumDescriptionDTO<OrderState>> ordersStates = ordersService.GetOrdersStates();
            return Json(ordersStates);
        }
    }
}
