using Microsoft.Extensions.DependencyInjection;
using Task_1.Application;
using Task_1.Infrastructure;
using Task_1.Models;
using Task_1.Presentation;
using Task_1.Exceptions;

namespace Task_1;

public class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите режим вывода (console или file): ");
        string outputMode = Console.ReadLine()?.Trim().ToLower();

        var services = new ServiceCollection();
        services.AddTransient<IOrderReader, CsvOrderReader>();
        services.AddTransient<StructuredData>();

        if (outputMode == "console")
        {
            services.AddTransient<IFinalWriter, ConsoleWriter>();
        }
        else if (outputMode == "file")
        {
            services.AddTransient<IFinalWriter, FileWriter>();
        }
        else
        {
            Console.WriteLine("Тип вывода не предусмотрен.");
            return;
        }
        
        var serviceProvider = services.BuildServiceProvider();
        var reportGenerator = serviceProvider.GetRequiredService<StructuredData>();
        var writer = serviceProvider.GetRequiredService<IFinalWriter>();
        
        
        Console.Write("Введите ID клиентов через пробел (например: 1010 1020 999): ");
        string inputIds = Console.ReadLine();
        string[] stringIds = inputIds?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

        var reports = new List<TotalOrdersByClient>();
        
        foreach (var stringId in stringIds)
        {
            if (long.TryParse(stringId, out long clientId))
            {
                try
                {
                    var order = reportGenerator.ReduceTheData(clientId);
                    reports.Add(order);
                }
                catch (ClientNotFoundException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            else
            {
                Console.WriteLine($"Ошибка: '{stringId}' не является корректным числом.");
            }
        }
        
        if (reports.Count > 0)
        {
            writer.Write(reports);
            Console.WriteLine("Готово!");
        }
    }
}