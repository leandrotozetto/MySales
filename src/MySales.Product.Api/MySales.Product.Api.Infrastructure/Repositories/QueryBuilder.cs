using Microsoft.EntityFrameworkCore;
using MySales.Product.Api.Domain.Interfaces.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MySales.Product.Api.Infrastructure.Repositories
{
    public class QueryBuilder<T> : IQueryBuilder<T>
         where T : class
    {
        protected DbContext Context;

        /// <summary>
        /// Can be used to linq query.
        /// </summary>
        public DbSet<T> DbSet { get; private set; }


        private IQueryable<T> _query;

        /// <summary>
        /// Creates a new instance of Repository <see cref="QueryBuilder"/>.
        /// </summary>
        /// <param name="context">Context for queries in db.</param>
        public QueryBuilder(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public IQueryInclude<T> Where(IEnumerable<Expression<Func<T, bool>>> filters)
        {
            _query = DbSet.AsNoTracking().AsQueryable();

            foreach (var filter in filters ?? Enumerable.Empty<Expression<Func<T, bool>>>())
            {
                _query = _query.Where(filter);
            }

            return this;
        }

        public IQueryInclude<T> Include(Expression<Func<T, object>> includes)
        {
            if (includes != null)
            {
                _query.Include(includes);
            }

            return this;
        }

        public IQueryResult<T> OrderBy(string orderColumn, string orderType = "asc")
        {
            var orderBy = CreateOrderBy(orderColumn, orderType);

            if (orderBy != null)
            {
                _query = orderBy(_query);
            }

            return this;
        }


        private Func<IQueryable<T>, IOrderedQueryable<T>> CreateOrderBy(string orderColumn, string orderType = null)
        {
            if (string.IsNullOrWhiteSpace(orderColumn))
            {
                return null;
            }

            Type typeQueryable = typeof(IQueryable<T>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            string[] props = orderColumn.ToLower().Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            var hasProp = type.GetProperties().FirstOrDefault(x => props.Contains(x.Name.ToLower()) == true);

            if (hasProp == null)
            {
                //TODO: corrigir exception
                //throw new DomainException(orderColumn, "O nome da coluna utilizado para ordenação é inválido");
            }

            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (pi != null)
                {
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
            }

            LambdaExpression lambda = Expression.Lambda(expr, arg);
            string methodName = orderType == "asc" || string.IsNullOrWhiteSpace(orderType) ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp =
                Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), type }, outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);

            return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
        }

        public async Task<T> SingleAsync()
        {
            return await _query.FirstAsync();
        }

        public TResult FieldAsync<TResult>(Func<T, TResult> column, Expression<Func<T, bool>> filter)
        {
            return _query.Where(filter).Select(column).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> ListAsync(int currentPage, int itemsPerPage)
        {
            itemsPerPage = itemsPerPage == 0 ? 1 : itemsPerPage;
            var skip = currentPage <= 1 ? 0 : (currentPage - 1) * itemsPerPage;

            if (itemsPerPage == 0)
            {
                itemsPerPage = 50;
            }

            return await _query.Skip(skip).Take(itemsPerPage).ToListAsync();
        }

        public async Task<int> CountAsync(IEnumerable<Expression<Func<T, bool>>> filters)
        {
            var query = DbSet.AsNoTracking().AsQueryable();

            foreach (var filter in filters ?? Enumerable.Empty<Expression<Func<T, bool>>>())
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }
    }
}
