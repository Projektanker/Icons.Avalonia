using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Projektanker.Icons.Avalonia.Test
{
    public class IconProviderTest : IClassFixture<IconProviderFixture>
    {
        public IconProviderTest(IconProviderFixture iconProviderFixture)
        {
            _ = iconProviderFixture;
        }

        [Fact]
        public void Echo()
        {
            // Arrange
            const string echo = "Test-test";

            // Act
            var iconPath = IconProvider.GetIconPath(echo);

            // Assert
            iconPath.Should().Be(echo);
        }

        [Fact]
        public void EmptyValue()
        {
            // Act
            var iconPath = IconProvider.GetIconPath(string.Empty);

            // Assert
            iconPath.Should().BeEmpty();
        }

        [Fact]
        public void NullValue()
        {
            // Act
            var iconPath = IconProvider.GetIconPath(null);

            // Assert
            iconPath.Should().BeEmpty();
        }

        [Fact]
        public void ProviderNotFound()
        {
            // Act
            Func<string> func = () => IconProvider.GetIconPath("YouCantFindMe");

            // Assert
            func.Should().Throw<KeyNotFoundException>();
        }
    }
}