using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridge.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Test
{
    [TestClass]
    public class ModelTests
    {
        public void ProductModelTest()
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Laptop",
                Price = 10000,
                Description = "HP-C2-UPDATE-TEST",
                CreatedBy = "TestUser",
                UpdatedBy = "TestUser",
                IsActive = true,

            };
            Assert.IsNotNull(product, "product is null");
            Assert.IsNotNull(product.Id, "Id is null");
            Assert.AreEqual(product.Name, "Laptop", "Product Name does not match");
            Assert.AreEqual(product.Price, 10000, "Product Name does not match");
            Assert.AreEqual(product.CreatedBy, "TestUser", "Product Name does not match");
            Assert.AreEqual(product.Description, "HP-C2-UPDATE-TEST", "Product Name does not match");
            Assert.AreEqual(product.UpdatedBy, "TestUser", "Product Name does not match");
            Assert.IsTrue(product.IsActive);

        }

        [TestMethod]
        public void EntityModelTest()
        {
            var entity = new Entity
            {
                CreatedBy = "TEST",
                UpdatedBy = "TEST",
                UpdatedOn = DateTime.Now,
                CreatedOn = DateTime.Now,
                Id = Guid.NewGuid(),
                IsActive = true
            };
            Assert.IsNotNull(entity, "Entity is null");
            Assert.IsNotNull(entity.Id, "Id is null");

            Assert.AreEqual(entity.CreatedBy, "TEST", "CreatedBy does not match");
            Assert.AreEqual(entity.UpdatedBy, "TEST", "UpdatedBy does not match");

            Assert.IsNotNull(entity.CreatedOn);
            Assert.IsNotNull(entity.UpdatedOn);
            Assert.IsTrue(entity.IsActive);
        }
    }
}
