using System;
using System.Collections.Generic;
using System.Linq;
using refactor_me.Database;
using refactor_me.Models;

namespace refactor_me.Repositories
{
    /// <summary>
    ///     Implementation of <see cref="IProductsRepository" />.
    /// </summary>
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductDataPool _productDataPool;

        /// <summary>
        ///     Creates a new instance of <see cref="ProductsRepository" />.
        /// </summary>
        /// <param name="dataPoolSize">The size of the data pool available to the repository.</param>
        public ProductsRepository(int dataPoolSize)
        {
            _productDataPool = new ProductDataPool(dataPoolSize);
        }

        /// <summary>
        ///     <see cref="IProductsRepository.CreateProduct" />.
        /// </summary>
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

        /// <summary>
        ///     <see cref="IProductsRepository.DeleteProduct" />.
        /// </summary>
        public void DeleteProduct(Guid productId)
        {
            var productData = _productDataPool.CheckOut();
            var attached = productData.Products.FirstOrDefault(item => item.Id == productId);
            if (attached == null)
            {
                _productDataPool.CheckIn(productData);
                return;
            }
            productData.Products.Remove(attached);
            productData.SaveChanges();
            _productDataPool.CheckIn(productData);
        }

        /// <summary>
        ///     <see cref="IProductsRepository.GetAllProducts" />.
        /// </summary>
        public Products GetAllProducts()
        {
            var productData = _productDataPool.CheckOut();
            List<ProductTable> result = productData.Products.ToList();
            _productDataPool.CheckIn(productData);
            return new Products(result.Select(item => item.ToProduct()).ToList());
        }

        /// <summary>
        ///     <see cref="IProductsRepository.GetAllProductsWithNameLike" />.
        /// </summary>
        public Products GetAllProductsWithNameLike(string name)
        {
            var productData = _productDataPool.CheckOut();
            List<ProductTable> result = productData.Products.Where(item => item.Name.Contains(name)).ToList();
            _productDataPool.CheckIn(productData);
            return new Products(result.Select(item => item.ToProduct()).ToList());
        }

        /// <summary>
        ///     <see cref="IProductsRepository.GetProduct" />.
        /// </summary>
        public Product GetProduct(Guid productId)
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.Products.FirstOrDefault(item => item.Id == productId);
            _productDataPool.CheckIn(productData);
            return result?.ToProduct();
        }

        /// <summary>
        ///     <see cref="IProductsRepository.UpdateProduct" />.
        /// </summary>
        public Product UpdateProduct(Guid productId, Product update)
        {
            var productData = _productDataPool.CheckOut();
            var result = productData.Products.FirstOrDefault(item => item.Id == productId);
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