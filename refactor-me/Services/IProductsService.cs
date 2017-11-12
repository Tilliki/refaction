using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Services
{
    public interface IProductsService
    {
        Products GetAllProducts();
        Products GetAllProductsWithNameLike(string name);
        Product GetProduct(Guid id);
        Product CreateProduct(Product toCreate);
        Product UpdateProduct(Guid id, Product update);
        bool DeleteProduct(Guid id, bool includeOptions);
        ProductOptions GetAllProductOptions(Guid productId);
        ProductOption GetProductOption(Guid productId, Guid id);
        ProductOption CreateProductOption(Guid productId, ProductOption option);
        ProductOption UpdateProductOption(Guid productId, Guid id, ProductOption option);
        void DeleteProductOption(Guid productId, Guid id);
    }
}
