namespace Projektanker.Icons.Avalonia
{
    /// <summary>
    /// Represents an icon provider.
    /// </summary>
    public interface IIconProvider : IIconReader
    {
        /// <summary>
        /// Gets the prefix of the <see cref="IIconProvider"/>.
        /// </summary>
        string Prefix { get; }
    }
}