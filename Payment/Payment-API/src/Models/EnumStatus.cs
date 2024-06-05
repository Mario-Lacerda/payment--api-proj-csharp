using System.ComponentModel.DataAnnotations;

namespace Payment_API.src.Models
{
    public enum EnumStatus
    {
        [Display(Name = "Aguardando pagamento")]
        Aguardando,

        [Display(Name = "Pagamento Aprovado")]
        Aprovado,

        Cancelada,

        [Display(Name = "Enviado para Transportadora")]
        Enviado,

        Entregue
    }
}