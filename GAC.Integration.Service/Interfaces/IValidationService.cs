using FluentValidation.Results;
using GAC.Integration.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAC.Integration.Service.Interfaces
{
    public interface IValidationService
    {
        Task<List<ValidationFailure>> ValidateCustomerDetails(CustomerDto req);
    }
}
