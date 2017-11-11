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
        [JsonConstructor]
        public ProductOption(Guid id, Guid productId, string name, string description)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Description = description;
        }

        [JsonProperty]
        public Guid Id { get; }

        [JsonProperty]
        public Guid ProductId { get; }

        [JsonProperty]
        public string Name { get; }

        [JsonProperty]
        public string Description { get; }
    }
}