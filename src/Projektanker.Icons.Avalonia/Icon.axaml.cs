using System;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using Avalonia.Threading;

#nullable enable

namespace Projektanker.Icons.Avalonia
{
    public partial class Icon : TemplatedControl
    {
        private const double AnimationDegreesPerSecond = 180.0;
        private static readonly TimeSpan AnimationFrameInterval = TimeSpan.FromMilliseconds(1000.0 / 60.0);
        public static readonly StyledProperty<string> ValueProperty = AvaloniaProperty.Register<
            Icon,
            string
        >(nameof(Value), string.Empty);

        public static readonly StyledProperty<IconAnimation> AnimationProperty =
            AvaloniaProperty.Register<Icon, IconAnimation>(nameof(Animation));

        internal static readonly StyledProperty<IconImage> ImageProperty =
            AvaloniaProperty.Register<Icon, IconImage>(nameof(Image));

        private static readonly SolidColorBrush _fallbackForeground = new(Colors.Black);
        private readonly RotateTransform _rotateTransform = new();
        private readonly DispatcherTimer _animationTimer;
        private DateTime _animationStartedAt;

        public Icon()
        {
            Image = new();
            RenderTransform = _rotateTransform;
            RenderTransformOrigin = RelativePoint.Center;
            Template = new FuncControlTemplate<Icon>((control, _) =>
            {
                var image = new Image();
                image.Bind(
                    global::Avalonia.Controls.Image.SourceProperty,
                    control.GetObservable(ImageProperty)
                );
                image.Bind(WidthProperty, control.GetObservable(FontSizeProperty));
                image.Bind(HeightProperty, control.GetObservable(FontSizeProperty));
                return image;
            });

            _animationTimer = new DispatcherTimer(AnimationFrameInterval, DispatcherPriority.Render, Dispatcher);
            _animationTimer.Tick += HandleAnimationTick;
        }

        public string Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public IconAnimation Animation
        {
            get => GetValue(AnimationProperty);
            set => SetValue(AnimationProperty, value);
        }

        internal IconImage Image
        {
            get => GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == ValueProperty || change.Property == ForegroundProperty)
            {
                // Create new IconImage to prevent https://github.com/Projektanker/Icons.Avalonia/issues/138
                // Otherwise the Image.Draw method is not invoked
                Image = new IconImage(Value, Foreground ?? _fallbackForeground);
            }
            else if (change.Property == AnimationProperty)
            {
                UpdateAnimation();
            }
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            _animationTimer.Stop();
            base.OnDetachedFromVisualTree(e);
        }

        private void UpdateAnimation()
        {
            _rotateTransform.Angle = 0;

            if (Animation == IconAnimation.None)
            {
                _animationTimer.Stop();
                return;
            }

            _animationStartedAt = DateTime.UtcNow;
            _animationTimer.Start();
        }

        private void HandleAnimationTick(object? sender, EventArgs e)
        {
            var elapsed = DateTime.UtcNow - _animationStartedAt;

            _rotateTransform.Angle = Animation switch
            {
                IconAnimation.Spin => GetRotationAngle(elapsed),
                IconAnimation.Pulse => GetRotationAngle(elapsed),
                _ => 0.0,
            };
        }

        private static double GetRotationAngle(TimeSpan elapsed)
            => (elapsed.TotalSeconds * AnimationDegreesPerSecond) % 360.0;
    }
}
