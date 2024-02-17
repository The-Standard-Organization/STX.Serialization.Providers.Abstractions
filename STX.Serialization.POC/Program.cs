// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

namespace STX.Serialization.POC
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            CallProviderWithoutDISingleProvider();
            CallProviderWithoutDIMultipleProviders();
            CallProviderWithoutDIMultipleProvidersSwitching();

            CallProviderWithDISingleProvider();
            CallProviderWithDIMultipleProviders();
            CallProviderWithDIMultipleProvidersSwitching();
        }
    }
}
