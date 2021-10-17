using System;
using System.Collections.Generic;
using FluentAssertions;
using SkiaSharp;
using Xunit;

namespace Projektanker.Icons.Avalonia.MaterialDesign.Test
{
    public class MaterialDesignIconProviderTest
    {
        private readonly IIconProvider _iconProvider = new MaterialDesignIconProvider();

        [Theory]
        [InlineData("ab-testing")]
        [InlineData("arrow-left")]
        [InlineData("arrow-right")]
        [InlineData("github")]
        public void Icon_Should_Exist_And_Be_Valid_SVG_Path(string key)
        {
            // Act #1
            var path = _iconProvider.GetIconPath($"mdi-{key}");

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
            Func<string> func = () => _iconProvider.GetIconPath("mdi-you-cant-find-me");

            // Assert
            func.Should().Throw<KeyNotFoundException>();
        }
    }
}