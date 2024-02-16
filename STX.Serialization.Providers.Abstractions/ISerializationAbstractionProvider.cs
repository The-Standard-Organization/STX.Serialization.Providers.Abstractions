// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

namespace STX.Serialization.Providers.Abstractions
{
    public interface ISerializationAbstractionProvider : ISerializationOperations
    {
        void UseSerializationProvider<T>(string spalId = null) where T : ISerializationProvider;
        void UseSerializationProvider(string spalId);
    }
}
