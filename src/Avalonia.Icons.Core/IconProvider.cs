using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Avalonia.Icons
{
    public static class IconProvider
    {
        private static readonly IconProviderCollection _iconProviders = new IconProviderCollection();

        public static void Register(IIconProvider iconProvider)
        {
            if (iconProvider is null)
            {
                throw new ArgumentNullException(nameof(iconProvider));
            }
            else if (_iconProviders.Any(existing => IsPrefix(existing.Prefix, iconProvider.Prefix)))
            {
                throw new ArgumentException($"Prefix \"{iconProvider.Prefix}\" conflicts with existing icon provider.");
            }
            
            _iconProviders.Add(iconProvider);
        }

        private static bool IsPrefix(string left, string right)
        {
            bool isPrefix = true;            
            for (int i = 0; i < Math.Min(left.Length, right.Length); i++)
            {
                isPrefix = char.ToUpperInvariant(left[i]) == char.ToUpperInvariant(right[i]);
                if (isPrefix)
                {
                    break;
                }
            }

            return isPrefix;
        }

        public static string GetIconPath(string value)
        {
            foreach (var iconProvicer in _iconProviders)
            {
                if (iconProvicer.TryGetIconPath(value, out string path))
                {
                    return path;
                }
            }

            return string.Empty;
        }
    }
}
