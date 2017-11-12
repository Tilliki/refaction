using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Models;
using refactor_me.Repositories;

namespace refactor_me.Services
{
    public class ProductsService : IProductsService
    {
        private IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Product CreateProduct(Product toCreate)
        {
            return _productsRepository.CreateProduct(toCreate);
        }

        public void DeleteProduct(Guid id)
        {
            _productsRepository.DeleteProduct(id);
        }

        public Products GetAllProducts()
        {
            return _productsRepository.GetAllProducts();
        }

        public Products GetAllProductsWithNameLike(string name)
        {
            return _productsRepository.GetAllProductsWithNameLike(name);
        }

        public Product GetProduct(Guid id)
        {
            return _productsRepository.GetProduct(id);
        }

        public Product UpdateProduct(Guid id, Product update)
        {
            return _productsRepository.UpdateProduct(id, update);
        }

        public List<ProductOption> GetAllProductOptions(Guid productId)
        {
            return _productsRepository.GetAllProductOptions(productId);
        }

        public ProductOption GetProductOption(Guid productId, Guid id)
        {
            return _productsRepository.GetProductOption(productId, id);
        }

        public ProductOption CreateProductOption(Guid productId, ProductOption option)
        {
            return _productsRepository.CreateProductOption(productId, option);
        }

        public ProductOption UpdateProductOption(Guid productId, Guid id, ProductOption option)
        {
            return _productsRepository.UpdateProductOption(productId, id, option);
        }

        public void DeleteProductOption(Guid productId, Guid id)
        {
            _productsRepository.DeleteProductOption(productId, id);
        }
    }
}