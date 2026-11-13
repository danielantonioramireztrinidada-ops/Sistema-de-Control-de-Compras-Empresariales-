namespace SistemaCompras.Infrastructure.Exceptions;

public class CompraException : Exception
{
    public CompraException(string message)
        : base(message)
    {
    }
}