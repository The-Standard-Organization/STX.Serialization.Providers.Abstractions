// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace STX.Serialization.Providers.Abstractions
{
    public partial class SerializationAbstractionProvider : ISerializationAbstractionProvider
    {
        private readonly ISerializationProvider SerializationProvider;

        public SerializationAbstractionProvider(ISerializationProvider serializationProvider) =>
            SerializationProvider = serializationProvider;

        public async ValueTask<string> Serialize<T>(T obj) =>
            await this.SerializationProvider.Serialize(obj);

        public ValueTask<T> Deserialize<T>(string json) =>
            throw new NotImplementedException();
    }
}
