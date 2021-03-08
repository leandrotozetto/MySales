namespace MySales.Product.Api.Domain.Interfaces.Repositories.Filters
{
    public interface IProductFilter : IFilter
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Status.
        /// </summary>        
        public bool? Status { get; }
    }
}
