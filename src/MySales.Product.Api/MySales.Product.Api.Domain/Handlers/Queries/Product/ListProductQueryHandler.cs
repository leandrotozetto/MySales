using MediatR;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Dtos.Product;
using MySales.Product.Api.Domain.Interfaces.Applications;
using MySales.Product.Api.Domain.Requests.Queries.Product;
using System.Threading;
using System.Threading.Tasks;

namespace MySales.Product.Api.Domain.Handlers.Queries.Product
{
    public class ListProductQueryHandler : IRequestHandler<ProductFilterQueryRequest, IPaging<ProductQueryDto>>
    {
        private readonly IProductApplication _productApplication;

        public ListProductQueryHandler(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        public async Task<IPaging<ProductQueryDto>> Handle(ProductFilterQueryRequest listProductQueryRequest, CancellationToken cancellationToken)
        {
            return await _productApplication.ListAsync(listProductQueryRequest);
        }
    }
}
