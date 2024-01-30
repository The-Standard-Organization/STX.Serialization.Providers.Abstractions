// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using STX.Serialization.Providers.Abstractions.Models.Exceptions;
using STX.Serialization.Providers.Abstractions.Models.Exceptions.Bases;
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

            Mock<SerializationValidationExceptionBase> serializationValidationExceptionMock =
                new Mock<SerializationValidationExceptionBase>();

            var expectedSerializationValidationProviderException = new SerializationValidationProviderException(
                message: "Serialization validation errors occurred, please try again.",
                innerException: serializationValidationExceptionMock.Object);

            this.serializationProviderMock.Setup(provider =>
                provider.Serialize(It.IsAny<object>()))
                    .ThrowsAsync(serializationValidationExceptionMock.Object);

            // when
            ValueTask<SerializationValidationExceptionBase> serializationTask =
                this.serializationAbstractionProvider.Serialize(inputPerson);

            SerializationValidationExceptionBase actualSerializationValidationProviderException =
                await Assert.ThrowsAsync<SerializationValidationExceptionBase>(
                    serializationTask.AsTask);

            // then
            actualSerializationValidationProviderException.Should()
                .BeEquivalentTo(expectedSerializationValidationProviderException);
        }
    }
}