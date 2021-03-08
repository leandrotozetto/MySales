using MySales.Product.Api.Domain.Identifiers;

namespace MySales.Product.Api.Domain.Aggregates.Interfaces
{
    public interface ISize
    {
        string Name { get; }

        SizeId SizeId { get; }
    }
}