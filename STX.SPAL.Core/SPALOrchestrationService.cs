// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

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
									&& interfaceType.Namespace == spalInterfaceType.Namespace
									&& interfaceType.Name == spalInterfaceType.Name
							))
					.ToArray();

			return implementations;
			}

		private static Assembly[] GetDependantAssemblies(
				Assembly assembly,
				int currentDepth = 0,
				int maximumDepth = DEFAULT_MAXIMUM_DEPTH_ANALISYS)
			{
			return assembly
				.GetReferencedAssemblies()
				.SelectMany(referencedAssemblyName =>
					{
						Assembly referencedAssembly = Assembly.Load(referencedAssemblyName);

						return currentDepth < maximumDepth
							? GetDependantAssemblies(referencedAssembly, currentDepth + 1, maximumDepth)
							: new Assembly[] { referencedAssembly };
					})
				.ToArray();
			}

		private static Assembly[] GetAllAssemblies(Assembly rootAssembly)
			{
			Assembly[] assemblies =
				GetDependantAssemblies(rootAssembly)
				.Distinct()
				.ToArray();

			return assemblies;
			}

		// Maybe this should go under an Extension Class for IServiceColletion
		public static ServiceCollection RegisterAllImplementations<T>(ServiceCollection services)
			where T : ISPALProvider
			{
			// Not work because, even STX.Serializations.Providers.XXXXX were added as a dependant project, there is no
			// type used from the main project. So the compiler "removes" those assemblies from GetReferencedAssemblies.
			Assembly[] assemblies = GetAllAssemblies(Assembly.GetEntryAssembly());

			Type[] typesToRegister = assemblies
				.SelectMany(assembly => GetInterfaceImplementations<T>(assembly))
				.ToArray();

			// This strategy uses MetadataLoadContext. Allows inspecting without explicit dependencies and more memory-efficient.
			// https://learn.microsoft.com/en-us/dotnet/standard/assembly/inspect-contents-using-metadataloadcontext
			string[] runtimeAssemblies = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll");
			var paths = new List<string>(runtimeAssemblies);

			string applicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			string[] applicationAssembliesPaths = Directory.GetFiles(applicationPath, "*.dll");
			paths.AddRange(applicationAssembliesPaths);

			var pathAssemblyResolver = new PathAssemblyResolver(paths);
			string coreAssemblyName = typeof(object).Assembly.GetName().Name;

			using (var metadaLoadContext = new MetadataLoadContext(pathAssemblyResolver, coreAssemblyName))
				{
				foreach (string applicationAssemblyPath in applicationAssembliesPaths)
					{
					Assembly assembly = metadaLoadContext.LoadFromAssemblyPath(applicationAssemblyPath);

					GetInterfaceImplementations<T>(assembly)
						.Select(implementationType =>
							{
								Assembly runtimeAssembly = Assembly.LoadFrom(applicationAssemblyPath);
								implementationType = runtimeAssembly.GetExportedTypes()
									.Single(type => type.Name == implementationType.Name);

								if (services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(T)))
									throw new Exception($"More than one implementation registered for {typeof(T).Name}. Please specify one of them or use keys for specifing multiple implementations for the same interface.");

								services.AddTransient(typeof(T), implementationType);
								Console.WriteLine($"Registered {implementationType.FullName} ({typeof(T).Name})");

								return services;
							})
						.ToArray();
					}
				}

			Console.WriteLine($"Register Done!.");

			return services;
			}

		public T GetImplementation<T>() =>
			TryCatch(() =>
				serviceProvider.GetRequiredService<T>());
		}
	}
