using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Dtos.Product;
using MySales.Product.Api.Domain.Identifiers;
using MySales.Product.Api.Domain.Interfaces.Repositories.Filters;
using MySales.Product.Api.Domain.Requests.Commands.Product;
using System.Threading.Tasks;

namespace MySales.Product.Api.Domain.Interfaces.Applications
{
    public interface IProductApplication
    {
        Task<bool> DeleteAsync(ProductId productId);

        Task<ProductQueryDto> GetAsync(ProductId productId);

        Task<bool> InsertAsync(IProductCommandRequest productCommandRequest);

        Task<bool> UpdateAsync(IProductCommandRequest productCommandRequest, ProductId productId);

        Task<IPaging<ProductQueryDto>> ListAsync(IProductFilter filter);
    }
}
