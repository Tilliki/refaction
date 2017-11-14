using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace refactor_me.Models
{
    /// <summary>
    ///     A list of products options.
    /// </summary>
    public class ProductOptions
    {
        private readonly List<ProductOption> _productOptions;

        /// <summary>
        ///     Creates a new instance of <see cref="ProductOptions" />.
        /// </summary>
        /// <param name="productOptions">The list of product options to add.</param>
        [JsonConstructor]
        public ProductOptions(List<ProductOption> productOptions)
        {
            _productOptions = new List<ProductOption>();
            if (productOptions != null)
                _productOptions.AddRange(productOptions);
        }

        /// <summary>
        ///     The list of product options.
        /// </summary>
        [JsonProperty]
        public List<ProductOption> Items => new List<ProductOption>(_productOptions);

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

            var rhs = obj as ProductOptions;

            if (rhs == null)
                return false;

            return (Items.SequenceEqual(rhs.Items));
        }

        public static bool operator ==(ProductOptions lhs, ProductOptions rhs)
        {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);

            return lhs.Equals(rhs);
        }

        public static bool operator !=(ProductOptions lhs, ProductOptions rhs)
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