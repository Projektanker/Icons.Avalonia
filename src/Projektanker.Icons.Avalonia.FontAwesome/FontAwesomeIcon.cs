using System.Collections.Generic;

namespace Projektanker.Icons.Avalonia.FontAwesome
{
    internal class FontAwesomeIcon
    {
        public List<Style> Styles { get; set; }

        public string Unicode { get; set; }

        public List<string> Free { get; set; }

        public Dictionary<Style, Svg> Svg { get; set; }
    }
}