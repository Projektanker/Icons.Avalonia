using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;

#nullable enable

namespace Projektanker.Icons.Avalonia
{
    public class IconImage : DrawingImage, IImage
    {
        public static readonly StyledProperty<string> ValueProperty = AvaloniaProperty.Register<
            IconImage,
            string
        >(nameof(Value), string.Empty);

        public static readonly StyledProperty<IBrush> BrushProperty = AvaloniaProperty.Register<
            IconImage,
            IBrush
        >(nameof(Brush), new SolidColorBrush(Colors.Black));

        public static readonly StyledProperty<IPen> PenProperty = AvaloniaProperty.Register<
            IconImage,
            IPen
        >(nameof(Pen), new ImmutablePen(Colors.Black.ToUInt32(), 0));

        private Rect _bounds;

        public IconImage()
            : this(string.Empty, new SolidColorBrush(Colors.Black)) { }

        public IconImage(string value, IBrush brush)
        {
            Value = value;
            Brush = brush;
        }

        public string Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public IBrush Brush
        {
            get => GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
        }

        public IPen Pen
        {
            get => GetValue(PenProperty);
            set => SetValue(PenProperty, value);
        }

        /// <inheritdoc>
        public new Size Size => _bounds.Size;

        /// <inheritdoc>
        Size IImage.Size => _bounds.Size;

        /// <inheritdoc/>
        void IImage.Draw(DrawingContext context, Rect sourceRect, Rect destRect)
        {
            var drawing = Drawing;
            if (drawing == null)
            {
                return;
            }

            var bounds = _bounds;
            var scale = Matrix.CreateScale(
                destRect.Width / sourceRect.Width,
                destRect.Height / sourceRect.Height
            );
            var translate = Matrix.CreateTranslation(
                -sourceRect.X + destRect.X - bounds.X,
                -sourceRect.Y + destRect.Y - bounds.Y
            );

            using (context.PushClip(destRect))
            using (context.PushTransform(translate * scale))
            {
                drawing.Draw(context);
            }
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == ValueProperty)
            {
                HandleValueChanged();
                RaiseInvalidated(EventArgs.Empty);
            }
            else if (change.Property == BrushProperty)
            {
                HandleBrushChanged();
                RaiseInvalidated(EventArgs.Empty);
            }
            else if (change.Property == PenProperty)
            {
                HandlePenChanged();
                RaiseInvalidated(EventArgs.Empty);
            }
        }

        private void HandleValueChanged()
        {
            var iconModel = IconProvider.Current.GetIcon(Value);

            _bounds = new Rect(
                x: iconModel.ViewBox.X,
                y: iconModel.ViewBox.Y,
                width: iconModel.ViewBox.Width,
                height: iconModel.ViewBox.Height
            );

            var drawing = GetGeometryDrawing();
            drawing.Geometry = StreamGeometry.Parse(iconModel.Path);
        }

        private void HandleBrushChanged()
        {
            var drawing = GetGeometryDrawing();
            drawing.Brush = Brush;
        }

        private void HandlePenChanged()
        {
            var drawing = GetGeometryDrawing();
            drawing.Pen = Pen;
        }

        private GeometryDrawing GetGeometryDrawing()
        {
            return (GeometryDrawing)(Drawing ??= new GeometryDrawing());
        }
    }
}
