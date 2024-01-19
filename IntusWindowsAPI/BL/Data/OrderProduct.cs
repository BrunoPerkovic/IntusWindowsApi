namespace IntusWindowsAPI.BL.Data;

public class OrderProduct : DbBase
{
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}