﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.Abstractions;
using System.Threading.Tasks;

namespace STX.Serialization.Providers.SystemTextJson
	{
	public class SerializationProvider : ISerializationProvider
		{
		public string GetName() =>
			this.GetType().FullName;

		public ValueTask<T> Deserialize<T>(string content)
			{
			throw new System.NotImplementedException();
			}

		public ValueTask<string> Serialize<T>(T @object)
			{
			throw new System.NotImplementedException();
			}
		}
	}