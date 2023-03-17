using webshop_backend.Models;

namespace webshop_backend.Services
{
    public interface IProductService
    {
        Task<int> CreateProduct(CreateProductDto dto);
        Task<List<Product>> GetAllProducts();
    }
}
