using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Api.Models;
using ShopBridge.Api.Services;

namespace ShopBridge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                if (products != null)
                    return Ok(products);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // GET api/<ProductsController>/5
        [HttpGet("int:Guid")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            try
            {
                var result = await _productService.GetProductAsync(id);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }

        // POST api/<ProductsController>
        [HttpPost]

        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            product.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                try
                {
                    return await _productService.AddProductAsync(product);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        // PUT api/<ProductsController>/5
        [HttpPut]
        public async Task<ActionResult<Product>> Put(Guid id, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.GetProductAsync(id);
                    if (result != null)
                    {
                        product.Id = id;
                      return  await _productService.UpdateProductAsync(product);
                    }
                    else
                    {
                        return NotFound("Product not found");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var product = await _productService.GetProductAsync(id);
                if (product != null)
                    result = await _productService.DeleteProductAsync(id);
                if (result == 0)
                    return BadRequest();
                else
                    return Ok();
            }

            catch
            {
                return BadRequest();
            }
        }
    }
}