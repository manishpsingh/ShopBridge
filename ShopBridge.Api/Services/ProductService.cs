using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Api.Context;
using ShopBridge.Api.Models;

namespace ShopBridge.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _Context;

        public ProductService(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            var result = await _Context.Products.AddAsync(product);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> DeleteProductAsync(Guid Id)
        {
            int result = 0;
            var product = await _Context.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
            if (product != null)
            {
                _Context.Products.Remove(product);
                result = await _Context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Product> GetProductAsync(Guid Id)
        {
            return await _Context.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _Context.Products.ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(Product updatedProduct)
        {
            var product = await _Context.Products.Where(p => p.Id == updatedProduct.Id).FirstOrDefaultAsync();
            if (product != null)
            {

                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                product.UpdatedBy = updatedProduct.UpdatedBy;
                product.UpdatedOn = updatedProduct.UpdatedOn;
                await _Context.SaveChangesAsync();
                return product;

            }
            return null;
        }
    }
}
