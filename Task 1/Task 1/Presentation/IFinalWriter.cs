using Task_1.Models;

namespace Task_1.Presentation;

public interface IFinalWriter
{
    void Write(List<TotalOrdersByClient> ordersByClient);
}