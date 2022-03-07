using System;
using Avalonia;
using Avalonia.Controls;

namespace Projektanker.Icons.Avalonia
{
    public static class Attached
    {
        /// <summary>
        /// Identifies the FontAwesome.Avalonia.Awesome.Content attached dependency property.
        /// </summary>
        public static readonly AttachedProperty<string> ContentControlIconProperty =
            AvaloniaProperty.RegisterAttached<Icon, ContentControl, string>("Icon", string.Empty);

        /// <summary>
        /// Identifies the FontAwesome.Avalonia.Awesome.Content attached dependency property.
        /// </summary>
        public static readonly AttachedProperty<string> MenuItemIconProperty =
            AvaloniaProperty.RegisterAttached<Icon, MenuItem, string>("Icon", string.Empty);

        /// <summary>
        /// Identifies the FontAwesome.Avalonia.Awesome.Content attached dependency property.
        /// </summary>
        public static readonly AttachedProperty<string> NativeMenuItemIconProperty =
            AvaloniaProperty.RegisterAttached<Icon, NativeMenuItem, string>("Icon", string.Empty);


        static Attached()
        {
            ContentControlIconProperty.Changed.Subscribe(IconChanged);
            MenuItemIconProperty.Changed.Subscribe(IconChanged);
            NativeMenuItemIconProperty.Changed.Subscribe(IconChanged);
        }

        public static string GetIcon(ContentControl target)
        {
            return target.GetValue(ContentControlIconProperty);
        }

        public static void SetIcon(ContentControl target, string value)
        {
            target.SetValue(ContentControlIconProperty, value);
        }

        public static string GetIcon(MenuItem target)
        {
            return target.GetValue(MenuItemIconProperty);
        }

        public static void SetIcon(MenuItem target, string value)
        {
            target.SetValue(MenuItemIconProperty, value);
        }

        public static string GetIcon(NativeMenuItem target)
        {
            return target.GetValue(NativeMenuItemIconProperty);
        }

        public static void SetIcon(NativeMenuItem target, string value)
        {
            target.SetValue(NativeMenuItemIconProperty, value);
        }

        private static void IconChanged(AvaloniaPropertyChangedEventArgs evt)
        {
            if (evt.NewValue is not string value)
            {
                return;
            }

            switch (evt.Sender)
            {
                case ContentControl target:
                    target.Content = MakeIcon(value);
                    break;
                case MenuItem target:
                    target.Icon = MakeIcon(value);
                    break;
                case NativeMenuItem target:
                    target.Icon = MakeIcon(value).BitmapImage;
                    break;
            }
        }

        private static Icon MakeIcon(string value)
        {
            return new Icon
            {
                Value = value,
            };
        }
    }
}