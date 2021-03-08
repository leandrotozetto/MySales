using MediatR;
using MySales.Product.Api.Domain.Interfaces.Applications;
using MySales.Product.Api.Domain.Requests.Commands.Product;
using System.Threading;
using System.Threading.Tasks;

namespace MySales.Product.Api.Domain.Handlers.Commands.Product
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, bool>
    {
        private readonly IProductApplication _productApplication;

        public DeleteProductCommandHandler(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        public async Task<bool> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productApplication.DeleteAsync(request.ProductId);

            return  true;
        }
    }
}
