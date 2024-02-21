// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;

namespace STX.SPAL.Providers
{
    internal partial class AbstractionProvider<T>
    {
        private static TResult TryCatch<TResult>(
           Func<TResult> function)
        {
            try
            {
                return function();
            }

            catch (InvalidOperationException invalidOperationException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
