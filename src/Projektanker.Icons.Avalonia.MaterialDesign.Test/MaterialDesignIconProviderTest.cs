using System;
using System.Collections.Generic;
using FluentAssertions;
using Projektanker.Icons.Avalonia.Models;
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
        public void Icon_Should_Exist_And_Be_Valid_SVG_Path(string value)
        {
            // Act
            var icon = _iconProvider.GetIcon($"mdi-{value}");
            var skiaPath = SKPath.ParseSvgPathData(icon.Path);

            // Assert
            skiaPath.Should().NotBeNull();
            skiaPath.Bounds.IsEmpty.Should().BeFalse();
        }

        [Theory]
        [InlineData("mdi-you-cant-find-me")]
        [InlineData("mdi")]
        public void IconProvider_Should_Throw_Exception_If_Icon_Does_Not_Exist(string value)
        {
            // Act
            Func<IconModel> func = () => _iconProvider.GetIcon(value);

            // Assert
            func.Should().Throw<KeyNotFoundException>()
                .WithMessage($"*{value}*");
        }
    }
}