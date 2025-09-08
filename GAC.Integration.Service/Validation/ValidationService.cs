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
        private readonly IMapper _mapper;
        public ValidationService(ILogger<ValidationService> logger,
            ICustomerRepository customerRepository,
            IUserSession userSession,
            IMapper mapper) : base(userSession)
        {
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<ValidationFailure>> ValidateCustomerDetails(CustomerDto req)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();
          
            try
            {
          //any code to check with data in db against this entity
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
        public async Task<List<ValidationFailure>> ValidateProductDetails(ProductDto req)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();

            try
            {
                //any code to check with data in db against this entity
            }
            catch (ValidationException vex)
            {
                errors.AddRange(vex.Errors);
            }
            try
            {
                await new ProductsDtoValidator().ValidateAndThrowAsync(req);
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
        public async Task<List<ValidationFailure>> ValidatePurchaseOrderDetails(PurchaseOrderDto req)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();

            try
            {
                //any code to check with data in db against this entity
            }
            catch (ValidationException vex)
            {
                errors.AddRange(vex.Errors);
            }
            try
            {
                await new PurchaseOrderValidator().ValidateAndThrowAsync(req);
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
        public async Task<List<ValidationFailure>> ValidatePurchaseOrderItemDetails(PurchaseOrderItemsDto req)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();

            try
            {
                //any code to check with data in db against this entity
            }
            catch (ValidationException vex)
            {
                errors.AddRange(vex.Errors);
            }
            try
            {
                await new PurchaseOrderItemValidator().ValidateAndThrowAsync(req);
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
        public async Task<List<ValidationFailure>> ValidateSalesOrderDetails(SalesOrderDto req)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();

            try
            {
                //any code to check with data in db against this entity
            }
            catch (ValidationException vex)
            {
                errors.AddRange(vex.Errors);
            }
            try
            {
                await new SalesOrderValidator().ValidateAndThrowAsync(req);
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
        public async Task<List<ValidationFailure>> ValidateSalesOrderItemDetails(SalesOrderItemsDto req)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();

            try
            {
                //any code to check with data in db against this entity
            }
            catch (ValidationException vex)
            {
                errors.AddRange(vex.Errors);
            }
            try
            {
                await new SalesOrderItemValidator().ValidateAndThrowAsync(req);
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
