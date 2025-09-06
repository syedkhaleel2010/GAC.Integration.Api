using AutoMapper;
using FluentValidation;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure;
using GAC.Integration.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GAC.Integration.Service
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly IUserSession _userSession;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IValidationService _validationService;
        public CustomerService(ILogger<CustomerService> logger,
            IUserSession userSession,
            IServiceScopeFactory serviceScopeFactory,
            IMapper mapper,
            ICustomerRepository customerRepository,
            IValidationService validationService):base(userSession)
        {
            _logger = logger;
            _userSession = userSession;
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _validationService = validationService;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto)
        {
            // Implementation for creating a customer  
            _logger.LogInformation("Creating a new customer.");
            customerDto.ID = Guid.NewGuid();
            customerDto.ExternalCustomerIdentifier = "CUST-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            var entity = _mapper.Map<Customer>(customerDto);
            SetCreatedBy(entity); 
            await _customerRepository.CreateCustomer(entity);
            return await Task.FromResult(customerDto);
        }

        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto)
        {
            // Implementation for updating a customer  
            _logger.LogInformation("Updating customer with ID: {CustomerId}", customerDto.ID);

            var validationFailures = await _validationService.ValidateCustomerDetails(customerDto);
            if (validationFailures.Count > 0)
                throw new ValidationException("please correct the validation", validationFailures);
           
            if (!await _customerRepository.CustomerExists(customerDto.ID))
                throw new Exception($"Customer with ID {customerDto.ID} does not exist.");

            var entity = _mapper.Map<Customer>(customerDto);
            SetUpdatedBy(entity);
            await _customerRepository.UpdateCustomer(entity);
            
            return await Task.FromResult(customerDto);
        }

        public async Task<IList<CustomerDto>> GetCustomersAsync()
        {
            // Implementation for retrieving customers  
            _logger.LogInformation("Retrieving all customers.");
            var customers = await _customerRepository.GetCustomers();
            return customers.ToList();
        }
    }
}
