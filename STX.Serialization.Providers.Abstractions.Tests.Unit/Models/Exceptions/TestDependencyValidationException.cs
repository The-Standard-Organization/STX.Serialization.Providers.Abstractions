// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.Abstractions.Models.Exceptions;
using Xeptions;

namespace STX.Serialization.Providers.Abstractions.Tests.Unit.Models.Exceptions
{
    internal class TestDependencyValidationException : Xeption, ISerializationDependencyValidationException
    {
        public TestDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
