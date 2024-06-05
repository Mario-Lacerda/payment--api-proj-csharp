using System.ComponentModel.DataAnnotations;
using Payment_API.src.Models;

namespace Payment_API.src.DTOs
{
    public class NewSaleDTO
    {
        public SellerDTO Seller { get; set; }


        [Required(ErrorMessage = "Informação obrigatória")]
        [MinLength(1, ErrorMessage = "É necessário acrescentar no mínimo 1 item à lista")]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}