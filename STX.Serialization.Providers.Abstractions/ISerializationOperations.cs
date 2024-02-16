// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace STX.Serialization.Providers.Abstractions
{
    public interface ISerializationOperations
    {
        string GetName();
        ValueTask<string> Serialize<T>(T @object);
        ValueTask<T> Deserialize<T>(string content);
    }
}
