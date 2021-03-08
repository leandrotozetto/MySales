using MySales.Product.Api.Domain.Core.Entities;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Core.Enum;
using MySales.Product.Api.Domain.Core.Notifications.Interfaces;
using MySales.Product.Api.Domain.Core.Validations.Argument;
using MySales.Product.Api.Domain.Dtos.Product;
using MySales.Product.Api.Domain.Identifiers;
using MySales.Product.Api.Domain.Interfaces.Applications;
using MySales.Product.Api.Domain.Interfaces.Repositories;
using MySales.Product.Api.Domain.Interfaces.Repositories.Filters;
using MySales.Product.Api.Domain.Mappers;
using MySales.Product.Api.Domain.Requests.Commands.Product;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MySales.Product.Api.Application
{
    /// <summary>
    /// Contains related actions with products in application layer.
    /// </summary>
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        private readonly IEnsure _ensure;

        private readonly INotification _notification;

        /// <summary>
        /// Creates a new instance of ProductApplication <see cref="ProductApplication"/>.
        /// </summary>
        /// <param name="productRepositorio"></param>
        public ProductApplication(IProductRepository productRepositorio, IEnsure ensure, INotification notification)
        {
            _productRepository = productRepositorio;
            _ensure = ensure;
            _notification = notification;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">Product <see cref="ProductDto"/> to be created.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task<bool> InsertAsync(IProductCommandRequest productCommandRequest)
        {
            _ensure.That(productCommandRequest, nameof(productCommandRequest)).EntityIsNotNull();

            var product = ProductMapper.Map(productCommandRequest);

            return await _productRepository.InsertAsync(product);
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="id">Product's id will be update.</param>
        /// <param name="productDto">Product <see cref="ProductDto"/> to be updated.</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task<bool> UpdateAsync(IProductCommandRequest productCommandRequest, ProductId productId)
        {
            _ensure.That(productId as IIdentifier, nameof(productId)).IdIsNotEmpty();

            var entity = await _productRepository.GetAsync(productId);

            _ensure.That(entity, nameof(productId)).EntityExists();

            entity.ChangeName(productCommandRequest.Name)
                .ChangeStatus(productCommandRequest.Status);

            return _productRepository.Update(entity);
        }

        /// <summary>
        /// Get all product.
        /// </summary>
        /// <param name="name">name.</param>
        /// <param name="status">status.</param>
        /// <param name="orderBy">Columns name to order products.</param>
        /// <param name="currentPage">Current page.</param>
        /// <param name="itemsPerPage">Quantity of registers per page.</param>
        /// <returns>Returns a pagination list of products <see cref="IPaging{ProductQueryDto}"/>.</returns>
        public async Task<IPaging<ProductQueryDto>> ListAsync(IProductFilter productFilter)
        {
            var listPage = await _productRepository.ListAsync(productFilter);

            if (listPage.IsEmpty)
            {
                return Paging<ProductQueryDto>.Empty;
            }

            var productDtos = ProductMapper.Map(listPage.Entities);
            var paginationDto = Paging<ProductQueryDto>.New(productDtos, listPage.Count, productFilter.ItemsPerPage);

            return paginationDto;
        }

        /// <summary>
        /// Get product by id.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <returns>Returns product found.</returns>
        public async Task<ProductQueryDto> GetAsync(ProductId productId)
        {
            if (_ensure.That(productId as IIdentifier, nameof(productId)).IdIsNotEmpty().IsInvalid)
            {
                EntityNotFound();
            }

            var product = await _productRepository.GetAsync(productId);

            if (product.IsEmpty)
            {
                EntityNotFound();
            }

            var productDto = ProductMapper.Map(product);

            void EntityNotFound()
            {
                _notification.AddNotification("Product", "Produto não encontrado", NotificationTypeEnum.Information);
            }

            return productDto;
        }

        /// <summary>
        /// Removes a product.
        /// </summary>
        /// <param name="productId">ProductId that will be updated..</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task<bool> DeleteAsync(ProductId productId)
        {
            try
            {
                if (_ensure.That(productId as IIdentifier, nameof(productId)).IdIsNotEmpty().IsInvalid)
                {
                    return false;
                }

                return await _productRepository.DeleteAsync(productId);
            }
            catch
            {
                return false;
            }
        }

        private bool disposedValue = false;

        /// <summary>
        /// Dispose managed resource.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _productRepository.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose managed resource.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
