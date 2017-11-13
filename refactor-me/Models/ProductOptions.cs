using System.Collections.Generic;
using Newtonsoft.Json;

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
    }
}