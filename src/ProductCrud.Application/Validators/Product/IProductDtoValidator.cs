using FluentValidation;
using ProductCrud.Application.DTOs.Product;

namespace ProductCrud.Application.Validators.Product
{
    public class IProductDtoValidator : AbstractValidator<IProductDto>
    {
        public IProductDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(c => c.ProduceDate)
                .NotEqual(DateTime.MinValue).WithMessage("{PropertyName} is required.")
                .LessThan(DateTime.Now).WithMessage("{PropertyName} cannot be in the future.")
                .GreaterThan(DateTime.Now.AddYears(-500)).WithMessage("This {PropertyName} cannot be true.");

            RuleFor(c => c.ManufacturePhone)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Matches("^\\+?[1-9][0-9]{7,13}$").WithMessage("{PropertyName} must be in the correct format");

            RuleFor(c => c.ManufactureEmail)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("{PropertyName} must be in the correct format")
                .MaximumLength(254).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
