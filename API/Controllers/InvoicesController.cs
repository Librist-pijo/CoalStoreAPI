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
        /// <summary>
        /// Gets all invoices
        /// </summary>
        /// <returns>List of all invoices</returns>
        [HttpGet("get-Invoices")]
        public async Task<IActionResult> GetInvoices()
        {
            var value = await _InvoicesService.Get();
            return Json(value);
        }

        /// <summary>
        /// Gets invoice by id of invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>One invoice that match invoice id</returns>
        [HttpGet("get-invoice-by-id/{invoiceId}")]
        public async Task<IActionResult> GetInvoicesById(int invoiceId)
        {
            var value = await _InvoicesService.GetById(invoiceId);
            return Json(value);
        }
        /// <summary>
        /// Gets inovoice by id of order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>One invoice that match order id</returns>
        [HttpGet("get-invoices-by-orderid/{orderId}")]
        public async Task<IActionResult> GetCategoryByName(int orderId)
        {
            var value = await _InvoicesService.GetByOrderId(orderId);
            return Json(value);
        }
    }
}