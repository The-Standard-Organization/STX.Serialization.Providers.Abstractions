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

namespace LHDS.Core.Tests.Unit.Services.Foundations.Addresses
{
    public partial class SerializationAbstractionProviderTests
    {
        [Fact]
        public async Task ShouldCaptureAndLocaliseValidationExceptionsAsync()
        {
            // given
            dynamic dynamicPerson = new
            {
                Name = GetRandomString(),
                Surname = GetRandomString(),
                Age = GetRandomNumber()
            };

            dynamic inputPerson = dynamicPerson;
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
                provider.Serialize(It.IsAny<object>()))
                    .ThrowsAsync(testValidationException);

            // when
            ValueTask<string> serializationTask =
                this.serializationAbstractionProvider.Serialize(inputPerson);

            SerializationValidationProviderException actualSerializationValidationProviderException =
                await Assert.ThrowsAsync<SerializationValidationProviderException>(
                    serializationTask.AsTask);

            // then
            actualSerializationValidationProviderException.Should()
                .BeEquivalentTo(expectedSerializationValidationProviderException);
        }

        [Fact]
        public async Task ShouldCaptureAndLocaliseDependencyValidationExceptionsAsync()
        {
            // given
            dynamic dynamicPerson = new
            {
                Name = GetRandomString(),
                Surname = GetRandomString(),
                Age = GetRandomNumber()
            };

            dynamic inputPerson = dynamicPerson;
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
                provider.Serialize(It.IsAny<object>()))
                    .ThrowsAsync(testDependencyValidationException);

            // when
            ValueTask<string> serializationTask =
                this.serializationAbstractionProvider.Serialize(inputPerson);

            SerializationValidationProviderException actualSerializationValidationProviderException =
                await Assert.ThrowsAsync<SerializationValidationProviderException>(
                    serializationTask.AsTask);

            // then
            actualSerializationValidationProviderException.Should()
                .BeEquivalentTo(expectedSerializationValidationProviderException);
        }

        [Fact]
        public async Task ShouldCaptureAndLocaliseDependencyExceptionsAsync()
        {
            // given
            dynamic dynamicPerson = new
            {
                Name = GetRandomString(),
                Surname = GetRandomString(),
                Age = GetRandomNumber()
            };

            dynamic inputPerson = dynamicPerson;
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
                provider.Serialize(It.IsAny<object>()))
                    .ThrowsAsync(testDependencyException);

            // when
            ValueTask<string> serializationTask =
                this.serializationAbstractionProvider.Serialize(inputPerson);

            SerializationDependencyProviderException actualSerializationDependencyProviderException =
                await Assert.ThrowsAsync<SerializationDependencyProviderException>(
                    serializationTask.AsTask);

            // then
            actualSerializationDependencyProviderException.Should()
                .BeEquivalentTo(expectedSerializationDependencyProviderException);
        }
    }
}