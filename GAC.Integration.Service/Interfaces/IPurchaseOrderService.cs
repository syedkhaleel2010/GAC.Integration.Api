using GAC.Integration.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAC.Integration.Service.Interfaces
{
    public interface IPurchaseOrderService
    {
        Task<PurchaseOrderDto> CreatePurchaseOrder(PurchaseOrderDto purchaseOrder);
        Task<PurchaseOrderDto> GetByIdAsync(Guid id);
        Task<IEnumerable<PurchaseOrderDto>> GetAllAsync();
        Task<bool> UpdateAsync(PurchaseOrderDto purchaseOrder);
        Task<bool> DeleteAsync(Guid id);
    }
}
