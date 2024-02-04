// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Moq;
using Tynamix.ObjectFiller;

namespace STX.Serialization.Providers.Abstractions.Tests.Unit
{
    public partial class SerializationAbstractionProviderTests
    {
        private readonly Mock<ISerializationProvider> serializationProviderMock;
        private readonly ISerializationAbstractionProvider serializationAbstractionProvider;

        public SerializationAbstractionProviderTests()
        {
            this.serializationProviderMock = new Mock<ISerializationProvider>();

            this.serializationAbstractionProvider = new SerializationAbstractionProvider(
                serializationProvider: this.serializationProviderMock.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();
    }
}