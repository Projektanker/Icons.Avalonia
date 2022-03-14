using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Projektanker.Icons.Avalonia.FontAwesome
{
    /// <summary>
    /// Implements the <see cref="IIconProvider"/> interface to provide font-awesome icons.
    /// </summary>
    public class FontAwesomeIconProvider : IIconProvider
    {
        private const string _faProviderPrefix = "fa";
        private static readonly Lazy<Dictionary<string, FontAwesomeIcon>> _lazyIcons = new Lazy<Dictionary<string, FontAwesomeIcon>>(Parse);

        public string Prefix => _faProviderPrefix;
        private static Dictionary<string, FontAwesomeIcon> Icons => _lazyIcons.Value;

        /// <inheritdoc/>
        public string GetIconPath(string value)
        {
            if (!FontAwesomeIconKey.TryParse(value, out FontAwesomeIconKey key))
            {
                throw new KeyNotFoundException($"FontAwesome Icon \"{value}\" not found!");
            }
            else if (!Icons.TryGetValue(key.Value, out FontAwesomeIcon icon))
            {
                throw new KeyNotFoundException($"FontAwesome Icon \"{key.Value}\" not found!");
            }
            else if (!key.Style.HasValue)
            {
                return icon.Svg.Values.First().Path;
            }
            else if (icon.Svg.TryGetValue(key.Style.Value, out Svg svg))
            {
                return svg.Path;
            }

            throw new KeyNotFoundException($"FontAwesome Style \"{key.Style}\" not found!");
        }

        private static Dictionary<string, FontAwesomeIcon> Parse()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"{assembly.GetName().Name}.Assets.icons.json";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                var result = JsonSerializer.Deserialize<Dictionary<string, FontAwesomeIcon>>(
                    stream,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new JsonStringEnumConverter() }
                    });

                return result;
            }
        }
    }
}