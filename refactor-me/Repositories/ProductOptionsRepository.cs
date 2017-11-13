using System;
using System.Collections.Generic;
using System.Linq;
using refactor_me.Database;
using refactor_me.Models;

namespace refactor_me.Repositories
{
    /// <summary>
    ///     Implementation of <see cref="IProductOptionsRepository" />.
    /// </summary>
    public class ProductOptionsRepository : IProductOptionsRepository
    {
        private readonly ProductDataPool _productDataPool;

        /// <summary>
        ///     Creates a new instance of <see cref="ProductOptionsRepository" />.
        /// </summary>
        /// <param name="dataPoolSize">The size of the data pool available to the repository.</param>
        public ProductOptionsRepository(int dataPoolSize)
        {
            _productDataPool = new ProductDataPool(dataPoolSize);
        }

        /// <summary>
        ///     <see cref="IProductOptionsRepository.GetAllProductOptions" />.
        /// </summary>
        public ProductOptions GetAllProductOptions(Guid productId)
        {
            var productData = _productDataPool.CheckOut();
            List<ProductOptionTable> result =
                productData.ProductOptions.Where(item => item.ProductId == productId).ToList();
            _productDataPool.CheckIn(productData);
            return new ProductOptions(result.Select(item => item.ToProductOption()).ToList());
        }

        /// <summary>
        ///     <see cref="IProductOptionsRepository.GetProductOption" />.
        /// </summary>
        public ProductOption GetProductOption(Guid productId, Guid optionId)
        {
            var productData = _productDataPool.CheckOut();
            var result =
                productData.ProductOptions
                    .FirstOrDefault(item => (item.ProductId == productId) && (item.Id == optionId));
            _productDataPool.CheckIn(productData);
            return result?.ToProductOption();
        }

        /// <summary>
        ///     <see cref="IProductOptionsRepository.CreateProductOption" />.
        /// </summary>
        public ProductOption CreateProductOption(Guid productId, ProductOption option)
        {
            var productData = _productDataPool.CheckOut();
            var result =
                productData.ProductOptions.Add(new ProductOptionTable
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Name = option.Name,
                    Description = option.Description
                });
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
            return result.ToProductOption();
        }

        /// <summary>
        ///     <see cref="IProductOptionsRepository.UpdateProductOption" />.
        /// </summary>
        public ProductOption UpdateProductOption(Guid productId, Guid optionId, ProductOption option)
        {
            var productData = _productDataPool.CheckOut();
            var result =
                productData.ProductOptions
                    .FirstOrDefault(item => (item.Id == optionId) && (item.ProductId == productId));
            if (result == null)
                return null;
            result.Name = option.Name;
            result.Description = option.Description;
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
            return result.ToProductOption();
        }

        /// <summary>
        ///     <see cref="IProductOptionsRepository.DeleteProductOption" />.
        /// </summary>
        public void DeleteProductOption(Guid productId, Guid optionId)
        {
            var productData = _productDataPool.CheckOut();
            var toDelete =
                productData.ProductOptions
                    .FirstOrDefault(item => (item.Id == optionId) && (item.ProductId == productId));
            if (toDelete == null)
            {
                _productDataPool.CheckIn(productData);
                return;
            }
            productData.ProductOptions.Remove(toDelete);
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
        }

        /// <summary>
        ///     <see cref="IProductOptionsRepository.DeleteAllProductOptionsForProduct" />.
        /// </summary>
        public void DeleteAllProductOptionsForProduct(Guid productId)
        {
            var productData = _productDataPool.CheckOut();
            List<ProductOptionTable> toDelete =
                productData.ProductOptions.Where(item => item.ProductId == productId).ToList();
            if (toDelete.Count == 0)
            {
                _productDataPool.CheckIn(productData);
                return;
            }
            productData.ProductOptions.RemoveRange(toDelete);
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
        }
    }
}