﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.Serialization.Providers;
using STX.Serialization.Providers.Abstractions;
using System;
using ProviderNewtonsoft = STX.Serialization.Providers.NewtonsoftJson;
using ProviderSystemTextJson = STX.Serialization.Providers.SystemTextJson;

namespace STX.Serialization.POC
{
    internal partial class Program
    {
        private static void CallProviderWithoutDISingleProvider()
        {
            TryCatch(() =>
            {
                ISerializationAbstractionProvider serializationAbstractionProvider =
                    new SerializationAbstractionProvider();

                Console.WriteLine(serializationAbstractionProvider.GetName());
            });
        }

        private static void CallProviderWithoutDIMultipleProviders()
        {
            TryCatch(() =>
            {
                ISerializationAbstractionProvider serializationAbstractionProvider =
                    new SerializationAbstractionProvider(typeof(ProviderNewtonsoft.SerializationProvider));

                Console.WriteLine(serializationAbstractionProvider.GetName());
            });
        }

        private static void CallProviderWithoutDIMultipleProvidersSwitching()
        {
            TryCatch(() =>
            {
                ISerializationAbstractionProvider serializationAbstractionProvider =
                    new SerializationAbstractionProvider(typeof(ProviderNewtonsoft.SerializationProvider));

                Console.WriteLine(serializationAbstractionProvider.GetName());

                serializationAbstractionProvider.UseSerializationProvider<ProviderSystemTextJson.SerializationProvider>();
                Console.WriteLine(serializationAbstractionProvider.GetName());
            });
        }

        private static void CallProviderWithDISingleProvider()
        {
            TryCatch(() =>
            {
                IServiceCollection services = new ServiceCollection();

                services
                    .RegisterSerializationProviders(defaultProviderType: typeof(ProviderNewtonsoft.SerializationProvider));

                IServiceProvider serviceProvider = services.BuildServiceProvider();
                using IServiceScope scope = serviceProvider.CreateScope();

                ISerializationAbstractionProvider serializationAbstractionProvider =
                    scope.ServiceProvider
                        .GetRequiredService<ISerializationAbstractionProvider>();

                Console.WriteLine(serializationAbstractionProvider.GetName());
            });
        }

        private static void CallProviderWithDIMultipleProviders()
        {
            TryCatch(() =>
            {
                IServiceCollection services = new ServiceCollection();

                services
                    .RegisterSerializationProviders(defaultProviderType: typeof(ProviderNewtonsoft.SerializationProvider));

                IServiceProvider serviceProvider = services.BuildServiceProvider();
                using IServiceScope scope = serviceProvider.CreateScope();

                ISerializationAbstractionProvider serializationAbstractionProvider =
                    scope.ServiceProvider
                        .GetRequiredService<ISerializationAbstractionProvider>();

                Console.WriteLine(serializationAbstractionProvider.GetName());
            });
        }

        private static void CallProviderWithDIMultipleProvidersSwitching()
        {
            TryCatch(() =>
            {
                IServiceCollection services = new ServiceCollection();

                services
                    .RegisterSerializationProviders(defaultProviderType: typeof(ProviderNewtonsoft.SerializationProvider));

                IServiceProvider serviceProvider = services.BuildServiceProvider();
                using IServiceScope scope = serviceProvider.CreateScope();

                ISerializationAbstractionProvider serializationAbstractionProvider =
                    scope.ServiceProvider
                        .GetRequiredService<ISerializationAbstractionProvider>();

                Console.WriteLine(serializationAbstractionProvider.GetName());

                serializationAbstractionProvider.UseSerializationProvider<ProviderSystemTextJson.SerializationProvider>();
                Console.WriteLine(serializationAbstractionProvider.GetName());
            });
        }
    }
}