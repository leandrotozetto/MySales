using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MySales.Product.Api.Domain.Interfaces.Repositories.Query
{
    interface IQueryWhere<T>
    {
        public IQueryInclude<T> Where(IEnumerable<Expression<Func<T, bool>>> filters);
    }
}
