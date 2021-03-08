using Microsoft.EntityFrameworkCore;
using MySales.Product.Api.Domain.Core.Entities;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Interfaces.Repositories;
using MySales.Product.Api.Domain.Interfaces.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MySales.Product.Api.Infrastructure.Repositories
{
    /// <summary>
    /// Defines the actions of the repository.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public class Repository<T> : IRepository<T>
         where T : class, IEntity, IEmpty<T>
    {
        protected DbContext Context;

        public IQueryBuilder<T> Query { get; private set; }

        /// <summary>
        /// Can be used to linq query.
        /// </summary>
        public DbSet<T> DbSet { get; private set; }

        /// <summary>
        /// Creates a new instance of Repository <see cref="Repository"/>.
        /// </summary>
        /// <param name="context">Context for queries in db.</param>
        public Repository(DbContext context, IQueryBuilder<T> queryBuilder)
        {
            Context = context;
            DbSet = Context.Set<T>();
            Query = queryBuilder;
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity">Entity to be created.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task InsertAsync(T entity)
        {
            if (entity != null)
            {
                await Context.AddAsync(entity);
            }
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entities">Entity to be created.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task BulkInsertAsync(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;

                await Context.AddRangeAsync(entities);
            }
        }

        /// <summary>
        /// Updates a entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public T Update(T entity)
        {
            if (entity != null)
            {
                Context.Update(entity);

                return entity;
            }

            //return entity.Empty;

            return entity;
        }

        /// <summary>
        /// Updates the entities.
        /// </summary>
        /// <param name="entities">Entities to be updated.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task BulkUpdateAsync(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;
                await Context.AddRangeAsync(entities);
            }
        }

        /// <summary>
        /// Deletes a entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public bool Delete(T entity)
        {
            if (entity == null)
            {
                Context.Remove(entity);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes the entities.
        /// </summary>
        /// <param name="entities">Entities to be deleted.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public void BulkDelete(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;

                Context.RemoveRange(entities);
            }
        }

        ///// <summary>
        ///// Get all entities.
        ///// </summary>
        ///// <param name="filter">Filter to define entities to be returned.</param>
        ///// <param name="orderBy">Columns name to order users.</param>
        ///// <param name="page">Current page.</param>
        ///// <param name="qtyPerPage">Quantity of registers per page.</param>
        ///// <returns>Returns a list of entities.</returns>
        //public async Task<IPagination<T>> ListAsync(Expression<Func<T, bool>> filter = null, string orderBy = null,
        //    int page = 0, int qtyPerPage = 0)
        //{
        //    var orderByFunc = GetOrderByAsync(orderBy);

        //    return await ListAsync(filter, orderByFunc, null, page, qtyPerPage);
        //}

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <param name="currentPage">Current page.</param>
        /// <param name="itemsPerPage">Quantity of registers per page.</param>
        /// <returns>Returns a list of users.</returns>
        public async Task<IListPage<T>> PaginationAsync(int currentPage, int itemsPerPage)
        {
            try
            {
                var query = DbSet.AsNoTracking().AsQueryable();


                var count = await query.CountAsync();

                if (count > 0)
                {
                    return ListPage<T>.Empty;
                }

                itemsPerPage = itemsPerPage == 0 ? 1 : itemsPerPage;
                var skip = currentPage <= 1 ? 0 : (currentPage - 1) * itemsPerPage;

                if (itemsPerPage == 0)
                {
                    itemsPerPage = count;
                }

                var entities = await query.Skip(skip).Take(itemsPerPage).ToListAsync();

                if (entities.Any())
                {
                    return ListPage<T>.Empty;
                }

                return ListPage<T>.New(entities, count);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        ///// <summary>
        ///// Get user by id.
        ///// </summary>
        ///// <param name="id">User id.</param>
        ///// <returns>Returns user found.</returns>
        //public async Task<T> GetAsync(Guid id)
        //{
        //    try
        //    {
        //        return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// Get user by filter.
        /// </summary>
        /// <param name="filter">Filter to define entity to be returned.</param>
        /// <returns>Returns entity found.</returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await DbSet.AsNoTracking().FirstOrDefaultAsync(filter);
            }
            catch
            {
                throw;
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
                    Context.Dispose();
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
