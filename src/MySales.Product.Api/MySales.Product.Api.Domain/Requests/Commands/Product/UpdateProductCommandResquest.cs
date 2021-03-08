using MediatR;
using System;

namespace MySales.Product.Api.Domain.Requests.Commands.Product
{
    public class UpdateProductCommandResquest : ProductCommandRequest, IRequest<bool>
    {
        public Guid ProductId { get; private set; }

        private UpdateProductCommandResquest() { }

        public static UpdateProductCommandResquest New(string name, bool status, Guid productId, Guid tenantId)
        {
            return new UpdateProductCommandResquest
            {
                Name = name,
                Status = status,
                ProductId = productId,
                TenantId = tenantId
            };
        }
    }
}
