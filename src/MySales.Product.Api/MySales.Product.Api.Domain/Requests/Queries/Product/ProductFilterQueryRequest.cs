using MediatR;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Dtos.Product;
using MySales.Product.Api.Domain.Interfaces.Repositories.Filters;

namespace MySales.Product.Api.Domain.Requests.Queries.Product
{
    public class ProductFilterQueryRequest : IRequest<IPaging<ProductQueryDto>>, IProductFilter
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Status.
        /// </summary>        
        public bool? Status { get; set; }

        /// <summary>
        /// Current page.
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Quantity of items per page.
        /// </summary>
        public int ItemsPerPage { get; set; }

        public ProductFilterQueryRequest() { }

        public static ProductFilterQueryRequest New(string name, bool? status, string orderBy = null,
            int currentPage = 0, int itemsPerPage = 0)
        {
            return new ProductFilterQueryRequest()
            {
                Name = name,
                Status = status,
                OrderBy = orderBy,
                CurrentPage = currentPage,
                ItemsPerPage = itemsPerPage
            };
        }
    }
}
