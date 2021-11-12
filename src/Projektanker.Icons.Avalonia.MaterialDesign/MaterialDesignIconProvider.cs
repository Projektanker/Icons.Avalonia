using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Projektanker.Icons.Avalonia.MaterialDesign
{
    /// <summary>
    /// Implements the <see cref="IIconProvider"/> interface to provide Material Design icons.
    /// </summary>
    public class MaterialDesignIconProvider : IIconProvider
    {
        private const string _mdiProviderPrefix = "mdi";

        private static readonly string _resourceNameTemplate
            = $"{typeof(MaterialDesignIconProvider).Assembly.GetName().Name}.Assets.{{0}}.svg";

        private static readonly Regex _pathRegex = new Regex("<path d=\"(.+)\"");
        private readonly Dictionary<string, string> _icons = new Dictionary<string, string>();

        public string Prefix => _mdiProviderPrefix;

        /// <inheritdoc/>
        public string GetIconPath(string value)
        {
            if (_icons.TryGetValue(value, out var icon))
            {
                return icon;
            }

            icon = GetIconFromResource(value);
            return _icons[value] = icon;
        }

        private static string GetIconFromResource(string value)
        {
            using (Stream stream = GetIconResourceStream(value))
            using (TextReader textReader = new StreamReader(stream))
            {
                var svg = textReader.ReadToEnd();
                var pathMatch = _pathRegex.Match(svg);
                var path = pathMatch.Groups[1].Value;
                return path;
            }
        }

        private static Stream GetIconResourceStream(string value)
        {
            return TryGetIconResourceStream(value, out var stream)
                ? stream
                : throw new KeyNotFoundException($"Material Design Icon \"{value}\" not found!");
        }

        private static bool TryGetIconResourceStream(string value, out Stream stream)
        {
            stream = default;

            if (value.Length <= _mdiProviderPrefix.Length + 1)
            {
                return false;
            }

            var withoutPrefix = value.Substring(_mdiProviderPrefix.Length + 1);
            var resourceName = string.Format(_resourceNameTemplate, withoutPrefix);
            stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(resourceName);

            return stream != default;
        }
    }
}