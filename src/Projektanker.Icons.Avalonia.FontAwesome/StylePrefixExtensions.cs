using System;

namespace Projektanker.Icons.Avalonia.FontAwesome
{
    internal static class StylePrefixExtensions
    {
        internal static Style ToStyle(this StylePrefix stylePrefix)
        {
            return stylePrefix switch
            {
                StylePrefix.Fas => Style.Solid,
                StylePrefix.Far => Style.Regular,
                StylePrefix.Fab => Style.Brands,
                _ => throw new NotImplementedException(),
            };
        }
    }
}