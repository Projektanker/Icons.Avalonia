using System.Collections.ObjectModel;

namespace Avalonia.Icons
{
    internal class IconProviderCollection : KeyedCollection<string, IIconProvider>
    {
        protected override string GetKeyForItem(IIconProvider item)
        {
            return item.Prefix;
        }
    }
}