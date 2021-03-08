using MediatR;
using MySales.Product.Api.Domain.Identifiers;
using MySales.Product.Api.Domain.Interfaces.Applications;
using MySales.Product.Api.Domain.Requests.Commands.Product;
using System.Threading;
using System.Threading.Tasks;

namespace MySales.Product.Api.Domain.Handlers.Commands.Product
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandResquest, bool>
    {
        private readonly IProductApplication _productApplication;

        public UpdateProductCommandHandler(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        public async Task<bool> Handle(UpdateProductCommandResquest request, CancellationToken cancellationToken)
        {
            return await _productApplication.UpdateAsync(request, ProductId.New(request.ProductId));
        }
    }
}
