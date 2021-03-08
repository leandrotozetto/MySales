namespace MySales.Product.Api.Domain.Core.Entities.Interfaces
{
    public interface IPaging<T> : IListPage<T>, IEmpty<IPaging<T>>
    {
        /// <summary>
        /// Current page.
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// Total of pages.
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Quantity of items per page.
        /// </summary>
        int ItemsPerPage { get; }
    }
}
