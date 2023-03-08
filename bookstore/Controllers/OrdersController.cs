using bookstore.Entities;
using bookstore.Models;
using bookstore.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly AppDbContext _context;
        private readonly RentCart _rentCart;

        public OrdersController(IOrdersService ordersService, IBooksService booksService, ShoppingCart shoppingCart, UserManager<ApplicationUser> userManager, AppDbContext context, RentCart rentCart)
        {
            _ordersService = ordersService;
            _booksService = booksService;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
            _context = context;
            _rentCart = rentCart;
        }

        [Authorize(Roles = "Customer")]
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

        public async Task<IActionResult> CompletePurchaseOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            string type = "Purchase";
            int totalPrice = (int)_shoppingCart.GetShoppingCartTotal();


            foreach (var item in items)
            {
                var bookInfo = _context.BookInfos.FirstOrDefault(n => n.BookId == item.Book.Id);
                bookInfo.AmountPurchase -= item.Amount;
            }
            _context.SaveChanges();
            await _ordersService.StoreOrderAsync(items, userId, type, totalPrice);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }


        public IActionResult RentCart()
        {
            var items = _rentCart.GetRentCartItems();
            _rentCart.RentCartItems = items;

            var response = new RentCartViewModel()
            {
                RentCart = _rentCart,
                RentCartTotal = _rentCart.GetRentCartTotal()
            };

            return View(response);
        }


        public async Task<IActionResult> AddItemToRentCart(int id)
        {
            var item = await _booksService.GetByIdAsync(id);

            if (item != null)
            {
                _rentCart.AddItemToCart(item.Book);
            }
            return RedirectToAction(nameof(RentCart));
        }
        public async Task<IActionResult> RemoveItemFromRentCart(int id)
        {
            var item = await _booksService.GetByIdAsync(id);

            if (item != null)
            {
                _rentCart.RemoveItemFromCart(item.Book);
            }
            return RedirectToAction(nameof(RentCart));
        }

        public async Task<IActionResult> CompleteRentOrder()
        {
            var items = _rentCart.GetRentCartItems();
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim?.Value;
            string type = "Rent";
            int totalPrice = (int)_rentCart.GetRentCartTotal();


            foreach (var item in items)
            {
                var bookInfo = _context.BookInfos.FirstOrDefault(n => n.BookId == item.Book.Id);
                bookInfo.AmountRent -= item.Amount;
            }
            _context.SaveChanges();
            await _ordersService.StoreOrderAsync(items, userId, type, totalPrice);
            await _rentCart.ClearRentCartAsync();

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
