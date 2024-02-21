// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;

namespace STX.SPAL
{
    internal partial class SPALOrchestrationService
    {
        private static IServiceCollection RegisterImplementation(
            IServiceCollection services,
            Type spalInterfaceType,
            Type implementationType,
            ServiceLifetime serviceLifetime,
            bool registeringMultipleProviders)
        {
            ValidateImplementationTypeRegistered(services, spalInterfaceType, allowMultipleProviders);

            if (registeringMultipleProviders)
            {
                string spalId = implementationType.Namespace;
                services.Add(new ServiceDescriptor(spalInterfaceType, spalId, implementationType, serviceLifetime));
                //services.AddKeyedScoped(spalInterfaceType, spalId, implementationType);
                Console.WriteLine($"Registered {implementationType.FullName} ({spalInterfaceType.Name}) with SPAL Id {spalId} with Lifetime {serviceLifetime}");
            }
            else
            {
                services.Add(new ServiceDescriptor(spalInterfaceType, implementationType, serviceLifetime));
                //services.AddScoped(typeof(T), implementationType);
                Console.WriteLine($"Registered {implementationType.FullName} ({spalInterfaceType.Name}) with Lifetime {serviceLifetime}");
            }

            return services;
        }

        private static IServiceCollection RegisterImplementations(
            IServiceCollection services,
            Type spalInterfaceType,
            ServiceLifetime serviceLifetime)
        {
        Type[] exportedTypesOfT = GetExportedTypesFromAssembliesPaths(
               spalInterfaceType,
               concreteTypeProvider: null,
               spalId: null);

            foreach (var exportedTypeOfT in exportedTypesOfT)
                RegisterImplementation(services, spalInterfaceType, exportedTypeOfT, serviceLifetime, exportedTypesOfT.Length > 1);

            Console.WriteLine($"Register Done!.");

            return services;

        }
    }
}
