// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace STX.Serialization.Providers.Abstractions.Tests.Unit
{
    public partial class SerializationAbstractionProviderTests
    {
        [Fact]
        public async Task ShouldDeserializeAsync()
        {
            // given
            dynamic somePerson = new
            {
                Name = GetRandomString(),
                Surname = GetRandomString(),
                Age = GetRandomNumber()
            };

            string randomJson = GetRandomJson(somePerson);
            object outputObject = somePerson;
            object expectedObject = outputObject;

            this.serializationProviderMock.Setup(provider =>
                provider.Deserialize<object>(It.IsAny<string>()))
                    .ReturnsAsync(outputObject);

            // when
            object actualObject = await this.serializationAbstractionProvider.Deserialize<object>(randomJson);

            // then
            actualObject.Should().BeEquivalentTo(expectedObject);
        }
    }
}