using Microsoft.EntityFrameworkCore;
using MySales.Product.Api.Domain.Aggregates.Interfaces;
using MySales.Product.Api.Domain.Core.Entities;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Identifiers;
using MySales.Product.Api.Domain.Interfaces.Repositories;
using MySales.Product.Api.Domain.Interfaces.Repositories.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MySales.Product.Api.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepository<Domain.Aggregates.Product> _repository;

        public ProductRepository(IRepository<Domain.Aggregates.Product> repository)
        {
            _repository = repository;
        }

        public async Task<IListPage<IProduct>> ListAsync(IProductFilter productFilter)
        {
            var filters = GetFilter(productFilter.Name, productFilter.Status);

            var query = _repository.Query
                .Where(filters);

            var entities = await query
                .OrderBy(productFilter.OrderBy)
                .ListAsync(productFilter.CurrentPage, productFilter.ItemsPerPage) as IEnumerable<IProduct>;

            if (entities.Any())
            {
                var totalItems = await query.CountAsync(filters);

                return ListPage<IProduct>.New(entities, totalItems);
            }

            return ListPage<IProduct>.Empty;
        }

        public async Task<IProduct> GetAsync(ProductId productId)
        {
            var entity = await _repository.GetAsync(x => x.ProductId.Equals(productId));

            if (entity is null)
            {
                return Domain.Aggregates.Product.NewEmpty();
            }

            return entity;
        }

        public async Task<bool> InsertAsync(IProduct product)
        {
            await _repository.InsertAsync(product as Domain.Aggregates.Product);

            return true;
        }

        public bool Update(IProduct product)
        {
            _repository.Update(product as Domain.Aggregates.Product);

            return true;
        }

        public async Task<bool> DeleteAsync(ProductId productId)
        {
            if (productId.IsEmpty)
            {
                return false;
            }

            var product = await GetAsync(productId);

            _repository.Delete(product as Domain.Aggregates.Product);

            return true;
        }

        private IEnumerable<Expression<Func<Domain.Aggregates.Product, bool>>> GetFilter(string name, bool? status)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                yield return x => EF.Functions.Like(x.Name, $"{name}%");
            }

            if (status != null)
            {
                yield return x => x.Status.Value == status;
            }
        }

        private bool disposedValue = false;

        /// <summary>
        /// Dispose managed resource.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _repository.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose managed resource.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
