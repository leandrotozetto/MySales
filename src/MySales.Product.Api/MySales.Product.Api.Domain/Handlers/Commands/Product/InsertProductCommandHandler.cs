using MediatR;
using MySales.Product.Api.Domain.Interfaces.Applications;
using MySales.Product.Api.Domain.Requests.Commands.Product;
using System.Threading;
using System.Threading.Tasks;

namespace MySales.Product.Api.Domain.Handlers.Commands.Product
{
    public class InsertProductCommandHandler : IRequestHandler<InsertProductCommandRequest, bool>
    {
        private readonly IProductApplication _productApplication;

        public InsertProductCommandHandler(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        public async Task<bool> Handle(InsertProductCommandRequest request, CancellationToken cancellationToken)
        {
            return await _productApplication.InsertAsync(request);
        }
    }
}
