using API.Enums;
using API.ModelsDTO;
using API.ModelsDTO.InvoicesDTO;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IInvoicesService _InvoicesService;

        public InvoicesController(IConfiguration config, IInvoicesService InvoicesService)
        {
            _config = config;
            _InvoicesService = InvoicesService;
        }

        [HttpGet("get-Invoices")]
        public IActionResult GetInvoices()
        {
            var value = _InvoicesService.Get();
            return Json(value);
        }
        [HttpGet("get-invoice-by-id")]
        public IActionResult GetInvoicesById(int invoiceId)
        {
            var value = _InvoicesService.GetById(invoiceId);
            return Json(value);
        }
        [HttpGet("get-invoices-by-orderid")]
        public IActionResult GetCategoryByName(int orderId)
        {
            var value = _InvoicesService.GetByOrderId(orderId);
            return Json(value);
        }
    }
}