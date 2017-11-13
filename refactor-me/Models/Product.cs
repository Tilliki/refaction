using System;
using Newtonsoft.Json;

namespace refactor_me.Models
{
    /// <summary>
    ///     This is the model representing the product. It is designed to be easily serialized and deserialized from Json.
    /// </summary>
    public class Product
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Product" />.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <param name="name">The product name.</param>
        /// <param name="description">A description of the product.</param>
        /// <param name="price">The product's price.</param>
        /// <param name="deliveryPrice">The price for delivering the product.</param>
        [JsonConstructor]
        public Product(Guid id, string name, string description, decimal price, decimal deliveryPrice)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
        }

        /// <summary>
        ///     The unique identifier of the product.
        /// </summary>
        [JsonProperty]
        public Guid Id { get; }

        /// <summary>
        ///     The product name.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        ///     A description of the product.
        /// </summary>
        [JsonProperty]
        public string Description { get; }

        /// <summary>
        ///     The product's price.
        /// </summary>
        [JsonProperty]
        public decimal Price { get; }

        /// <summary>
        ///     The price for delivering the product.
        /// </summary>
        [JsonProperty]
        public decimal DeliveryPrice { get; }
    }
}