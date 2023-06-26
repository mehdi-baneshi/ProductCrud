using ProductCrud.Application.DTOs.Product;
using ProductCrud.Application.Features.Product.Requests.Commands;
using ProductCrud.Application.Features.Product.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProductCrud.Application.Constants;

namespace ProductCrud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<List<ProductGetDto>>> Get([FromQuery] ProductFilterDto filterDto)
        {
            var products = await _mediator.Send(new GetProductsListQuery
            {
                FilterDto = filterDto
            });

            return Ok(products);
        }

        // GET api/<PproductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGetDto>> Get(int id)
        {
            var product = await _mediator.Send(new GetProductQuery { Id = id });

            return Ok(product);
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] ProductCreateDto product)
        {
            var command = new CreateProductCommand { ProductDto = product};
            var repsonse = await _mediator.Send(command);

            return Ok(repsonse);
        }

        // PUT api/<productsController>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] ProductUpdateDto product)
        {
            var command = new UpdateProductCommand { ProductDto = product };
            await _mediator.Send(command);

            return NoContent();
        }

        // DELETE api/<productsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
