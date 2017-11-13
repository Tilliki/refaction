using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using refactor_me.Models;

namespace refactor_me.Database
{
    /// <summary>
    ///     Represents the Product table in the database.
    /// </summary>
    [Table("Product")]
    public class ProductTable
    {
        /// <summary>
        ///     The unique identifier of the product.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        ///     The product name.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        ///     A description of the product.
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        ///     The product's price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     The price for delivering the product.
        /// </summary>
        public decimal DeliveryPrice { get; set; }

        /// <summary>
        ///     A list of options that belong to the product.
        /// </summary>
        public IEnumerable<ProductOptionTable> ProductOptions { get; set; }

        /// <summary>
        ///     A helper function that converts the data object to a data transfer object.
        /// </summary>
        /// <returns>The data transfer object.</returns>
        public Product ToProduct()
        {
            return new Product(Id, Name, Description, Price, DeliveryPrice);
        }
    }
}