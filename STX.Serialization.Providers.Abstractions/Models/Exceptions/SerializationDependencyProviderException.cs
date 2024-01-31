// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections;
using STX.Serialization.Providers.Abstractions.Models.Exceptions.Bases;
using Xeptions;

namespace STX.Serialization.Providers.Abstractions.Models.Exceptions
{
    public class SerializationDependencyProviderException : SerializationDependencyExceptionBase
    {
        public SerializationDependencyProviderException(string message, Xeption innerException)
            : base(message, innerException)
        { }

        public SerializationDependencyProviderException(string message, Xeption innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
