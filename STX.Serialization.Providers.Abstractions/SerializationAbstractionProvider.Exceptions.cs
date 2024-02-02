// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using STX.Serialization.Providers.Abstractions.Models.Exceptions;
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
            catch (Exception ex) when (ex is ISerializationValidationException)
            {
                throw CreateValidationException(ex);
            }
            catch (Exception ex) when (ex is ISerializationDependencyValidationException)
            {
                throw CreateValidationException(ex);
            }
            catch (Exception ex) when (ex is ISerializationDependencyException)
            {
                throw CreateDependencyException(ex);
            }
            catch (Exception ex) when (ex is ISerializationServiceException)
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
            Exception exception)
        {
            var serializationValidationException =
                new SerializationValidationProviderException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: exception as Xeption,
                    data: exception.Data);

            return serializationValidationException;
        }

        private SerializationDependencyProviderException CreateDependencyException(
            Exception exception)
        {
            var serializationDependencyException = new SerializationDependencyProviderException(
                message: "Serialization dependency error occurred, contact support.",
                innerException: exception as Xeption,
                data: exception.Data);

            return serializationDependencyException;
        }

        private SerializationServiceProviderException CreateServiceException(
            Exception exception)
        {
            var serializationServiceException = new SerializationServiceProviderException(
                message: "Serialization service error occurred, contact support.",
                innerException: exception as Xeption,
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
