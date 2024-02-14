// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;
using System;
using System.Linq;
using System.Reflection;

namespace STX.SPAL.Core
	{
	public partial class SPALOrchestrationService : ISPALOrchestrationService
		{
		private const int DEFAULT_MAXIMUM_DEPTH_ANALISYS = 0;

		private readonly IServiceProvider serviceProvider;

		public SPALOrchestrationService(IServiceProvider serviceProvider)
			{
			this.serviceProvider = serviceProvider;
			}

		private static Type[] GetInterfaceImplementations<T>(Assembly assembly)
			where T : ISPALProvider
			{
			Type spalInterfaceType = typeof(T);

			Type[] implementations =
				assembly
					.GetExportedTypes()
					.Where(type =>
						type.GetInterfaces()
							.Any(interfaceType =>
								//@interface is T
								interfaceType.Assembly.FullName == spalInterfaceType.Assembly.FullName
									&& interfaceType.FullName == spalInterfaceType.FullName
							))
					.ToArray();

			return implementations;
			}

		private static Type[] GetExportedTypesFromAssemblyPath<T>(string assemblyPath)
			where T : ISPALProvider
			{
			Assembly applicationAssembly = Assembly.LoadFrom(assemblyPath);

			return GetInterfaceImplementations<T>(applicationAssembly);
			}

		private static IServiceCollection RegisterImplementation<T>(
				IServiceCollection services,
				Type implementationType,
				bool allowMultipleTypes)
			{
			ValidateImplementationTypeRegistered<T>(services, allowMultipleTypes);

			services.AddScoped(typeof(T), implementationType);
			Console.WriteLine($"Registered {implementationType.FullName} ({typeof(T).Name})");

			return services;
			}

		private static IServiceCollection RegisterImplementations<T>(
				IServiceCollection services,
				Type[] implementationTypes,
				bool allowMultipleTypes)
			{
			foreach (var implementationType in implementationTypes)
				RegisterImplementation<T>(services, implementationType, allowMultipleTypes);

			return services;
			}

		// Maybe this should go under an Extension Class for IServiceCollection
		public static IServiceCollection RegisterAllImplementations<T>(IServiceCollection services, bool allowMultipleProviders = false)
			where T : ISPALProvider
			{
			string[] applicationAssembliesPaths = GetApplicationAssemblies();
			foreach (string applicationAssemblyPath in applicationAssembliesPaths)
				{
				Type[] exportedTypesOfT = GetExportedTypesFromAssemblyPath<T>(applicationAssemblyPath);
				RegisterImplementations<T>(services, exportedTypesOfT, allowMultipleProviders);
				}

			Console.WriteLine($"Register Done!.");

			return services;
			}

		public T GetImplementation<T>() =>
			TryCatch(() =>
			{
				T instance = serviceProvider.GetRequiredService<T>();
				ValidateInstance(instance);

				return instance;
			});
		}
	}
