using FluentValidation;
using GAC.Integration.Domain.Dto;

public class SalesOrderValidator : AbstractValidator<SalesOrderDto>
{
    public SalesOrderValidator()
    {
        RuleFor(x => x.ID)
             .NotEmpty().WithMessage("ID is required.");

        RuleFor(x => x.ExternalOrderID)
            .NotEmpty().WithMessage("External Order Id is required.");

        RuleFor(x => x.ProcessingDate)
            .NotEmpty().WithMessage("Processing Date  is required");

        RuleFor(x => x.CustomerID)
            .NotEmpty().WithMessage("Customer Id is required");

    }
}
