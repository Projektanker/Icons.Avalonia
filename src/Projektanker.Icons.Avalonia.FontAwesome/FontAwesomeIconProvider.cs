using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private const string _faKeyPrefix = "fa-";
        private const string _faProviderPrefix = "fa";
        private const string _resource = "Assets.icons.json";
        private static readonly Lazy<Dictionary<string, FontAwesomeIcon>> _lazyIcons = new Lazy<Dictionary<string, FontAwesomeIcon>>(Parse);

        public string Prefix => _faProviderPrefix;
        private static Dictionary<string, FontAwesomeIcon> Icons => _lazyIcons.Value;

        /// <inheritdoc/>
        public string GetIconPath(string value)
        {
            string[] splitted = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string key;
            Style? style;
            if (splitted.Length == 1)
            {
                key = splitted[0];
                style = null;
            }
            else if (splitted.Length == 2)
            {
                key = splitted[1];

                StylePrefix stylePrefix = Enum.Parse<StylePrefix>(splitted[0], ignoreCase: true);
                style = stylePrefix.ToStyle();
            }
            else
            {
                throw new KeyNotFoundException($"Icon \"{value}\" not found!");
            }

            // Remove "fa-" substring
            key = key.Substring(_faKeyPrefix.Length);

            if (!Icons.TryGetValue(key, out FontAwesomeIcon icon))
            {
                throw new KeyNotFoundException($"FontAwesome-Icon \"{key}\" not found!");
            }
            else if (!style.HasValue)
            {
                return icon.Svg.Values.First().Path;
            }
            else if (icon.Svg.TryGetValue(style.Value, out Svg svg))
            {
                return svg.Path;
            }
            else
            {
                throw new KeyNotFoundException($"FontAwesome-Style \"{style}\" not found!");
            }
        }

        private static Dictionary<string, FontAwesomeIcon> Parse()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{_resource}");
            using TextReader textReader = new StreamReader(stream);
            using JsonReader jsonReader = new JsonTextReader(textReader);
            JsonSerializer serializer = JsonSerializer.Create();
            var result = serializer.Deserialize<Dictionary<string, FontAwesomeIcon>>(jsonReader);
            return result;
        }
    }
}