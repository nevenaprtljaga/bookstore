using Microsoft.EntityFrameworkCore;

namespace bookstore.Entities
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddItemToCart(Book Book)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Book.Id == Book.Id && n.ShoppingCartId == ShoppingCartId);
            var bookInfo = _context.BookInfos.FirstOrDefault(n => n.BookId == Book.Id);
            if (bookInfo.AmountPurchase > 0)
            {
                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItem()
                    {
                        ShoppingCartId = ShoppingCartId,
                        Book = Book,
                        Amount = 1
                    };

                    _context.ShoppingCartItems.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Amount++;
                }
                if (bookInfo != null)
                {
                    bookInfo.AmountPurchase = bookInfo.AmountPurchase - 1;
                }
            }
            _context.SaveChanges();
        }


        public void RemoveItemFromCart(Book Book)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Book.Id == Book.Id && n.ShoppingCartId == ShoppingCartId);
            var bookInfo = _context.BookInfos.FirstOrDefault(n => n.BookId == Book.Id);
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            if (bookInfo != null)
            {
                bookInfo.AmountPurchase = bookInfo.AmountPurchase + 1;
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Book).ToList());
        }

        public double GetShoppingCartTotal() => _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Book.Price * n.Amount).Sum();

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
