/*namespace IntusWindowsAPI.BL.Dto;

public class OrderDto
{
    public string Name { get; set; }
    public string State { get; set; }
    public List<ProductDto> Products { get; set; }
}*/

namespace IntusWindowsAPI.BL.Dto;

public record OrderDto(string Name, string State);
