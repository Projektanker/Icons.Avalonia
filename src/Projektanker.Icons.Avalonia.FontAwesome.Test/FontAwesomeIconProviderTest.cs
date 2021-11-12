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
        [InlineData("far fa-address-book")]
        public void Icon_Should_Exist_And_Be_Valid_SVG_Path(string value)
        {
            // Act #1
            var path = _iconProvider.GetIconPath(value);

            // Assert #1
            path.Should().NotBeNullOrEmpty();

            // Act #2
            var skiaPath = SKPath.ParseSvgPathData(path);

            // Assert #2
            skiaPath.Should().NotBeNull();
            skiaPath.Bounds.IsEmpty.Should().BeFalse();
        }

        [Theory]
        [InlineData("fa-you-cant-find-me")]
        [InlineData("fa")]
        [InlineData("far fa-arrow-left")]
        public void IconProvider_Should_Throw_Exception_If_Icon_Does_Not_Exist(string value)
        {
            // Act
            Func<string> func = () => _iconProvider.GetIconPath(value);

            // Assert
            func.Should().Throw<KeyNotFoundException>();
        }
    }
}