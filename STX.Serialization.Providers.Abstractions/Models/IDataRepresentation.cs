// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

namespace STX.Serialization.Providers.Abstractions.Models
{
    public interface IDataRepresentation<T>
    {
        T Value { get; set; }
    }
}
