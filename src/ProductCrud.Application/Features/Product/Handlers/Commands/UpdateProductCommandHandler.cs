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

namespace ProductCrud.Application.Features.product.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.ProductDto.Id) ?? throw new NotFoundException(nameof(Domain.Entities.Product), request.ProductDto.Id);

            var validator = new ProductUpdateDtoValidator(_productRepository);
            var validationResult = await validator.ValidateAsync(request.ProductDto);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            if (request.ProductDto != null)
            {
                _mapper.Map(request.ProductDto, product);

                await _productRepository.Update(product);
            }

            return Unit.Value;
        }
    }
}
