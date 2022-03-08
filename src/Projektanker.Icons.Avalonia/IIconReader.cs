namespace Projektanker.Icons.Avalonia
{
    /// <summary>
    /// Represents an icon reader.
    /// </summary>
    public interface IIconReader
    {
        /// <summary>
        /// Gets the SVG path of the requested icon.
        /// </summary>
        /// <param name="value">The value specifying the icon to return it's path from.</param>
        /// <returns>The path of the icon.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// The icon associated with the specified <paramref name="value"/> does not exists.
        /// </exception>
        string GetIconPath(string value);
    }
}