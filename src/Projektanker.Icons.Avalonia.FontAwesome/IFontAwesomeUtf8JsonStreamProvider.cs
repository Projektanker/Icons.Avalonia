using System.IO;

namespace Projektanker.Icons.Avalonia.FontAwesome;

/// <summary>
/// Provides an UTF-8 encoded JSON stream to deserialize the icon collection from.
/// </summary>
public interface IFontAwesomeUtf8JsonStreamProvider
{
    Stream GetUtf8JsonStream();
}
