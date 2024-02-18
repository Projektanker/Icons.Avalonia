using Avalonia;
using Avalonia.Controls.Primitives;

#nullable enable

namespace Projektanker.Icons.Avalonia
{
    public class Icon : TemplatedControl
    {
        public static readonly StyledProperty<string> ValueProperty =
            AvaloniaProperty.Register<Icon, string>(nameof(Value), string.Empty);

        public static readonly StyledProperty<IconAnimation> AnimationProperty =
            AvaloniaProperty.Register<Icon, IconAnimation>(nameof(Animation));

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
    }
}
