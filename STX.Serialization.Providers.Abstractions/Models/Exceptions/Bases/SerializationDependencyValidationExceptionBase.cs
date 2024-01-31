﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace STX.Serialization.Providers.Abstractions.Models.Exceptions.Bases
{
    public abstract class SerializationDependencyValidationExceptionBase : Xeption
    {
        public SerializationDependencyValidationExceptionBase(string message, Xeption innerException)
            : base(message, innerException)
        { }

        public SerializationDependencyValidationExceptionBase(string message, Xeption innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}