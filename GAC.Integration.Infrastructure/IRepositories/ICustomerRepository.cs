using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;

namespace GAC.Integration.Infrastructure
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomer(Customer entity);
        Task<bool> UpdateCustomer(Customer entity);
        Task<bool> CustomerExists(Guid id);
        Task<IEnumerable<CustomerDto>> GetCustomers();
        Task<bool> BulkInsertCustomers(IEnumerable<Customer> customers);
    }
}
