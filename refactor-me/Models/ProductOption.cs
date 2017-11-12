using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    /// <summary>
    /// This allows the derializing and deserializing of a single product option to json.
    /// </summary>
    public class ProductOption
    {
        /// <summary>
        /// Creates a new instance of <see cref="ProductOption"/>.
        /// </summary>
        /// <param name="id">The unique identifier of the option.</param>
        /// <param name="productId">The unique identifier of the product the option belongs to.</param>
        /// <param name="name">The name of the option.</param>
        /// <param name="description">A description of the option.</param>
        [JsonConstructor]
        public ProductOption(Guid id, Guid productId, string name, string description)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// The unique identifier of the option.
        /// </summary>
        [JsonProperty]
        public Guid Id { get; }

        /// <summary>
        /// The unique identifier of the product the option belongs to.
        /// </summary>
        [JsonProperty]
        public Guid ProductId { get; }

        /// <summary>
        /// The name of the option.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        /// A description of the option.
        /// </summary>
        [JsonProperty]
        public string Description { get; }
    }
}