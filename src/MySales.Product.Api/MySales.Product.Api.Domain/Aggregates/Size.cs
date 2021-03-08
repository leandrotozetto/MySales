using MySales.Product.Api.Domain.Aggregates.Interfaces;
using MySales.Product.Api.Domain.Identifiers;

namespace MySales.Product.Api.Domain.Aggregates
{
    public class Size : ISize
    {
        public SizeId SizeId { get; private set; }

        public string Name { get; private set; }
    }
}
