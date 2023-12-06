using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

#nullable enable

namespace Projektanker.Icons.Avalonia
{
    public class Icon : TemplatedControl
    {
        public static readonly StyledProperty<string> ValueProperty =
            AvaloniaProperty.Register<Icon, string>(nameof(Value));

        public static readonly StyledProperty<IconAnimation> AnimationProperty =
            AvaloniaProperty.Register<Icon, IconAnimation>(nameof(Animation));

        internal static readonly DirectProperty<Icon, IImage?> ImageProperty =
            AvaloniaProperty.RegisterDirect<Icon, IImage?>(nameof(Image), o => o.Image);

        private IImage? _image;

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

        internal IImage? Image
        {
            get => _image;
            private set => SetAndRaise(ImageProperty, ref _image, value);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == ValueProperty || change.Property == ForegroundProperty)
            {
                UpdateIconImage();
            }
        }

        private void UpdateIconImage()
        {
            Image = new IconImage(Value, Foreground);
        }
    }
}
