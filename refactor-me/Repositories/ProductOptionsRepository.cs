using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Models;
using refactor_me.Database;

namespace refactor_me.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IProductOptionsRepository"/>.
    /// </summary>
    public class ProductOptionsRepository : IProductOptionsRepository
    {
        private ProductDataPool _productDataPool;

        public ProductOptionsRepository()
        {
            _productDataPool = new ProductDataPool(5);
        }

        public ProductOptions GetAllProductOptions(Guid productId)
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.ProductOptions.Where(item => item.ProductId == productId).ToList();
            _productDataPool.CheckIn(productData);
            return new ProductOptions(result.Select(item => item.ToProductOption()).ToList());
        }

        public ProductOption GetProductOption(Guid productId, Guid id)
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.ProductOptions.Where(item => item.ProductId == productId && item.Id == id).FirstOrDefault();
            _productDataPool.CheckIn(productData);
            return result?.ToProductOption();
        }

        public ProductOption CreateProductOption(Guid productId, ProductOption option)
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.ProductOptions.Add(new ProductOptionTable() { Id = Guid.NewGuid(), ProductId = productId, Name = option.Name, Description = option.Description });
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
            return result.ToProductOption();
        }

        public ProductOption UpdateProductOption(Guid productId, Guid id, ProductOption option)
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.ProductOptions.Where(item => item.Id == id && item.ProductId == productId).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            result.Name = option.Name;
            result.Description = option.Description;
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
            return result.ToProductOption();
        }

        public void DeleteProductOption(Guid productId, Guid id)
        {
            var productData = _productDataPool.CheckOut();
            var toDelete = productData.ProductOptions.Where(item => item.Id == id && item.ProductId == productId).FirstOrDefault();
            if (toDelete == null)
            {
                _productDataPool.CheckIn(productData);
                return;
            }
            productData.ProductOptions.Remove(toDelete);
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
        }

        public void DeleteAllProductOptionsForProduct(Guid productId)
        {
            var productData = _productDataPool.CheckOut();
            var toDelete = productData.ProductOptions.Where(item => item.ProductId == productId).ToList();
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