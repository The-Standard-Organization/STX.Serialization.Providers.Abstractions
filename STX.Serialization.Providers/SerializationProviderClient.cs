// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.Abstractions;
using STX.SPAL.Core;
using System.Threading.Tasks;

namespace STX.Serialization.Providers
	{
	public class SerializationProviderClient : ISerializationAbstractionProvider
		{
		private readonly ISPALOrchestrationService spalOrchestrationService;
		private readonly ISerializationProvider serializationProvider;

		public SerializationProviderClient(
				ISPALOrchestrationService spalOrchestrationService)
			{
			this.spalOrchestrationService = spalOrchestrationService;
			this.serializationProvider = spalOrchestrationService.GetImplementation<ISerializationProvider>();
			}

		public SerializationProviderClient(
				ISerializationProvider serializationProvider)
			{
			this.serializationProvider = serializationProvider;
			}

		public string GetName() =>
			this.serializationProvider.GetName();

		public ValueTask<T> Deserialize<T>(string content) =>
			this.serializationProvider.Deserialize<T>(content);

		public ValueTask<string> Serialize<T>(T @object) =>
			this.serializationProvider.Serialize<T>(@object);
		}
	}
