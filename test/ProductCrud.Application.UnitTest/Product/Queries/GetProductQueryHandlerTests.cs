using AutoMapper;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.DTOs.Product;
using ProductCrud.Application.Features.Product.Handlers.Commands;
using ProductCrud.Application.Features.Product.Handlers.Queries;
using ProductCrud.Application.Features.Product.Requests.Commands;
using ProductCrud.Application.Features.Product.Requests.Queries;
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

namespace ProductCrud.Application.UnitTests.products.Queries
{
    public class GetProductQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly GetProductQueryHandler _handler;

        public GetProductQueryHandlerTests()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetProductQueryHandler(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task Valid_Getproduct()
        {
            int productId = 1;
            var result = await _handler.Handle(new GetProductQuery() { Id = productId }, CancellationToken.None);

            var products = await _mockRepo.Object.GetItems();

            result.Id.ShouldBe(productId);
            result.ShouldBeOfType<ProductGetDto>();
            products.FirstOrDefault(c => c.Id == result.Id).ShouldNotBeNull();
        }
    }
}
