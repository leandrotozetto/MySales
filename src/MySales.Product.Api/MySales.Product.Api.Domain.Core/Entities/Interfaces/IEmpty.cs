namespace MySales.Product.Api.Domain.Core.Entities.Interfaces
{
    public interface IEmpty<T>
    {
        /// <summary>
        /// It checks if this object is empty. 
        /// </summary>
        /// <returns></returns>
        bool IsEmpty { get; }
    }
}
