using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using System;

namespace MySales.Product.Api.Domain.Dtos.Product
{
    public class ProductQueryDto : IEmpty<ProductQueryDto>
    {
        private static readonly ProductQueryDto _empty = new ProductQueryDto();

        public static readonly ProductQueryDto Empty = _empty;

        /// <summary>
        /// Id.
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Status.
        /// </summary>        
        public bool Status { get; private set; }

        private ProductQueryDto() { }

        static ProductQueryDto()
        {
            _empty ??= new ProductQueryDto();
        }

        public static ProductQueryDto New(Guid id, string name, bool status)
        {
            return new ProductQueryDto
            {
                ProductId = id,
                Name = name,
                Status = status
            };
        }

        public bool IsEmpty => Equals(_empty);
    }
}
