using MySales.Product.Api.Domain.Aggregates.Interfaces;
using MySales.Product.Api.Domain.Core.Entities.Identifiers;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Core.Enum;
using MySales.Product.Api.Domain.Core.ValuesObjects;
using MySales.Product.Api.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MySales.Product.Api.Domain.Aggregates
{
    public class Product : IProduct, IEmpty<Product>
    {
        public ProductId ProductId { get; private set; }

        public string Name { get; private set; }

        public ReadOnlyCollection<Sku> Skus => _skus.AsReadOnly();

        public Status Status { get; private set; }

        public DateTime CreationDate { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public TenantId TenantId { get; private set; }

        private readonly List<Sku> _skus;

        private static readonly IProduct _empty;

        public bool IsEmpty => Equals(_empty);

        public IProduct Empty => _empty;

        private Product()
        {
            _skus = new List<Sku>();
        }

        static Product()
        {
            _empty ??= new Product();
        }

        public static IProduct NewEmpty()
        {
            return _empty;
        }

        public static IProduct New(string name, Status status, TenantId tenantId)
        {
            return new Product()
            {
                ProductId = ProductId.New(),
                Name = name,
                CreationDate = DateTime.Now,
                Status = status,
                TenantId = tenantId
            };
        }

        public IProduct AddSku(string partNumber, string description, int stock, SizeId sizeId, Status status)
        {
            var sku = Sku.New(partNumber, description, stock, sizeId, status, TenantId);

            _skus.Add(sku as Sku);

            return this;
        }

        public IProduct ChangeSku(SkuId skuId, string partNumber, string description, int stock, SizeId sizeId, Status status)
        {
            if (!skuId.Equals(SkuId.Empty))
            {
                var currentSku = Skus.FirstOrDefault(x => x.SkuId.Equals(skuId));
                var sku = Sku.New(skuId, partNumber, description, stock, sizeId, status, TenantId, currentSku.CreationDate);

                if (currentSku != null)
                {
                    _skus.Remove(currentSku);
                }

                _skus.Add(sku as Sku);
            }

            return this;
        }

        public IProduct ChangeName(string name)
        {
            if (!Name.Equals(name))
            {
                Name = name;
                ChangeUpdateDate();
            }

            return this;
        }

        public IProduct ChangeStatus(bool status)
        {
            if (Status.Value != status)
            {
                Status = StatusEnum.New(status);
                ChangeUpdateDate();
            }

            return this;
        }

        private void ChangeUpdateDate()
        {
            UpdateDate = DateTime.Now;
        }
    }
}
