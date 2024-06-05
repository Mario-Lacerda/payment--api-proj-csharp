using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payment_API.src.DTOs
{
    public enum EnumStatusUpdateDTO
    {
        [Display(Name = "Pagamento Aprovado")]
        Aprovado = 1,

        Cancelada = 2,

        [Display(Name = "Enviado para Transportadora")]
        Enviado = 3,

        Entregue = 4
    }
}