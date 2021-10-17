using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Projektanker.Icons.Avalonia.MaterialDesign
{
    /// <summary>
    /// Implements the <see cref="IIconProvider"/> interface to provide Material Design icons.
    /// </summary>
    public class MaterialDesignIconProvider : IIconProvider
    {
        private const string _mdiProviderPrefix = "mdi";
        private static readonly Lazy<Dictionary<string, string>> _lazyIcons = new Lazy<Dictionary<string, string>>(Parse);

        public string Prefix => _mdiProviderPrefix;
        private static Dictionary<string, string> Icons => _lazyIcons.Value;

        /// <inheritdoc/>
        public string GetIconPath(string value)
        {
            if (!Icons.TryGetValue(value, out var icon))
            {
                throw new KeyNotFoundException($"Material Design Icon \"{value}\" not found!");
            }

            return icon;
        }

        private static Dictionary<string, string> Parse()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();

            var icons = resourceNames
                .Where(name => name.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
                .Select(name => IconFromResource(assembly, name))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            return icons;
        }

        private static KeyValuePair<string, string> IconFromResource(Assembly assembly, string resourceName)
        {
            var key = IconKeyFromResourceName(resourceName);
            var path = IconPathFromResource(assembly, resourceName);
            return new KeyValuePair<string, string>(key, path);
        }

        private static string IconKeyFromResourceName(string resourceName)
        {
            var parts = resourceName.Split('.');
            return $"{_mdiProviderPrefix}-{parts[parts.Length - 2]}";
        }

        private static string IconPathFromResource(Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (TextReader textReader = new StreamReader(stream))
            {
                var svg = textReader.ReadToEnd();
                var pathMatch = Regex.Match(svg, "<path d=\"(.+)\"");
                var path = pathMatch.Groups[1].Value;
                return path;
            }
        }
    }
}