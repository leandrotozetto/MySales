using MySales.Product.Api.Domain.Aggregates.Interfaces;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Identifiers;
using MySales.Product.Api.Domain.Interfaces.Repositories.Filters;
using System;
using System.Threading.Tasks;

namespace MySales.Product.Api.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IDisposable
    {
        Task<IProduct> GetAsync(ProductId productId);

        Task<bool> InsertAsync(IProduct product);

        bool Update(IProduct product);

        Task<IListPage<IProduct>> ListAsync(IProductFilter productFilter);

        Task<bool> DeleteAsync(ProductId productId);
    }
}