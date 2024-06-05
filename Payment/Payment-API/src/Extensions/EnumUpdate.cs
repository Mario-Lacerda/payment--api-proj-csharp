using Payment_API.src.Models;

namespace Payment_API.src.Extensions
{
    public class EnumUpdate
    {
        public static Sale ValidateStatusChange(Sale sale, EnumStatus newStatus)
        {
            if (sale.Status == EnumStatus.Aguardando)
            {
                switch(newStatus)
                {
                    case EnumStatus.Aprovado:
                        sale.Status = EnumStatus.Aprovado;
                        break;
                    case EnumStatus.Cancelada:
                        sale.Status = EnumStatus.Cancelada;
                        break;
                    default:
                        throw new InvalidOperationException ("Opção inválida! Opções válidas: Aprovado ou Cancelada."); 
                }
            }
            else if (sale.Status == EnumStatus.Aprovado)
            {
                switch(newStatus)
                {
                    case EnumStatus.Enviado:
                        sale.Status = EnumStatus.Enviado;
                        break;
                    case EnumStatus.Cancelada:
                        sale.Status = EnumStatus.Cancelada;
                        break;
                    default:
                        throw new InvalidOperationException ("Opção inválida! Opções válidas: Enviado ou Cancelada."); 
                }
            }
            else if (sale.Status == EnumStatus.Enviado)
            {
                switch(newStatus)
                {
                    case EnumStatus.Entregue:
                        sale.Status = EnumStatus.Entregue;
                        break;
                    default:
                        throw new InvalidOperationException ( "Opção inválida! Opção válida: Entregue."); 
                }
            }
            else
            {
                throw new InvalidOperationException ("Não é mais possível alterar o status.");
            }
            
            return sale;
        }
    }
}