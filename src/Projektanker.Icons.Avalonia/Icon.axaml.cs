using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

#nullable enable

namespace Projektanker.Icons.Avalonia
{
    public class Icon : TemplatedControl
    {
        private static readonly SolidColorBrush _fallbackForeground = new(Colors.Black);

        public static readonly StyledProperty<string> ValueProperty = AvaloniaProperty.Register<
            Icon,
            string
        >(nameof(Value), string.Empty);

        public static readonly StyledProperty<IconAnimation> AnimationProperty =
            AvaloniaProperty.Register<Icon, IconAnimation>(nameof(Animation));

        internal static readonly DirectProperty<Icon, IconImage> ImageProperty =
            AvaloniaProperty.RegisterDirect<Icon, IconImage>(nameof(Image), o => o.Image);

        public Icon()
        {
            Image = new();
            AvaloniaXamlLoader.Load(this);
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

        internal IconImage Image { get; }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == ValueProperty)
            {
                Image.Value = Value;
            }
            else if (change.Property == ForegroundProperty)
            {
                Image.Brush = Foreground ?? _fallbackForeground;
            }
        }
    }
}
