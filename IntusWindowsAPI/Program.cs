using IntusWindowsAPI.BL.Data;
using IntusWindowsAPI.BL.Interfaces;
using IntusWindowsAPI.BL.Services;
using IntusWindowsAPI.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("IntusDb"));
    o.EnableDetailedErrors();
});
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ILogger, Logger<ProductService>>();
builder.Services.AddScoped<ILogger, Logger<OrderService>>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();