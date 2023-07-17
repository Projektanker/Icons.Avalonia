using System;
using Projektanker.Icons.Avalonia.FontAwesome.Models;

namespace Projektanker.Icons.Avalonia.FontAwesome
{
    internal partial class FontAwesomeIconKey
    {
        private const string _faKeyPrefix = "fa-";
        public string Value { get; set; }
        public Style? Style { get; set; }

        public static bool TryParse(string value, out FontAwesomeIconKey key)
        {
            var parts = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                key = new FontAwesomeIconKey
                {
                    Value = GetValue(parts[0]),
                };
                return true;
            }

            if (parts.Length == 2)
            {
                key = new FontAwesomeIconKey
                {
                    Style = GetStyle(parts[0]),
                    Value = GetValue(parts[1]),
                };

                return true;
            }

            key = null;
            return false;
        }

        private static Style? GetStyle(string value)
        {
            return value.ToUpperInvariant() switch
            {
                "FA-SOLID" or "FAS" => (Style?)Models.Style.Solid,
                "FA-REGULAR" or "FAR" => (Style?)Models.Style.Regular,
                "FA-BRANDS" or "FAB" => (Style?)Models.Style.Brands,
                _ => null,
            };
        }

        private static string GetValue(string input)
        {
            var value = input.Length <= _faKeyPrefix.Length
                ? string.Empty
                : input.Substring(_faKeyPrefix.Length);

            return SupportLegacy(value);
        }
    }
}