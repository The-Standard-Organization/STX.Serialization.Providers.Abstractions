// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;

namespace STX.SPAL
{
    public static class SPALExtensions
    {
        public static IServiceCollection RegisterAllImplementations<T>(
            IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where T : ISPALProvider
        {
            return SPALOrchestrationService.RegisterAllImplementations<T>(services, serviceLifetime)
                .AddScoped<ISPALOrchestrationService, SPALOrchestrationService>(sp => new SPALOrchestrationService(sp));
        }

        public static ISPALOrchestrationService GetSPAL<T>(
            this ISPALProvider spalProvider,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            return new SPALOrchestrationService(typeof(T), serviceLifetime);
        }
    }
}
