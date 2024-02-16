// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;

namespace STX.SPAL.Core
{
    public static class SPALExtensions
    {
        public static IServiceCollection RegisterAllImplementations<T>(IServiceCollection services)
            where T : ISPALProvider
        {
            return SPALOrchestrationService.RegisterAllImplementations<T>(services)
                .AddScoped<ISPALOrchestrationService, SPALOrchestrationService>(sp => new SPALOrchestrationService(sp));
        }
    }
}
