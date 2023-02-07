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

        public async Task<OrdersViewModel> GetByIdAsync(int id)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(n => n.Id == id);
            return new OrdersViewModel { Order = result };
        }

        public async Task<IEnumerable<OrdersViewModel>> GetAll()
        {
            List<OrdersViewModel> vm = new List<OrdersViewModel>();
            var result = await _context.Orders.ToListAsync();
            foreach (var item in result)
            {
                vm.Add(new OrdersViewModel
                {
                    Order = item
                });
            }
            return vm;
        }
    }
}
