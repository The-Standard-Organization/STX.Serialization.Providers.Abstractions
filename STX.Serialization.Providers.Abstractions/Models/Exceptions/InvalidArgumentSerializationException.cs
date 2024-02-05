// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace STX.Serialization.Providers.Abstractions.Models.Exceptions
{
    public class InvalidArgumentSerializationException : Xeption
    {
        public InvalidArgumentSerializationException(string message)
            : base(message)
        { }
    }
}