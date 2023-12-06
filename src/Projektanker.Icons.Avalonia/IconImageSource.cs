using Avalonia;
using Avalonia.Media;
using System;

namespace Projektanker.Icons.Avalonia
{
    public class IconImageSource : DrawingImage
    {
        public static readonly StyledProperty<string> ValueProperty =
            AvaloniaProperty.Register<IconImageSource, string>(nameof(Value));

        public static readonly StyledProperty<IBrush?> BrushProperty =
            AvaloniaProperty.Register<IconImageSource, IBrush?>(nameof(Brush));

        public static readonly StyledProperty<IPen?> PenProperty =
            AvaloniaProperty.Register<IconImageSource, IPen?>(nameof(Pen));

        public string Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public IBrush? Brush
        {
            get => GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
        }

        public IPen? Pen
        {
            get => GetValue(PenProperty);
            set => SetValue(PenProperty, value);
        }

        private GeometryDrawing GetGeometryDrawing()
        {
            if (Drawing is GeometryDrawing geometryDrawing)
                return geometryDrawing;

            Drawing = geometryDrawing = new GeometryDrawing();
            return geometryDrawing;
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == ValueProperty)
            {
                HandleValueChanged();
            } else if (change.Property == BrushProperty || change.Property == PenProperty)
            {
                HandleBrushChanged();
            }
        }

        private void HandleBrushChanged()
        {
            var geometry = GetGeometryDrawing();
            geometry.Brush = Brush;
            geometry.Pen = Pen;
        }

        private void HandleValueChanged()
        {
            var iconModel = IconProvider.Current.GetIcon(Value);
            GetGeometryDrawing().Geometry = StreamGeometry.Parse(iconModel.Path);
        }
    }
} 