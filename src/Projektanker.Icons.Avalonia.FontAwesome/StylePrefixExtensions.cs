using System;

namespace Projektanker.Icons.Avalonia.FontAwesome
{
    internal static class StylePrefixExtensions
    {
        internal static Style ToStyle(this StylePrefix stylePrefix)
        {
            switch (stylePrefix)
            {
                case StylePrefix.Fas:
                    return Style.Solid;

                case StylePrefix.Far:
                    return Style.Regular;

                case StylePrefix.Fab:
                    return Style.Brands;

                default:
                    throw new NotImplementedException($"Style prefix {stylePrefix} not implemented.");
            }
        }
    }
}