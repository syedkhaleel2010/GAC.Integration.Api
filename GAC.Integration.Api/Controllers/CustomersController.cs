using FluentValidation;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GAC.Integration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ServiceControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;

        public CustomersController(ILogger<CustomersController> logger,
            ICustomerService customerService) : base(logger)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customer)
        {
            try
            {
                customer = await _customerService.CreateCustomerAsync(customer);
                return OkServiceResponse(customer);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error occurred while creating customer.");
                return HandleValidationException(ex);
            }
        }
        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customer)
        {
            try
            {
                customer = await _customerService.UpdateCustomerAsync(customer);
                return OkServiceResponse(customer);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error occurred while updating customer.");
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while updating customer.");
                return HandleOtherException(ex);
            }
        }

        [HttpGet("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customers = await _customerService.GetCustomersAsync();
                return OkServiceResponse(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving customers.");
                return HandleOtherException(ex);
            }
        }
        [HttpPost("bulk-insert")]
        public async Task<IActionResult> BulkInsertCustomers([FromBody] List<CustomerDto> customersDto)
        {
            if (customersDto == null || !customersDto.Any())
                return BadRequest("No customers provided for bulk insert.");

            return OkServiceResponse(await _customerService.BulkInsertCustomers(customersDto));

        }
    }
}
