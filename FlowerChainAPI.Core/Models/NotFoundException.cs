using System;

namespace FlowerChainAPI.Models
{
    // a custom exception inherits from a regular exception
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/exceptions/
    public class NotFoundException : Exception
    {
    }
}