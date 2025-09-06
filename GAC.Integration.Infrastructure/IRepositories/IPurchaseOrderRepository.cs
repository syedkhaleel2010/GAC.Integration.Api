using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;

namespace GAC.Integration.Infrastructure.IRepositories
{
    public interface IPurchaseOrderRepository
    {
        Task<bool> CreatePurchaseOrder(PurchaseOrder purchaseOrder);
        Task<bool> UpdateAsync(PurchaseOrder purchaseOrder);
        Task<PurchaseOrderDto> GetByIdAsync(Guid id);
        Task<IEnumerable<PurchaseOrderDto>> GetAllAsync();
    }
}
