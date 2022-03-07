using System;
using Avalonia.Controls;

namespace Projektanker.Icons.Avalonia
{
    public static class AppBuilderExtensions
    {
        public static TAppBuilder WithIcons<TAppBuilder>(this TAppBuilder appBuilder, Action<IconProvider> configure)
             where TAppBuilder : AppBuilderBase<TAppBuilder>, new()
        {
            var iconProvider = new IconProvider();
            configure(iconProvider);
            appBuilder.With(iconProvider);

            return appBuilder;
        }
    }
}