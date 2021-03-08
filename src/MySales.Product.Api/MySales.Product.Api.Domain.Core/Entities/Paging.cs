using Microsoft.EntityFrameworkCore;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MySales.Product.Api.Domain.Core.Entities
{
    /// <summary>
    /// Return a list of entities with informations of the pagetion.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    [Serializable]
    public class Paging<T> : IPaging<T>
        where T : class
    {
        /// <summary>
        /// List of the entities.
        /// </summary>
        public IEnumerable<T> Entities { get; private set; }

        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Total of pages.
        /// </summary>
        public int TotalPages => Count == 0 || Entities == null ? 0 : Count % ItemsPerPage == 0 ? Count / ItemsPerPage : (Count / ItemsPerPage) + 1;

        /// <summary>
        /// Total quantity of entities.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Quantity of items per page.
        /// </summary>
        public int ItemsPerPage { get; private set; }

        public static IPaging<T> Empty { get; } = new Paging<T>();

        public bool IsEmpty => Equals(Empty);

        /// <summary>
        /// Create a pagetion object.
        /// </summary>
        private Paging() { }

        static Paging()
        {
            Empty ??= new Paging<T>()
            {
                Entities = Enumerable.Empty<T>()
            };
        }

        /// <summary>
        /// Create a pagetion object.
        /// </summary>
        /// <param name="query">Entity will be display.</param>
        /// <param name="itemsPerPage">Quantity of register per page.</param>
        /// <param name="currentPage">Current page.</param>
        //public static IPagination<T> New(IQueryable<T> query, int count, int itemsPerPage)
        //{
        //    //Ensure.That(totalPages, nameof(totalPages)).HasValue();
        //    //Ensure.That(currentPage, nameof(currentPage)).HasValue();
        //    //Ensure.That(itemsPerPage, nameof(itemsPerPage)).HasValue();
        //    //Ensure.That(entities, nameof(entities)).EntityIsNotNull();

        //    return new Pagination<T>()
        //    {
        //        _query = query,
        //        ItemsPerPage = itemsPerPage,
        //        Count = count
        //    };
        //}

        /// <summary>
        /// Create a pagetion object.
        /// </summary>
        /// <param name="entities">Entity will be display.</param>
        /// <param name="itemsPerPage">Quantity of register per page.</param>
        /// <param name="currentPage">Current page.</param>
        public static IPaging<T> New(IEnumerable<T> entities, int currentPage, int itemsPerPage)
        {
            //Ensure.That(totalPages, nameof(totalPages)).HasValue();
            //Ensure.That(currentPage, nameof(currentPage)).HasValue();
            //Ensure.That(itemsPerPage, nameof(itemsPerPage)).HasValue();
            //Ensure.That(entities, nameof(entities)).EntityIsNotNull();

            return new Paging<T>()
            {
                Entities = entities,
                ItemsPerPage = itemsPerPage,
                CurrentPage = currentPage
            };
        }

        //public async Task<IPaginatedList<T>> CreatePaginatedList()
        //{
        //    Count = await _query.CountAsync();
        //    ItemsPerPage = ItemsPerPage == 0 ? 1 : ItemsPerPage;
        //    var skip = CurrentPage <= 1 ? 0 : (CurrentPage - 1) * ItemsPerPage;

        //    if (ItemsPerPage == 0)
        //    {
        //        ItemsPerPage = Count;
        //    }

        //    Entities = await _query.Skip(skip).Take(ItemsPerPage).ToListAsync();

        //    if (Entities.Any())
        //    {
        //        return this;
        //    }

        //    return Empty;
        //}

        /// <summary>
        /// Releases unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

       //public IPagination<T> Where(IEnumerable<Expression<Func<T, bool>>> filters)
       // {
       //     foreach (var filter in filters ?? Enumerable.Empty<Expression<Func<T, bool>>>())
       //     {
       //         _query = _query.Where(filter);
       //     }

       //     return this;
       // }

       // public IPagination<T> Include(Expression<Func<T, object>> includes)
       // {
       //     if (includes != null)
       //     {
       //         _query.Include(includes);
       //     }

       //     return this;
       // }

       // public IPagination<T> OrderBy(string orderColumn, string orderType = "asc")
       // {
       //     var orderBy = CreateOrderBy(orderColumn, orderType);

       //     if (orderBy != null)
       //     {
       //         _query = orderBy(_query);
       //     }
 
       //     return this;
       // }

       // public IPagination<T> ApplyFilter(Expression<Func<T, bool>> predicate)
       // {
       //     _query = _query.Where(predicate);

       //     return this;
       // }

        //private Func<IQueryable<T>, IOrderedQueryable<T>> CreateOrderBy(string orderColumn, string orderType = null)
        //{
        //    if (string.IsNullOrWhiteSpace(orderColumn))
        //    {
        //        return null;
        //    }

        //    Type typeQueryable = typeof(IQueryable<T>);
        //    ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
        //    var outerExpression = Expression.Lambda(argQueryable, argQueryable);
        //    string[] props = orderColumn.ToLower().Split('.');
        //    Type type = typeof(T);
        //    ParameterExpression arg = Expression.Parameter(type, "x");
        //    Expression expr = arg;

        //    var hasProp = type.GetProperties().FirstOrDefault(x => props.Contains(x.Name.ToLower()) == true);

        //    if (hasProp == null)
        //    {
        //        //TODO: corrigir exception
        //        //throw new DomainException(orderColumn, "O nome da coluna utilizado para ordenação é inválido");
        //    }

        //    foreach (string prop in props)
        //    {
        //        PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        //        if (pi != null)
        //        {
        //            expr = Expression.Property(expr, pi);
        //            type = pi.PropertyType;
        //        }
        //    }

        //    LambdaExpression lambda = Expression.Lambda(expr, arg);
        //    string methodName = orderType == "asc" || string.IsNullOrWhiteSpace(orderType) ? "OrderBy" : "OrderByDescending";

        //    MethodCallExpression resultExp =
        //        Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), type }, outerExpression.Body, Expression.Quote(lambda));
        //    var finalLambda = Expression.Lambda(resultExp, argQueryable);

        //    return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
        //}
    }
}