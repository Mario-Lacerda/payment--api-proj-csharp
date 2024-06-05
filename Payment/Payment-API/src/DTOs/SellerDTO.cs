using System.ComponentModel.DataAnnotations;

namespace Payment_API.src.DTOs
{
    public class SellerDTO
    {
        public int Id { get; set; }


        [RegularExpression(@"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$", ErrorMessage = "Adicione um nome válido.")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }


        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$", 
            ErrorMessage = "Este campo deve ser preenchido com 11 dígitos numéricos (000.000.000-00)")]
        public string CPF { get; set; }
        

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\-]+\.)*[\w\- ]+@([\w\- ]+\.)+([\w\-]{2,3})$", ErrorMessage = "E-mail inválido! O e-mail para ser válido deve seguir o padrão: user@example.com")]
        public string Email { get; set; }
        
         
        [RegularExpression(@"^[0-9]{2}\-?[0-9]{4,5}\-?[0-9]{4}$", 
            ErrorMessage = "Este campo deve ser preenchido com número de telefone válido (00-0000-0000 ou 00-00000-0000)")]
        public string Telephone { get; set; }
    }
}