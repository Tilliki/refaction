namespace refactor_me.Database
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductOption")]
    public partial class ProductOptionTable
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public ProductTable Product { get; set; }

        public ProductOption ToProductOption()
        {
            return new ProductOption(Id, ProductId, Name, Description);
        }
    }
}
