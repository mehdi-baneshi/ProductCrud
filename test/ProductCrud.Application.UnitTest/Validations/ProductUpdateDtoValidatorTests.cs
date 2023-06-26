using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.DTOs.Product;
using ProductCrud.Application.UnitTests.Mocks;
using ProductCrud.Application.Validators.Product;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductCrud.Application.UnitTests.Validations
{
    public class ProductUpdateDtoValidatorTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductUpdateDto _productDto;
        private readonly ProductUpdateDtoValidator _validator;

        public ProductUpdateDtoValidatorTests()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            _validator = new ProductUpdateDtoValidator(_mockRepo.Object);

            _productDto = new ProductUpdateDto
            {
                Id = 2,
                Name = "Breitling Watch, Top Time B01 Ford ThunderBird",
                IsAvailable = false,
                ManufacturePhone = "+989112223335",
                ManufactureEmail = "info.us@breitling.com",
                ProduceDate = DateTime.Now.AddYears(-12)
            };
        }

        [Fact]
        public async Task UpdateCommandValidator_IfProductIsNotUnique_ShouldThrowValidationException()
        {
            var products = await _mockRepo.Object.GetItems();
            _productDto.ManufactureEmail = products[0].ManufactureEmail;
            _productDto.ProduceDate = products[0].ProduceDate;

            var result = await _validator.TestValidateAsync(_productDto);

            result.ShouldHaveValidationErrorFor(c => c);
        }

        [Fact]
        public async Task UpdateCommandValidator_IfIdHasUnacceptableValue_ShouldThrowValidationException()
        {
            _productDto.Id = -100;

            var result = await _validator.TestValidateAsync(_productDto);

            result.ShouldHaveValidationErrorFor(c => c.Id);
        }
    }
}
