using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Projektanker.Icons.Avalonia.FontAwesome.Models;
using Projektanker.Icons.Avalonia.Models;

namespace Projektanker.Icons.Avalonia.FontAwesome
{
    /// <summary>
    /// Implements the <see cref="IIconProvider"/> interface to provide font-awesome icons.
    /// </summary>
    public class FontAwesomeIconProvider : IIconProvider
    {
        private const string _faProviderPrefix = "fa";
        private static readonly Lazy<Dictionary<string, FontAwesomeIcon>> _lazyIcons = new(Parse);

        public string Prefix => _faProviderPrefix;
        private static Dictionary<string, FontAwesomeIcon> Icons => _lazyIcons.Value;

        /// <inheritdoc/>
        public IconModel GetIcon(string value)
        {
            if (!FontAwesomeIconKey.TryParse(value, out FontAwesomeIconKey key))
            {
                throw new KeyNotFoundException($"FontAwesome icon \"{value}\" not found!");
            }
            else if (!Icons.TryGetValue(key.Value, out FontAwesomeIcon icon))
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

        private static Dictionary<string, FontAwesomeIcon> Parse()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"{assembly.GetName().Name}.Assets.icons.json";
            using var stream = assembly.GetManifestResourceStream(resourceName);

            var result = JsonSerializer.Deserialize(
                stream,
                FontAwesomeIconsJsonContext.Default.DictionaryStringFontAwesomeIcon);

            return result;
        }
    }
}