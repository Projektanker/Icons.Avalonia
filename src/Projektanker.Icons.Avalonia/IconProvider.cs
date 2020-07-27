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
        /// <returns>If <paramref name="value"/> is not <c>null</c> the path of the icon; otherwise <c>string.Empty</c>.</returns>
        /// <exception cref="KeyNotFoundException">No provider with prefix matching <paramref name="value"/> found.</exception>
        /// <exception cref="KeyNotFoundException">No icon associated
        /// with the specified <paramref name="value"/> found.</exception>
        public static string GetIconPath(string value)
        {
            if (value is null)
            {
                return string.Empty;
            }

            IIconProvider provider = _iconProvidersByPrefix
                .Select(prefixProviderPair => prefixProviderPair.Value)
                .FirstOrDefault(provider => value.StartsWith(provider.Prefix, StringComparison.OrdinalIgnoreCase));

            if (provider is null)
            {
                throw new KeyNotFoundException($"No provider with prefix matching \"{value}\" found.");
            }
            else
            {
                return provider.GetIconPath(value);
            }
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