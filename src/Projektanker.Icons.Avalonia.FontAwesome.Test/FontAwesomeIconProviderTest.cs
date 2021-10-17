using System;
using System.Collections.Generic;
using FluentAssertions;
using SkiaSharp;
using Xunit;

namespace Projektanker.Icons.Avalonia.FontAwesome.Test
{
    public class FontAwesomeIconProviderTest
    {
        private readonly IIconProvider _iconProvider = new FontAwesomeIconProvider();

        [Theory]
        [InlineData("fa-github")]
        [InlineData("fa-arrow-left")]
        [InlineData("fa-arrow-right")]
        [InlineData("fab fa-github")]
        [InlineData("fas fa-arrow-left")]
        [InlineData("fas fa-arrow-right")]
        public void Icon_Should_Exist_And_Be_Valid_SVG_Path(string key)
        {
            // Act #1
            var path = _iconProvider.GetIconPath(key);

            // Assert #1
            path.Should().NotBeNullOrEmpty();

            // Act #2
            var skiaPath = SKPath.ParseSvgPathData(path);

            // Assert #2
            skiaPath.Should().NotBeNull();
            skiaPath.Bounds.IsEmpty.Should().BeFalse();
        }

        [Fact]
        public void IconProvider_Should_Throw_Exception_If_Icon_Does_Not_Exist()
        {
            // Act
            Func<string> func = () => _iconProvider.GetIconPath("fa-you-cant-find-me");

            // Assert
            func.Should().Throw<KeyNotFoundException>();
        }
    }
}