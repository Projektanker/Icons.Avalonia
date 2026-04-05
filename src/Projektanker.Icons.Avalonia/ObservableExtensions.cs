using System;
using Avalonia.Reactive;

namespace Projektanker.Icons.Avalonia
{
    public static class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext)
            => observable.Subscribe(new AnonymousObserver<T>(onNext));

        /*
        private class Observer<T> : IObserver<T>
        {
            private readonly Action<T> _onNext;

            public Observer(Action<T> onNext)
            {
                _onNext = onNext ?? throw new ArgumentNullException(nameof(onNext));
            }

            public void OnNext(T value) => _onNext(value);
            public void OnError(Exception error) { }
            public void OnCompleted() { }
        }
        */
    }
}
