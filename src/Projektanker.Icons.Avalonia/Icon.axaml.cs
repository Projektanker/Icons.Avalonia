using System;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Projektanker.Icons.Avalonia
{
    public class Icon : TemplatedControl
    {
        public static readonly DirectProperty<Icon, DrawingImage> DrawingImageProperty =
            AvaloniaProperty.RegisterDirect<Icon, DrawingImage>(nameof(DrawingImage), o => o.DrawingImage);

        public static readonly StyledProperty<string> ValueProperty =
            AvaloniaProperty.Register<Icon, string>(nameof(Value));

        private DrawingImage _drawingImage;

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

        public DrawingImage DrawingImage
        {
            get => _drawingImage;
            private set => SetAndRaise(DrawingImageProperty, ref _drawingImage, value);
        }

        public IBitmap BitmapImage
        {
            get
            {
                if (_drawingImage is null) return null;
                var renderBitmap = new RenderTargetBitmap(new PixelSize((int) _drawingImage.Size.Width, (int) _drawingImage.Size.Height));
                
                using (var ctx = renderBitmap.CreateDrawingContext(null))
                using (var dtx = new DrawingContext(ctx))
                {
                    _drawingImage.Drawing.Draw(dtx);
                }

                return renderBitmap;
            }
        }

        public string Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private void OnValueChanged()
        {
            var iconProvider = AvaloniaLocator.Current.GetService<IIconReader>();
            string path = iconProvider.GetIconPath(Value);
            var drawing = new GeometryDrawing()
            {
                Geometry = Geometry.Parse(path),
                Brush = Foreground ?? new SolidColorBrush(0),
            };

            DrawingImage = new DrawingImage { Drawing = drawing };
        }

        private void OnForegroundChanged()
        {
            if (DrawingImage?.Drawing is GeometryDrawing geometryDrawing)
            {
                DrawingImage.Drawing = new GeometryDrawing
                {
                    Geometry = geometryDrawing.Geometry,
                    Brush = Foreground,
                };
            }
        }
    }
}