using System;

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
            switch (value.ToUpperInvariant())
            {
                case "FA-SOLID":
                case "FAS":
                    return FontAwesome.Style.Solid;

                case "FA-REGULAR":
                case "FAR":
                    return FontAwesome.Style.Regular;

                case "FA-BRANDS":
                case "FAB":
                    return FontAwesome.Style.Brands;

                default:
                    return null;
            }
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