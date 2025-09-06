using GAC.Integration.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAC.Integration.Service.Interfaces
{
    public interface ISalesOrderService
    {
        Task<SalesOrderDto> CreateSalesOrderAsync(SalesOrderDto salesOrder);
        Task<SalesOrderDto> GetByIdAsync(Guid id);
        Task<IEnumerable<SalesOrderDto>> GetAllAsync();
        Task<bool> UpdateAsync(SalesOrderDto salesOrder);
        Task<bool> DeleteAsync(Guid id);
    }
}
