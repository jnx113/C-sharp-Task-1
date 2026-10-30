using System.Linq;
using System.IO;
using System.Globalization;
using Task_1.Models;


namespace Task_1.Infrastructure;

public class CsvOrderReader:  IOrderReader
{
    public List<Order> ReadOrders(string filePath)
    {
        List<Order> orders = new List<Order>();
        var lines = File.ReadLines(filePath).Skip(1);

        foreach (var line in lines)
        {
            var values = line.Split(',');
            Order order = new Order
            {
                ClientId = long.Parse(values[0]),
                OrderId = int.Parse(values[1]),
                ItemName = values[2],
                ItemQuantity = int.Parse(values[3]),
                ItemPrice = float.Parse(values[4], CultureInfo.InvariantCulture),
            };
            
            orders.Add(order);
        }
        
        return orders;
    }
}