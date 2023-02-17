using bookstore.Entities;
using bookstore.Models;

namespace bookstore.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrdersViewModel>> GetAll();
        Task<IEnumerable<OrdersViewModel>> GetByIdAsync(int id);
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string TypeOfOrder, int TotalPrice);
    }
}
