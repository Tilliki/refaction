using System;
using refactor_me.Models;

namespace refactor_me.Services
{
    /// <summary>
    ///     Defines the business layer of the application. All business rules are implemented here.
    /// </summary>
    public interface IProductsService
    {
        /// <summary>
        ///     Get a list of all products.
        /// </summary>
        /// <returns>The list of products.</returns>
        Products GetAllProducts();

        /// <summary>
        ///     Get a list of products containing a specific string in their names.
        /// </summary>
        /// <param name="name">The string the names should contain.</param>
        /// <returns>The filtered list of products.</returns>
        Products GetAllProductsWithNameLike(string name);

        /// <summary>
        ///     Get a specific product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <returns>The details of the product.</returns>
        Product GetProduct(Guid productId);

        /// <summary>
        ///     Create a new product.
        /// </summary>
        /// <param name="toCreate">The details of the product to create.</param>
        /// <returns>The created product.</returns>
        Product CreateProduct(Product toCreate);

        /// <summary>
        ///     Update the details of a product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to update.</param>
        /// <param name="update">The details of the update.</param>
        /// <returns>The updated product.</returns>
        Product UpdateProduct(Guid productId, Product update);

        /// <summary>
        ///     Delete a specific product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to delete.</param>
        /// <param name="includeOptions">Should any options associated with the product be deleted as well?</param>
        /// <returns>True if the product was deleted.</returns>
        bool DeleteProduct(Guid productId, bool includeOptions);

        /// <summary>
        ///     Get a list of all the options associated with a product.
        /// </summary>
        /// <param name="productId">The unique identifier whose options are required.</param>
        /// <returns>The list of options.</returns>
        ProductOptions GetAllProductOptions(Guid productId);

        /// <summary>
        ///     Get a specific product option.
        /// </summary>
        /// <param name="productId">The unique identifier of the product the option belongs to.</param>
        /// <param name="optionId">The unique identifier of the option whose details are required.</param>
        /// <returns>The details of the product option.</returns>
        ProductOption GetProductOption(Guid productId, Guid optionId);

        /// <summary>
        ///     Create a product option.
        /// </summary>
        /// <param name="productId">The unique identifier of the product the option should be added to.</param>
        /// <param name="option">The details of the option.</param>
        /// <returns>The created product option.</returns>
        ProductOption CreateProductOption(Guid productId, ProductOption option);

        /// <summary>
        ///     Update the details of a product option.
        /// </summary>
        /// <param name="productId">The unique identifier of the product the option belongs to.</param>
        /// <param name="optionId">The unique identifier of the option that needs to be updated.</param>
        /// <param name="option">The details of the update.</param>
        /// <returns>The updated option.</returns>
        ProductOption UpdateProductOption(Guid productId, Guid optionId, ProductOption option);

        /// <summary>
        ///     Delete a product option.
        /// </summary>
        /// <param name="productId">The unique identifier of the product the option belongs to.</param>
        /// <param name="optionId">The unique identifier of the option to be deleted.</param>
        void DeleteProductOption(Guid productId, Guid optionId);
    }
}