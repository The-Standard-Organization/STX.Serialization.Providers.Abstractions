// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;
using System;
using System.Linq;

namespace STX.SPAL
{
    internal partial class SPALOrchestrationService
    {
        private T ResolveImplementation<T>(Type concreteProviderType, string spalId)
            where T : ISPALProvider
        {
            if (serviceProvider == null)
                throw new Exception("Service Provider not initialized.");

            T implementation = default;
            if (concreteProviderType != null
                    && !string.IsNullOrEmpty(spalId))
            {
                implementation = serviceProvider.GetKeyedService<T>(spalId);
                if (implementation == null)
                    implementation = serviceProvider.GetService<T>();
            }

            else if (concreteProviderType != null
                        && string.IsNullOrEmpty(spalId))
            {
                spalId = concreteProviderType.Namespace;
                implementation = serviceProvider.GetKeyedService<T>(spalId);
                if (implementation == null)
                    implementation = serviceProvider.GetService<T>();
            }

            else if (concreteProviderType == null
                        && string.IsNullOrEmpty(spalId))
            {
                implementation = serviceProvider.GetService<T>();
            }

            else if (concreteProviderType == null
                        && !string.IsNullOrEmpty(spalId))
            {
                implementation = serviceProvider.GetKeyedService<T>(spalId);
                if (implementation == null)
                    implementation = serviceProvider.GetService<T>();
            }

            ValidateInstance(implementation, concreteProviderType, spalId);

            return implementation;
        }

        private T[] ResolveImplementations<T>(Type concreteProviderType, string spalId)
            where T : ISPALProvider
        {
            // TODO - We need a way to get all the services registered. This method implemented doesn´t return anything because dependending
            // on how the registering was done maybe it used a keyService.
            return serviceProvider.GetServices<T>().ToArray();
        }
    }
}
