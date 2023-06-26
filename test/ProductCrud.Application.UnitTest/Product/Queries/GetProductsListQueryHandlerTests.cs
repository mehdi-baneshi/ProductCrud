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
    public class GetProductsListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductFilterDto _ProductFilter;
        private readonly GetProductsListQueryHandler _handler;

        public GetProductsListQueryHandlerTests()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetProductsListQueryHandler(_mockRepo.Object, _mapper);

            _ProductFilter = new ProductFilterDto()
            {
                CreatedBy = "test"
            };
        }

        [Fact]
        public async Task Valid_GetAllProducts()
        {
            var result = await _handler.Handle(new GetProductsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<ProductGetDto>>();

            result.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Valid_GetFilterdProducts()
        {
            var result = await _handler.Handle(new GetProductsListQuery(){ FilterDto= _ProductFilter }, CancellationToken.None);

            result.ShouldBeOfType<List<ProductGetDto>>();

            result.Count.ShouldBe(1);
            result[0].CreatedBy.ShouldBe(_ProductFilter.CreatedBy);
        }
    }
}
