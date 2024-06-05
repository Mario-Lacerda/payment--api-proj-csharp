using Microsoft.EntityFrameworkCore;
using Payment_API.src.Models;
using Payment_API.src.Models.Interfaces;

namespace Payment_API.src.Persistence.Repository
{
    public class SaleRepository : BaseRepository, ISaleRepository
    {
        public SaleRepository(PaymentContext context) : base(context)
        {
        }

        public Sale FindById(int id)
        {
            return _context.Sales
                    .Include(p => p.Seller)
                    .Include(p => p.Products)
                    .AsNoTracking()
                    .SingleOrDefault(p => p.Id == id);
        }

        public void Add(Sale sale)
        {
            var seller = sale.Seller.Id;
            var existingSeller = _context.Sellers.Find(seller);

            if (existingSeller is not null)
                sale.Seller = existingSeller;

            _context.Sales.Add(sale);
        }

        public void Update(Sale sale)
        {
            _context.Sales.Update(sale);
        }
    }
}