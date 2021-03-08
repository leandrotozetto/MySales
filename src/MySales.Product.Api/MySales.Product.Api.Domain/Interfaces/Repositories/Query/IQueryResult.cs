using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MySales.Product.Api.Domain.Interfaces.Repositories.Query
{
    public interface IQueryResult<T>
    {
        Task<T> SingleAsync();

        Task<IEnumerable<T>> ListAsync(int currentPage, int itemsPerPage);

        Task<int> CountAsync(IEnumerable<Expression<Func<T, bool>>> filters);

        TResult FieldAsync<TResult>(Func<T, TResult> column, Expression<Func<T, bool>> filter);
    }
}
