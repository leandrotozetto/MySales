using MediatR;
using Microsoft.AspNetCore.Mvc;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Requests.Commands.Product;
using MySales.Product.Api.Domain.Requests.Queries.Product;
using System;
using System.Threading.Tasks;

namespace MySales.Product.Api.Interface.Controllers
{
    /// <summary>
    /// Provides endpoints for product.
    /// </summary>
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly Domain.Core.Notifications.Interfaces.INotification _notification;

        /// <summary>
        /// Creates a instance of ProductController <see cref="ProductController"/>.
        /// </summary>
        /// <param name="mediator"></param>
        public ProductController(IMediator mediator,Domain.Core.Notifications.Interfaces.INotification notification)
        {
            _mediator = mediator;
            _notification = notification;
        }

        /// <summary>
        /// Get a product by Id.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <returns>Returns a product <see cref="Domain.Dtos.Product.ProductQueryDto"/>.</returns>
        [HttpGet("{productId}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var productQueryRequest = GetProductQueryRequest.New(id);
            var product = await _mediator.Send(productQueryRequest);

            return CreateResponse(product);
        }

        /// <summary>
        /// Get a paginate list.
        /// </summary>
        /// <param name="productFilterQueryRequest">Filters.</param>
        /// <returns>Returns a paginate list <see cref="IPaging{ProductQueryDto}"/></returns>
        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] ProductFilterQueryRequest productFilterQueryRequest)
        {
            var paginatedList = await _mediator.Send(productFilterQueryRequest);

            return CreateResponse(paginatedList);
        }

        /// <summary>
        /// Creates a product.
        /// </summary>
        /// <param name="productCommandRequest">Product's data.</param>
        /// <returns>Returns the product inserted.</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductCommandRequest productCommandRequest)
        {
            var insertProductCommandRequest = InsertProductCommandRequest.New(productCommandRequest.Name,
                productCommandRequest.Status,
                productCommandRequest.TenantId);

            await _mediator.Send(insertProductCommandRequest);

            return CreateResponse();
        }

        [HttpPost("{productId}/sku")]
        public async Task<ActionResult> CreateSku([FromBody] ProductCommandRequest productCommandRequest)
        {
            var insertProductCommandRequest = InsertProductCommandRequest.New(productCommandRequest.Name,
                productCommandRequest.Status,
                productCommandRequest.TenantId);

            await _mediator.Send(insertProductCommandRequest);

            return CreateResponse();
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="productCommandRequest">Product's data.</param>
        /// <returns>Returns the product updated.</returns>
        [HttpPut("{productId}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ProductCommandRequest productCommandRequest)
        {
            var updateProductCommandResquest = UpdateProductCommandResquest.New(productCommandRequest.Name,
                productCommandRequest.Status,
                id,
                productCommandRequest.TenantId);

            var product = await _mediator.Send(updateProductCommandResquest);

            return CreateResponse();
        }

        [HttpPut("{productId}/sku/{skuId}")]
        public async Task<ActionResult> UpdateSku(Guid id, [FromBody] ProductCommandRequest productCommandRequest)
        {
            var updateProductCommandResquest = UpdateProductCommandResquest.New(productCommandRequest.Name,
                productCommandRequest.Status,
                id,
                productCommandRequest.TenantId);

            var product = await _mediator.Send(updateProductCommandResquest);

            return CreateResponse();
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="productId">ProductId.</param>
        /// <returns>Returns transaction's status (http).</returns>
        [HttpDelete("{productId}")]
        public async Task<ActionResult> Delete(Guid productId)
        {
            var deleteProductRequest = DeleteProductCommandRequest.New(productId);

            await _mediator.Send(deleteProductRequest);

            return CreateResponse();
        }

        private ActionResult CreateResponse<T>(T result) where T : IEmpty<T>
        {
            if (_notification.HasErrors)
            {
                return ResponseError();
            }
            else if (result.IsEmpty)
            {
                return NoContent();
            }

            return Ok(result);

        }

        private ActionResult CreateResponse()
        {
            if (_notification.HasErrors)
            {
                return ResponseError();
            }

            return NoContent();
        }

        private ActionResult ResponseError()
        {
            return _notification.ErrorCode switch
            {
                400 => BadRequest(_notification.Errors),
                _ => StatusCode(500),
            };
        }
    }
}
