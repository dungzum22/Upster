using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Entities;

namespace Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UpsertProduct(Product productDto)
        {

            if (productDto.Id > 0)
            {

                var existingProduct = await _context.Products.FindAsync(productDto.Id);


                if (existingProduct != null)
                {

                    if (existingProduct.IsDeleted == true)
                    {

                        var newProduct = new Product
                        {
                            Name = productDto.Name,
                            Description = productDto.Description,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            IsDeleted = false
                        };

                        _context.Products.Add(newProduct);
                        await _context.SaveChangesAsync();

                        return CreatedAtAction(nameof(UpsertProduct), new { id = newProduct.Id }, newProduct);
                    }
                    else
                    {

                        existingProduct.Name = productDto.Name;
                        existingProduct.Description = productDto.Description;
                        existingProduct.UpdatedAt = DateTime.Now;


                        if (productDto.IsDeleted == true)
                        {
                            existingProduct.IsDeleted = true;
                        }

                        await _context.SaveChangesAsync();

                        return Ok(existingProduct);
                    }
                }
            }


            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(UpsertProduct), new { id = product.Id }, product);
        }

        // API để lấy danh sách sản phẩm (không bao gồm đã xóa)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products
                .Where(p => p.IsDeleted != true)
                .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.IsDeleted == true)
            {
                return NotFound();
            }

            return product;
        }
    }
}