using Moq;

namespace Projektanker.Icons.Avalonia.Test
{
    public class IconProviderFixture
    {
        public Mock<IIconProvider> Mock { get; }

        public IconProviderFixture()
        {
            Mock = new Mock<IIconProvider>();
            Mock.Setup(provider => provider.GetIconPath(It.IsAny<string>()))
                .Returns<string>(arg => arg);

            Mock.SetupGet(provider => provider.Prefix)
                .Returns("Test");

            IconProvider.Register(Mock.Object);
        }
    }
}