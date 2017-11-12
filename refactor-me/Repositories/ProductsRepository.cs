using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Models;
using refactor_me.Database;

namespace refactor_me.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private ProductDataPool _productDataPool;

        public ProductsRepository()
        {
            _productDataPool = new ProductDataPool(5);
        }

        public Product CreateProduct(Product toCreate)
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.Products.Add(new ProductTable() { Id = Guid.NewGuid(), Name = toCreate.Name, Description = toCreate.Description, Price = toCreate.Price, DeliveryPrice = toCreate.DeliveryPrice });
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
            return result.ToProduct();
        }

        public void DeleteProduct(Guid id)
        {
            var productData = _productDataPool.CheckOut();
            var attached = productData.Products.Where(item => item.Id == id).FirstOrDefault();
            if (attached == null)
            {
                _productDataPool.CheckIn(productData);
                return;
            }
            productData.Products.Remove(attached);
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
        }

        public Products GetAllProducts()
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.Products.ToList();
            _productDataPool.CheckIn(productData);
            return new Products(result.Select(item => item.ToProduct()).ToList());
        }

        public Products GetAllProductsWithNameLike(string name)
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.Products.Where(item => item.Name.Contains(name)).ToList();
            _productDataPool.CheckIn(productData);
            return new Products(result.Select(item => item.ToProduct()).ToList());
        }

        public Product GetProduct(Guid id)
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.Products.Where(item => item.Id == id).FirstOrDefault();
            _productDataPool.CheckIn(productData);
            return result?.ToProduct();
        }

        public Product UpdateProduct(Guid id, Product update)
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.Products.Where(item => item.Id == id).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            result.Name = update.Name;
            result.Description = update.Description;
            result.Price = update.Price;
            result.DeliveryPrice = update.DeliveryPrice;
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
            return result.ToProduct();
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