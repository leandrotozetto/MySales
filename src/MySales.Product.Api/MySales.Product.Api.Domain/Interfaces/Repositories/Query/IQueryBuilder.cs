using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MySales.Product.Api.Domain.Interfaces.Repositories.Query
{
    public interface IQueryBuilder<T> : IQueryInclude<T>, IQueryResult<T>
    {
        IQueryInclude<T> Where(IEnumerable<Expression<Func<T, bool>>> filters);
    }
}
