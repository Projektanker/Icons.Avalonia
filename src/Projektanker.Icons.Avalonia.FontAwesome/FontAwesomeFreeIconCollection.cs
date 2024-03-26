using System.IO;
using System.Reflection;

namespace Projektanker.Icons.Avalonia.FontAwesome;

public sealed class FontAwesomeFreeIconCollection : FontAwesomeIconCollection
{
    protected override Stream GetIconsStream()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"{assembly.GetName().Name}.Assets.icons.json";
        return assembly.GetManifestResourceStream(resourceName);
    }
}
