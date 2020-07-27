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
        /// <returns>The path of the icon.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The icon associated
        /// with the specified <paramref name="value"/> does not exists.</exception>
        string GetIconPath(string value);
    }
}