// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;
using STX.SPAL.Providers.Abstractions;
using System;

namespace STX.SPAL.Providers
{
    public static class AbstractionProviderExtensions
    {
        public static IServiceCollection RegisterAllImplementations<T>(
            this IServiceCollection services,
            Type defaultProviderType,
            string defaultProviderSPALId,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where T : ISPALProvider
        {
            return services =
                SPALExtensions.RegisterAllImplementations<T>(services, serviceLifetime)
                .AddScoped<IAbstractionProvider<T>, AbstractionProvider<T>>(sp =>
                {
                    ISPALOrchestrationService spalOrchestrationService = sp.GetRequiredService<ISPALOrchestrationService>();

                    return new AbstractionProvider<T>(
                        spalOrchestrationService,
                        defaultProviderType: defaultProviderType,
                        defaultProviderSPALId: defaultProviderSPALId);
                });
        }

        public static IAbstractionProvider<T> GetAbstractionProvider<T>(
            this IAbstractionProvider<T> abstractionProvider,
            Type defaultProviderType,
            string defaultProviderSPALId,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where T : ISPALProvider
        {
            return new AbstractionProvider<T>(defaultProviderType, defaultProviderSPALId, serviceLifetime);
        }

        public static IAbstractionProvider<T> GetAbstractionProvider<T>(
            this IAbstractionProvider<T> abstractionProvider,
            T provider)
            where T : ISPALProvider
        {
            return new AbstractionProvider<T>(provider);
        }
    }
}
