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
        [InlineData("fa-brands fa-github")]
        [InlineData("fa-solid fa-arrow-left")]
        [InlineData("fa-regular fa-address-book")]
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
        [InlineData("fab fa-github", "fa-brands fa-github")]
        [InlineData("fas fa-arrow-left", "fa-solid fa-arrow-left")]
        [InlineData("far fa-address-book", "fa-regular fa-address-book")]
        public void Legacy_Style_Should_Still_Work(string legacy, string version6)
        {
            // Act
            var legacyPath = _iconProvider.GetIconPath(legacy);
            var version6Path = _iconProvider.GetIconPath(version6);

            // Assert
            legacyPath.Should().Be(version6Path);
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