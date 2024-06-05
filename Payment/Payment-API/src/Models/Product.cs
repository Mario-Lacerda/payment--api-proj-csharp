using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Payment_API.src.Models
{
    public class Product
    {
        [JsonIgnore]
        public int Id { get; set; }


        [Required(ErrorMessage ="Adicione um item", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 2)]
        public string Item { get; set; }
    }
}