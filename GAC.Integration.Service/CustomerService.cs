using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities.GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.IRepositories;
using GAC.Integration.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAC.Integration.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly IUserSession _userSession;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ILogger<CustomerService> logger,
            IUserSession userSession,
            IServiceScopeFactory serviceScopeFactory,
            IMapper mapper,
            ICustomerRepository customerRepository)
        {
            _logger = logger;
            _userSession = userSession;
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto)
        {
            // Implementation for creating a customer  
            _logger.LogInformation("Creating a new customer.");
            var entity = _mapper.Map<Customer>(customerDto);
            entity.CreatedBy = _userSession?.GetUser()?.Name ?? "system";
            await _customerRepository.CreateCustomer(entity);
            return await Task.FromResult(customerDto);
        }

        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto)
        {
            // Implementation for updating a customer  
            _logger.LogInformation("Updating customer with ID: {CustomerId}", customerDto.CustomerId);
            // Example: Validate and map the DTO, then update in the database  
            return await Task.FromResult(customerDto);
        }

        public async Task<IList<CustomerDto>> GetCustomersAsync()
        {
            // Implementation for retrieving customers  
            _logger.LogInformation("Retrieving all customers.");
            // Example: Fetch customers from the database and map to DTOs  
            return await Task.FromResult(new List<CustomerDto>());
        }
    }
}
