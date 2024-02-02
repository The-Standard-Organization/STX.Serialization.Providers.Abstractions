// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace STX.Serialization.Providers.Abstractions.Models.Exceptions
{
    public class SerializationValidationProviderException : Xeption
    {
        public SerializationValidationProviderException(string message, Xeption innerException)
            : base(message, innerException)
        { }

        public SerializationValidationProviderException(string message, Xeption innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
