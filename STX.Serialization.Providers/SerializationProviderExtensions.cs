// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.Serialization.Providers.Abstractions;
using STX.SPAL.Core;

namespace STX.Serialization.Providers
	{
	public static class SerializationProviderExtensions
		{
		public static IServiceCollection RegisterSerializationProviders(this IServiceCollection services, bool allowMultipleProviders = false)
			{
			services = SPALOrchestrationService.RegisterAllImplementations<ISerializationProvider>(services, allowMultipleProviders);

			return services
				//.AddScoped<ISPALOrchestrationService, SPALOrchestrationService>()
				.AddScoped<ISerializationAbstractionProvider, SerializationProviderClient>();
			}
		}
	}
