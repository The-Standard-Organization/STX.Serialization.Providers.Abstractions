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
        [Fact]
        public async Task ShouldValidateInputsOnSerializeAsync()
        {
            // given
            dynamic invalidObject = null;

            var invalidArgumentSerializationException =
                new InvalidArgumentSerializationException(message: "Invalid serialization argument(s), " +
                    "please correct the errors and try again.");

            invalidArgumentSerializationException.AddData(
                key: "Object",
                values: "Value is required");

            var expectedSerializationValidationProviderException =
                new SerializationValidationProviderException(
                    message: "Serialization validation errors occurred, please try again.",
                    innerException: invalidArgumentSerializationException);

            // when
            ValueTask<string> serializeTask =
                await this.serializationAbstractionProvider.Serialize(invalidObject);

            SerializationValidationProviderException actualSerializationValidationProviderException =
                await Assert.ThrowsAsync<SerializationValidationProviderException>(() =>
                    serializeTask.AsTask());

            // then
            actualSerializationValidationProviderException.Should()
                .BeEquivalentTo(expectedSerializationValidationProviderException);

            this.serializationProviderMock.VerifyNoOtherCalls();
        }
    }
}