using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopBridge.Api.Context;
using ShopBridge.Api.Controllers;
using ShopBridge.Api.Services;
using System;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Api.Models;
using System.Threading.Tasks;
using Namotion.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ShopBridge.Test
{
    [TestClass]
    public class ProductControllerTests : BaseTest
    {
        public static Guid ProductId = Guid.Parse("433d7f3f-e6cf-48e4-a1bf-7000954c97ca");
        private ProductController productController;

        [TestInitialize]
        public void TestSetUp()
        {
            productController = new ProductController(this.ProductService);
        }

        [TestMethod]
        public void Post_Test()
        {
            var product = new Product
            {
                Name = "Laptop",
                Price = 10000,
                Description = "HP-C2",
                CreatedBy = "TestUser",
                UpdatedBy = "TestUser",
                IsActive = true
            };
            var apiResult = productController.Post(product).GetAwaiter().GetResult();
            var savedProduct = apiResult.Value;
            Assert.IsNotNull(apiResult, "API Result is null");
            Assert.IsNotNull(savedProduct.Id, "Id is null");
            ProductId = savedProduct.Id;
            Assert.AreEqual(savedProduct.Name, product.Name, "Product Name does not match");
            Assert.AreEqual(savedProduct.Price, product.Price, "Product Name does not match");
            Assert.AreEqual(savedProduct.CreatedBy, product.CreatedBy, "Product Name does not match");
            Assert.AreEqual(savedProduct.Description, product.Description, "Product Name does not match");
            Assert.AreEqual(savedProduct.UpdatedBy, product.UpdatedBy, "Product Name does not match");
        }

        [TestMethod]
        public void Post_Exception_Test()
        {
            ThrowException(() =>
            {
                productController.Post(null).GetAwaiter().GetResult();
            });
        }
         
        [TestMethod]
        public void Put_Test()
        {
            var product = new Product
            {
                Id = ProductId,
                Name = "Laptop",
                Price = 10000,
                Description = "HP-C2-UPDATE-TEST",
                CreatedBy = "TestUser",
                UpdatedBy = "TestUser",
                IsActive = true
            };
            var apiResult = productController.Put(ProductId, product).GetAwaiter().GetResult();
            var savedProduct = apiResult.Value;
            Assert.IsNotNull(apiResult, "API Result is null");
            Assert.IsNotNull(savedProduct.Id, "Id is null");
            Assert.AreEqual(savedProduct.Name, product.Name, "Product Name does not match");
            Assert.AreEqual(savedProduct.Price, product.Price, "Product Name does not match");
            Assert.AreEqual(savedProduct.CreatedBy, product.CreatedBy, "Product Name does not match");
            Assert.AreEqual(savedProduct.Description, product.Description, "Product Name does not match");
            Assert.AreEqual(savedProduct.UpdatedBy, product.UpdatedBy, "Product Name does not match");
        }

        [TestMethod]
        public void Put_NotFound_Test()
        {
            var product = new Product
            {
                Id = ProductId,
                Name = "Laptop",
                Price = 10000,
                Description = "HP-C2-UPDATE-TEST",
                CreatedBy = "TestUser",
                UpdatedBy = "TestUser",
                IsActive = true
            };
            var notFoundapiResult = productController.Put(Guid.NewGuid(), product).GetAwaiter().GetResult();             
            var resultObj = (NotFoundObjectResult)notFoundapiResult.Result;
            Assert.AreEqual(resultObj.StatusCode, 404);
        }

        [TestMethod]
        public void Get_Test()
        {

            var apiResult = productController.Get().GetAwaiter().GetResult();
            Assert.IsNotNull(apiResult, "API Result is null");
            var products = (OkObjectResult)apiResult.Result;
            Assert.IsNotNull(apiResult.Result);
            var productList = products.Value as List<Product>;
            ProductId = productList.FirstOrDefault().Id;
            productList.ForEach(prod =>
            {
                Assert.IsNotNull(prod.Id, "Id is null");
                Assert.IsNotNull(prod.Name, "Name is null");
                Assert.IsNotNull(prod.Description, "Description is null");
                Assert.IsNotNull(prod.Price, "Price is null");
                Assert.IsNotNull(prod.IsActive, "IsActive is null");
            });

            var getProductResult = productController.Get(ProductId).GetAwaiter().GetResult();
            var productObj = (OkObjectResult)getProductResult.Result;
            var product = productObj.Value as Product;
            Assert.IsNotNull(product);
            Assert.IsNotNull(product.Id);

            var notFoundProductResult = productController.Get(Guid.NewGuid()).GetAwaiter().GetResult();
            var notFoundproductObj = (NotFoundResult)notFoundProductResult.Result;
            Assert.AreEqual(notFoundproductObj.StatusCode, 404);

        }

        [TestMethod]
        public void DeleteAsync_Test()
        {
            var apiResult = productController.DeleteAsync(ProductId).GetAwaiter().GetResult();
            var result = (OkResult)apiResult;
            Assert.AreEqual(200, result.StatusCode);

            Guid productId = Guid.NewGuid();
            apiResult = productController.DeleteAsync(productId).GetAwaiter().GetResult();
            var badRequestResult = (BadRequestResult)apiResult;
            Assert.AreEqual(400, badRequestResult.StatusCode);

        }
    }

}


