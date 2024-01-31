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
            catch (Xeption ex) when (ex is SerializationDependencyValidationExceptionBase)
            {
                throw CreateValidationException(ex);
            }
            catch (Xeption ex) when (ex is SerializationDependencyExceptionBase)
            {
                throw CreateDependencyException(ex);
            }
            catch (Xeption ex) when (ex is SerializationServiceExceptionBase)
            {
                throw CreateServiceException(ex);
            }
            catch (Exception ex)
            {
                var uncatagorizedSerializationProviderException =
                    new UncatagorizedSerializationProviderException(
                        message: "Serialization provider not properly implemented. Uncatagorized errors found, " +
                            "contact the serialization provider owner for support.",
                        innerException: ex,
                        data: ex.Data);

                throw CreateUncatagorizedServiceException(uncatagorizedSerializationProviderException);
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

        private SerializationDependencyProviderException CreateDependencyException(
            Xeption exception)
        {
            var serializationDependencyException = new SerializationDependencyProviderException(
                message: "Serialization dependency error occurred, contact support.",
                innerException: exception,
                data: exception.Data);

            return serializationDependencyException;
        }

        private SerializationServiceProviderException CreateServiceException(
            Xeption exception)
        {
            var serializationServiceException = new SerializationServiceProviderException(
                message: "Serialization service error occurred, contact support.",
                innerException: exception,
                data: exception.Data);

            return serializationServiceException;
        }

        private SerializationServiceProviderException CreateUncatagorizedServiceException(
            Exception exception)
        {
            var serializationServiceException = new SerializationServiceProviderException(
                message: "Uncatagorized serialization service error occurred, contact support.",
                innerException: exception as Xeption,
                data: exception.Data);

            return serializationServiceException;
        }
    }
}
