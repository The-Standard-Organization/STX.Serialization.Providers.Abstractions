// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.Abstractions.Models.Exceptions.Bases;
using Xeptions;

namespace STX.Serialization.Providers.Abstractions.Tests.Unit.Models.Exceptions
{
    internal class TestDependencyException : SerializationDependencyExceptionBase
    {
        public TestDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
