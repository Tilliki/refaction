using System;
using refactor_me.Models;
using refactor_me.Repositories;

namespace refactor_me.Services
{
    /// <summary>
    ///     Implementation of <see cref="IProductsService" />.
    /// </summary>
    public class ProductsService : IProductsService
    {
        private readonly IProductOptionsRepository _productOptionsRepository;
        private readonly IProductsRepository _productsRepository;

        /// <summary>
        ///     Creates a new instance of <see cref="ProductsService" />.
        /// </summary>
        /// <param name="productsRepository">The products repository.</param>
        /// <param name="productOptionsRepository">The product options repository.</param>
        public ProductsService(IProductsRepository productsRepository,
            IProductOptionsRepository productOptionsRepository)
        {
            _productsRepository = productsRepository;
            _productOptionsRepository = productOptionsRepository;
        }

        /// <summary>
        ///     <see cref="IProductsService.CreateProduct" />.
        /// </summary>
        public Product CreateProduct(Product toCreate)
        {
            return _productsRepository.CreateProduct(toCreate);
        }

        /// <summary>
        ///     <see cref="IProductsService.DeleteProduct" />
        /// </summary>
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

        /// <summary>
        ///     <see cref="IProductsService.GetAllProducts" />.
        /// </summary>
        public Products GetAllProducts()
        {
            return _productsRepository.GetAllProducts();
        }

        /// <summary>
        ///     <see cref="IProductsService.GetAllProductsWithNameLike" />.
        /// </summary>
        public Products GetAllProductsWithNameLike(string name)
        {
            return _productsRepository.GetAllProductsWithNameLike(name);
        }

        /// <summary>
        ///     <see cref="IProductsService.GetProduct" />.
        /// </summary>
        public Product GetProduct(Guid productId)
        {
            return _productsRepository.GetProduct(productId);
        }

        /// <summary>
        ///     <see cref="IProductsService.UpdateProduct" />.
        /// </summary>
        public Product UpdateProduct(Guid productId, Product update)
        {
            return _productsRepository.UpdateProduct(productId, update);
        }

        /// <summary>
        ///     <see cref="IProductsService.GetAllProductOptions" />.
        /// </summary>
        public ProductOptions GetAllProductOptions(Guid productId)
        {
            return _productOptionsRepository.GetAllProductOptions(productId);
        }

        /// <summary>
        ///     <see cref="IProductsService.GetProductOption" />.
        /// </summary>
        public ProductOption GetProductOption(Guid productId, Guid optionId)
        {
            return _productOptionsRepository.GetProductOption(productId, optionId);
        }

        /// <summary>
        ///     <see cref="IProductsService.CreateProductOption" />.
        /// </summary>
        public ProductOption CreateProductOption(Guid productId, ProductOption option)
        {
            return _productOptionsRepository.CreateProductOption(productId, option);
        }

        /// <summary>
        ///     <see cref="IProductsService.UpdateProductOption" />.
        /// </summary>
        public ProductOption UpdateProductOption(Guid productId, Guid optionId, ProductOption option)
        {
            return _productOptionsRepository.UpdateProductOption(productId, optionId, option);
        }

        /// <summary>
        ///     <see cref="IProductsService.DeleteProductOption" />.
        /// </summary>
        public void DeleteProductOption(Guid productId, Guid optionId)
        {
            _productOptionsRepository.DeleteProductOption(productId, optionId);
        }
    }
}