using refactor_me.Models;
using refactor_me.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace refactor_me.Controllers
{
    /// <summary>
    /// Controls the various URI's of the web service. Handles all of HTTP specific functionality for product options.
    /// </summary>
    [RoutePrefix("products/{productId}/options")]
    public class ProductOptionsController : ApiController
    {
        private IProductsService _productsService;

        /// <summary>
        /// Creates a new instance of <see cref="ProductOptionsController"/>.
        /// </summary>
        /// <param name="productsService">The business layer controlling how products should be handled.</param>
        public ProductOptionsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Gets all of the options associated with a product.
        /// </summary>
        /// <param name="productId">The id of the product whose options are required.</param>
        /// <returns>A list of product options</returns>
        [Route]
        [HttpGet]
        [ResponseType(typeof(ProductOptions))]
        public IHttpActionResult GetAllOptionsForProduct(Guid productId)
        {
            try
            {
                return Ok(_productsService.GetAllProductOptions(productId));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Get a single option associated with a product.
        /// </summary>
        /// <param name="productId">The id of the product the option is associated with.</param>
        /// <param name="optionId">The id of the required option.</param>
        /// <returns>The details of the option.</returns>
        [Route("{optionId}")]
        [HttpGet]
        [ResponseType(typeof(ProductOption))]
        public IHttpActionResult GetProductOption(Guid productId, Guid optionId)
        {
            try
            {
                var productOption = _productsService.GetProductOption(productId, optionId);
                if (productOption != null)
                {
                    return Ok(productOption);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Create an option for a product.
        /// </summary>
        /// <param name="productId">The id of the product the option is related to.</param>
        /// <param name="option">The details of the option.</param>
        /// <returns>The details of the created option.</returns>
        [Route]
        [HttpPost]
        [ResponseType(typeof(ProductOption))]
        public IHttpActionResult CreateOption(Guid productId, ProductOption option)
        {
            try
            {
                if (!ValidateOption(option))
                {
                    return BadRequest("The product details sent through are invalid");
                }
                var created = _productsService.CreateProductOption(productId, option);
                return Created($"/products/{productId}/options/{created.Id}", created);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Update the details of a product option.
        /// </summary>
        /// <param name="productId">The id of the product the option is associated with.</param>
        /// <param name="optionId">The id of the option.</param>
        /// <param name="option">The details of the updated option.</param>
        /// <returns>The updated option.</returns>
        [Route("{optionId}")]
        [HttpPut]
        [ResponseType(typeof(ProductOption))]
        public IHttpActionResult UpdateOption(Guid productId, Guid optionId, ProductOption option)
        {
            try
            {
                if (!ValidateOption(option))
                {
                    return BadRequest("The product details sent through are invalid");
                }
                var result = _productsService.UpdateProductOption(productId, optionId, option);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Delete an option associated with a product.
        /// </summary>
        /// <param name="productId">The id of the product the option belongs to.</param>
        /// <param name="optionId">The id of the option to delete.</param>
        /// <returns>The delete status.</returns>
        [Route("{optionId}")]
        [HttpDelete]
        public IHttpActionResult DeleteOption(Guid productId, Guid optionId)
        {
            try
            {
                _productsService.DeleteProductOption(productId, optionId);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        private bool ValidateOption(ProductOption option)
        {
            return !string.IsNullOrEmpty(option.Name);
        }
    }
}