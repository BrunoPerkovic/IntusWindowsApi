namespace IntusWindowsAPI.BL.Data;

public class Order : DbBase
{
    public string Name { get; set; }
    public string State { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
}