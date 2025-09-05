using GAC.Integration.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAC.Integration.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto);
        Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto);
        Task<IList<CustomerDto>> GetCustomersAsync();
    }
}
