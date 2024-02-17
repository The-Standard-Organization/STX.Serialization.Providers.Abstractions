// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;
using System;
using System.Linq;

namespace STX.SPAL.Core
{
    internal partial class SPALOrchestrationService
    {
        private static void ValidateImplementationTypeRegistered<T>(IServiceCollection services, bool allowMultipleTypes)
        {
            if (!allowMultipleTypes
                    && services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(T)))
                throw new Exception($"More than one implementation registered for {typeof(T).Name}. Please specify one of them, disallow multiple types or use keys for specifing multiple implementations for the same interface.");
        }

        private static void ValidateExportedTypes<T>(Type[] exportedTypesOfT)
            where T : ISPALProvider
        {
            if (exportedTypesOfT == null
                    || !exportedTypesOfT.Any())
                throw new Exception($"There is no concrete implementation for {typeof(T).FullName}.");

            else if (exportedTypesOfT.Length > 1)
            {
                string exportedTypesName = string.Join(
                    Environment.NewLine,
                    exportedTypesOfT
                        .Select(exportedType => $"\t{exportedType.FullName}")
                .ToArray());

                throw new Exception($"There is more than one concrete implementation for {typeof(T).FullName}.Found ->{Environment.NewLine}{exportedTypesName}");
            }
        }

        private static void ValidateInstance<T>(T instance, Type concreteProviderType, string spalId)
            where T : ISPALProvider
        {
            if (instance == null)
            {
                if (string.IsNullOrEmpty(spalId)
                        && concreteProviderType == null)
                    throw new Exception($"There is no dependency to resolve {typeof(T).Name}. Please add a package with one implementation or maybe SPAL detected more than one implementations.");
                
                else if (!string.IsNullOrEmpty(spalId)
                            && concreteProviderType == null)
                    throw new Exception($"There is no dependency to resolve {typeof(T).Name} for concrete provider spal Id {spalId}. Please add a package with one implementation or maybe SPAL detected more than one implementations.");

                else if (string.IsNullOrEmpty(spalId)
                            && concreteProviderType != null)
                    throw new Exception($"There is no dependency to resolve {typeof(T).Name} for concrete provider type {concreteProviderType.FullName}. Please add a package with one implementation or maybe SPAL detected more than one implementations.");

                else if (!string.IsNullOrEmpty(spalId)
                            && concreteProviderType != null)
                    throw new Exception($"There is no dependency to resolve {typeof(T).Name} for concrete provider type {concreteProviderType.FullName} with spal Id {spalId}. Please add a package with one implementation or maybe SPAL detected more than one implementations.");
            }

            else
            {
                if (!string.IsNullOrEmpty(spalId)
                        && instance.GetSPALId() != spalId)
                {
                    throw new Exception($"Instance found for {typeof(T).Name} but with different spal id ({spalId} - {instance.GetSPALId()}");
                }
            }
        }
    }
}
