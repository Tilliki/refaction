using System;
using System.Collections.Generic;
using System.Linq;
using refactor_me.Database;
using refactor_me.Models;

namespace refactor_me.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductDataPool _productDataPool;

        public ProductsRepository()
        {
            _productDataPool = new ProductDataPool(5);
        }

        public Product CreateProduct(Product toCreate)
        {
            var productData = _productDataPool.CheckOut();
            var result =
                productData.Products.Add(new ProductTable
                {
                    Id = Guid.NewGuid(),
                    Name = toCreate.Name,
                    Description = toCreate.Description,
                    Price = toCreate.Price,
                    DeliveryPrice = toCreate.DeliveryPrice
                });
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
            List<ProductTable> result = productData.Products.ToList();
            _productDataPool.CheckIn(productData);
            return new Products(result.Select(item => item.ToProduct()).ToList());
        }

        public Products GetAllProductsWithNameLike(string name)
        {
            var productData = _productDataPool.CheckOut();
            List<ProductTable> result = productData.Products.Where(item => item.Name.Contains(name)).ToList();
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
                return null;
            result.Name = update.Name;
            result.Description = update.Description;
            result.Price = update.Price;
            result.DeliveryPrice = update.DeliveryPrice;
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
            return result.ToProduct();
        }
    }
}