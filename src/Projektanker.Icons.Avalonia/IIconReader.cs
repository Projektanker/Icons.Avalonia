using Projektanker.Icons.Avalonia.Models;

namespace Projektanker.Icons.Avalonia
{
    /// <summary>
    /// Represents an icon reader.
    /// </summary>
    public interface IIconReader
    {
        /// <summary>
        /// Gets the model of the requested icon.
        /// </summary>
        /// <param name="value">The value specifying the icon to return it's model from.</param>
        /// <returns>The model of the icon.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// The icon associated with the specified <paramref name="value"/> does not exists.
        /// </exception>
        IconModel GetIcon(string value);
    }
}