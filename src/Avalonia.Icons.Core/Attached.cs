using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;

namespace Avalonia.Icons
{
    public class Attached
    {
        /// <summary>
        /// Identifies the FontAwesome.Avalonia.Awesome.Content attached dependency property.
        /// </summary>
        public static readonly AttachedProperty<string> IconProperty =
            AvaloniaProperty.RegisterAttached<Icon, ContentControl, string>("Icon", string.Empty);

        static Attached()
        {

            IconProperty.Changed.Subscribe(IconChanged);
        }
        public static string GetIcon(ContentControl target)
        {
            return target.GetValue(IconProperty);
        }

        public static void SetIcon(ContentControl target, string value)
        {
            target.SetValue(IconProperty, value);
        }

        private static void IconChanged(AvaloniaPropertyChangedEventArgs evt)
        {
            if (!(evt.NewValue is string value))
            {
                return;
            }
            if (!(evt.Sender is ContentControl target))
            {
                return;
            }

            var fa = new Icon()
            {
                Value = value,
            };

            target.Content = fa;
        }
    }
}
