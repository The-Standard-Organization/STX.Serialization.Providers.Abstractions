// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;
using STX.SPAL.Providers.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace STX.SPAL.Providers
{
    internal partial class AbstractionProvider<T> : IAbstractionProvider<T>
        where T : ISPALProvider
    {
        private readonly ISPALOrchestrationService spalOrchestrationService;
        private readonly T defaultProvider;
        private T provider;

        public AbstractionProvider(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            : this(spalOrchestrationService: null, defaultProviderType: null, defaultProviderSPALId: null, serviceLifetime)
        {
        }

        public AbstractionProvider(Type defaultProviderType, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            : this(spalOrchestrationService: null, defaultProviderType: defaultProviderType, defaultProviderSPALId: null, serviceLifetime)
        {
        }

        public AbstractionProvider(string defaultProviderSPALId, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            : this(spalOrchestrationService: null, defaultProviderType: null, defaultProviderSPALId: defaultProviderSPALId, serviceLifetime)
        {
        }

        public AbstractionProvider(Type defaultProviderType, string defaultProviderSPALId, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            : this(spalOrchestrationService: null, defaultProviderType: defaultProviderType, defaultProviderSPALId: defaultProviderSPALId, serviceLifetime)
        {
        }

        public AbstractionProvider(
            ISPALOrchestrationService spalOrchestrationService,
            Type defaultProviderType,
            string defaultProviderSPALId,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            this.spalOrchestrationService =
                spalOrchestrationService == null
                    ? defaultProvider.GetSPAL<T>(serviceLifetime)
                    : spalOrchestrationService;

            this.defaultProvider = this.provider =
                GetProvider(
                    providerType: defaultProviderType,
                    spalId: defaultProviderSPALId);
        }

        public AbstractionProvider(T serializationProvider)
        {
            this.defaultProvider = this.provider = serializationProvider;
        }

        public AbstractionProvider(
            ISPALOrchestrationService spalOrchestrationService,
            Type defaultProviderType = null,
            string defaultProviderSPALId = null)
        {
            this.spalOrchestrationService = spalOrchestrationService;
            this.defaultProvider = this.provider =
                this.GetProvider(
                    providerType: defaultProviderType,
                    spalId: defaultProviderSPALId);
        }

        private T GetProvider(Type providerType, string spalId)
        {
            T provider = default;

            if (providerType == null
                    && string.IsNullOrEmpty(spalId))
            {
                if (defaultProvider != null)
                    provider = defaultProvider;
                else
                    provider = spalOrchestrationService.GetImplementation<T>();
            }
            else
                provider = spalOrchestrationService.GetImplementation<T>(providerType, spalId);

            return provider;
        }

        public T GetProvider() =>
            TryCatch(() =>
            {
                ValidateProvider(provider);

                return provider;
            });

        public void UseProvider(string spalId = null)
        {
            provider =
                GetProvider(
                    providerType: null,
                    spalId: spalId);
        }

        public void UseProvider(Type concreteProviderType = null, string spalId = null)
        {
            provider =
                GetProvider(
                    providerType: concreteProviderType,
                    spalId: spalId);
        }

        public void UseProvider<TConcreteProviderType>(string spalId = null)
            where TConcreteProviderType : T
        {
            provider =
                GetProvider(
                    providerType: typeof(TConcreteProviderType),
                    spalId: spalId);
        }

        public void Invoke<TResult>(Action<T> spalFunction) =>
            TryCatch(() =>
            {
                ValidateProvider(provider);
                spalFunction(provider);

                return true;
            });

        public void InvokeWithProvider<TConcreteProviderType, TResult>(Action<T> spalFunction)
            where TConcreteProviderType : T =>
            TryCatch(() =>
            {
                return spalOrchestrationService
                    .GetImplementations<T>(typeof(TConcreteProviderType), null)
                    .Select(implementation =>
                    {
                        spalFunction(implementation);
                        return true;
                    })
                    .ToArray();
            });

        public TResult Invoke<TResult>(Func<T, TResult> spalFunction) =>
            TryCatch<TResult>(() =>
            {
                ValidateProvider(provider);

                return spalFunction(provider);
            });


        public ValueTask InvokeAsync<TResult>(Func<T, ValueTask> spalFunction) =>
            TryCatch<ValueTask>(async () =>
            {
                ValidateProvider(provider);
                await spalFunction(provider);
            });

        public ValueTask<TResult> InvokeAsync<TResult>(Func<T, ValueTask<TResult>> spalFunction) =>
            TryCatch<ValueTask<TResult>>(async () =>
                {
                    ValidateProvider(provider);

                    return await spalFunction(provider);
                });
    }
}
