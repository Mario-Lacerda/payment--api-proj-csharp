using Payment_API.src.Extensions;
using Payment_API.src.Models;
using Payment_API.src.Models.Interfaces;

namespace Payment_API.src.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SaleService(ISaleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Sale GetById(int id)
        {
            return _repository.FindById(id);
        }

        public Sale Create(Sale newSale)
        {
            try
            {
                _repository.Add(newSale);
                _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                
                throw new InvalidOperationException ("Ocorreu um erro durante o registro da venda.", e);
            }
            return newSale;
        }

        public void UpdateStatus(int id, EnumStatus newStatus)
        {
            var sale = _repository.FindById(id);
            
            if (sale is null)
            {
                throw new InvalidOperationException("Este registro de venda não existe.");
            }

            var saleUpdated = EnumUpdate.ValidateStatusChange(sale, newStatus);

            try
            {
                _repository.Update(saleUpdated);
                _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                
                throw new InvalidOperationException("Ocorreu um erro durante a atualização.", e);
            }
        }
    }
}