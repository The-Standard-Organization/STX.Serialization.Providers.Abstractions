// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

namespace STX.Serialization.POC.NoProvidersReferenced
{
    internal partial class Program
    {
        private static void TryCatch(Action action, [CallerMemberName] string testName = "none")
        {
            try
            {
                Console.WriteLine(testName);
                action();
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine();
        }
    }
}
