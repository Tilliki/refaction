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

        public bool DeleteProduct(Guid productId, bool includeOptions)
        {
            if (_productOptionsRepository.GetAllProductOptions(productId).Items.Count > 0)
            {
                if (!includeOptions)
                    return false;
                _productOptionsRepository.DeleteAllProductOptionsForProduct(productId);
            }
            _productsRepository.DeleteProduct(productId);
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

        public Product GetProduct(Guid productId)
        {
            return _productsRepository.GetProduct(productId);
        }

        public Product UpdateProduct(Guid productId, Product update)
        {
            return _productsRepository.UpdateProduct(productId, update);
        }

        public ProductOptions GetAllProductOptions(Guid productId)
        {
            return _productOptionsRepository.GetAllProductOptions(productId);
        }

        public ProductOption GetProductOption(Guid productId, Guid optionId)
        {
            return _productOptionsRepository.GetProductOption(productId, optionId);
        }

        public ProductOption CreateProductOption(Guid productId, ProductOption option)
        {
            return _productOptionsRepository.CreateProductOption(productId, option);
        }

        public ProductOption UpdateProductOption(Guid productId, Guid optionId, ProductOption option)
        {
            return _productOptionsRepository.UpdateProductOption(productId, optionId, option);
        }

        public void DeleteProductOption(Guid productId, Guid optionId)
        {
            _productOptionsRepository.DeleteProductOption(productId, optionId);
        }
    }
}