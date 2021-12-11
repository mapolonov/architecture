using System;
using System.Threading;
using System.Threading.Tasks;
using CQRSApiVariant01.Features.Commands;
using CQRSApiVariant01.Features.Queries;
using CQRSApiVariant01.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CQRSApiVariant01.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)] // !!!! for swagger documentation
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post([FromBody] AddProductCommand client, ApiVersion apiVersion,
            CancellationToken token)
        {
            var entity = await _mediator.Send(client, token);
            return CreatedAtAction(nameof(Get), new { id = entity.Id, version = apiVersion.ToString() }, entity);
        }

        [HttpGet]
        [ProducesResponseType(typeof(DataWithTotal<Product>), StatusCodes.Status200OK)] // !!!! for swagger documentation
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // !!!! for swagger documentation
        [ProducesResponseType(StatusCodes.Status404NotFound)] // !!!! for swagger documentation
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DataWithTotal<Product>>> Get([FromQuery] GetProductsQuery request,
            CancellationToken token) =>
            Ok(await _mediator.Send(request, token));
    }
}
