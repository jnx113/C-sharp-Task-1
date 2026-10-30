namespace Task_1.Exceptions;

public class ClientNotFoundException : Exception
{
    public ClientNotFoundException(string message) : base(message){}
}