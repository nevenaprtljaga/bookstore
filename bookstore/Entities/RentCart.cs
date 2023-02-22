using Microsoft.EntityFrameworkCore;

namespace bookstore.Entities
{
    public class RentCart
    {
        public AppDbContext _context { get; set; }
        public string RentCartId { get; set; }
        public List<RentCartItem> RentCartItems { get; set; }

        public RentCart(AppDbContext context)
        {
            _context = context;
        }

        public static RentCart GetRentCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new RentCart(context) { RentCartId = cartId };
        }


        public void AddItemToCart(Book Book)
        {
            var rentCartItem = _context.RentCartItems.FirstOrDefault(n => n.Book.Id == Book.Id && n.RentCartId == RentCartId);
            var bookInfo = _context.BookInfos.FirstOrDefault(n => n.BookId == Book.Id);
            if (bookInfo.AmountRent > 0)
            {
                if (rentCartItem == null)
                {
                    rentCartItem = new RentCartItem()
                    {
                        RentCartId = RentCartId,
                        Book = Book,
                        Amount = 1
                    };

                    _context.RentCartItems.Add(rentCartItem);

                }
                else if (rentCartItem.Amount == bookInfo.AmountRent)
                {

                }
                else
                {
                    rentCartItem.Amount++;
                }
            }
            _context.SaveChanges();
        }


        public void RemoveItemFromCart(Book Book)
        {
            var rentCartItem = _context.RentCartItems.FirstOrDefault(n => n.Book.Id == Book.Id && n.RentCartId == RentCartId);
            var bookInfo = _context.BookInfos.FirstOrDefault(n => n.BookId == Book.Id);
            if (rentCartItem != null)
            {
                if (rentCartItem.Amount > 1)
                {
                    rentCartItem.Amount--;
                }
                else
                {
                    _context.RentCartItems.Remove(rentCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<RentCartItem> GetRentCartItems()
        {
            return RentCartItems ?? (RentCartItems = _context.RentCartItems.Where(n => n.RentCartId == RentCartId).Include(n => n.Book).ToList());
        }

        public double GetRentCartTotal() => _context.RentCartItems.Where(n => n.RentCartId == RentCartId).Select(n => n.Book.Price * n.Amount).Sum();

        public async Task ClearRentCartAsync()
        {
            var items = await _context.RentCartItems.Where(n => n.RentCartId == RentCartId).ToListAsync();
            _context.RentCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}

