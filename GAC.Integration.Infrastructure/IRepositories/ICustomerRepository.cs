using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities.GAC.Integration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAC.Integration.Infrastructure.IRepositories
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomer(Customer entity);
        Task<bool> UpdateCustomer(Customer entity);
        Task<IEnumerable<CustomerDto>> GetCustomers();
    }
}
