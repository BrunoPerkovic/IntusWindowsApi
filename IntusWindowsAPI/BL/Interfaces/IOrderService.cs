using IntusWindowsAPI.BL.Data;
using IntusWindowsAPI.BL.Dto;

namespace IntusWindowsAPI.BL.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(OrderDto orderDto);
    Task<Order> GetOrderAsync(int id);
    Task<List<Order>> GetOrdersAsync(int pageNumber, int pageSize);
    Task<Order> UpdateOrderAsync(int id, UpdateOrderDto orderDto);
    Task<OrderDto> AddProductToOrderAsync(int id, int productId);
    Task<OrderDto> RemoveProductFromOrderAsync(int id, int productId);
    Task<OrderDto> RemoveAllProductsFromOrderAsync(int id);
    Task DeleteOrderAsync(int id);
}