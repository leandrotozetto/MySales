using MediatR;
using MySales.Product.Api.Domain.Dtos.Product;
using MySales.Product.Api.Domain.Interfaces.Applications;
using MySales.Product.Api.Domain.Requests.Queries.Product;
using System.Threading;
using System.Threading.Tasks;

namespace MySales.Product.Api.Domain.Handlers.Queries.Product
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, ProductQueryDto>
    {
        private readonly IProductApplication _productApplication;

        public GetProductQueryHandler(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        public async Task<ProductQueryDto> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            return await _productApplication.GetAsync(request.ProductId);
        }
    }
}


//public class UsersController : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public UsersController(IMediator mediator)
//    {
//        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
//    }

//    [HttpGet("{id}")]
//    public async Task<Order> Get(int id)
//    {
//        var user = await _mediator.Send(new GetUserDetailQuery(id));
//        if user == null {
//            return NotFound();
//        }
//        return Ok(user);
//    }
//}