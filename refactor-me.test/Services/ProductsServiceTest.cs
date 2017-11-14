using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Repositories;
using NSubstitute;
using refactor_me.Models;
using refactor_me.Services;
using System.Collections.Generic;

namespace refactor_me.test.Services
{
    [TestClass]
    public class ProductsServiceTest
    {
        private IProductsRepository _productsRepository;
        private IProductOptionsRepository _productOptionsRepository;
        private IProductsService _productsService;

        private static readonly Guid ProductId = Guid.NewGuid();

        [TestInitialize]
        public void SetUp()
        {
            _productsRepository = Substitute.For<IProductsRepository>();
            _productOptionsRepository = Substitute.For<IProductOptionsRepository>();
            _productsService = new ProductsService(_productsRepository, _productOptionsRepository);
        }

        [TestMethod]
        public void TestDeleteProductNoOptions()
        {
            // Given
            _productOptionsRepository.GetAllProductOptions(ProductId).Returns(new ProductOptions(null));

            // When
            var result = _productsService.DeleteProduct(ProductId, false);

            // Then
            _productOptionsRepository.DidNotReceive().DeleteAllProductOptionsForProduct(ProductId);
            _productsRepository.Received().DeleteProduct(ProductId);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestDeleteProductIncludeOptions()
        {
            // Given
            _productOptionsRepository.GetAllProductOptions(ProductId).Returns(
                new ProductOptions(new List<ProductOption> { new ProductOption(Guid.NewGuid(), ProductId, "Name", "Description") })
             );

            // When
            var result = _productsService.DeleteProduct(ProductId, true);

            // Then
            _productOptionsRepository.Received().DeleteAllProductOptionsForProduct(ProductId);
            _productsRepository.Received().DeleteProduct(ProductId);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestDeleteProductDontIncludeOptions()
        {
            // Given
            _productOptionsRepository.GetAllProductOptions(ProductId).Returns(
                new ProductOptions(new List<ProductOption> { new ProductOption(Guid.NewGuid(), ProductId, "Name", "Description") })
             );

            // When
            var result = _productsService.DeleteProduct(ProductId, false);

            // Then
            _productOptionsRepository.DidNotReceive().DeleteAllProductOptionsForProduct(ProductId);
            _productsRepository.DidNotReceive().DeleteProduct(ProductId);
            Assert.IsFalse(result);
        }
    }
}
