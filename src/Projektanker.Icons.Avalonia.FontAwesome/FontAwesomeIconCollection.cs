using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Projektanker.Icons.Avalonia.FontAwesome.Models;

namespace Projektanker.Icons.Avalonia.FontAwesome;

public abstract class FontAwesomeIconCollection
{
    internal Dictionary<string, FontAwesomeIcon> Icons { get; }

    protected FontAwesomeIconCollection() =>
        Icons = Parse();

    private Dictionary<string, FontAwesomeIcon> Parse()
    {
        using var stream = GetIconsStream();

        var result = JsonSerializer.Deserialize(
            stream,
            FontAwesomeIconsJsonContext.Default.DictionaryStringFontAwesomeIcon);

        return result;
    }

    protected abstract Stream GetIconsStream();
}
