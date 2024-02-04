// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.Serialization.Providers.Abstractions;
using STX.SPAL.Core;

namespace STX.Serialization.POC
	{
	internal class Program
		{
		static void Main(string[] args)
			{
			var services = new ServiceCollection();
			services = SPALOrchestrationService.RegisterAllImplementations<ISerializationProvider>(services);
			services
				.AddScoped<ISPALOrchestrationService, SPALOrchestrationService>();

			IServiceProvider serviceProvider = services.BuildServiceProvider();
			using IServiceScope scope = serviceProvider.CreateScope();

			ISPALOrchestrationService spalOrchestrationService =
				scope.ServiceProvider
					.GetRequiredService<ISPALOrchestrationService>();

			ISerializationProvider serializationProvider =
				spalOrchestrationService.GetImplementation<ISerializationProvider>();

			System.Console.WriteLine(serializationProvider.GetName());
			}
		}
	}
