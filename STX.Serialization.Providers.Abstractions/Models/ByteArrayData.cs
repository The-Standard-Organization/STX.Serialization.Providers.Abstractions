// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

namespace STX.Serialization.Providers.Abstractions.Models
{
    public class ByteArrayData : IDataRepresentation<byte[]>
    {
        public byte[] Value { get; set; }
    }
}
