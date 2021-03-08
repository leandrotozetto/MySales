using MySales.Product.Api.Domain.Core.Entities;
using System;

namespace MySales.Product.Api.Domain.Identifiers
{
    public class SizeId : Identifier
    {
        public static SizeId _empty;

        public static SizeId Empty
        {
            get
            {
                _empty ??= new SizeId(Guid.Empty);

                return _empty;
            }
        }

        private SizeId(Guid value) : base(value) { }

        public static SizeId New()
        {
            return new SizeId(Guid.NewGuid());
        }

        public static SizeId New(Guid value)
        {
            //TODO: resolve this validation.
            // Ensure.That(value, nameof(value)).IdNotEmpty();

            return new SizeId(value);
        }
    }
}