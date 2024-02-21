// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.Serialization.Providers.Abstractions;
using STX.SPAL.Providers;
using STX.SPAL.Providers.Abstractions;
using System;
using System.Threading.Tasks;

namespace STX.Serialization.Providers
{
    internal partial class SerializationAbstractionProvider : ISerializationAbstractionProvider
    {
        private readonly IAbstractionProvider<ISerializationProvider> abstractionProvider;

        public SerializationAbstractionProvider(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            : this(defaultProviderType: null,
                  defaultProviderSPALId: null,
                  serviceLifetime)
        {
        }

        public SerializationAbstractionProvider(Type defaultProviderType, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            : this(defaultProviderType: defaultProviderType,
                  defaultProviderSPALId: null,
                  serviceLifetime)
        {
        }

        public SerializationAbstractionProvider(string defaultProviderSPALId, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            : this(defaultProviderType: null,
                  defaultProviderSPALId: defaultProviderSPALId,
                  serviceLifetime)
        {
        }

        public SerializationAbstractionProvider(
            Type defaultProviderType,
            string defaultProviderSPALId,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            this.abstractionProvider = abstractionProvider.GetAbstractionProvider<ISerializationProvider>(defaultProviderType, defaultProviderSPALId, serviceLifetime);
        }

        public SerializationAbstractionProvider(ISerializationProvider serializationProvider)
        {
            this.abstractionProvider = abstractionProvider.GetAbstractionProvider(serializationProvider);
        }

        public SerializationAbstractionProvider(
            IAbstractionProvider<ISerializationProvider> abstractionProvider,
            Type defaultProviderType,
            string defaultProviderSPALId)
        {
            this.abstractionProvider = abstractionProvider;
            abstractionProvider.UseProvider(defaultProviderType, defaultProviderSPALId);
        }

        public void UseProvider(string spalId = null)
        {
            this.abstractionProvider.UseProvider(spalId: spalId);
        }

        public void UseProvider(Type concreteProviderType = null, string spalId = null)
        {
            throw new NotImplementedException();
        }

        public void UseProvider<TConcreteProviderType>(string spalId = null) where TConcreteProviderType : ISerializationProvider
        {
            this.abstractionProvider.UseProvider<TConcreteProviderType>(spalId);
        }

        public string GetName()
        {
            ValidateSerializationProvider(this.abstractionProvider);

            return this.abstractionProvider.GetProvider().GetName();
        }

        public ValueTask<T> Deserialize<T>(string content)
        {
            ValidateSerializationProvider(this.abstractionProvider);

            return this.abstractionProvider.GetProvider().Deserialize<T>(content);
        }

        public ValueTask<string> Serialize<T>(T @object)
        {
            ValidateSerializationProvider(this.abstractionProvider);

            return this.abstractionProvider.GetProvider().Serialize<T>(@object);
        }

        public void InvokeWithProvider<TConcreteProviderType, TResult>(Action<ISerializationProvider> providerFunction) where TConcreteProviderType : ISerializationProvider
        {
            this.abstractionProvider.InvokeWithProvider<TConcreteProviderType, TResult>(providerFunction);
        }

        public ISerializationProvider GetProvider()
        {
            throw new NotImplementedException();
        }

        public void Invoke<TResult>(Action<ISerializationProvider> spalFunction)
        {
            throw new NotImplementedException();
        }

        public TResult Invoke<TResult>(Func<ISerializationProvider, TResult> spalFunction)
        {
            throw new NotImplementedException();
        }

        public ValueTask InvokeAsync<TResult>(Func<ISerializationProvider, ValueTask> spalFunction)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TResult> InvokeAsync<TResult>(Func<ISerializationProvider, ValueTask<TResult>> spalFunction)
        {
            throw new NotImplementedException();
        }
    }
}
