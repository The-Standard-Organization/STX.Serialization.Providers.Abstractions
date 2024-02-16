// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;

namespace STX.SPAL.Core
{
    public partial class SPALOrchestrationService
    {
        private static T TryCatch<T>(
            Func<T> function)
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
