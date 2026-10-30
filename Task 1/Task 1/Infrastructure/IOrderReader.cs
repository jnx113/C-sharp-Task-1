using Task_1.Models;

namespace Task_1.Infrastructure;

public interface IOrderReader
{
    List<Order> ReadOrders(string filePath);
}