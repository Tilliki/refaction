using System;
using refactor_me.Models;
using refactor_me.Repositories;

namespace refactor_me.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductOptionsRepository _productOptionsRepository;
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository,
            IProductOptionsRepository productOptionsRepository)
        {
            _productsRepository = productsRepository;
            _productOptionsRepository = productOptionsRepository;
        }

        public Product CreateProduct(Product toCreate)
        {
            return _productsRepository.CreateProduct(toCreate);
        }

        public bool DeleteProduct(Guid id, bool includeOptions)
        {
            if (_productOptionsRepository.GetAllProductOptions(id).Items.Count > 0)
            {
                if (!includeOptions)
                    return false;
                _productOptionsRepository.DeleteAllProductOptionsForProduct(id);
            }
            _productsRepository.DeleteProduct(id);
            return true;
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

        public ProductOptions GetAllProductOptions(Guid productId)
        {
            return _productOptionsRepository.GetAllProductOptions(productId);
        }

        public ProductOption GetProductOption(Guid productId, Guid id)
        {
            return _productOptionsRepository.GetProductOption(productId, id);
        }

        public ProductOption CreateProductOption(Guid productId, ProductOption option)
        {
            return _productOptionsRepository.CreateProductOption(productId, option);
        }

        public ProductOption UpdateProductOption(Guid productId, Guid id, ProductOption option)
        {
            return _productOptionsRepository.UpdateProductOption(productId, id, option);
        }

        public void DeleteProductOption(Guid productId, Guid id)
        {
            _productOptionsRepository.DeleteProductOption(productId, id);
        }
    }
}