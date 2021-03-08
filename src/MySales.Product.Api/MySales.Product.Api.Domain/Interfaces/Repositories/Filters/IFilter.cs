namespace MySales.Product.Api.Domain.Interfaces.Repositories.Filters
{
    public interface IFilter
    {
        /// <summary>
        /// Current page.
        /// </summary>
        public string OrderBy { get; }

        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; }

        /// <summary>
        /// Quantity of items per page.
        /// </summary>
        public int ItemsPerPage { get; }
    }
}