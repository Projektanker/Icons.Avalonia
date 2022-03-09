using System;
using Avalonia;
using AvaloniaMenuItem = Avalonia.Controls.MenuItem;

namespace Projektanker.Icons.Avalonia
{
    public static class MenuItem
    {
        /// <summary>
        /// Identifies the <seealso cref="IconProperty"/> avalonia attached property.
        /// </summary>
        public static readonly AttachedProperty<string> IconProperty =
            AvaloniaProperty.RegisterAttached<Icon, AvaloniaMenuItem, string>("Icon", string.Empty);

        static MenuItem()
        {
            IconProperty.Changed.Subscribe(IconChanged);
        }

        /// <summary>
        /// Accessor for attached property <see cref="IconProperty"/>
        /// </summary>
        public static string GetIcon(AvaloniaMenuItem target)
        {
            return target.GetValue(IconProperty);
        }

        /// <summary>
        /// Accessor for attached property <see cref="IconProperty"/>
        /// </summary>
        public static void SetIcon(AvaloniaMenuItem target, string value)
        {
            target.SetValue(IconProperty, value);
        }

        private static void IconChanged(AvaloniaPropertyChangedEventArgs evt)
        {
            if (evt.NewValue is not string value || evt.Sender is not AvaloniaMenuItem target)
            {
                return;
            }

            target.Icon = new Icon()
            {
                Value = value,
            };
        }
    }
}