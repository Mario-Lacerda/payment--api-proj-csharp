using Payment_API.src.Models.Interfaces;

namespace Payment_API.src.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentContext _context;

        public UnitOfWork(PaymentContext context)
        {
            _context = context;
        }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}