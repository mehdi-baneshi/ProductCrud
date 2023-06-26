using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ProductCrud.Application.Validators.Product;
using ProductCrud.Application.Features.Product.Handlers.Commands;
using ProductCrud.Application.Features.Product.Requests.Commands;
using ProductCrud.Application.DTOs.Product;
using AutoMapper;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.UnitTests.Mocks;
using Moq;
using ProductCrud.Application.Profiles;

namespace ProductCrud.Application.UnitTests.Validations
{
    public class ProductCreateDtoValidatorTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductCreateDto _productDto;
        private readonly ProductCreateDtoValidator _validator;

        public ProductCreateDtoValidatorTests()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            _validator = new ProductCreateDtoValidator(_mockRepo.Object);

            _productDto = new ProductCreateDto
            {
                Name = "Manifest Book",
                IsAvailable = true,
                ManufacturePhone = "+19998000000",
                ManufactureEmail = "ecobooks@gmail.com",
                ProduceDate = DateTime.Now.AddYears(-20),
            };
        }

        [Fact]
        public async Task CreateCommandValidator_IfProductIsNotUnique_ShouldThrowValidationException()
        {
            var products = await _mockRepo.Object.GetItems();
            _productDto.ManufactureEmail = products[0].ManufactureEmail;
            _productDto.ProduceDate = products[0].ProduceDate;

            var result = await _validator.TestValidateAsync(_productDto);

            result.ShouldHaveValidationErrorFor(c => c);
        }
    }
}
