using System;
using System.Collections.Generic;
using System.Linq;
using Projektanker.Icons.Avalonia.FontAwesome.Models;
using Projektanker.Icons.Avalonia.Models;

namespace Projektanker.Icons.Avalonia.FontAwesome
{
    public class FontAwesomeIconProvider : FontAwesomeIconProvider<FontAwesomeFreeIconCollection>
    {
    }

    /// <summary>
    /// Implements the <see cref="IIconProvider"/> interface to provide font-awesome icons.
    /// </summary>
    public class FontAwesomeIconProvider<T> : IIconProvider where T : FontAwesomeIconCollection, new()
    {
        private const string _faProviderPrefix = "fa";
        private readonly Lazy<FontAwesomeIconCollection> _lazyCollection = new(() => new T());

        public string Prefix => _faProviderPrefix;
        private FontAwesomeIconCollection Collection => _lazyCollection.Value;

        /// <inheritdoc/>
        public IconModel GetIcon(string value)
        {
            if (!FontAwesomeIconKey.TryParse(value, out FontAwesomeIconKey key))
            {
                throw new KeyNotFoundException($"FontAwesome icon \"{value}\" not found!");
            }
            else if (!Collection.Icons.TryGetValue(key.Value, out FontAwesomeIcon icon))
            {
                throw new KeyNotFoundException($"FontAwesome icon \"{key.Value}\" not found!");
            }
            else if (!key.Style.HasValue)
            {
                return icon.Svg.Values.First().ToIconModel();
            }
            else if (icon.Svg.TryGetValue(key.Style.Value, out Svg svg))
            {
                return svg.ToIconModel();
            }

            throw new KeyNotFoundException($"FontAwesome style \"{key.Style}\" not found for icon \"{key.Value}\". Maybe you are trying to use an unsupported pro icon.");
        }
    }
}
