// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.Abstractions;
using STX.SPAL.Core;
using System;
using System.Threading.Tasks;

namespace STX.Serialization.Providers
{
    internal partial class SerializationAbstractionProvider : ISerializationAbstractionProvider
    {
        private readonly ISPALOrchestrationService spalOrchestrationService;
        private readonly ISerializationProvider defaultSerializationProvider;
        private ISerializationProvider serializationProvider;

        public SerializationAbstractionProvider()
        {
            this.spalOrchestrationService = defaultSerializationProvider.GetSPAL();
            this.defaultSerializationProvider = this.serializationProvider =
                GetSerializationProvider(serializationProviderType: null, spalId: null);
        }

        public SerializationAbstractionProvider(Type defaultProviderType)
        {
            this.spalOrchestrationService = defaultSerializationProvider.GetSPAL();
            this.defaultSerializationProvider = this.serializationProvider =
                GetSerializationProvider(
                    serializationProviderType: defaultProviderType,
                    spalId: null);
        }

        public SerializationAbstractionProvider(string defaultProviderSPALId)
        {
            this.spalOrchestrationService = defaultSerializationProvider.GetSPAL();
            this.defaultSerializationProvider = this.serializationProvider =
                GetSerializationProvider(
                    serializationProviderType: null,
                    spalId: defaultProviderSPALId);
        }

        public SerializationAbstractionProvider(Type defaultProviderType, string defaultProviderSPALId)
        {
            this.spalOrchestrationService = defaultSerializationProvider.GetSPAL();
            this.defaultSerializationProvider = this.serializationProvider =
                GetSerializationProvider(
                    serializationProviderType: defaultProviderType,
                    spalId: defaultProviderSPALId);
        }

        public SerializationAbstractionProvider(ISerializationProvider serializationProvider)
        {
            this.defaultSerializationProvider = this.serializationProvider = serializationProvider;
        }

        public SerializationAbstractionProvider(
            ISPALOrchestrationService spalOrchestrationService,
            Type defaultProviderType = null,
            string defaultProviderSPALId = null)
        {
            this.spalOrchestrationService = spalOrchestrationService;
            this.defaultSerializationProvider = this.serializationProvider =
                this.GetSerializationProvider(
                    serializationProviderType: defaultProviderType,
                    spalId: defaultProviderSPALId);
        }

        private ISerializationProvider GetSerializationProvider(Type serializationProviderType, string spalId)
        {
            ISerializationProvider serializationProvider = null;

            if (serializationProviderType == null
                    && string.IsNullOrEmpty(spalId))
            {
                if (defaultSerializationProvider != null)
                    serializationProvider = defaultSerializationProvider;
                else
                    serializationProvider = spalOrchestrationService.GetImplementation<ISerializationProvider>();
            }
            else
                serializationProvider = spalOrchestrationService.GetImplementation<ISerializationProvider>(serializationProviderType, spalId);

            return serializationProvider;
        }

        public void UseSerializationProvider<T>(string spalId = null)
            where T : ISerializationProvider
        {
            serializationProvider =
                GetSerializationProvider(
                    serializationProviderType: typeof(T),
                    spalId: spalId);
        }

        public void UseSerializationProvider(string spalId)
        {
            serializationProvider =
                GetSerializationProvider(
                    serializationProviderType: null,
                    spalId: spalId);
        }

        public string GetName()
        {
            ValidateSerializationProvider(this.serializationProvider);

            return this.serializationProvider.GetName();
        }

        public ValueTask<T> Deserialize<T>(string content)
        {
            ValidateSerializationProvider(this.serializationProvider);

            return this.serializationProvider.Deserialize<T>(content);
        }

        public ValueTask<string> Serialize<T>(T @object)
        {
            ValidateSerializationProvider(this.serializationProvider);

            return this.serializationProvider.Serialize<T>(@object);
        }
    }
}
