using System;
using Avalonia.Reactive;

namespace Projektanker.Icons.Avalonia
{
    public static class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext)
            => observable.Subscribe(new AnonymousObserver<T>(onNext));
    }
}
