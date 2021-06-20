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


namespace ShopBridge.Test
{
    public class BaseTest
    {
       
        protected IProductService ProductService;
        private static IServiceProvider ServicesProvider { get; set; }

        static BaseTest()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServicesProvider = services.BuildServiceProvider();
            SeedInitialData();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddDbContext<ApplicationDbContext>(context =>
            {
                context.UseInMemoryDatabase("ShopBridge");
            });

        }

        [TestInitialize]
        public void SetUp()
        {
            this.ProductService = ServicesProvider.GetService<IProductService>();
        }

        public static void SeedInitialData()
        {
            var applicationDbContext = ServicesProvider.GetService<ApplicationDbContext>();

            //Seeding Inital Data
            applicationDbContext.Products.Add(new Product
            {
                Id = Guid.Parse("433d7f3f-e6cf-48e4-a1bf-7000954c97ca"),
                Description = "Test-Desc",
                CreatedBy = "TestUser",
                CreatedOn = DateTime.Now,
                IsActive = true,
                Name = "TestProduct",
                Price = 100,
                UpdatedBy = "Test",
                UpdatedOn = DateTime.Now
            });
            applicationDbContext.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Description = "Test-Desc2",
                CreatedBy = "TestUser",
                CreatedOn = DateTime.Now,
                IsActive = true,
                Name = "Mobile",
                Price = 200,
                UpdatedBy = "Test",
                UpdatedOn = DateTime.Now
            });
            applicationDbContext.SaveChanges();
        }

        protected bool ThrowException(Action metod)
        {
            try
            {
                metod();
                Assert.Fail("Exception was expected");
                return false;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
                return true;
            }
        }
    }
}
