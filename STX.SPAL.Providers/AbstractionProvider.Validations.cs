// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.SPAL.Abstractions;
using System;

namespace STX.SPAL.Providers
{
    internal partial class AbstractionProvider<T>
    {
        private static void ValidateProvider(ISPALProvider provider)
        {
            if (provider == null)
                throw new Exception("There is no provider initialized.");
        }
    }
}
