using Payment_API.src.Models;

namespace Payment_API.src.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public SellerDTO Seller { get; set; }
        public DateTime Created { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public string Status { get; set; }
    }
}