using Task_1.Models;

namespace Task_1.Presentation;

public class ConsoleWriter : IFinalWriter
{
    public void Write(List<TotalOrdersByClient> reports)
    {
        Console.WriteLine("client_id\torders_sum\tfavorite_item_name");
        foreach (var report in reports)
        {
            Console.WriteLine($"{report.ClientId}\t{report.OrdersSum}\t{report.ItemName}");
        }
    }
}