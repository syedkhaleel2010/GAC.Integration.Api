using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.Data;
using GAC.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MG.Marine.Ticketing.SQL.Infrastructure
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

            var entity = _dbContext.Customers.Find(customer.ID);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer not found");

            entity.Name = customer.Name ?? string.Empty;
            entity.Address = customer.Address;
            entity.UpdatedBy = customer.UpdatedBy;
            entity.UpdatedAt = customer.UpdatedAt;

            _dbContext.Customers.Update(entity);
           await _unitOfWork.CommitAsync();
            return true;
        }
    
        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            var entity = await _dbContext.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(entity);
        }

        public async Task<bool> CustomerExists(Guid id)
        {
            var isExist = _dbContext.Customers.Any(x=>x.ID == id);
            return await Task.FromResult(isExist);
        }
        public async Task<bool> BulkInsertCustomers(IEnumerable<Customer> customers)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
           
                await _dbContext.Customers.AddRangeAsync(customers);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

            return await Task.FromResult(true);
        }
    }
}