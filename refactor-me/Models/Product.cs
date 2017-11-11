using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    /// <summary>
    /// This is the model representing the product. It is designed to be easily serialized and deserialized from Json.
    /// </summary>
    public class Product
    {
        [JsonConstructor]
        public Product(Guid id, string name, string description, decimal price, decimal deliveryPrice)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
        }

        [JsonProperty]
        public Guid Id { get; }

        [JsonProperty]
        public string Name { get; }

        [JsonProperty]
        public string Description { get; }

        [JsonProperty]
        public decimal Price { get; }

        [JsonProperty]
        public decimal DeliveryPrice { get; }
    }
}