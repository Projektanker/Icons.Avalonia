using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Projektanker.Icons.Avalonia.FontAwesome.Models;
using Projektanker.Icons.Avalonia.Models;

namespace Projektanker.Icons.Avalonia.FontAwesome;

/// <summary>
/// Implements the <see cref="IIconProvider"/> interface to provide font-awesome icons.
/// </summary>
public class FontAwesomeIconProvider : IIconProvider
{
    private const string _faProviderPrefix = "fa";
    private readonly IFontAwesomeUtf8JsonStreamProvider _streamProvider;
    private readonly Lazy<Dictionary<string, FontAwesomeIcon>> _lazyIcons;

    /// <summary>
    /// Initializes a new instance of the <see cref="FontAwesomeIconProvider"/> using the <see cref="FontAwesomeFreeUtf8JsonStreamProvider"/>
    /// to get the UTF-8 encoded json stream to deserialize the icon collection.
    /// </summary>
    public FontAwesomeIconProvider()
        : this(new FontAwesomeFreeUtf8JsonStreamProvider()) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="FontAwesomeIconProvider"/> using the specified <see cref="IFontAwesomeUtf8JsonStreamProvider"/>
    /// to get the UTF-8 encoded json stream to deserialize the icon collection.
    /// </summary>
    public FontAwesomeIconProvider(IFontAwesomeUtf8JsonStreamProvider streamProvider)
    {
        _streamProvider = streamProvider;
        _lazyIcons = new(Parse);
    }

    /// <inheritdoc/>
    public string Prefix => _faProviderPrefix;
    private Dictionary<string, FontAwesomeIcon> Icons => _lazyIcons.Value;

    /// <inheritdoc/>
    public IconModel GetIcon(string value)
    {
        if (!FontAwesomeIconKey.TryParse(value, out FontAwesomeIconKey key))
        {
            throw new KeyNotFoundException($"FontAwesome icon \"{value}\" not found!");
        }
        else if (!Icons.TryGetValue(key.Value, out FontAwesomeIcon icon))
        {
            throw new KeyNotFoundException($"FontAwesome icon \"{key.Value}\" not found!");
        }
        else if (!key.Style.HasValue)
        {
            return icon.Svg.Values.First().ToIconModel();
        }
        else if (icon.Svg.TryGetValue(key.Style.Value, out Svg svg))
        {
            return svg.ToIconModel();
        }

        throw new KeyNotFoundException(
            $"FontAwesome style \"{key.Style}\" not found for icon \"{key.Value}\". Maybe you are trying to use an unsupported pro icon."
        );
    }

    private Dictionary<string, FontAwesomeIcon> Parse()
    {
        using var stream = _streamProvider.GetUtf8JsonStream();

        var result = JsonSerializer.Deserialize(
            stream,
            FontAwesomeIconsJsonContext.Default.DictionaryStringFontAwesomeIcon
        );

        return result;
    }
}
