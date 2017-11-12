using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace refactor_me.Database
{
    /// <summary>
    /// The database context for the product data.
    /// </summary>
    public partial class ProductDataContext : DbContext
    {
        /// <summary>
        /// Creates a new instance of <see cref="ProductDataContext"/>.
        /// </summary>
        public ProductDataContext()
            : base("name=ProductDataContext")
        {
        }

        /// <summary>
        /// The set of products in the database.
        /// </summary>
        public virtual DbSet<ProductTable> Products { get; set; }
        /// <summary>
        /// The set of product options in the database.
        /// </summary>
        public virtual DbSet<ProductOptionTable> ProductOptions { get; set; }

        /// <summary>
        /// <see cref="DbContext.OnModelCreating(DbModelBuilder)"/> 
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
