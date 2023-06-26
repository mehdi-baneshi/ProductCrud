using AutoMapper;
using ProductCrud.Application.Profiles;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.DTOs.Product;
using ProductCrud.Application.Exceptions;
using ProductCrud.Application.Features.Product.Handlers.Commands;
using ProductCrud.Application.Features.Product.Handlers.Queries;
using ProductCrud.Application.Features.Product.Requests.Commands;
using ProductCrud.Application.Features.Product.Requests.Queries;
using ProductCrud.Application.UnitTests.Mocks;
using ProductCrud.Domain;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace ProductCrud.Application.UnitTests.products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductCreateDto _productDto;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateProductCommandHandler(_mockRepo.Object, _mapper);

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
        public async Task Valid_product_Added()
        {
            var result = await _handler.Handle(new CreateProductCommand() { ProductDto = _productDto }, CancellationToken.None);

            var products = await _mockRepo.Object.GetItems();

            result.ShouldBeOfType<int>();

            products.Count.ShouldBe(4);
        }
    }
}
