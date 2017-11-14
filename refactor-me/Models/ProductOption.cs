using System;
using Newtonsoft.Json;

namespace refactor_me.Models
{
    /// <summary>
    ///     This allows the derializing and deserializing of a single product option to json.
    /// </summary>
    public class ProductOption
    {
        /// <summary>
        ///     Creates a new instance of <see cref="ProductOption" />.
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
        ///     The unique identifier of the option.
        /// </summary>
        [JsonProperty]
        public Guid Id { get; }

        /// <summary>
        ///     The unique identifier of the product the option belongs to.
        /// </summary>
        [JsonProperty]
        public Guid ProductId { get; }

        /// <summary>
        ///     The name of the option.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        ///     A description of the option.
        /// </summary>
        [JsonProperty]
        public string Description { get; }

        /// <summary>
        ///     <see cref="object.GetHashCode"/>
        /// </summary>
        public override int GetHashCode()
        {
            return new
            {
                id = Id,
                productId = ProductId,
                name = Name,
                description = Description
            }.GetHashCode();
        }

        /// <summary>
        ///     <see cref="object.Equals(object)"/>
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var rhs = obj as ProductOption;

            if (rhs == null)
                return false;

            return (Id == rhs.Id) &&
                (ProductId == rhs.ProductId) &&
                (Name == rhs.Name) &&
                (Description == rhs.Description);
        }

        public static bool operator ==(ProductOption lhs, ProductOption rhs)
        {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);

            return lhs.Equals(rhs);
        }

        public static bool operator !=(ProductOption lhs, ProductOption rhs)
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