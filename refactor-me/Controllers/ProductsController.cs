using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using System.Collections.Generic;
using refactor_me.Services;
using System.Net.Http;
using System.Web.Http.Description;

namespace refactor_me.Controllers
{
    /// <summary>
    /// Controls the various URI's of the web service. Handles all of HTTP specific functionality.
    /// </summary>
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private IProductsService _productsService;

        /// <summary>
        /// Creates a new instance of <see cref="ProductsController"/>.
        /// </summary>
        /// <param name="productsService">The business layer controlling how products should be handled.</param>
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Get a list of all of the available products.
        /// </summary>
        /// <returns></returns>
        [Route]
        [HttpGet]
        [ResponseType(typeof(List<Product>))]
        public IHttpActionResult GetAll()
        {
            var products = _productsService.GetAllProducts();
            return Ok(new { Items = products });
        }

        [Route]
        [HttpGet]
        [ResponseType(typeof(List<Product>))]
        public IHttpActionResult SearchByName(string name)
        {
            var products = _productsService.GetAllProductsWithNameLike(name);
            return Ok(new { Items = products } );
        }

        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(Guid id)
        {
            var product = _productsService.GetProduct(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [Route]
        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
            var created = _productsService.CreateProduct(product);
            return Created($"/products/{created.Id}", created);
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(Guid id, Product product)
        {
            var result = _productsService.UpdateProduct(id, product);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            _productsService.DeleteProduct(id);
            return Ok();
        }

        [Route("{productId}/options")]
        [HttpGet]
        public IHttpActionResult GetOptions(Guid productId)
        {
            var productOptions = _productsService.GetAllProductOptions(productId);
            return Ok(new { Items = productOptions });
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public IHttpActionResult GetOption(Guid productId, Guid id)
        {
            var productOption = _productsService.GetProductOption(productId, id);
            if (productOption != null)
            {
                return Ok(productOption);
            }
            return NotFound();
        }

        [Route("{productId}/options")]
        [HttpPost]
        public IHttpActionResult CreateOption(Guid productId, ProductOption option)
        {
            var created = _productsService.CreateProductOption(productId, option);
            return Created($"/products/{productId}/options/{created.Id}", created);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateOption(Guid productId, Guid id, ProductOption option)
        {
            var result = _productsService.UpdateProductOption(productId, id, option);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteOption(Guid productId, Guid id)
        {
            _productsService.DeleteProductOption(productId, id);
            return Ok();
        }
    }
}
