using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Services;
using refactor_me.Controllers;
using NSubstitute;
using refactor_me.Models;
using System.Collections.Generic;
using System.Web.Http.Results;
using NSubstitute.ExceptionExtensions;

namespace refactor_me.test.Controllers
{
    [TestClass]
    public class ProductOptionsControllerTest
    {
        private IProductsService _productsService;
        private ProductOptionsController _productOptionsController;

        private static readonly Guid ProductId = Guid.NewGuid();
        private static readonly Guid OptionId = Guid.NewGuid();

        [TestInitialize]
        public void SetUp()
        {
            _productsService = Substitute.For<IProductsService>();
            _productOptionsController = new ProductOptionsController(_productsService);
        }

        [TestMethod]
        public void TestGetAllOptionsForProductSuccess()
        {
            // Given
            var productOptions = new ProductOptions(
                    new List<ProductOption> {
                        new ProductOption(Guid.NewGuid(), ProductId, "Option Name", "Option Description") });
            _productsService.GetAllProductOptions(ProductId).Returns(productOptions);

            // When
            var actionResult = _productOptionsController.GetAllOptionsForProduct(ProductId);
            var contentResult = actionResult as OkNegotiatedContentResult<ProductOptions>;

            // Then
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(productOptions, contentResult.Content);
        }

        [TestMethod]
        public void TestGetAllOptionsForProductFailure()
        {
            // Given
            _productsService.GetAllProductOptions(ProductId).Throws(new Exception());

            // When
            var actionResult = _productOptionsController.GetAllOptionsForProduct(ProductId);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }

        [TestMethod]
        public void TestGetProductOptionSuccess()
        {
            // Given
            var productOption = new ProductOption(OptionId, ProductId, "Option Name", "Option Description");
            _productsService.GetProductOption(ProductId, OptionId).Returns(productOption);

            // When
            var actionResult = _productOptionsController.GetProductOption(ProductId, OptionId);
            var contentResult = actionResult as OkNegotiatedContentResult<ProductOption>;

            // Then
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(productOption, contentResult.Content);
        }

        [TestMethod]
        public void TestGetProductOptionNotFound()
        {
            // Given
            // GetProductOption returns null. No code, as this is the default.

            // When
            var actionResult = _productOptionsController.GetProductOption(ProductId, OptionId);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestGetProductOptionFailure()
        {
            // Given
            _productsService.GetProductOption(ProductId, OptionId).Throws(new Exception());

            // When
            var actionResult = _productOptionsController.GetProductOption(ProductId, OptionId);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }

        [TestMethod]
        public void TestCreateOptionSuccess()
        {
            // Given
            var option = new ProductOption(Guid.Empty, Guid.Empty, "Option Name", "Option Description");
            var createdOption = new ProductOption(OptionId, ProductId, "Option Name", "Option Description");
            _productsService.CreateProductOption(ProductId, option).Returns(createdOption);

            // When
            var actionResult = _productOptionsController.CreateOption(ProductId, option);
            var createdResult = actionResult as CreatedNegotiatedContentResult<ProductOption>;

            // Then
            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Location);
            Assert.IsNotNull(createdResult.Content);
            Assert.AreEqual($"/products/{ProductId}/options/{OptionId}", createdResult.Location.ToString());
            Assert.AreEqual(createdOption, createdResult.Content);
        }

        [TestMethod]
        public void TestCreateOptionBadRequest()
        {
            // Given
            var option = new ProductOption(Guid.Empty, Guid.Empty, "", "Option Description");

            // When
            var actionResult = _productOptionsController.CreateOption(ProductId, option);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void TestCreateOptionFailure()
        {
            // Given
            var option = new ProductOption(Guid.Empty, Guid.Empty, "Option Name", "Option Description");
            _productsService.CreateProductOption(ProductId, option).Throws(new Exception());

            // When
            var actionResult = _productOptionsController.CreateOption(ProductId, option);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }

        [TestMethod]
        public void TestUpdateOptionSuccess()
        {
            // Given
            var option = new ProductOption(Guid.Empty, Guid.Empty, "Option Name", "Option Description");
            var updatedOption = new ProductOption(OptionId, ProductId, "Option Name", "Option Description");
            _productsService.UpdateProductOption(ProductId, OptionId, option).Returns(updatedOption);

            // When
            var actionResult = _productOptionsController.UpdateOption(ProductId, OptionId, option);
            var updatedResult = actionResult as OkNegotiatedContentResult<ProductOption>;

            // Then
            Assert.IsNotNull(updatedResult);
            Assert.IsNotNull(updatedResult.Content);
            Assert.AreEqual(updatedOption, updatedResult.Content);
        }

        [TestMethod]
        public void TestUpdateOptionBadRequest()
        {
            // Given
            var option = new ProductOption(Guid.Empty, Guid.Empty, "", "Option Description");

            // When
            var actionResult = _productOptionsController.UpdateOption(ProductId, OptionId, option);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void TestUpdateOptionNotFound()
        {
            // Given
            var option = new ProductOption(Guid.Empty, Guid.Empty, "Option Name", "Option Description");
            // UpdateProductOption returns null. No code, as this is the default.

            // When
            var actionResult = _productOptionsController.UpdateOption(ProductId, OptionId, option);
            var updatedResult = actionResult as OkNegotiatedContentResult<ProductOption>;

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestUpdateOptionFailure()
        {
            // Given
            var option = new ProductOption(Guid.Empty, Guid.Empty, "Option Name", "Option Description");
            _productsService.UpdateProductOption(ProductId, OptionId, option).Throws(new Exception());

            // When
            var actionResult = _productOptionsController.UpdateOption(ProductId, OptionId, option);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }

        [TestMethod]
        public void TestDeleteOptionSuccess()
        {
            // Given
            // DeleteOption does not throw an exception. This is the default, so no code added.
            
            // When
            var actionResult = _productOptionsController.DeleteOption(ProductId, OptionId);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void TestDeleteOptionFailure()
        {
            // Given
            _productsService.When(fake => fake.DeleteProductOption(ProductId, OptionId)).Do(call => { throw new Exception(); });

            // When
            var actionResult = _productOptionsController.DeleteOption(ProductId, OptionId);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }
    }
}
