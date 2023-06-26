using FluentValidation.TestHelper;
using ProductCrud.Application.DTOs.Product;
using ProductCrud.Application.Validators.Product;
using Xunit;

namespace ProductCrud.Application.UnitTests.Validations
{
    public class IProductDtoValidatorTests
    {
        private readonly IProductDto _productDto;
        private readonly IProductDtoValidator _validator;

        public IProductDtoValidatorTests()
        {
            _validator = new IProductDtoValidator();

            _productDto = new ProductCreateDto
            {
                Name = "Alpiner Watch, Heritage Carrée Mechanical 140 Years",
                IsAvailable = true,
                ManufacturePhone = "+19994445556",
                ManufactureEmail = "customercare@us.alpinawathes.com",
                ProduceDate = DateTime.Now.AddYears(-3),
            };
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public async Task IProductDtoValidator_IfNameNullOrEmpty_ShouldThrowValidationException(string name)
        {
            _productDto.Name = name;

            var result = await _validator.TestValidateAsync(_productDto);

            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public async Task IProductDtoValidator_IfProduceDateHasIncorrectFormat_ShouldThrowValidationException()
        {
            _productDto.ProduceDate = DateTime.Now.AddYears(10);

            var result = await _validator.TestValidateAsync(_productDto);

            result.ShouldHaveValidationErrorFor(c => c.ProduceDate);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("+98123")]
        [InlineData("09388377701")]
        public async Task IProductDtoValidator_IfManufacturePhoneHasIncorrectFormat_ShouldThrowValidationException(string manufacturePhone)
        {
            _productDto.ManufacturePhone = manufacturePhone;

            var result = await _validator.TestValidateAsync(_productDto);

            result.ShouldHaveValidationErrorFor(c => c.ManufacturePhone);
        }

        [Theory]
        [InlineData("")]
        [InlineData("google.com")]
        [InlineData("asghar@gmail.com.")]
        public async Task IProductDtoValidator_IfManufactureEmailHasIncorrectFormat_ShouldThrowValidationException(string manufactureEmail)
        {
            _productDto.ManufactureEmail = manufactureEmail;

            var result = await _validator.TestValidateAsync(_productDto);

            result.ShouldHaveValidationErrorFor(c => c.ManufactureEmail);
        }
    }
}
