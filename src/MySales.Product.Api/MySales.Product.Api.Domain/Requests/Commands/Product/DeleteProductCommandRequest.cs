using MediatR;
using MySales.Product.Api.Domain.Identifiers;
using System;

namespace MySales.Product.Api.Domain.Requests.Commands.Product
{
    public class DeleteProductCommandRequest : IRequest<bool>
    {
        public ProductId ProductId { get; private set; }

        private DeleteProductCommandRequest() { }

        public static DeleteProductCommandRequest New(Guid productId)
        {
            return new DeleteProductCommandRequest
            {
                ProductId = ProductId.New(productId)
            };
        }
    }
}
