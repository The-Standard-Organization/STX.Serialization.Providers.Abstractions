// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.Abstractions;

namespace STX.Serialization.Providers.NewtonsoftJson
	{
	public class SerializationProvider : ISerializationProvider
		{
		public string GetName() =>
			this.GetType().FullName;
		}
	}
