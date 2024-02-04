// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.SPAL.Abstractions;

namespace STX.Serialization.Providers.Abstractions
	{
	public interface ISerializationProvider : ISPALProvider
		{
		string GetName();
		}
	}
