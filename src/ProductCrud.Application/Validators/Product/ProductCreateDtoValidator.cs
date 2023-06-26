using FluentValidation;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.DTOs.Product;

namespace ProductCrud.Application.Validators.Product
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        private readonly IProductRepository _productRepository;

        public ProductCreateDtoValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            Include(new IProductDtoValidator());

            RuleFor(c => c)
                .MustAsync(async (product, token) =>
                {
                    var isUnique = await _productRepository.IsProductUnique(0, product.ManufactureEmail, product.ProduceDate);
                    return isUnique;
                }).WithMessage("Product must be unique");
        }
    }
}
