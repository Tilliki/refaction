using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Repositories
{
    /// <summary>
    /// Provides a basic CRUD repository for Product Options.
    /// </summary>
    public interface IProductOptionsRepository
    {
        ProductOptions GetAllProductOptions(Guid productId);
        ProductOption GetProductOption(Guid productId, Guid id);
        ProductOption CreateProductOption(Guid productId, ProductOption option);
        ProductOption UpdateProductOption(Guid productId, Guid id, ProductOption option);
        void DeleteProductOption(Guid productId, Guid id);
        void DeleteAllProductOptionsForProduct(Guid productId);
    }
}
