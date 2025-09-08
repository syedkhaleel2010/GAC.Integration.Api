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
        Task<List<ValidationFailure>> ValidateProductDetails(ProductDto req);
        Task<List<ValidationFailure>> ValidatePurchaseOrderDetails(PurchaseOrderDto req);
        Task<List<ValidationFailure>> ValidatePurchaseOrderItemDetails(PurchaseOrderItemsDto req);
        Task<List<ValidationFailure>> ValidateSalesOrderDetails(SalesOrderDto req);
        Task<List<ValidationFailure>> ValidateSalesOrderItemDetails(SalesOrderItemsDto req);
    }
}
