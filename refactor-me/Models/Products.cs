using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace refactor_me.Models
{
    /// <summary>
    ///     A list of products.
    /// </summary>
    public class Products
    {
        private readonly List<Product> _products;

        /// <summary>
        ///     Creates a new instance of <see cref="Products" />.
        /// </summary>
        /// <param name="products">The list of products to add.</param>
        [JsonConstructor]
        public Products(List<Product> products)
        {
            _products = new List<Product>();
            if (products != null)
                _products.AddRange(products);
        }

        /// <summary>
        ///     The list of products.
        /// </summary>
        [JsonProperty]
        public List<Product> Items => new List<Product>(_products);

        /// <summary>
        ///     <see cref="object.GetHashCode"/>
        /// </summary>
        public override int GetHashCode()
        {
            return new
            {
                items = Items
            }.GetHashCode();
        }

        /// <summary>
        ///     <see cref="object.Equals(object)"/>
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var rhs = obj as Products;

            if (rhs == null)
                return false;

            return (Items.SequenceEqual(rhs.Items));
        }

        public static bool operator ==(Products lhs, Products rhs)
        {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Products lhs, Products rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        ///     <see cref="object.ToString"/>
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}