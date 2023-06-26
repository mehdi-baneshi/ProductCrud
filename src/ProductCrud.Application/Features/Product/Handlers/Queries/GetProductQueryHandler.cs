using AutoMapper;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.DTOs.Product;
using ProductCrud.Application.Exceptions;
using ProductCrud.Application.Features.Product.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Features.Product.Handlers.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductGetDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductGetDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);
            }

            return _mapper.Map<ProductGetDto>(product);
        }
    }
}
