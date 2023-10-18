using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using Projektanker.Icons.Avalonia.Models;
using Xunit;

namespace Projektanker.Icons.Avalonia.Test
{
    public class IconProviderTest
    {
        private readonly IconProvider _iconProvider;

        public IconProviderTest()
        {
            var reader = Substitute.For<IIconProvider>();
            reader.GetIcon(Arg.Any<string>())
                .Returns(info => new IconModel(new ViewBoxModel(0, 0, 0, 0), new PathModel(info.Arg<string>())));

            reader.Prefix
                .Returns("Test");

            _iconProvider = new();
            _iconProvider.Register(reader);
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
