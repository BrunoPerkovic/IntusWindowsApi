/*using IntusWindowsAPI.BL.Data;

namespace IntusWindowsAPI.BL.Dto;

public class ProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ProductType Type { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

}*/

using IntusWindowsAPI.BL.Data;

namespace IntusWindowsAPI.BL.Dto;

public record ProductDto(string Name, string Description, decimal Price, ProductType Type, int Width, int Height);