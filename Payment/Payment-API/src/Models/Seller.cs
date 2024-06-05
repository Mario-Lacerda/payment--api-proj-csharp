using System.Text.Json.Serialization;

namespace Payment_API.src.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
         public string Email { get; set; }
        public string Telephone { get; set; }

        [JsonIgnore]
        public List<Sale> Sales { get; set; }
    }
}