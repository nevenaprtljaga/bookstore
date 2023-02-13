namespace bookstore.Entities
{
    public class BookInfo
    {
        public int Id { get; set; }
        public int AmountRent { get; set; }
        public int AmountPurchase { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public int TotalAmount()
        {
            return AmountRent + AmountPurchase;
        }
    }
}
