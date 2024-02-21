// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.SPAL.Providers.Abstractions;

namespace STX.Serialization.Providers.Abstractions
{
    public interface ISerializationAbstractionProvider : IAbstractionProvider<ISerializationProvider>, ISerializationOperations
    {
    }
}
