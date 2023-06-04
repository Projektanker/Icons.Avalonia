using System;
using Avalonia;

namespace Projektanker.Icons.Avalonia
{
    public static class AppBuilderIconsExtensions
    {
        public static AppBuilder WithIcons(this AppBuilder appBuilder, Action<IIconProviderContainer> configure)
        {
            IconProvider.Shared = new();
            configure(IconProvider.Shared);
            return appBuilder;
        }
    }
}