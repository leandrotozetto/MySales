using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using System.Collections.Generic;

namespace MySales.Product.Api.Domain.Core.Entities
{
    public class ListPage<T> : IListPage<T>
    {
        /// <summary>
        /// List of entities.
        /// </summary>
        public IEnumerable<T> Entities { get; private set; }

        /// <summary>
        /// Total quantity of entities.
        /// </summary>
        public int Count { get; private set; }

        public static ListPage<T> Empty { get; } = new ListPage<T>();

        public bool IsEmpty => Equals(Empty);

        public static ListPage<T> New(IEnumerable<T> entities, int count)
        {
            return new ListPage<T>
            {
                Entities = entities,
                Count = count
            };
        }
    }
}
