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
    }
}