using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace refactor_me.Database
{
    public partial class ProductDataContext : DbContext
    {
        public ProductDataContext()
            : base("name=ProductDataContext")
        {
        }

        public virtual DbSet<ProductTable> Products { get; set; }
        public virtual DbSet<ProductOptionTable> ProductOptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
