using Microsoft.EntityFrameworkCore;
using webshop_backend.Data;
using webshop_backend.Models;

namespace webshop_backend.Services
{
    public class ProductService : IProductService
    {
        private readonly WebshopDbContext _dbContext;

        private IWebHostEnvironment _webHostEnvironment;
        public ProductService(WebshopDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<int> CreateProduct(CreateProductDto dto)
        {
            // Save the image to disk
            string fileName = null;
            if (dto.Image != null && dto.Image.Length > 0)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
                string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }
            }

            // Create the product entity
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                ImageUrl = fileName != null ? "/Images/" + fileName : null
            };

            // Add the product to the database
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return product.Id;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        /*
        public async Task<Product> UpdateProduct(int id, UpdateProductDto dto)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;

            await _dbContext.SaveChangesAsync();

            return product;
        }
        */
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
