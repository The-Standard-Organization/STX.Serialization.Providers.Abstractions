// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace STX.Serialization.Providers.Abstractions
{
    public partial class SerializationAbstractionProvider : ISerializationAbstractionProvider
    {
        private readonly ISerializationProvider SerializationProvider;

        public SerializationAbstractionProvider(ISerializationProvider serializationProvider) =>
            SerializationProvider = serializationProvider;

        public ValueTask<string> Serialize<T>(T @object) =>
            TryCatch<T, string>(async () =>
            {
                ValidateSerializationArgs(@object);

                return await this.SerializationProvider.Serialize(@object);
            });

        public ValueTask<T> Deserialize<T>(string json) =>
            TryCatch<string, T>(async () =>
            {
                ValidateDeserializationArgs(json);

                return await this.SerializationProvider.Deserialize<T>(json);
            });
    }
}
