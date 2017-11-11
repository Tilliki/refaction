namespace refactor_me.Database
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class ProductTable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public IEnumerable<ProductOptionTable> ProductOptions { get; set; }

        public Product ToProduct()
        {
            return new Product(Id, Name, Description, Price, DeliveryPrice);
        }
    }
}
