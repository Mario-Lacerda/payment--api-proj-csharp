namespace Payment_API.src.Models.Interfaces
{
    public interface ISaleService
    {
        Sale GetById(int id);
        Sale Create(Sale newSale);
        void UpdateStatus(int id, EnumStatus newStatus);
    }
}