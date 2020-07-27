using System;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Projektanker.Icons.Avalonia
{
    public class Icon : TemplatedControl
    {
        public static readonly DirectProperty<Icon, Drawing> DrawingProperty =
            AvaloniaProperty.RegisterDirect<Icon, Drawing>(
                nameof(Drawing),
                o => o.Drawing);

        public static readonly StyledProperty<string> ValueProperty =
            AvaloniaProperty.Register<Icon, string>(nameof(Value));

        private Drawing _drawing;

        static Icon()
        {
            ValueProperty.Changed.Subscribe(OnValuePropertyChanged);
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

        private static void OnValuePropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            (e.Sender as Icon)?.OnValuePropertyChanged();
        }

        private void OnValuePropertyChanged()
        {
            string path = IconProvider.GetIconPath(Value);

            GeometryDrawing drawing = new GeometryDrawing()
            {
                Geometry = Geometry.Parse(path),
            };

            // Bind drawing foreground to icon foreground
            IObservable<IBrush> foregroundObservable = this.GetObservable(ForegroundProperty);
            drawing.Bind(GeometryDrawing.BrushProperty, foregroundObservable);

            Drawing = drawing;
        }
    }
}