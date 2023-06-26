using AutoMapper;
using MediatR;
using ProductCrud.Application.Contracts.Identity;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Application.Exceptions;
using ProductCrud.Application.Features.Product.Requests.Commands;

namespace ProductCrud.Application.Features.Product.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUserService userService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.Id) ?? throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

            if (_userService.GetCurrentUserName().Result != product.CreatedBy)
            {
                throw new Exception($"You don't have permission to make any changes to records created by someone else.");
            }

            await _productRepository.Delete(product);

            return Unit.Value;
        }
    }
}
