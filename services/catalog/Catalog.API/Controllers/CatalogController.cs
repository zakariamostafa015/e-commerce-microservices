using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    public class CatalogController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IList<ProductResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductResponseDto>> GetProductByName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var results = await _mediator.Send(query);
            if (results == null)
            {
                return NotFound();
            }
            return Ok(results);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<ProductResponseDto>>> GetAllProducts([FromQuery] CatalogSpecsParams catalogSpecsParams)
        {
            var query = new GetAllProductsQuery(catalogSpecsParams);
            var results = await _mediator.Send(query);
            
            return Ok(results);
        }

        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return BadRequest("Product creation failed.");
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseDto>> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return BadRequest("Product update failed.");
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("[action]/{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseDto>> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest("Product deletion failed.");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<BrandResponseDto>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var results = await _mediator.Send(query);

            return Ok(results);
        }

        [HttpGet]
        [Route("GetAllProductTypes")]
        [ProducesResponseType(typeof(IList<ProductTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<ProductTypeResponseDto>>> GetAllProductTypes()
        {
            var query = new GetAllProductTypesQuery();
            var results = await _mediator.Send(query);

            return Ok(results);
        }
    }
}
