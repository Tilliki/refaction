using System;
using refactor_me.Models;

namespace refactor_me.Repositories
{
    /// <summary>
    ///     Provides a basic CRUD repository for Product Options.
    /// </summary>
    public interface IProductOptionsRepository
    {
        /// <summary>
        ///     Get all options belonging to a specific product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product whose options are required.</param>
        /// <returns>The product options.</returns>
        ProductOptions GetAllProductOptions(Guid productId);

        /// <summary>
        ///     Get a specific product option belonging to a product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product the option belongs to.</param>
        /// <param name="optionId">The unique identifier of the option whose details are required.</param>
        /// <returns>The details of the product option.</returns>
        ProductOption GetProductOption(Guid productId, Guid optionId);

        /// <summary>
        ///     Create a new option for a specific product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product you want to add the option to.</param>
        /// <param name="option">The details of the option to add.</param>
        /// <returns>The created option.</returns>
        ProductOption CreateProductOption(Guid productId, ProductOption option);

        /// <summary>
        ///     Update the details of an existing product option.
        /// </summary>
        /// <param name="productId">The unique identifier of the product the option belongs to.</param>
        /// <param name="optionId">The unique identifier of the option that should be updated.</param>
        /// <param name="option">The details that need to be updated.</param>
        /// <returns>The updated option.</returns>
        ProductOption UpdateProductOption(Guid productId, Guid optionId, ProductOption option);

        /// <summary>
        ///     Remove a single option belonging to a product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product the option belongs to.</param>
        /// <param name="optionId">The unique identifier of the option that should be deleted.</param>
        void DeleteProductOption(Guid productId, Guid optionId);

        /// <summary>
        ///     Remove all options belonging to a product.
        /// </summary>
        /// <param name="productId">The unique identifier of the product that should be stripped of options.</param>
        void DeleteAllProductOptionsForProduct(Guid productId);
    }
}