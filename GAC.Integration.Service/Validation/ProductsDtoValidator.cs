using FluentValidation;
using GAC.Integration.Domain.Dto;

public class ProductsDtoValidator : AbstractValidator<ProductDto>
{
    public ProductsDtoValidator()
    {
        RuleFor(x => x.ID)
            .NotEmpty().WithMessage("ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(255).WithMessage("Name cannot exceed 200 characters.");

        RuleFor(x => x.ProductCode)
            .NotEmpty().WithMessage("Product Code is required");

        RuleFor(x => x.Length).NotEmpty().WithMessage("Length is required.");

        RuleFor(x => x.Width).NotEmpty().WithMessage("Width is required.");

        RuleFor(x => x.Weight).NotEmpty().WithMessage("Weight is required.");
    }
}
