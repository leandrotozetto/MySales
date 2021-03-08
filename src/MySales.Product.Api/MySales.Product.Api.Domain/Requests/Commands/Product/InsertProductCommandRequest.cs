using MediatR;
using System;

namespace MySales.Product.Api.Domain.Requests.Commands.Product
{
    public class InsertProductCommandRequest : ProductCommandRequest, IRequest<bool>
    {
        private InsertProductCommandRequest() { }

        public static InsertProductCommandRequest New(string name, bool status, Guid tenantId)
        {
            return new InsertProductCommandRequest
            {
                Name = name,
                Status = status,
                TenantId = tenantId
            };
        }
    }
}
