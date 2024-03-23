// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;

namespace STX.Serialization.Providers.Abstractions.Models
{
    public class StreamData : IDataRepresentation<Stream>
    {
        public Stream Value { get; set; }
    }
}
