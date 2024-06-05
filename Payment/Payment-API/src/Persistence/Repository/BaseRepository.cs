namespace Payment_API.src.Persistence.Repository
{
    public abstract class BaseRepository
    {
        protected readonly PaymentContext _context;

        public BaseRepository(PaymentContext context)
        {
            _context = context;
        }
    }
}