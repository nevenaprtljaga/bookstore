using bookstore.Entities;
using bookstore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrdersService(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<OrdersViewModel>> GetByIdAsync(int id)
        {
            var result = await (from orderItem in _context.OrderItems
                                join order in _context.Orders on orderItem.OrderId equals order.Id
                                join book in _context.Books on orderItem.BookId equals book.Id
                                join user in _userManager.Users on order.ApplicationUserId equals user.Id

                                where orderItem.OrderId == id
                                select new OrdersViewModel
                                {
                                    Order = order,
                                    Book = book,
                                    OrderItem = orderItem,
                                    ApplicationUser = user
                                }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<OrdersViewModel>> GetAll()
        {
            var result = await (from orderItem in _context.OrderItems
                                join order in _context.Orders on orderItem.OrderId equals order.Id
                                join book in _context.Books on orderItem.BookId equals book.Id
                                join user in _userManager.Users on order.ApplicationUserId equals user.Id

                                select new OrdersViewModel
                                {
                                    Order = order,
                                    Book = book,
                                    OrderItem = orderItem,
                                    ApplicationUser = user
                                }).ToListAsync();
            return result.DistinctBy(n => n.OrderItem.OrderId);
        }


        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string TypeOfOrder, int TotalPrice)
        {
            var order = new Order()
            {
                ApplicationUserId = userId,
                TypeOfOrder = TypeOfOrder,
                Date = DateTime.Now,
                TotalPrice = TotalPrice

            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                while (item.Amount > 0)
                {
                    var orderItem = new OrderItem()
                    {
                        BookId = item.Book.Id,
                        OrderId = order.Id
                    };
                    item.Amount--;
                    await _context.OrderItems.AddAsync(orderItem);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task StoreOrderAsync(List<RentCartItem> items, string userId, string TypeOfOrder, int TotalPrice)
        {
            var order = new Order()
            {
                ApplicationUserId = userId,
                TypeOfOrder = TypeOfOrder,
                Date = DateTime.Now,
                TotalPrice = TotalPrice

            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                while (item.Amount > 0)
                {
                    var orderItem = new OrderItem()
                    {
                        BookId = item.Book.Id,
                        OrderId = order.Id
                    };
                    item.Amount--;
                    await _context.OrderItems.AddAsync(orderItem);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
