using Microsoft.AspNetCore.Mvc;

namespace bookstore.Entities
{
    public class RentCartSummary : ViewComponent
    {
        private readonly RentCart _rentCart;
        public RentCartSummary(RentCart rentCart)
        {
            _rentCart = rentCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _rentCart.GetRentCartItems();

            return View(items.Count);
        }
    }
}
