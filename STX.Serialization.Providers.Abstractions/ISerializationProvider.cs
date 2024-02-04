// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace STX.Serialization.Providers.Abstractions
{
    public interface ISerializationProvider
    {
        ValueTask<string> Serialize<T>(T obj);
        ValueTask<T> Deserialize<T>(string json);
    }
}
