using System;
using System.Web.Http;
using System.Web.Http.Description;
using refactor_me.Models;
using refactor_me.Services;

namespace refactor_me.Controllers
{
    /// <summary>
    ///     Controls the various URI's of the web service. Handles all of HTTP specific functionality for products.
    /// </summary>
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IProductsService _productsService;

        /// <summary>
        ///     Creates a new instance of <see cref="ProductsController" />.
        /// </summary>
        /// <param name="productsService">The business layer controlling how products should be handled.</param>
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        ///     Get a list of all of the available products.
        /// </summary>
        /// <returns>The list of all products that are available/</returns>
        [Route]
        [HttpGet]
        [ResponseType(typeof(Products))]
        public IHttpActionResult GetAllProducts()
        {
            try
            {
                return Ok(_productsService.GetAllProducts());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        ///     Get a list of all products that contain the search string in their names.
        /// </summary>
        /// <param name="name">The search string to filter names by.</param>
        /// <returns>The list of filtered names.</returns>
        [Route]
        [HttpGet]
        [ResponseType(typeof(Products))]
        public IHttpActionResult GetAllProductsWithNameLike(string name)
        {
            try
            {
                return Ok(_productsService.GetAllProductsWithNameLike(name));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        ///     Get the product with the specified id.
        /// </summary>
        /// <param name="productId">The id of the product to get.</param>
        /// <returns>The specified product.</returns>
        [Route("{productId}")]
        [HttpGet]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(Guid productId)
        {
            try
            {
                var product = _productsService.GetProduct(productId);
                if (product != null)
                    return Ok(product);
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        ///     Creates a new product.
        /// </summary>
        /// <param name="product">The details of the product to create.</param>
        /// <returns>The location and details of the created product.</returns>
        [Route]
        [HttpPost]
        [ResponseType(typeof(Product))]
        public IHttpActionResult CreateProduct(Product product)
        {
            try
            {
                if (!ValidateProduct(product))
                    return BadRequest("The product details sent through are invalid");
                var created = _productsService.CreateProduct(product);
                return Created($"/products/{created.Id}", created);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        ///     Updates the specified product with new information.
        /// </summary>
        /// <param name="productId">The id of the product to update.</param>
        /// <param name="product">The details of the required update.</param>
        /// <returns>The updated product details.</returns>
        [Route("{productId}")]
        [HttpPut]
        [ResponseType(typeof(Product))]
        public IHttpActionResult UpdateProduct(Guid productId, Product product)
        {
            try
            {
                if (!ValidateProduct(product))
                    return BadRequest("The product details sent through are invalid");

                var result = _productsService.UpdateProduct(productId, product);
                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        private static bool ValidateProduct(Product product)
        {
            return !string.IsNullOrEmpty(product.Name);
        }

        /// <summary>
        ///     Deletes the specified product.
        /// </summary>
        /// <param name="productId">The id of the product to delete.</param>
        /// <param name="includeOptions">
        ///     A boolean indicating whether to delete any options related to the product as well.
        ///     Defaults to true. If false, the delete will fail if the product has options associated.
        /// </param>
        /// <returns>The delete result.</returns>
        [Route("{productId}")]
        [HttpDelete]
        public IHttpActionResult DeleteProduct(Guid productId, bool includeOptions = true)
        {
            try
            {
                if (_productsService.DeleteProduct(productId, includeOptions))
                    return Ok();
                return
                    BadRequest(
                        "The requested product has options associated with it, " +
                        "and you have included ?includeOptions=false.");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}