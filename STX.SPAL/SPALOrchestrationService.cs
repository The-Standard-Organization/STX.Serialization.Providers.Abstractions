// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;
using System;

namespace STX.SPAL
{
    internal partial class SPALOrchestrationService : ISPALOrchestrationService
    {
        private const int DEFAULT_MAXIMUM_DEPTH_ANALISYS = 0;

        private readonly IServiceProvider serviceProvider;
        private static readonly bool allowMultipleProviders = true;

        public SPALOrchestrationService(Type spalInterfaceType, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            IServiceCollection services = new ServiceCollection();
            RegisterImplementations(services, spalInterfaceType, serviceLifetime);
            this.serviceProvider = services.BuildServiceProvider();
        }

        public SPALOrchestrationService(IServiceProvider serviceProvider) =>
            this.serviceProvider = serviceProvider;


        public static IServiceCollection RegisterAllImplementations<T>(IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where T : ISPALProvider
        {
            Type spalInterfaceType = typeof(T);
            RegisterImplementations(services, spalInterfaceType, serviceLifetime);
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

        public T[] GetImplementations<T>(Type concreteProviderType, string spalId) where T : ISPALProvider =>
            TryCatch(() =>
                ResolveImplementations<T>(concreteProviderType: concreteProviderType, spalId: spalId));
    }
}
