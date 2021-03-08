using MediatR;
using MySales.Product.Api.Domain.Dtos.Product;
using MySales.Product.Api.Domain.Identifiers;
using System;

namespace MySales.Product.Api.Domain.Requests.Queries.Product
{
    public class GetProductQueryRequest : IRequest<ProductQueryDto>
    {
        public ProductId ProductId { get; private set; }

        private GetProductQueryRequest() { }

        public static GetProductQueryRequest Empty = _empty;

        private static readonly GetProductQueryRequest _empty;

        static GetProductQueryRequest()
        {
            _empty = new GetProductQueryRequest
            {
                ProductId = ProductId.Empty
            };
        }

        public static GetProductQueryRequest New(Guid id)
        {
            return new GetProductQueryRequest
            {
                ProductId = ProductId.New(id)
            };
        }
    }
}
