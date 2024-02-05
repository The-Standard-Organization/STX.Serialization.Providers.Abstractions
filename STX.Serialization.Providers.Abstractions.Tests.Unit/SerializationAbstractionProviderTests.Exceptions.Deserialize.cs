// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using STX.Serialization.Providers.Abstractions.Models.Exceptions;
using STX.Serialization.Providers.Abstractions.Tests.Unit.Models.Exceptions;
using Xeptions;
using Xunit;

namespace STX.Serialization.Providers.Abstractions.Tests.Unit
{
    public partial class SerializationAbstractionProviderTests
    {
        [Fact]
        public async Task ShouldCaptureAndLocaliseValidationExceptionsOnDeserializeAsync()
        {
            // given
            string someString = GetRandomString();
            string inputString = someString;
            Xeption someException = new Xeption(message: "Some exception occurred.");

            TestValidationException testValidationException =
                new TestValidationException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: someException);

            SerializationValidationProviderException expectedSerializationValidationProviderException =
                new SerializationValidationProviderException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: testValidationException,
                    data: testValidationException.Data);

            this.serializationProviderMock.Setup(provider =>
                provider.Deserialize<object>(It.IsAny<string>()))
                    .ThrowsAsync(testValidationException);

            // when
            ValueTask<object> deserializationTask =
                this.serializationAbstractionProvider.Deserialize<object>(inputString);

            SerializationValidationProviderException actualSerializationValidationProviderException =
                await Assert.ThrowsAsync<SerializationValidationProviderException>(
                    deserializationTask.AsTask);

            // then
            actualSerializationValidationProviderException.Should()
                .BeEquivalentTo(expectedSerializationValidationProviderException);
        }

        [Fact]
        public async Task ShouldCaptureAndLocaliseDependencyValidationExceptionsOnDeserializeAsync()
        {
            // given
            string someString = GetRandomString();
            string inputString = someString;
            Xeption someException = new Xeption(message: "Some exception occurred.");

            TestDependencyValidationException testDependencyValidationException =
                new TestDependencyValidationException(
                    message: "Serialization dependency validation errors occurred, please try again.",
                    innerException: someException);

            SerializationValidationProviderException expectedSerializationValidationProviderException =
                new SerializationValidationProviderException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: testDependencyValidationException,
                    data: testDependencyValidationException.Data);

            this.serializationProviderMock.Setup(provider =>
                provider.Deserialize<object>(It.IsAny<string>()))
                    .ThrowsAsync(testDependencyValidationException);

            // when
            ValueTask<object> deserializationTask =
                this.serializationAbstractionProvider.Deserialize<object>(inputString);

            SerializationValidationProviderException actualSerializationValidationProviderException =
                await Assert.ThrowsAsync<SerializationValidationProviderException>(
                    deserializationTask.AsTask);

            // then
            actualSerializationValidationProviderException.Should()
                .BeEquivalentTo(expectedSerializationValidationProviderException);
        }

        [Fact]
        public async Task ShouldCaptureAndLocaliseDependencyExceptionsOnDeserializeAsync()
        {
            // given
            string someString = GetRandomString();
            string inputString = someString;
            Xeption someException = new Xeption(message: "Some exception occurred.");

            TestDependencyException testDependencyException =
                new TestDependencyException(
                    message: "Serialization dependency error occurred, contact support.",
                    innerException: someException);

            SerializationDependencyProviderException expectedSerializationDependencyProviderException =
                new SerializationDependencyProviderException(
                    message: "Serialization dependency error occurred, contact support.",
                    innerException: testDependencyException,
                    data: testDependencyException.Data);

            this.serializationProviderMock.Setup(provider =>
                provider.Deserialize<object>(It.IsAny<string>()))
                    .ThrowsAsync(testDependencyException);

            // when
            ValueTask<object> deserializationTask =
                this.serializationAbstractionProvider.Deserialize<object>(inputString);

            SerializationDependencyProviderException actualSerializationDependencyProviderException =
                await Assert.ThrowsAsync<SerializationDependencyProviderException>(
                    deserializationTask.AsTask);

            // then
            actualSerializationDependencyProviderException.Should()
                .BeEquivalentTo(expectedSerializationDependencyProviderException);
        }

        [Fact]
        public async Task ShouldCaptureAndLocaliseServiceExceptionsOnDeserializeAsync()
        {
            // given
            string someString = GetRandomString();
            string inputString = someString;
            Xeption someException = new Xeption(message: "Some exception occurred.");

            TestServiceException testServiceException =
                new TestServiceException(
                    message: "Serialization service error occurred, contact support.",
                    innerException: someException);

            SerializationServiceProviderException expectedSerializationServiceProviderException =
                new SerializationServiceProviderException(
                    message: "Serialization service error occurred, contact support.",
                    innerException: testServiceException,
                    data: testServiceException.Data);

            this.serializationProviderMock.Setup(provider =>
                provider.Deserialize<object>(It.IsAny<string>()))
                    .ThrowsAsync(testServiceException);

            // when
            ValueTask<object> deserializationTask =
                this.serializationAbstractionProvider.Deserialize<object>(inputString);

            SerializationServiceProviderException actualSerializationServiceProviderException =
                await Assert.ThrowsAsync<SerializationServiceProviderException>(
                    deserializationTask.AsTask);

            // then
            actualSerializationServiceProviderException.Should()
                .BeEquivalentTo(expectedSerializationServiceProviderException);
        }

        [Fact]
        public async Task ShouldCaptureAndLocaliseAnyNonServiceExceptionsOnDeserializeAsync()
        {
            // given
            string someString = GetRandomString();
            string inputString = someString;
            Xeption someException = new Xeption(message: "Some exception occurred.");

            UncatagorizedSerializationProviderException notImplementedSerializationProviderException =
                new UncatagorizedSerializationProviderException(
                    message: "Serialization provider not properly implemented. Uncatagorized errors found, " +
                        "contact the serialization provider owner for support.",

                    innerException: someException,
                    data: someException.Data);

            SerializationServiceProviderException expectedSerializationServiceProviderException =
                new SerializationServiceProviderException(
                    message: "Uncatagorized serialization service error occurred, contact support.",
                    innerException: notImplementedSerializationProviderException,
                    data: notImplementedSerializationProviderException.Data);

            this.serializationProviderMock.Setup(provider =>
                provider.Deserialize<object>(It.IsAny<string>()))
                    .ThrowsAsync(someException);

            // when
            ValueTask<object> deserializationTask =
                this.serializationAbstractionProvider.Deserialize<object>(inputString);

            SerializationServiceProviderException actualSerializationServiceProviderException =
                await Assert.ThrowsAsync<SerializationServiceProviderException>(
                    deserializationTask.AsTask);

            // then
            actualSerializationServiceProviderException.Should()
                .BeEquivalentTo(expectedSerializationServiceProviderException);
        }
    }
}