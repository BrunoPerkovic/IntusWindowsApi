using IntusWindowsAPI.BL.Data;
using IntusWindowsAPI.BL.Dto;

namespace IntusWindowsAPI.BL.Interfaces;

public interface IProductService
{
    Task<Product> CreateProductAsync(ProductDto productDto);
    Task<Product> GetProductAsync(int productId);
    Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize);
    Task<List<Product>> GetProductsByOrderAsync(int orderId);
    Task<List<Product>> GetProductsByTypeAsync(ProductType type);
    Task<Product> UpdateProductAsync(int productId, ProductDto productDto);
    Task DeleteProductAsync(int productId);
    
}