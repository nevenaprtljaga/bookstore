using bookstore.Models;

namespace bookstore.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrdersViewModel>> GetAll();
        Task<OrdersViewModel> GetByIdAsync(int id);
    }
}
