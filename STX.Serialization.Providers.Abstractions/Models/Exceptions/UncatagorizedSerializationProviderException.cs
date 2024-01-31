// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace STX.Serialization.Providers.Abstractions.Models.Exceptions
{
    public class UncatagorizedSerializationProviderException : Xeption
    {
        public UncatagorizedSerializationProviderException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public UncatagorizedSerializationProviderException(string message, Exception innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
