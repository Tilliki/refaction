using System;
using refactor_me.Models;

namespace refactor_me.Repositories
{
    /// <summary>
    ///     Provides a basic CRUD repository for Products.
    /// </summary>
    public interface IProductsRepository
    {
        /// <summary>
        ///     Get a list of all possible products.
        /// </summary>
        /// <returns>The list of products.</returns>
        Products GetAllProducts();

        /// <summary>
        ///     Get a list of all products whose names contain a particular string.
        /// </summary>
        /// <param name="name">The string the name should contain.</param>
        /// <returns>The list of eligible products.</returns>
        Products GetAllProductsWithNameLike(string name);

        /// <summary>
        ///     Get a specific product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product</param>
        /// <returns>The product details.</returns>
        Product GetProduct(Guid productId);

        /// <summary>
        ///     Create a product.
        /// </summary>
        /// <param name="toCreate">The details of the product to create.</param>
        /// <returns>The created product.</returns>
        Product CreateProduct(Product toCreate);

        /// <summary>
        ///     Update the details of a product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to update.</param>
        /// <param name="update">The details to be updated.</param>
        /// <returns>The updated product.</returns>
        Product UpdateProduct(Guid productId, Product update);

        /// <summary>
        ///     Delete a product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to delete.</param>
        void DeleteProduct(Guid productId);
    }
}