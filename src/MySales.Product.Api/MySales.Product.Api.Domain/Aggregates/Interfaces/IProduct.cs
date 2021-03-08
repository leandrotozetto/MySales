using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Core.ValuesObjects;
using MySales.Product.Api.Domain.Identifiers;
using System.Collections.ObjectModel;

namespace MySales.Product.Api.Domain.Aggregates.Interfaces
{
    public interface IProduct : IEntity, IEmpty<IProduct>
    {
        string Name { get; }

        ProductId ProductId { get; }

        ReadOnlyCollection<Sku> Skus { get; }

        IProduct ChangeName(string name);

        IProduct ChangeStatus(bool status);

        IProduct AddSku(string partNumber, string description, int stock, SizeId sizeId, Status status);

        IProduct ChangeSku(SkuId skuId, string partNumber, string description, int stock, SizeId sizeId, Status status);
    }
}