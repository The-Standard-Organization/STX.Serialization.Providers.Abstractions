// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace STX.SPAL.Core
	{
	public partial class SPALOrchestrationService
		{
		private static void ValidateImplementationTypeRegistered<T>(IServiceCollection services, bool allowMultipleTypes)
			{
			if (!allowMultipleTypes
					&& services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(T)))
				throw new Exception($"More than one implementation registered for {typeof(T).Name}. Please specify one of them, disallow multiple types or use keys for specifing multiple implementations for the same interface.");
			}

		private static void ValidateInstance<T>(T instance)
			{
			if (instance == null)
				throw new Exception($"There is no dependency to resolve {typeof(T).Name}. Please add a package with one implementation.");
			}
		}
	}
