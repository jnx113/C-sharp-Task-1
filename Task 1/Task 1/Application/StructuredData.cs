using Task_1.Models;
using Task_1.Exceptions;
using Task_1.Infrastructure;

namespace Task_1.Application;

public class StructuredData
{
    private readonly IOrderReader _orderReader;

    public StructuredData(IOrderReader orderReader)
    {
        _orderReader = orderReader;
    }
    
    public TotalOrdersByClient ReduceTheData(long clientId)
    {
        List<Order> allOrders = _orderReader.ReadOrders("orders_log.csv");

        var ordersByClient = allOrders.Where(order => order.ClientId == clientId);
        if (!ordersByClient.Any())
        {
            throw new ClientNotFoundException($"Клиент с ID: {clientId} увы увы не найден.");
        }
        
        var totalSum = ordersByClient.Sum(order => order.ItemPrice * order.ItemQuantity);
        var favoriteItem = ordersByClient
            .GroupBy(order => order.ItemName)
            .Select(group => new
            {
                ItemName = group.Key,
                ItemsCount = group.Sum(order => order.ItemQuantity),
            })
            .OrderByDescending(q => q.ItemsCount)
            .FirstOrDefault();

        return new TotalOrdersByClient
        {
            ClientId = clientId,
            OrdersSum = MathF.Round(totalSum, 2),
            ItemName = favoriteItem.ItemName,
        };
    }
}