using System;
using Avalonia.Controls;

namespace Projektanker.Icons.Avalonia
{
    public static class AppBuilderIconsExtensions
    {
        public static TAppBuilder WithIcons<TAppBuilder>(this TAppBuilder appBuilder, Action<IIconProviderContainer> configure)
             where TAppBuilder : AppBuilderBase<TAppBuilder>, new()
        {
            var iconProvider = new IconProvider();
            configure(iconProvider);
            appBuilder.With<IIconReader>(iconProvider);

            return appBuilder;
        }
    }
}