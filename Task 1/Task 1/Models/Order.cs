namespace Task_1.Models;

public class Order
{
    public long ClientId { get; set; }
    public int OrderId { get; set; }
    public string ItemName { get; set; }
    public int ItemQuantity { get; set; }
    public float ItemPrice  { get; set; }
}