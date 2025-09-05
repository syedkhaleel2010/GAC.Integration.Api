using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities.GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.Data;
using GAC.Integration.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MG.Marine.Ticketing.SQL.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ServiceDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerRepository(ServiceDbContext dbContext,IUnitOfWork unitOfWork,IMapper mapper) 
           
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<bool> CreateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            await _dbContext.Customers.AddAsync(customer);
           _unitOfWork.Commit();
            return true;
        }
        public async Task<bool> UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            _dbContext.Customers.Update(customer);
           await _unitOfWork.CommitAsync();
            return true;
        }
    
        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            var entity = await _dbContext.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(entity);
        }
    }
}