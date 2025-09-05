using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Infrastructure;
using GAC.Integration.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace GAC.Integration.Service.Validation
{
    public class ValidationService : BaseService, IValidationService
    {
        private readonly ILogger<ValidationService> _logger;
        private readonly ICustomerRepository _customerRepository;
       
        private readonly IMapper _mapper;
        public ValidationService(ILogger<ValidationService> logger,
            ICustomerRepository customerRepository,
            IUserSession userSession,
            IMapper mapper) : base(userSession)
        {
            _logger = logger;
            _customerRepository = customerRepository;
           
            _mapper = mapper;
        }
        public async Task<List<ValidationFailure>> ValidateCustomerDetails(CustomerDto req)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();
          
            try
            {
          
            }
            catch (ValidationException vex)
            {
                errors.AddRange(vex.Errors);
            }
            try
            {
                await new CustomerDtoValidator().ValidateAndThrowAsync(req);
            }
            catch (ValidationException vex)
            {
                errors.AddRange(vex.Errors);
            }
            try
            {
                if (errors.Count > 0)
                {
                    throw new ValidationException("Validation Errors", errors);
                }
                return errors;
            }
            catch (ValidationException vex)
            {
                return errors;
            }
        }
    }
}
