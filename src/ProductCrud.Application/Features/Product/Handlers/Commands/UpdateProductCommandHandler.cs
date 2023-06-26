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
using ProductCrud.Application.Contracts.Identity;

namespace ProductCrud.Application.Features.product.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUserService userService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.ProductDto.Id) ?? throw new NotFoundException(nameof(Domain.Entities.Product), request.ProductDto.Id);

            if (_userService.GetCurrentUserName().Result != product.CreatedBy)
            {
                throw new Exception($"You don't have permission to make any changes to records created by someone else.");
            }

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
