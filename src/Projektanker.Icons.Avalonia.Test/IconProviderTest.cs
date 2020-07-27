using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Projektanker.Icons.Avalonia.Test
{
    [TestClass]
    public class IconProviderTest
    {
        [ClassInitialize]
        public static void ClassSetup(TestContext _)
        {
            IconProvider.Register(GetMockIconProvider());
        }

        [TestMethod]
        public void Echo()
        {
            string value = "Test-test";
            string iconPath = IconProvider.GetIconPath(value);
            Assert.AreEqual(value, iconPath);
        }

        [TestMethod]
        public void NullValue()
        {
            string iconPath = IconProvider.GetIconPath(null);
            Assert.AreEqual(string.Empty, iconPath);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ProviderNotFound()
        {
            string _ = IconProvider.GetIconPath("YouCantFindMe");
        }

        private static IIconProvider GetMockIconProvider()
        {
            Mock<IIconProvider> mock = new Mock<IIconProvider>();
            mock.Setup(provider => provider.GetIconPath(It.IsAny<string>())).Returns<string>(arg => arg);
            mock.Setup(provider => provider.Prefix).Returns("Test");
            return mock.Object;
        }
    }
}
