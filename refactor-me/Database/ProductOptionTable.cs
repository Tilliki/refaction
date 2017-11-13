using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using refactor_me.Models;

namespace refactor_me.Database
{
    /// <summary>
    ///     Represents the ProductOption table in the database.
    /// </summary>
    [Table("ProductOption")]
    public class ProductOptionTable
    {
        /// <summary>
        ///     The unique identifier for the option.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        ///     The foreign key pointing to the unique identifier of the product this option belongs to.
        /// </summary>
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        /// <summary>
        ///     The option name.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        ///     A description of the option.
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        ///     The owning product.
        /// </summary>
        public ProductTable Product { get; set; }

        /// <summary>
        ///     A helper function that converts the data object to a data transfer object.
        /// </summary>
        /// <returns>The data transfer object.</returns>
        public ProductOption ToProductOption()
        {
            return new ProductOption(Id, ProductId, Name, Description);
        }
    }
}