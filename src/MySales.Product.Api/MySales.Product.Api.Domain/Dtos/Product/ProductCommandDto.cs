using System;

namespace MySales.Product.Api.Domain.Dtos.Product
{
    public class ProductCommandDto
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Status.
        /// </summary>        
        public bool Status { get; private set; }

        public static ProductCommandDto New(string name, bool status)
        {
            return new ProductCommandDto
            {
                Name = name,
                Status = status
            };
        }

        private ProductCommandDto() { }
    }
}
