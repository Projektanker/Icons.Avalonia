using System.IO;
using System.Reflection;

namespace Projektanker.Icons.Avalonia.FontAwesome;

public sealed class FontAwesomeFreeUtf8JsonStreamProvider : IFontAwesomeUtf8JsonStreamProvider
{
    public Stream GetUtf8JsonStream()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"{assembly.GetName().Name}.Assets.icons.json";
        return assembly.GetManifestResourceStream(resourceName);
    }
}
