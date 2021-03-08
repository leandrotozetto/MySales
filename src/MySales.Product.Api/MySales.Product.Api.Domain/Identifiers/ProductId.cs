using MySales.Product.Api.Domain.Core.Entities;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using System;

namespace MySales.Product.Api.Domain.Identifiers
{
    public class ProductId : Identifier, IIdentifier
    {
        private static ProductId _empty;

        public static ProductId Empty
        {
            get
            {
                _empty ??= new ProductId(Guid.Empty);

                return _empty;
            }
        }

        private ProductId(Guid value) : base(value) { }

        private ProductId() : base(Guid.NewGuid()) { }

        public static ProductId New()
        {
            return new ProductId(Guid.NewGuid());
        }

        public static ProductId New(Guid value)
        {
            //TODO: resolve this validation.
            // Ensure.That(value, nameof(value)).IdNotEmpty();

            return new ProductId(value);
        }
    }
}