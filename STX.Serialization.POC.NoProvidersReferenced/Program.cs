// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.Serialization.Providers;
using STX.Serialization.Providers.Abstractions;
using System;
using System.Runtime.CompilerServices;

namespace STX.Serialization.POC
{
    internal class Program
    {
        const string NEWTONSOFT_SPAL_ID = "STX.Serialization.Providers.NewtonsoftJson";
        const string SYSTEMTEXT_SPAL_ID = "STX.Serialization.Providers.SystemTextJson";

        private static void TryCatch(Action action, [CallerMemberName] string testName = "none")
        {
            try
            {
                Console.WriteLine(testName);
                action();
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine();
        }

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
                    new SerializationAbstractionProvider(NEWTONSOFT_SPAL_ID);

                Console.WriteLine(serializationAbstractionProvider.GetName());
            });
        }

        private static void CallProviderWithoutDIMultipleProvidersSwitching()
        {
            TryCatch(() =>
            {
                ISerializationAbstractionProvider serializationAbstractionProvider =
                    new SerializationAbstractionProvider(NEWTONSOFT_SPAL_ID);

                Console.WriteLine(serializationAbstractionProvider.GetName());

                serializationAbstractionProvider.UseSerializationProvider(SYSTEMTEXT_SPAL_ID);
                Console.WriteLine(serializationAbstractionProvider.GetName());
            });
        }

        private static void CallProviderWithDISingleProvider()
        {
            TryCatch(() =>
            {
                IServiceCollection services = new ServiceCollection();

                services
                    .RegisterSerializationProviders(defaultProviderSPALId: NEWTONSOFT_SPAL_ID);

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
                    .RegisterSerializationProviders(defaultProviderSPALId: NEWTONSOFT_SPAL_ID);

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
                    .RegisterSerializationProviders(defaultProviderSPALId: NEWTONSOFT_SPAL_ID);

                IServiceProvider serviceProvider = services.BuildServiceProvider();
                using IServiceScope scope = serviceProvider.CreateScope();

                ISerializationAbstractionProvider serializationAbstractionProvider =
                    scope.ServiceProvider
                        .GetRequiredService<ISerializationAbstractionProvider>();

                Console.WriteLine(serializationAbstractionProvider.GetName());

                serializationAbstractionProvider.UseSerializationProvider(spalId: SYSTEMTEXT_SPAL_ID);
                Console.WriteLine(serializationAbstractionProvider.GetName());
            });
        }

        static void Main(string[] args)
        {
            CallProviderWithoutDISingleProvider();
            CallProviderWithoutDIMultipleProviders();
            CallProviderWithoutDIMultipleProvidersSwitching();

            CallProviderWithDISingleProvider();
            CallProviderWithDIMultipleProviders();
            CallProviderWithDIMultipleProvidersSwitching();
        }
    }
}
