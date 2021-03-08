using MySales.Product.Api.Domain.Core.Entities;
using System;

namespace MySales.Product.Api.Domain.Identifiers
{
    public class SkuId : Identifier
    {
        public static SkuId _empty;

        public static SkuId Empty
        {
            get
            {
                _empty ??= new SkuId(Guid.Empty);

                return _empty;
            }
        }

        private SkuId(Guid value) : base(value) { }

        public static SkuId New()
        {
            return new SkuId(Guid.NewGuid());
        }

        public static SkuId New(Guid value)
        {
            //TODO: resolve this validation.
            // Ensure.That(value, nameof(value)).IdNotEmpty();

            return new SkuId(value);
        }
    }
}