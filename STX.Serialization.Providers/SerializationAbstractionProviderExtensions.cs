// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.Serialization.Providers.Abstractions;
using STX.SPAL.Providers;
using STX.SPAL.Providers.Abstractions;
using System;

namespace STX.Serialization.Providers
{
    public static class SerializationAbstractionProviderExtensions
    {
        public static IServiceCollection RegisterSerializationProviders(
            this IServiceCollection services,
            Type defaultProviderType = null,
            string defaultProviderSPALId = null,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            return services
                .RegisterAllImplementations<ISerializationProvider>(defaultProviderType, defaultProviderSPALId, serviceLifetime)
                .AddScoped<ISerializationAbstractionProvider, SerializationAbstractionProvider>(sp =>
                    {
                        IAbstractionProvider<ISerializationProvider> abstractionProvider = sp.GetRequiredService<IAbstractionProvider<ISerializationProvider>>();
                        return new SerializationAbstractionProvider(abstractionProvider, defaultProviderType, defaultProviderSPALId);
                    });
        }

        public static ISerializationAbstractionProvider GetSerializationAbstractionProvider(
            this ISerializationAbstractionProvider serializationAbstractionProvider,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            return new SerializationAbstractionProvider(serviceLifetime);
        }

        public static ISerializationAbstractionProvider GetSerializationAbstractionProvider<T>(
            this ISerializationAbstractionProvider serializationAbstractionProvider,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where T : ISerializationProvider
        {
            return new SerializationAbstractionProvider(defaultProviderType: typeof(T), serviceLifetime);
        }

        public static ISerializationAbstractionProvider GetSerializationAbstractionProvider(
            this ISerializationAbstractionProvider serializationAbstractionProvider,
            string defaultProviderSPALId,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            return new SerializationAbstractionProvider(defaultProviderSPALId, serviceLifetime);
        }
    }
}
