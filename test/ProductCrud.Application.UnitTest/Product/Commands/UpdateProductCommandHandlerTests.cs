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
using ProductCrud.Application.Features.product.Handlers.Commands;
using ProductCrud.Application.Contracts.Identity;

namespace ProductCrud.Application.UnitTests.products.Commands
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<IUserService> _mockUserService;
        private readonly ProductUpdateDto _editableProductDto;
        private readonly ProductUpdateDto _notEditableProductDto;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _mockRepo = MockProductRepository.GetProductRepository();
            _mockUserService = MockUserService.GetUserService();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateProductCommandHandler(_mockRepo.Object, _mapper, _mockUserService.Object);

            _editableProductDto = new ProductUpdateDto
            {
                Id = 2,
                Name = "Breitling Watch, Top Time B01 Ford ThunderBird",
                IsAvailable = false,
                ManufacturePhone = "+989112223335",
                ManufactureEmail = "info.us@breitling.com",
                ProduceDate = DateTime.Now.AddYears(-12)
            };

            _notEditableProductDto = new ProductUpdateDto
            {
                Id = 3,
                Name = "Alpiner Watch, Heritage Carrée Mechanical 140 Years",
                IsAvailable = true,
                ManufacturePhone = "+19994445557",
                ManufactureEmail = "customercare@us.alpinawathes.com",
                ProduceDate = DateTime.Now.AddYears(-13)
            };
        }

        [Fact]
        public async Task Valid_product_Edited()
        {
            var result = await _handler.Handle(new UpdateProductCommand() { ProductDto = _editableProductDto }, CancellationToken.None);

            var products = await _mockRepo.Object.GetItems();
            var editedproduct = products.First(c => c.Id == _editableProductDto.Id);

            editedproduct.Name.ShouldBeEquivalentTo(_editableProductDto.Name);
            editedproduct.ManufactureEmail.ShouldBeEquivalentTo(_editableProductDto.ManufactureEmail);
            editedproduct.ManufacturePhone.ShouldBeEquivalentTo(_editableProductDto.ManufacturePhone);
            editedproduct.ProduceDate.ShouldBeEquivalentTo(_editableProductDto.ProduceDate);
            editedproduct.IsAvailable.ShouldBeEquivalentTo(_editableProductDto.IsAvailable);

            products.Count.ShouldBe(3);
        }

        [Fact]
        public async Task InValid_LeaveType_Added()
        {
            var beforeEditProducts = await _mockRepo.Object.GetItems();
            var beforeEditproduct = beforeEditProducts.First(c => c.Id == _editableProductDto.Id);

            Exception ex = await Should.ThrowAsync<Exception>
                (async () =>
                        await _handler.Handle(new UpdateProductCommand() { ProductDto = _notEditableProductDto }, CancellationToken.None)
                );

            var editedProducts = await _mockRepo.Object.GetItems();
            var editedproduct = editedProducts.First(c => c.Id == _editableProductDto.Id);

            editedproduct.Name.ShouldBeEquivalentTo(beforeEditproduct.Name);
            editedproduct.ManufactureEmail.ShouldBeEquivalentTo(beforeEditproduct.ManufactureEmail);
            editedproduct.ManufacturePhone.ShouldBeEquivalentTo(beforeEditproduct.ManufacturePhone);
            editedproduct.ProduceDate.ShouldBeEquivalentTo(beforeEditproduct.ProduceDate);
            editedproduct.IsAvailable.ShouldBeEquivalentTo(beforeEditproduct.IsAvailable);

            editedProducts.Count.ShouldBe(3);

            ex.ShouldNotBeNull();
        }
    }
}
