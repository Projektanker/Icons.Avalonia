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
                .Select(e => e.Sender)
                .OfType<Icon>()
                .Subscribe(icon => icon.OnValueChanged());

            ForegroundProperty.Changed
                .Select(e => e.Sender)
                .OfType<Icon>()
                .Subscribe(icon => icon.OnForegroundChanged());
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

        private void OnValueChanged()
        {
            string path = IconProvider.GetIconPath(Value);

            Drawing = new GeometryDrawing()
            {
                Geometry = Geometry.Parse(path),
                Brush = Foreground,
            };
        }

        private void OnForegroundChanged()
        {
            if (Drawing is GeometryDrawing geometryDrawing)
            {
                Drawing = new GeometryDrawing
                {
                    Geometry = geometryDrawing.Geometry,
                    Brush = Foreground,
                };
            }
        }
    }
}