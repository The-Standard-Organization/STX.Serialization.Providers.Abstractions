// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace STX.Serialization.Providers.Abstractions
{
    public interface ISerializationOperations
    {
        ValueTask<TOutput> SerializeAsync<TInput, TOutput>(TInput @object);

        ValueTask<TOutput> DeserializeAsync<TInput, TOutput>(TInput json);
    }
}
