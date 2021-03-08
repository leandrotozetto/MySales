using System;
using System.Linq.Expressions;

namespace MySales.Product.Api.Domain.Interfaces.Repositories.Query
{
    public interface IQueryInclude<T>: IQueryResult<T>
    {
        public IQueryInclude<T> Include(Expression<Func<T, object>> includes);

        public IQueryResult<T> OrderBy(string orderColumn, string orderType = "asc");
    }
}
