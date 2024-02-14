// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.Serialization.Providers;
using STX.Serialization.Providers.Abstractions;
using System;

namespace STX.Serialization.POC
	{
	internal class Program
		{
		static void Main(string[] args)
			{
			IServiceCollection services = new ServiceCollection();

			services
				.RegisterSerializationProviders();

			IServiceProvider serviceProvider = services.BuildServiceProvider();
			using IServiceScope scope = serviceProvider.CreateScope();

			ISerializationAbstractionProvider serializationAbstractionProvider =
				scope.ServiceProvider
					.GetRequiredService<ISerializationAbstractionProvider>();

			System.Console.WriteLine(serializationAbstractionProvider.GetName());
			}
		}
	}
