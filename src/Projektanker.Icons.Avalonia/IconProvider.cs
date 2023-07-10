using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projektanker.Icons.Avalonia
{
    /// <summary>
    /// Class providing the icon paths.
    /// </summary>
    public class IconProvider : IIconReader, IIconProviderContainer
    {
        private readonly SortedList<string, IIconProvider> _iconProvidersByPrefix = new(Comparer<string>.Default);
        public static IconProvider Current { get; } = new IconProvider();

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
                var msg = new StringBuilder()
                    .AppendLine($"No {nameof(IIconProvider)} with prefix matching \"{value}\" was registered with the application during startup.")
                    .AppendLine($"Please use {nameof(IconProvider)}.{nameof(Current)}.{nameof(Register)} to register at least one {nameof(IIconProvider)}")
                    .ToString();
                throw new KeyNotFoundException(msg);
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