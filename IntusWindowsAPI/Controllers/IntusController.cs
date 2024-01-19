using IntusWindowsAPI.BL.Data;
using IntusWindowsAPI.BL.Dto;
using IntusWindowsAPI.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntusWindowsAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class IntusController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly ILogger<IntusController> _logger;

    public IntusController(IProductService productService, IOrderService orderService, ILogger<IntusController> logger)
    {
        _productService = productService;
        _orderService = orderService;
        _logger = logger;
    }
    
    [HttpPost, Route("product", Name = nameof(CreateProduct))]
    public async Task<IActionResult> CreateProduct(ProductDto productDto)
    {
        _logger.LogInformation("Creating product");
        var result = await _productService.CreateProductAsync(productDto);
        _logger.LogInformation("Created product");
        return Ok(result);
    }
    
    [HttpPut, Route("product/{id}", Name = nameof(UpdateProduct))]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
    {
        if (id < 1 || productDto == null)
        {
            return BadRequest();
        }
        
        _logger.LogInformation("Updating product");
        var result = await _productService.UpdateProductAsync(id, productDto);
        _logger.LogInformation("Updated product");
        return Ok(result);
    }
    
    [HttpGet, Route("product/{id}", Name = nameof(GetProductBy))]
    public async Task<IActionResult> GetProductBy(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _logger.LogInformation("Getting product");
        var result = await _productService.GetProductAsync(id);
        _logger.LogInformation("Got product");
        return Ok(result);
    }
    
    [HttpGet, Route("product", Name = nameof(GetProducts))]
    public async Task<IActionResult> GetProducts()
    {
        _logger.LogInformation("Getting products");
        var result = await _productService.GetProductsAsync();
        _logger.LogInformation("Got products");
        return Ok(result);
    }
    
    [HttpGet, Route("product/{type}/type", Name = nameof(GetProductsByType))]
    public async Task<IActionResult> GetProductsByType(ProductType type)
    {
        _logger.LogInformation("Getting products");
        var result = await _productService.GetProductsByTypeAsync(type);
        _logger.LogInformation("Got products");
        return Ok(result);
    }
    
    [HttpDelete, Route("product/{id}", Name = nameof(DeleteProduct))]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _logger.LogInformation("Deleting product");
        await _productService.DeleteProductAsync(id);
        _logger.LogInformation("Deleted product");
        return Ok();
    }
    
    [HttpPost, Route("order", Name = nameof(CreateOrder))]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto)
    {
        _logger.LogInformation("Creating order");
        var result = await _orderService.CreateOrderAsync(orderDto);
        _logger.LogInformation("Created order");
        return Ok(result);
    }
    
    [HttpPut, Route("order/{id}", Name = nameof(UpdateOrder))]
    public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDto orderDto)
    {
        if (id < 1 || orderDto == null)
        {
            return BadRequest();
        }
        
        _logger.LogInformation("Updating order");
        var result = await _orderService.UpdateOrderAsync(id, orderDto);
        _logger.LogInformation("Updated order");
        return Ok(result);
    }
    
    [HttpGet, Route("order/{id}", Name = nameof(GetOrderById))]
    public async Task<IActionResult> GetOrderById(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _logger.LogInformation("Getting order");
        var result = await _orderService.GetOrderAsync(id);
        _logger.LogInformation("Got order");
        return Ok(result);
    }
    
    [HttpGet, Route("order", Name = nameof(GetOrders))]
    public async Task<IActionResult> GetOrders()
    {
        _logger.LogInformation("Getting orders");
        var result = await _orderService.GetOrdersAsync();
        _logger.LogInformation("Got orders");
        return Ok(result);
    }
    
    [HttpDelete, Route("order/{id}", Name = nameof(DeleteOrder))]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _logger.LogInformation("Deleting order");
        await _orderService.DeleteOrderAsync(id);
        _logger.LogInformation("Deleted order");
        return Ok();
    }
    
    [HttpPut, Route("order/{id}/product/add/{productId}", Name = nameof(AddProductToOrder))]
    public async Task<IActionResult> AddProductToOrder(int id, int productId)
    {
        if (id < 1 || productId < 1)
        {
            return BadRequest();
        }
        
        _logger.LogInformation("Adding product to order");
        var result = await _orderService.AddProductToOrderAsync(id, productId);
        _logger.LogInformation("Added product to order");
        return Ok(result);
    }
    
    [HttpPut, Route("order/{id}/product/remove/{productId}", Name = nameof(RemoveProductFromOrder))]
    public async Task<IActionResult> RemoveProductFromOrder(int id, int productId)
    {
        if (id < 1 || productId < 1)
        {
            return BadRequest($"Either id: {id} or productId: {productId} is invalid.");
        } 
        
        _logger.LogInformation("Removing product from order");
        var result = await _orderService.RemoveProductFromOrderAsync(id, productId);
        _logger.LogInformation("Removed product from order");
        return Ok(result);
    }
    
    [HttpPut, Route("order/{id}/remove/all", Name = nameof(RemoveAllProductsFromOrder))]
    public async Task<IActionResult> RemoveAllProductsFromOrder(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        
        _logger.LogInformation("Removing all products from order");
        var result = await _orderService.RemoveAllProductsFromOrderAsync(id);
        _logger.LogInformation("Removed all products from order");
        return Ok(result);
    }
}