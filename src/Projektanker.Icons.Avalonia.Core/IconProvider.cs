using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SharpDX.Direct2D1.Effects;

namespace Avalonia.Icons
{
    public static class IconProvider
    {
        private static readonly SortedList<string, IIconProvider> _iconProvidersByPrefix = 
            new SortedList<string, IIconProvider>(Comparer<string>.Default);

        public static string GetIconPath(string value)
        {
            var possibleProviders = _iconProvidersByPrefix
                .Where(kv => value.StartsWith(kv.Key, StringComparison.OrdinalIgnoreCase));

            foreach (KeyValuePair<string, IIconProvider> prefixProviderPair in possibleProviders)
            {
                if (prefixProviderPair.Value.TryGetIconPath(value, out string path))
                {
                    return path;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Registers an <see cref="IIconProvider"/> with the static <see cref="IconProvider"/> class.
        /// </summary>
        /// <param name="iconProvider">The <see cref="IIconProvider"/> to register.</param>
        /// <exception cref="ArgumentNullException"><paramref name="iconProvider"/> is null.</exception>
        /// <exception cref="ArgumentException">An <see cref="IIconProvider"/> with an conflicting prefix is already registered.</exception>
        public static void Register(IIconProvider iconProvider)
        {
            if (iconProvider is null)
            {
                throw new ArgumentNullException(nameof(iconProvider));
            }

            var conflicting = _iconProvidersByPrefix.Values.FirstOrDefault(existing => IsPrefix(existing.Prefix, iconProvider.Prefix));
            if (conflicting != null)
            {
                throw new ArgumentException($"Prefix \"{iconProvider.Prefix}\" conflicts with existing icon provider prefix \"{conflicting.Prefix}\".");
            }

            _iconProvidersByPrefix.Add(iconProvider.Prefix,iconProvider);
        }

        private static bool IsPrefix(string existing, string adding)
        {
            return existing.StartsWith(adding, StringComparison.OrdinalIgnoreCase)
                || adding.StartsWith(existing, StringComparison.OrdinalIgnoreCase);
        }
    }
}