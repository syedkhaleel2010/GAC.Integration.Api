using FluentValidation;
using GAC.Integration.Domain.Dto;

public class SalesOrderItemValidator : AbstractValidator<SalesOrderItemsDto>
{
    public SalesOrderItemValidator()
    {
        RuleFor(x => x.ID)
            .NotEmpty().WithMessage("ID is required.");

        RuleFor(x => x.SalesOrderID)
            .NotEmpty().WithMessage("Purchase Order Id is required.");

        RuleFor(x => x.ProductID)
            .NotEmpty().WithMessage("Prodcut Id is required.");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity cannot be empty.")
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}
