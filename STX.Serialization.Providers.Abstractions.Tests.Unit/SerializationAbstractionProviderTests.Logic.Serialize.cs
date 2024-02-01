// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace LHDS.Core.Tests.Unit.Services.Foundations.Addresses
{
    public partial class SerializationAbstractionProviderTests
    {
        [Fact]
        public async Task ShouldSerializeAsync()
        {
            // given
            dynamic somePerson = new
            {
                Name = GetRandomString(),
                Surname = GetRandomString(),
                Age = GetRandomNumber()
            };

            dynamic inputPerson = somePerson;
            string randomString = GetRandomString();
            string outputString = GetRandomString();
            string expectedString = outputString;

            this.serializationProviderMock.Setup(provider =>
                provider.Serialize(It.IsAny<object>()))
                    .ReturnsAsync(outputString);

            // when
            string actualString = await this.serializationAbstractionProvider.Serialize(inputPerson);

            // then
            actualString.Should().BeEquivalentTo(expectedString);
        }
    }
}