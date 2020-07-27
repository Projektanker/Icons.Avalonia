using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Projektanker.Icons.Avalonia.FontAwesome.Test
{
    [TestClass]
    public class FontAwesomeIconProviderTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            IconProvider.Register<FontAwesomeIconProvider>();
        }

        [TestMethod]
        public void NullValue()
        {
            string path = IconProvider.GetIconPath(null);
            Assert.AreEqual(string.Empty, path);
        }

        [TestMethod]
        public void ArrowLeft()
        {
            string path = IconProvider.GetIconPath("fa-arrow-left");
            Assert.IsFalse(string.IsNullOrEmpty(path));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void IconNotFound()
        {
            string _ = IconProvider.GetIconPath("fa-you-cant-find-me");
        }
    }
}
