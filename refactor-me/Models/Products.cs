﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    /// <summary>
    /// A list of products.
    /// </summary>
    public class Products
    {
        private List<Product> _products;

        /// <summary>
        /// Creates a new instance of <see cref="Products"/>.
        /// </summary>
        /// <param name="products">The list of products to add.</param>
        [JsonConstructor]
        public Products(List<Product> products)
        {
            _products = new List<Product>();
            if (products != null)
            {
                _products.AddRange(products);
            }
        }

        /// <summary>
        /// The list of products.
        /// </summary>
        [JsonProperty]
        public List<Product> Items => new List<Product>(_products);
    }
}