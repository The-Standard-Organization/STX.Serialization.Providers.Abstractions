﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.Serialization.Providers.Abstractions;
using STX.SPAL.Core;
using System;

namespace STX.Serialization.Providers
{
    public static class SerializationProviderExtensions
    {
        public static IServiceCollection RegisterSerializationProviders(
            this IServiceCollection services,
            Type defaultProviderType = null,
            string defaultProviderSPALId = null)
        {
            services = SPALExtensions.RegisterAllImplementations<ISerializationProvider>(services);

            return services
                .AddScoped<ISerializationAbstractionProvider, SerializationAbstractionProvider>(sp =>
                    {
                        ISPALOrchestrationService spalOrchestrationService = sp.GetRequiredService<ISPALOrchestrationService>();
                        return new SerializationAbstractionProvider(spalOrchestrationService, defaultProviderType, defaultProviderSPALId);
                    });
        }
    }
}
