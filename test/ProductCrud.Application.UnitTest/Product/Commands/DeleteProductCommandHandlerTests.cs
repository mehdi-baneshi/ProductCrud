using AutoMapper;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.DTOs.Product;
using ProductCrud.Application.Features.Product.Handlers.Commands;
using ProductCrud.Application.Features.Product.Requests.Commands;
using ProductCrud.Application.Profiles;
using ProductCrud.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ProductCrud.Application.Contracts.Identity;

namespace ProductCrud.Application.UnitTests.products.Commands
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<IUserService> _mockUserService;
        private readonly ProductUpdateDto _deletableProductDto;
        private readonly ProductUpdateDto _notDeletableProductDto;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _mockRepo = MockProductRepository.GetProductRepository();
            _mockUserService = MockUserService.GetUserService();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new DeleteProductCommandHandler(_mockRepo.Object, _mapper, _mockUserService.Object);

            _deletableProductDto = new ProductUpdateDto
            {
                Id = 2,
                Name = "Breitling Watch, Top Time B01 Ford ThunderBird",
                IsAvailable = false,
                ManufacturePhone = "+989112223334",
                ManufactureEmail = "info.us@breitling.com",
                ProduceDate = DateTime.Now.AddYears(-2),
            };

            _notDeletableProductDto = new ProductUpdateDto
            {
                Id = 3,
                Name = "Alpiner Watch, Heritage Carrée Mechanical 140 Years",
                IsAvailable = true,
                ManufacturePhone = "+19994445556",
                ManufactureEmail = "customercare@us.alpinawathes.com",
                ProduceDate = DateTime.Now.AddYears(-3)
            };
        }

        [Fact]
        public async Task Valid_product_Deleted()
        {
            var result = await _handler.Handle(new DeleteProductCommand() { Id = _deletableProductDto.Id }, CancellationToken.None);

            var products = await _mockRepo.Object.GetItems();

            products.FirstOrDefault(c => c.Id == _deletableProductDto.Id).ShouldBeNull();
            products.Count.ShouldBe(2);
        }

        [Fact]
        public async Task InValid_product_Deleted()
        {
            Exception ex = await Should.ThrowAsync<Exception>
                (async () =>
                        await _handler.Handle(new DeleteProductCommand() { Id = _notDeletableProductDto.Id }, CancellationToken.None)
                );

            var products = await _mockRepo.Object.GetItems();

            products.FirstOrDefault(c => c.Id == _notDeletableProductDto.Id).ShouldNotBeNull();
            products.Count.ShouldBe(3);
        }
    }
}
