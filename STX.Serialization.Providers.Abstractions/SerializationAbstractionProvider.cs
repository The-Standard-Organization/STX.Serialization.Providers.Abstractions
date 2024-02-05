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
            TryCatch<T, string>(() =>
            {
                ValidateSerializationArgs(@object);

                return this.SerializationProvider.Serialize(@object);
            });

        public ValueTask<T> Deserialize<T>(string json) =>
            this.SerializationProvider.Deserialize<T>(json);
    }
}
