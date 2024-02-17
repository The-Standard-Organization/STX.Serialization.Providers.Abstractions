// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;

namespace STX.SPAL.Core
{
    internal partial class SPALOrchestrationService
    {
        private static IServiceCollection RegisterImplementation<T>(
            IServiceCollection services,
            Type implementationType,
            bool registeringMultipleProviders)
        {
            ValidateImplementationTypeRegistered<T>(services, allowMultipleProviders);

            if (registeringMultipleProviders)
            {
                string spalId = implementationType.Namespace;
                services.AddKeyedScoped(typeof(T), spalId, implementationType);
                Console.WriteLine($"Registered {implementationType.FullName} ({typeof(T).Name}) with SPAL Id {spalId}");
            }
            else
            {
                services.AddScoped(typeof(T), implementationType);
                Console.WriteLine($"Registered {implementationType.FullName} ({typeof(T).Name})");
            }

            return services;
        }

        private static IServiceCollection RegisterImplementations<T>(
            IServiceCollection services,
            Type[] implementationTypes)
        {
            foreach (var implementationType in implementationTypes)
                RegisterImplementation<T>(services, implementationType, implementationTypes.Length > 1);

            return services;
        }
    }
}
