using AutoMapper;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.Validators.Product;
using ProductCrud.Application.Exceptions;
using ProductCrud.Application.Features.Product.Requests.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCrud.Domain.Entities;

namespace ProductCrud.Application.Features.Product.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new ProductCreateDtoValidator(_productRepository);
            var validationResult = await validator.ValidateAsync(request.ProductDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var product = _mapper.Map<Domain.Entities.Product>(request.ProductDto);

            product = await _productRepository.Add(product);

            return product.Id;
        }
    }
}
