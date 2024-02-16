// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;
using System;

namespace STX.SPAL.Core
{
    public partial class SPALOrchestrationService : ISPALOrchestrationService
    {
        private const int DEFAULT_MAXIMUM_DEPTH_ANALISYS = 0;

        private readonly IServiceProvider serviceProvider;
        private static readonly bool allowMultipleProviders = true;

        public SPALOrchestrationService()
        {
        }

        public SPALOrchestrationService(IServiceProvider serviceProvider) =>
            this.serviceProvider = serviceProvider;

        public static IServiceCollection RegisterAllImplementations<T>(IServiceCollection services)
            where T : ISPALProvider
        {
            Type[] exportedTypesOfT = GetExportedTypesFromAssembliesPaths<T>(concreteTypeProvider: null, spalId: null);
            RegisterImplementations<T>(services, exportedTypesOfT);
            Console.WriteLine($"Register Done!.");

            return services;
        }

        public T GetImplementation<T>() where T : ISPALProvider =>
            TryCatch(() =>
                ResolveImplementation<T>(concreteProviderType: null, spalId: null));

        public T GetImplementation<T>(Type concreteTypeProvider) where T : ISPALProvider =>
            TryCatch(() =>
                ResolveImplementation<T>(concreteTypeProvider, spalId: null));

        public T GetImplementation<T>(string spalId) where T : ISPALProvider =>
            TryCatch(() =>
                ResolveImplementation<T>(concreteProviderType: null, spalId: spalId));

        public T GetImplementation<T>(Type concreteProviderType, string spalId) where T : ISPALProvider =>
            TryCatch(() =>
                ResolveImplementation<T>(concreteProviderType: concreteProviderType, spalId: spalId));
    }
}
