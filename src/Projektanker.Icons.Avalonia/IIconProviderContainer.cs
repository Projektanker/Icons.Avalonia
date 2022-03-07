using System;

namespace Projektanker.Icons.Avalonia
{
    public interface IIconProviderContainer
    {
        /// <summary>
        /// Registers an <see cref="IIconProvider"/> with the <see cref="IIconProviderContainer"/>.
        /// </summary>
        /// <param name="iconProvider">The <see cref="IIconProvider"/> to register.</param>
        /// <returns>A reference to this instance after the registration has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="iconProvider"/> is null.</exception>
        IIconProviderContainer Register(IIconProvider iconProvider);

        /// <summary>
        /// Registers an <see cref="IIconProvider"/> with the <see cref="IIconProviderContainer"/>.
        /// </summary>
        /// <typeparam name="TIconProvider">
        /// The type of the <see cref="IIconProvider"/> to register.
        /// </typeparam>
        /// <returns>A reference to this instance after the registration has completed.</returns>
        /// <exception cref="ArgumentException">
        /// An <see cref="IIconProvider"/> with an conflicting prefix is already registered.
        /// </exception>
        IIconProviderContainer Register<TIconProvider>() where TIconProvider : IIconProvider, new();
    }
}