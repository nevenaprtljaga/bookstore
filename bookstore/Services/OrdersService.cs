using bookstore.Entities;
using bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdersViewModel>> GetByIdAsync(int id)
        {
            var result = await (from orderItem in _context.OrderItems
                                join order in _context.Orders on orderItem.OrderId equals order.Id
                                join book in _context.Books on orderItem.BookId equals book.Id
                                where orderItem.OrderId == id
                                select new OrdersViewModel
                                {
                                    Order = order,
                                    Book = book,
                                    OrderItem = orderItem
                                }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<OrdersViewModel>> GetAll()
        {
            var result = await (from orderItem in _context.OrderItems
                                join order in _context.Orders on orderItem.OrderId equals order.Id
                                join book in _context.Books on orderItem.BookId equals book.Id

                                select new OrdersViewModel
                                {
                                    Order = order,
                                    Book = book,
                                    OrderItem = orderItem
                                }).ToListAsync();
            return result.DistinctBy(n => n.OrderItem.OrderId);
        }


        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string TypeOfOrder)
        {
            var order = new Order()
            {
                ApplicationUserId = userId,
                TypeOfOrder = TypeOfOrder,
                Date = DateTime.Now,

            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    BookId = item.Book.Id,
                    OrderId = order.Id
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
