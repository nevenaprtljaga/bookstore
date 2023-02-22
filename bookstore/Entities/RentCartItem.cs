namespace bookstore.Entities
{
    public class RentCartItem
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int Amount { get; set; }
        public string RentCartId { get; set; }
    }
}
