using System;
using Avalonia;
using Avalonia.Controls;

namespace Projektanker.Icons.Avalonia
{
    public static class Attached
    {
        /// <summary>
        /// Identifies the <seealso cref="IconProperty"/> avalonia attached property.
        /// </summary>
        public static readonly AttachedProperty<string> IconProperty =
            AvaloniaProperty.RegisterAttached<Icon, ContentControl, string>("Icon", string.Empty);

        static Attached()
        {
            IconProperty.Changed.Subscribe(IconChanged);
        }

        /// <summary>
        /// Accessor for attached property <see cref="IconProperty"/>
        /// </summary>
        public static string GetIcon(ContentControl target)
        {
            return target.GetValue(IconProperty);
        }

        /// <summary>
        /// Accessor for attached property <see cref="IconProperty"/>
        /// </summary>
        public static void SetIcon(ContentControl target, string value)
        {
            target.SetValue(IconProperty, value);
        }

        private static void IconChanged(AvaloniaPropertyChangedEventArgs evt)
        {
            if (evt.NewValue is not string value)
            {
                return;
            }

            if (evt.Sender is not ContentControl target)
            {
                return;
            }

            target.Content = new Icon()
            {
                Value = value,
            };
        }
    }
}