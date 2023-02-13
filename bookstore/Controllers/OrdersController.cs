using bookstore.Entities;
using bookstore.Models;
using bookstore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bookstore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IBooksService _booksService;
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IOrdersService ordersService, IBooksService booksService, ShoppingCart shoppingCart, UserManager<ApplicationUser> userManager)
        {
            _ordersService = ordersService;
            _booksService = booksService;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var allOrders = await _ordersService.GetAll();
            return View(allOrders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }


        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _booksService.GetByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item.Book);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _booksService.GetByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item.Book);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            string type = "Purchase";
            // var totalPrice = _shoppingCart.GetShoppingCartTotal();

            await _ordersService.StoreOrderAsync(items, userId, type);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
        public async Task<IActionResult> Details(int id)
        {
            var orderDetails = await _ordersService.GetByIdAsync(id);
            if (orderDetails == null)
            {
                return View("NotFound");
            }
            return View(orderDetails);
        }


    }
}
