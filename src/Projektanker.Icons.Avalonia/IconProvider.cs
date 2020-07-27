using System;
using System.Collections.Generic;
using System.Linq;

namespace Projektanker.Icons.Avalonia
{
    /// <summary>
    /// Static class providing the icon paths.
    /// </summary>
    public static class IconProvider
    {
        private static readonly SortedList<string, IIconProvider> _iconProvidersByPrefix =
            new SortedList<string, IIconProvider>(Comparer<string>.Default);

        /// <summary>
        /// Gets the SVG path of the icon associated with the specified value using the registered icon providers.
        /// </summary>
        /// <param name="value">The value specifying the icon to return it's path from.</param>
        /// <returns>The path of the icon, if found; otherwise <c>string.Empty</c>.</returns>
        public static string GetIconPath(string value)
        {
            if (value is null)
            {
                return string.Empty;
            }

            var possibleProviders = _iconProvidersByPrefix
                .Where(kv => value.StartsWith(kv.Key, StringComparison.OrdinalIgnoreCase))
                .Select(prefixProviderPair => prefixProviderPair.Value);

            string path = null;
            var _ = possibleProviders.FirstOrDefault(provider => provider.TryGetIconPath(value, out path));
            return path ?? string.Empty;
        }

        /// <summary>
        /// Registers an <see cref="IIconProvider"/> with the static <see cref="IconProvider"/> class.
        /// </summary>
        /// <param name="iconProvider">The <see cref="IIconProvider"/> to register.</param>
        /// <exception cref="ArgumentNullException"><paramref name="iconProvider"/> is null.</exception>
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

            _iconProvidersByPrefix.Add(iconProvider.Prefix, iconProvider);
        }

        /// <summary>
        /// Registers an <see cref="IIconProvider"/> with the static <see cref="IconProvider"/> class.
        /// </summary>
        /// <typeparam name="TIconProvider">
        /// The type of the <see cref="IIconProvider"/> to register.
        /// </typeparam>
        /// <exception cref="ArgumentException">
        /// An <see cref="IIconProvider"/> with an conflicting prefix is already registered.
        /// </exception>
        public static void Register<TIconProvider>()
            where TIconProvider : IIconProvider, new()
        {
            Register(new TIconProvider());
        }

        private static bool IsPrefix(string existing, string adding)
        {
            return existing.StartsWith(adding, StringComparison.OrdinalIgnoreCase)
                || adding.StartsWith(existing, StringComparison.OrdinalIgnoreCase);
        }
    }
}