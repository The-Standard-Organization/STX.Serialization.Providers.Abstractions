// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;
using System;
using System.Linq;

namespace STX.SPAL.Core
{
    public partial class SPALOrchestrationService
    {
        private T ResolveImplementationWithDI<T>(Type concreteProviderType, string spalId)
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

            return implementation;
        }

        private T ResolveImplementationWithoutDI<T>(Type concreteProviderType, string spalId)
            where T : ISPALProvider
        {
            Type[] exportedTypesOfT = GetExportedTypesFromAssembliesPaths<T>(concreteProviderType, spalId);
            ValidateExportedTypes<T>(exportedTypesOfT);

            return (T)Activator.CreateInstance(exportedTypesOfT.Single());
        }

        private T ResolveImplementation<T>(Type concreteProviderType, string spalId)
            where T : ISPALProvider
        {
            T instance =
                serviceProvider != null
                    ? ResolveImplementationWithDI<T>(concreteProviderType, spalId)
                    : ResolveImplementationWithoutDI<T>(concreteProviderType, spalId);

            ValidateInstance(instance, concreteProviderType, spalId);

            return instance;
        }
    }
}
