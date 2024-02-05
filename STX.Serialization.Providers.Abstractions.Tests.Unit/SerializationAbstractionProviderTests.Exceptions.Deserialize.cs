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
    }
}