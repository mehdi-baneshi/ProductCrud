using AutoMapper;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.DTOs.Product;
using ProductCrud.Application.Features.Product.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCrud.Application.Helpers;

namespace ProductCrud.Application.Features.Product.Handlers.Queries
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, List<ProductGetDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsListQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductGetDto>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var filterExpressions = ApplicationHelper.GetExpressionsFromDto<Domain.Entities.Product, ProductFilterDto>(request.FilterDto);
            var products = await _productRepository.GetItems(filterExpressions);

            return _mapper.Map<List<ProductGetDto>>(products);
        }
    }
}
