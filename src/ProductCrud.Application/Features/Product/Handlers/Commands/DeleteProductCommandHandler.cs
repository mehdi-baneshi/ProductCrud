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

namespace ProductCrud.Application.Features.Product.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.Id) ?? throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

            await _productRepository.Delete(product);

            return Unit.Value;
        }
    }
}
