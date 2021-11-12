using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

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
            var key = FontAwesomeIconKey.TryParse(value, out var temp)
                ? temp
                : throw new KeyNotFoundException($"FontAwesome Icon \"{value}\" not found!");

            if (!Icons.TryGetValue(key.Value, out FontAwesomeIcon icon))
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
            else
            {
                throw new KeyNotFoundException($"FontAwesome Style \"{key.Style}\" not found!");
            }
        }

        private static Dictionary<string, FontAwesomeIcon> Parse()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"{assembly.GetName().Name}.Assets.icons.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (TextReader textReader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(textReader))
            {
                JsonSerializer serializer = JsonSerializer.Create();
                var result = serializer.Deserialize<Dictionary<string, FontAwesomeIcon>>(jsonReader);
                return result;
            }
        }
    }
}