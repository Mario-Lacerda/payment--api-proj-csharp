namespace Payment_API.src.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public Seller Seller { get; set; }
        public DateTime Created { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public EnumStatus Status { get; set; }
    }
}