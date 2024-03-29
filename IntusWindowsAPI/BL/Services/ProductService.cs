﻿using System.ComponentModel.DataAnnotations;
using IntusWindowsAPI.BL.Data;
using IntusWindowsAPI.BL.Dto;
using IntusWindowsAPI.BL.Interfaces;
using IntusWindowsAPI.DbContext;
using IntusWindowsAPI.Validation;
using Microsoft.EntityFrameworkCore;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace IntusWindowsAPI.BL.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger _logger;

    public ProductService(AppDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Product> CreateProductAsync(ProductDto productDto)
    {
        ProductDtoValidation validator = new ProductDtoValidation();
        ValidationResult result = validator.Validate(productDto);
        if (!result.IsValid)
        {
            throw new Exception($"ProductDto is not valid: {result}");
        }

        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Type = productDto.Type,
            Width = productDto.Width,
            Height = productDto.Height,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        return product;
    }

    public async Task<Product> GetProductAsync(int productId)
    {
        var product = await _dbContext.Products.FindAsync(productId);
        if (product == null)
        {
            throw new Exception($"Product with id: {productId}, hasn't been found in db.");
        }

        var productDto = new ProductDto(product.Name, product.Description, product.Price, product.Type,
            product.Width, product.Height);
        return product;
    }

    public async Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize)
    {
        var products = await _dbContext.Products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetProductsByOrderAsync(int orderId)
    {
        var products = await _dbContext.OrderProducts
            .Where(op => op.OrderId == orderId)
               .Select(op => op.Product)
            .ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetProductsByTypeAsync(ProductType type)
    {
        var products = await _dbContext.Products.Where(p => p.Type == type)
            .ToListAsync();
        return products;
    }

    public async Task<Product> UpdateProductAsync(int productId, ProductDto productDto)
    {
        ProductDtoValidation validator = new ProductDtoValidation();
        ValidationResult result = validator.Validate(productDto);
        if (!result.IsValid)
        {
            throw new Exception($"ProductDto is not valid: {result}");
        }

        var product = await GetProductAsync(productId: productId);
        if (product == null)
        {
            throw new Exception($"Product with id: {productId}, not found in db.");
        }

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.Type = productDto.Type;
        product.Width = productDto.Width;
        product.Height = productDto.Height;
        product.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return product;
    }

    public async Task DeleteProductAsync(int productId)
    {
        var product = await GetProductAsync(productId: productId);
        if (product == null)
        {
            throw new Exception($"Product with id: {productId}, not found in db.");
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }
}