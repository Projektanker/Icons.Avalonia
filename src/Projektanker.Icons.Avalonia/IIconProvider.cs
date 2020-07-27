namespace Projektanker.Icons.Avalonia
{
    /// <summary>
    /// Represents an icon provider.
    /// </summary>
    public interface IIconProvider
    {
        /// <summary>
        /// Gets the prefix of the <see cref="IIconProvider"/>.
        /// </summary>
        string Prefix { get; }

        /// <summary>
        /// Gets the SVG path of the requested icon using the registered icon providers.
        /// </summary>
        /// <param name="value">The value specifying the icon to return it's path from.</param>
        /// <returns><c>true</c> if the <see cref="IIconProvider"/> contains an icon with the specified value; otherwise, <c>false</c>.</returns>
        bool TryGetIconPath(string value, out string path);
    }
}