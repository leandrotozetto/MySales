using System;

namespace MySales.Product.Api.Domain.Requests.Commands.Product
{
    public class ProductCommandRequest : IProductCommandRequest
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Status.
        /// </summary>        
        public bool Status { get; protected set; }

        /// <summary>
        /// TenantId.
        /// </summary>        
        public Guid TenantId { get; protected set; }
    }

    public interface IProductCommandRequest
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Status.
        /// </summary>        
        public bool Status { get; }

        /// <summary>
        /// TenantId.
        /// </summary>        
        public Guid TenantId { get; }
    }
}
