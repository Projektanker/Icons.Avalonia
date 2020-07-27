using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.Icons
{
    public interface IIconProvider
    {
        string Prefix { get; }

        bool TryGetIconPath(string value, out string path);
    }
}
