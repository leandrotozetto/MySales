using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MySales.Product.Api.Domain.Core.Entities.Identifiers;
using MySales.Product.Api.Domain.Dtos.Product;
using MySales.Product.Api.Domain.Mappers;
using MySales.Product.Api.Domain.Requests.Commands.Product;
using MySales.Product.Api.Domain.Requests.Queries.Product;
using System;
using System.Threading.Tasks;

namespace MySales.Product.Api.Interface.Apis
{
    /// <summary>
    /// Provides endpoints for product.
    /// </summary>
    public class ProductApi
    {
        private readonly IMediator _mediator;

        private readonly Domain.Core.Notifications.Interfaces.INotification _notification;

        /// <summary>
        /// Creates a instance of ProductApi <see cref="ProductApi"/>.
        /// </summary>
        /// <param name="app">Provide of the mechanisms to configure an application's request</param>
        public ProductApi(IApplicationBuilder app, IMediator mediator, Domain.Core.Notifications.Interfaces.INotification notification)
        {
            _mediator = mediator;
            _notification = notification;
            Config(app);

            // _productApplication = app.ApplicationServices.GetService(typeof(IProductApplication)) as IProductApplication;
        }

        /// <summary>
        /// Configures methods of the product api.
        /// </summary>
        /// <param name="app">Provide of the mechanisms to configure an application's request</param>
        public void Config(IApplicationBuilder app) =>
            app.UseCors("CorsPolicy")
            .UseRouter(x =>
            {
                ///Method get a product.
                x.MapGet("products/{id}", GetByIdAsync);

                //Method for list products.
                x.MapGet("products/{filter}/{orderby}/{page:int}/{qtyperpage:int}", GetList);

                ///Method for create product.
                x.MapPost("products", Create);

                ///Method for update product.
                x.MapPut("products/{id}", Update);

                ///Method for update product.
                x.MapDelete("products/{id}", Delete);

                //_app.UseExceptionMiddleware();
            });

        private Func<HttpRequest, HttpResponse, RouteData, Task> GetByIdAsync => async (request, response, routeData) =>
        {
            var id = routeData.GetValue<Guid>("id");

            var productQueryRequest = GetProductQueryRequest.New(id);

            var product = await _mediator.Send(productQueryRequest);

            await response.Create(_notification, product);
        };

        private Func<HttpRequest, HttpResponse, RouteData, Task> GetList => async (request, response, routeData) =>
        {
            var listProductRequest = CreateListProductQueryRequest(routeData);
            var paginatedList = await _mediator.Send(listProductRequest);

            await response.Create(_notification, paginatedList);
        };

        private Func<HttpRequest, HttpResponse, RouteData, Task> Create => async (request, response, routeData) =>
        {
            var productCommandDto = await request.HttpContext.ReadFromJson<ProductCommandDto>();
            //TODO: obter tenantId
            var tenantId = TenantId.New().Value;
            var insertProductCommandRequest = ProductMapper.Map(productCommandDto, tenantId);

            await _mediator.Send(insertProductCommandRequest);

            await response.Create(_notification);
        };

        private Func<HttpRequest, HttpResponse, RouteData, Task> Update => async (request, response, routeData) =>
        {
            var id = routeData.GetValue<Guid>("id");
            //var dto = await request.HttpContext.ReadFromJson<ProductDto>(_webHostEnvironment);
            //https://docs.microsoft.com/pt-br/ef/core/saving/disconnected-entities
            //http://blog.maskalik.com/entity-framework/2013/12/23/entity-framework-updating-database-from-detached-objects/
            //await _productApplication.UpdateAsync(id, dto);

            await response.Create(_notification);
        };

        private Func<HttpRequest, HttpResponse, RouteData, Task> Delete => async (request, response, routeData) =>
        {
            var id = routeData.GetValue<Guid>("id");
            var deleteProductRequest = DeleteProductCommandRequest.New(id);

            await _mediator.Send(deleteProductRequest);

            await response.Create(_notification);
        };

        private ProductFilterQueryRequest CreateListProductQueryRequest(RouteData routeData)
        {
            var name = routeData.GetValue<string>("name");
            var status = routeData.GetValue<bool?>("status");
            var orderby = routeData.GetValue<string>("orderby");
            var currentPage = routeData.GetValue<int>("currentPage");
            var itemsPerPage = routeData.GetValue<int>("itemsPerPage");

            return ProductFilterQueryRequest.New(name, status, orderby, currentPage, itemsPerPage);
        }
    }

    /// <summary>
    /// Provides that extensions's method for ProductApi
    /// </summary>
    public static class ProductApiExtensions
    {
        /// <summary>
        /// Registers a ProductApi.
        /// </summary>
        /// <param name="app">Provide of the mechanisms to configure an application's request</param>
        public static void UseProductApi(this IApplicationBuilder app, IMediator mediator, Domain.Core.Notifications.Interfaces.INotification notification)
        {
            new ProductApi(app, mediator, notification);
        }
    }
}
