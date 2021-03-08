using System.Collections.Generic;

namespace MySales.Product.Api.Domain.Core.Entities.Interfaces
{
    public interface IListPage<T> : IEmpty<T>
    {
        /// <summary>
        /// List of entities.
        /// </summary>
        IEnumerable<T> Entities { get; }

        /// <summary>
        /// Total quantity of entities.
        /// </summary>
        int Count { get; }
    }
}
