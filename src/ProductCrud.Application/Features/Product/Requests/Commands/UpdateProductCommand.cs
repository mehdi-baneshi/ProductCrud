using ProductCrud.Application.DTOs.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Features.Product.Requests.Commands
{
    public class UpdateProductCommand:IRequest<Unit>
    {
        public ProductUpdateDto ProductDto { get; set; }
    }
}
