using Task_1.Models;

namespace Task_1.Presentation;

public class FileWriter : IFinalWriter
{
    public void Write(List<TotalOrdersByClient> reports)
    {
        var lines = new List<string>();
        lines.Add("client_id\torders_sum\tfavorite_item_name");
        foreach (var report in reports)
        {
            lines.Add($"{report.ClientId}\t{report.OrdersSum}\t{report.ItemName}");
        }
        File.WriteAllLines("reports.txt", lines);
    }
}