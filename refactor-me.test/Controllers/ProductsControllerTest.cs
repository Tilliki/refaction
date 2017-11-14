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
    public class ProductsControllerTest
    {
        private IProductsService _productsService;
        private ProductsController _productsController;

        private static readonly Guid ProductId = Guid.NewGuid();

        [TestInitialize]
        public void SetUp()
        {
            _productsService = Substitute.For<IProductsService>();
            _productsController = new ProductsController(_productsService);
        }

        [TestMethod]
        public void TestGetAllProductsSuccess()
        {
            // Given
            var products = new Products(
                    new List<Product> {
                        new Product(Guid.NewGuid(), "Product Name", "Product Description", 1, 2) });
            _productsService.GetAllProducts().Returns(products);

            // When
            var actionResult = _productsController.GetAllProducts();
            var contentResult = actionResult as OkNegotiatedContentResult<Products>;

            // Then
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(products, contentResult.Content);
        }

        [TestMethod]
        public void TestGetAllProductsFailure()
        {
            // Given
            _productsService.GetAllProducts().Throws(new Exception());

            // When
            var actionResult = _productsController.GetAllProducts();

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }

        [TestMethod]
        public void TestGetAllProductsWithNameLikeSuccess()
        {
            // Given
            var products = new Products(
                    new List<Product> {
                        new Product(Guid.NewGuid(), "Product Name", "Product Description", 1, 2) });
            _productsService.GetAllProductsWithNameLike("Product Name").Returns(products);

            // When
            var actionResult = _productsController.GetAllProductsWithNameLike("Product Name");
            var contentResult = actionResult as OkNegotiatedContentResult<Products>;

            // Then
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(products, contentResult.Content);
        }

        [TestMethod]
        public void TestGetAllProductsWithNameLikeFailure()
        {
            // Given
            _productsService.GetAllProductsWithNameLike("Product Name").Throws(new Exception());

            // When
            var actionResult = _productsController.GetAllProductsWithNameLike("Product Name");

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }

        [TestMethod]
        public void TestGetProductSuccess()
        {
            // Given
            var product = new Product(ProductId, "Product Name", "Product Description", 1, 2);
            _productsService.GetProduct(ProductId).Returns(product);

            // When
            var actionResult = _productsController.GetProduct(ProductId);
            var contentResult = actionResult as OkNegotiatedContentResult<Product>;

            // Then
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(product, contentResult.Content);
        }

        [TestMethod]
        public void TestGetProductNotFound()
        {
            // Given
            // GetProduct returns null. No code, as this is the default.

            // When
            var actionResult = _productsController.GetProduct(ProductId);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestGetProductFailure()
        {
            // Given
            _productsService.GetProduct(ProductId).Throws(new Exception());

            // When
            var actionResult = _productsController.GetProduct(ProductId);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }

        [TestMethod]
        public void TestCreateProductSuccess()
        {
            // Given
            var product = new Product(Guid.Empty, "Product Name", "Product Description", 1, 2);
            var createdProduct = new Product(ProductId, "Product Name", "Product Description", 1, 2);
            _productsService.CreateProduct(product).Returns(createdProduct);

            // When
            var actionResult = _productsController.CreateProduct(product);
            var createdResult = actionResult as CreatedNegotiatedContentResult<Product>;

            // Then
            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Location);
            Assert.IsNotNull(createdResult.Content);
            Assert.AreEqual($"/products/{ProductId}", createdResult.Location.ToString());
            Assert.AreEqual(createdProduct, createdResult.Content);
        }

        [TestMethod]
        public void TestCreateProductBadRequest()
        {
            // Given
            var product = new Product(Guid.Empty, "", "Product Description", 1, 2);

            // When
            var actionResult = _productsController.CreateProduct(product);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void TestCreateProductFailure()
        {
            // Given
            var product = new Product(Guid.Empty, "Product Name", "Product Description", 1, 2);
            _productsService.CreateProduct(product).Throws(new Exception());

            // When
            var actionResult = _productsController.CreateProduct(product);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }

        [TestMethod]
        public void TestUpdateProductSuccess()
        {
            // Given
            var product = new Product(Guid.Empty, "Product Name", "Product Description", 1, 2);
            var updatedProduct = new Product(ProductId, "Product Name", "Product Description", 1, 2);
            _productsService.UpdateProduct(ProductId, product).Returns(updatedProduct);

            // When
            var actionResult = _productsController.UpdateProduct(ProductId, product);
            var contentResult = actionResult as OkNegotiatedContentResult<Product>;

            // Then
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(updatedProduct, contentResult.Content);
        }

        [TestMethod]
        public void TestUpdateProductBadRequest()
        {
            // Given
            var product = new Product(Guid.Empty, "", "Product Description", 1, 2);

            // When
            var actionResult = _productsController.UpdateProduct(ProductId, product);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void TestUpdateProductNotFound()
        {
            // Given
            var product = new Product(Guid.Empty, "Product Name", "Product Description", 1, 2);
            // UpdateProduct returns null. No code, as this is the default.

            // When
            var actionResult = _productsController.UpdateProduct(ProductId, product);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestUpdateProductFailure()
        {
            // Given
            var product = new Product(Guid.Empty, "Product Name", "Product Description", 1, 2);
            _productsService.UpdateProduct(ProductId, product).Throws(new Exception());

            // When
            var actionResult = _productsController.UpdateProduct(ProductId, product);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }

        [TestMethod]
        public void TestDeleteProductSuccess()
        {
            // Given
            _productsService.DeleteProduct(ProductId, false).Returns(true);

            // When
            var actionResult = _productsController.DeleteProduct(ProductId, false);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void TestDeleteProductBadRequest()
        {
            // Given
            _productsService.DeleteProduct(ProductId, false).Returns(false);

            // When
            var actionResult = _productsController.DeleteProduct(ProductId, false);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void TestDeleteProductFailure()
        {
            // Given
            _productsService.DeleteProduct(ProductId, false).Throws(new Exception());

            // When
            var actionResult = _productsController.DeleteProduct(ProductId, false);

            // Then
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }
    }
}
