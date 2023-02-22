namespace bookstore.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string TypeOfOrder { get; set; } //kupovina ili iznajmljivanje
        public string ApplicationUserId { get; set; }
    }
}
