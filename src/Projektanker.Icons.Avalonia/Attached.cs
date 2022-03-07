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
        /// <value>Provide a icon identifier or binding.</value>
        public static readonly AttachedProperty<string> IconProperty =
            AvaloniaProperty.RegisterAttached<Icon, ContentControl, string>("Icon", string.Empty);

        static Attached()
        {
            IconProperty.Changed.Subscribe(IconChanged);
        }

        /// <summary>
        /// Accessor for attached property <see cref="IconProperty"/>
        /// </summary>
        /// <param name="target">Target host</param>
        /// <returns>The value of the <see cref="IconProperty"/></returns>
        public static string GetIcon(ContentControl target)
        {
            return target.GetValue(IconProperty);
        }

        /// <summary>
        /// Accessor for attached property <see cref="IconProperty"/>
        /// </summary>
        /// <param name="target">Target host</param>
        /// <param name="value">The value to be set.</param>
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