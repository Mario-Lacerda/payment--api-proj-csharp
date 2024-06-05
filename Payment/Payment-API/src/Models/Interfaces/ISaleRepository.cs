namespace Payment_API.src.Models.Interfaces
{
    public interface ISaleRepository
    {
        Sale FindById(int id);
        void Add(Sale sale);
        void Update(Sale sale);
    }
}