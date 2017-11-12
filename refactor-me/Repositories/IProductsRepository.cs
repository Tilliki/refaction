using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Repositories
{
    /// <summary>
    /// Provides a basic CRUD repository for Products.
    /// </summary>
    public interface IProductsRepository
    {
        Products GetAllProducts();
        Products GetAllProductsWithNameLike(string name);
        Product GetProduct(Guid id);
        Product CreateProduct(Product toCreate);
        Product UpdateProduct(Guid id, Product update);
        void DeleteProduct(Guid id);
    }
}
