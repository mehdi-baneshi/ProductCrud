using FluentValidation;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.DTOs.Product;

namespace ProductCrud.Application.Validators.Product
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        private readonly IProductRepository _productRepository;

        public ProductUpdateDtoValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            Include(new IProductDtoValidator());

            RuleFor(c => c.Id)
                .NotNull().WithMessage("{PropertyName} must be present")
                .GreaterThan(0).WithMessage("{PropertyName} must be greather than zero");

            RuleFor(c => c)
                .MustAsync(async (product, token) =>
                {
                    var isUnique = await _productRepository.IsProductUnique(product.Id, product.ManufactureEmail, product.ProduceDate);
                    return isUnique;
                }).WithMessage("product must be unique");
        }
    }
}
