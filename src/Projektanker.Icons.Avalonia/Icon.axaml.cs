using System;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Projektanker.Icons.Avalonia
{
    public class Icon : TemplatedControl
    {
        public static readonly DirectProperty<Icon, Geometry> PathDataProperty =
            AvaloniaProperty.RegisterDirect<Icon, Geometry>(nameof(PathData), o => o.PathData);

        public static readonly StyledProperty<string> ValueProperty =
            AvaloniaProperty.Register<Icon, string>(nameof(Value));

        public static readonly StyledProperty<IconAnimation> AnimationProperty =
            AvaloniaProperty.Register<Icon, IconAnimation>(nameof(Animation));

        private Geometry _pathData;

        static Icon()
        {
            ValueProperty.Changed.Subscribe(e => HandleValueChanged(e));
        }

        public Geometry PathData
        {
            get => _pathData;
            private set => SetAndRaise(PathDataProperty, ref _pathData, value);
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

        private static void HandleValueChanged(AvaloniaPropertyChangedEventArgs<string> args)
        {
            if (args.Sender is not Icon icon)
            {
                return;
            }

            var value = args.NewValue.GetValueOrDefault(string.Empty);
            var path = IconProvider.Current.GetIconPath(value);
            icon.PathData = StreamGeometry.Parse(path);
        }
    }
}
