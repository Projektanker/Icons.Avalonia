using System;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;

namespace Projektanker.Icons.Avalonia
{
    public class Icon : TemplatedControl
    {
        public static readonly DirectProperty<Icon, Drawing> DrawingProperty =
            AvaloniaProperty.RegisterDirect<Icon, Drawing>(nameof(Drawing), o => o.Drawing);

        public static readonly StyledProperty<string> ValueProperty =
            AvaloniaProperty.Register<Icon, string>(nameof(Value));

        private Drawing _drawing;

        static Icon()
        {
            ValueProperty.Changed
                .Where(e => e.Sender is Icon)
                .Select(e => new IconPropertyChangedEventArgs<string>(e))
                .Subscribe(SetDrawing);

            ForegroundProperty.Changed
                .Where(e => e.Sender is Icon)
                .Select(e => new IconPropertyChangedEventArgs<IBrush>(e))
                .Subscribe(SetDrawing);
        }

        public Drawing Drawing
        {
            get => _drawing;
            private set => SetAndRaise(DrawingProperty, ref _drawing, value);
        }

        public string Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void SetDrawing(IconPropertyChangedEventArgs<string> eventArgs)
        {
            var foreground = new BindingValue<IBrush>(eventArgs.Sender.Foreground);
            eventArgs.Sender.Drawing = GetDrawing(eventArgs.NewValue, foreground);
        }

        private static void SetDrawing(IconPropertyChangedEventArgs<IBrush> eventArgs)
        {
            var value = new BindingValue<string>(eventArgs.Sender.Value);
            eventArgs.Sender.Drawing = GetDrawing(value, eventArgs.NewValue);
        }

        private static Drawing GetDrawing(BindingValue<string> valueBindingValue, BindingValue<IBrush> brushBindingValue)
        {
            var value = valueBindingValue.GetValueOrDefault(string.Empty);
            var brush = brushBindingValue.GetValueOrDefault();

            string path = IconProvider.GetIconPath(value);

            return new GeometryDrawing()
            {
                Geometry = Geometry.Parse(path),
                Brush = brush,
            };
        }

        private class IconPropertyChangedEventArgs<T>
        {
            public IconPropertyChangedEventArgs(AvaloniaPropertyChangedEventArgs<T> eventArgs)
            {
                Sender = (Icon)eventArgs.Sender;
                NewValue = eventArgs.NewValue;
            }

            public Icon Sender { get; set; }
            public BindingValue<T> NewValue { get; set; }
        }
    }
}