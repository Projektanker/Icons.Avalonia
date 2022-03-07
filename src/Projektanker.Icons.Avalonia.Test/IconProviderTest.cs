using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Projektanker.Icons.Avalonia.Test
{
    public class IconProviderTest
    {
        private readonly IconProvider _iconProvider;

        public IconProviderTest()
        {
            var mock = new Mock<IIconProvider>();
            mock.Setup(provider => provider.GetIconPath(It.IsAny<string>()))
                .Returns<string>(arg => arg);

            mock.SetupGet(provider => provider.Prefix)
                .Returns("Test");

            _iconProvider = new();
            _iconProvider.Register(mock.Object);
        }

        [Fact]
        public void Echo()
        {
            // Arrange
            const string echo = "Test-test";

            // Act
            var iconPath = _iconProvider.GetIconPath(echo);

            // Assert
            iconPath.Should().Be(echo);
        }

        [Fact]
        public void EmptyValue()
        {
            // Act
            var iconPath = _iconProvider.GetIconPath(string.Empty);

            // Assert
            iconPath.Should().BeEmpty();
        }

        [Fact]
        public void NullValue()
        {
            // Act
            var iconPath = _iconProvider.GetIconPath(null);

            // Assert
            iconPath.Should().BeEmpty();
        }

        [Fact]
        public void ProviderNotFound()
        {
            // Act
            Func<string> func = () => _iconProvider.GetIconPath("YouCantFindMe");

            // Assert
            func.Should().Throw<KeyNotFoundException>();
        }
    }
}