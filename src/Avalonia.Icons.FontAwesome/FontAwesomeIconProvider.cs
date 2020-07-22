using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Avalonia.Icons.FontAwesome
{
    public class FontAwesomeIconProvider : IIconProvider
    {
        private const string _faKeyPrefix = "fa-";
        private const string _faProviderPrefix = "fa";
        private const string _resource = "Avalonia.Icons.FontAwesome.Assets.icons.json";
        private static readonly Lazy<Dictionary<string, FontAwesomeIcon>> _lazyIcons = new Lazy<Dictionary<string, FontAwesomeIcon>>(Parse);

        public string Prefix => _faProviderPrefix;
        private static Dictionary<string, FontAwesomeIcon> Icons => _lazyIcons.Value;
        public static void Register()
        {
            IconProvider.Register(new FontAwesomeIconProvider());
        }
        public bool TryGetIconPath(string value, out string path)
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
                path = null;
                return false;
            }

            // Remove "fa-" substring
            key = key.Substring(_faKeyPrefix.Length);

            if (!Icons.TryGetValue(key, out FontAwesomeIcon icon))
            {
                throw new KeyNotFoundException($"Icon \"{key}\" not found!");
            }
            else if (!style.HasValue)
            {
                path = icon.Svg.Values.First().Path;
                return true;
            }
            else if (icon.Svg.TryGetValue(style.Value, out Svg svg))
            {
                path = svg.Path;
                return true;
            }
            else
            {
                throw new KeyNotFoundException($"Style \"{style}\" not found!");
            }
        }

        private static Dictionary<string, FontAwesomeIcon> Parse()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(_resource))
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