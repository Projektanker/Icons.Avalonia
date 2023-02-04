using System;
using Avalonia;

namespace Projektanker.Icons.Avalonia
{
    public static class AppBuilderIconsExtensions
    {
        public static AppBuilder WithIcons(this AppBuilder appBuilder, Action<IIconProviderContainer> configure)
        {
            var iconProvider = new IconProvider();
            configure(iconProvider);
            appBuilder.With<IIconReader>(iconProvider);

            return appBuilder;
        }
    }
}