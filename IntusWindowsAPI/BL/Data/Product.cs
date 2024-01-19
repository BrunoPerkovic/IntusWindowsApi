namespace IntusWindowsAPI.BL.Data;

public class Product : DbBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ProductType Type { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
}