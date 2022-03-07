using System;
using System.Collections.Generic;
using System.Linq;

namespace Projektanker.Icons.Avalonia
{
    /// <summary>
    /// Class providing the icon paths.
    /// </summary>
    public class IconProvider : IIconReader, IIconProviderContainer
    {
        private readonly SortedList<string, IIconProvider> _iconProvidersByPrefix = new(Comparer<string>.Default);

        /// <inheritdoc/>
        public string GetIconPath(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var provider = _iconProvidersByPrefix
                .Select(prefixProviderPair => prefixProviderPair.Value)
                .FirstOrDefault(p => value.StartsWith(p.Prefix, StringComparison.OrdinalIgnoreCase));

            if (provider is null)
            {
                throw new KeyNotFoundException($"No provider with prefix matching \"{value}\" found.");
            }
            else
            {
                return provider.GetIconPath(value);
            }
        }

        /// <inheritdoc>/>
        public IIconProviderContainer Register(IIconProvider iconProvider)
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
            return this;
        }

        /// <inheritdoc/>
        public IIconProviderContainer Register<TIconProvider>()
            where TIconProvider : IIconProvider, new()
        {
            return Register(new TIconProvider());
        }

        private static bool IsPrefix(string existing, string adding)
        {
            return existing.StartsWith(adding, StringComparison.OrdinalIgnoreCase)
                || adding.StartsWith(existing, StringComparison.OrdinalIgnoreCase);
        }
    }
}