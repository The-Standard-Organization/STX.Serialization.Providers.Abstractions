// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using STX.Serialization.Providers.Abstractions.Models.Exceptions;
using Xunit;

namespace STX.Serialization.Providers.Abstractions.Tests.Unit
{
    public partial class SerializationAbstractionProviderTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldValidateInputsOnDeserializeAsync(string invalidText)
        {
            // given
            string invalidString = invalidText;

            var invalidArgumentSerializationException =
                new InvalidArgumentSerializationException(message: "Invalid serialization argument(s), " +
                    "please correct the errors and try again.");

            invalidArgumentSerializationException.AddData(
                key: "Json",
                values: "Text is required");

            var expectedSerializationValidationProviderException =
                new SerializationValidationProviderException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: invalidArgumentSerializationException,
                    data: invalidArgumentSerializationException.Data);

            // when
            ValueTask<object> deserializeTask =
                this.serializationAbstractionProvider.Deserialize<object>(invalidString);

            SerializationValidationProviderException actualSerializationValidationProviderException =
                await Assert.ThrowsAsync<SerializationValidationProviderException>(deserializeTask.AsTask);

            // then
            actualSerializationValidationProviderException.Should()
                .BeEquivalentTo(expectedSerializationValidationProviderException);

            this.serializationProviderMock.VerifyNoOtherCalls();
        }
    }
}