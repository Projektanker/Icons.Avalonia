using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Projektanker.Icons.Avalonia.Models;
using Xunit;

namespace Projektanker.Icons.Avalonia.Test
{
    public class IconProviderTest
    {
        private readonly IconProvider _iconProvider;

        public IconProviderTest()
        {
            var mock = new Mock<IIconProvider>();
            mock.Setup(provider => provider.GetIcon(It.IsAny<string>()))
                .Returns<string>(arg => new IconModel(new ViewBoxModel(0, 0, 0, 0), new PathModel(arg)));

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
            var icon = _iconProvider.GetIcon(echo);

            // Assert
            var expected = new IconModel(
                new ViewBoxModel(0, 0, 0, 0),
                new PathModel(echo));
            icon.Should().Be(expected);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NullOrEmptyValue(string value)
        {
            // Act
            var icon = _iconProvider.GetIcon(value);

            // Assert
            var expected = new IconModel(
                new ViewBoxModel(0, 0, 0, 0),
                new PathModel(string.Empty));
            icon.Should().Be(expected);
        }

        [Fact]
        public void ProviderNotFound()
        {
            // Act
            Func<IconModel> func = () => _iconProvider.GetIcon("YouCantFindMe");

            // Assert
            func.Should().Throw<KeyNotFoundException>();
        }
    }
}