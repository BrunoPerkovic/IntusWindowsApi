using IntusWindowsAPI.BL.Data;

namespace IntusWindowsAPI.BL.Dto;

public record ProductDto(string Name, string Description, decimal Price, ProductType Type, int Width, int Height);