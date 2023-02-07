using bookstore.Services;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _service;

        public OrdersController(IOrdersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allOrders = await _service.GetAll();
            return View(allOrders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var orderDetails = await _service.GetByIdAsync(id);
            if (orderDetails == null)
            {
                return View("NotFound");
            }
            return View(orderDetails);
        }


    }
}
