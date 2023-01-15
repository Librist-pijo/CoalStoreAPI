using API.ModelsDTO.CustomersDTO;
using API.Repositories.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ICustomersService _customersService;

        public CustomersController(IConfiguration config, ICustomersService customersService)
        {
            _config = config;
            _customersService = customersService;
        }

        [HttpGet("getcustomerbyid")]
        public IActionResult GetCustomerById(int id)
        {
            var result = _customersService.GetCustomerById(id);
            return Json(result);
        }

        [HttpGet("getcustomerbylogin")]
        public IActionResult GetCustomerByLogin(string login)
        {
            var result = _customersService.GetCustomerByLogin(login);
            return Json(result);
        }

        [HttpPut("updatecustomer")]
        public IActionResult UpdateCustomer([FromBody] Customers customer)
        {
            var result = _customersService.UpdateCustomer(customer);
            return Json(result);
        }

        [HttpPut("updatecustomerpassword")]
        public IActionResult UpdateCustomerPassword([FromBody] UpdatePasswordDTO updatePasswordDTO)
        {
            var result = _customersService.UpdateCustomerPassword(updatePasswordDTO);
            return Json(result);
        }
    }
}
