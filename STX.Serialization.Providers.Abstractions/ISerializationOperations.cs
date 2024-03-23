// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;
using STX.Serialization.Providers.Abstractions.Models;

namespace STX.Serialization.Providers.Abstractions
{
    public interface ISerializationOperations
    {
        ValueTask<TOutput> Serialize<TInput, TOutput>(TInput @object)
            where TOutput : IDataRepresentation<string>, IDataRepresentation<byte[]>, IDataRepresentation<Stream>;

        ValueTask<TOutput> Deserialize<TInput, TOutput>(TInput json)
            where TInput : IDataRepresentation<string>, IDataRepresentation<byte[]>, IDataRepresentation<Stream>;
    }
}
