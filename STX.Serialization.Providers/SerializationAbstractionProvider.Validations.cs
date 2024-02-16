// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.Abstractions;
using System;

namespace STX.Serialization.Providers
{
    public partial class SerializationAbstractionProvider
    {
        private static void ValidateSerializationProvider(ISerializationProvider serializationProvider)
        {
            if (serializationProvider == null)
                throw new Exception("There is no serialization provider initialized.");
        }
    }
}
