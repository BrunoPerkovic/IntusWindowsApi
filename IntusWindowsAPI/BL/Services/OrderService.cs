using FluentValidation.Results;
using IntusWindowsAPI.BL.Data;
using IntusWindowsAPI.BL.Dto;
using IntusWindowsAPI.BL.Interfaces;
using IntusWindowsAPI.DbContext;
using IntusWindowsAPI.Validation;
using Microsoft.EntityFrameworkCore;

namespace IntusWindowsAPI.BL.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger _logger;

    public OrderService(AppDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Order> CreateOrderAsync(OrderDto orderDto)
    {
        OrderDtoValidation validator = new OrderDtoValidation();
        ValidationResult result = validator.Validate(orderDto);
        if (!result.IsValid)
        {
            throw new Exception($"OrderDto is not valid: {result}");
        }

        var order = new Order
        {
            Name = orderDto.Name,
            State = orderDto.State,
            OrderProducts = new List<OrderProduct>() // Initialize the OrderProducts list
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderProducts) // Include the OrderProducts list
            .FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
        {
            throw new Exception($"Order with id: {id}, not found in db.");
        }
        return order;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        var orders = await _dbContext.Orders.ToListAsync();
        return orders;
    }

    public async Task<Order> UpdateOrderAsync(int id, UpdateOrderDto orderDto)
    {
        UpdateOrderDtoValidation validator = new UpdateOrderDtoValidation();
        ValidationResult result = validator.Validate(orderDto);
        if (!result.IsValid)
        {
            throw new Exception($"OrderDto is not valid: {result}");
        }
        
        var order = await GetOrderAsync(id: id);
        if (order == null)
        {
            throw new Exception($"Order with id: {id}, not found in db.");
        }
        
        order.Name = orderDto.Name;
        order.State = orderDto.State;
        
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<OrderDto> AddProductToOrderAsync(int id, int productId)
    {
        var order = await GetOrderAsync(id: id);
        if (order == null)
        {
            throw new Exception($"Order with id: {id}, not found in db.");
        }

        var product = await _dbContext.Products.FindAsync(productId);
        if (product == null)
        {
            throw new Exception($"Product with id: {productId}, not found in db.");
        }

        var orderProduct = new OrderProduct
        {
            OrderId = id,
            ProductId = productId,
            Order = order,
            Product = product,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (order.OrderProducts is null)
        {
            order.OrderProducts = new List<OrderProduct>();
        }

        order.OrderProducts.Add(orderProduct);
        await _dbContext.SaveChangesAsync();

        // Map the Order object to an OrderDto
        var orderDto = new OrderDto(order.Name, order.State);
        
        return orderDto;
    }

    public async Task<OrderDto> RemoveProductFromOrderAsync(int id, int productId)
    {
        var order = await GetOrderAsync(id: id);
        if (order == null)
        {
            throw new Exception($"Order with id: {id}, not found in db.");
        }

        var product = await _dbContext.Products.FindAsync(productId);
        if (product == null)
        {
            throw new Exception($"Product with id: {productId}, not found in db.");
        }

        if (order.OrderProducts == null || !order.OrderProducts.Any())
        {
            throw new Exception($"Order with id: {id}, does not contain any products.");
        }

        var orderProduct = order.OrderProducts.FirstOrDefault(op => op.ProductId == productId);
        if (orderProduct == null)
        {
            throw new Exception($"Product with id: {productId}, not found in order with id: {id}.");
        }

        order.OrderProducts.Remove(orderProduct);
        await _dbContext.SaveChangesAsync();
        
        var orderDto = new OrderDto(order.Name, order.State);
        
        return orderDto;
    }
    
    public async Task<OrderDto> RemoveAllProductsFromOrderAsync(int id)
    {
        var order = await GetOrderAsync(id: id);
        if (order == null)
        {
            throw new Exception($"Order with id: {id}, not found in db.");
        }

        if (order.OrderProducts.Count == 0)
        {
            throw new Exception($"Order with id: {id}, does not contain any products.");
        }

        order.OrderProducts.Clear();
        await _dbContext.SaveChangesAsync();
        
        var orderDto = new OrderDto(order.Name, order.State);
        return orderDto;
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await GetOrderAsync(id: id);
        if (order == null)
        {
            throw new Exception($"Order with id: {id}, not found in db.");
        }

        // Remove all associated OrderProduct entries
        _dbContext.OrderProducts.RemoveRange(order.OrderProducts);

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}