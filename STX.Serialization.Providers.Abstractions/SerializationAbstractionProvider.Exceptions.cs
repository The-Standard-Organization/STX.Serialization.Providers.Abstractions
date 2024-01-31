// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using STX.Serialization.Providers.Abstractions.Models.Exceptions;
using STX.Serialization.Providers.Abstractions.Models.Exceptions.Bases;
using Xeptions;

namespace STX.Serialization.Providers.Abstractions
{
    public partial class SerializationAbstractionProvider : ISerializationAbstractionProvider
    {
        private async ValueTask<TOutput> TryCatch<TInput, TOutput>(
            Func<ValueTask<TOutput>> asyncFunction)
        {
            try
            {
                return await asyncFunction();
            }
            catch (Xeption ex) when (ex is SerializationValidationExceptionBase)
            {
                throw CreateValidationException(ex);
            }
        }

        private SerializationValidationProviderException CreateValidationException(
            Xeption exception)
        {
            var serializationValidationException =
                new SerializationValidationProviderException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: exception,
                    data: exception.Data);

            return serializationValidationException;
        }
    }
}
