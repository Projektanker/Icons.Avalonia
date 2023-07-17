using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Projektanker.Icons.Avalonia.ViewModels;

namespace Projektanker.Icons.Avalonia
{
    public class Icon : TemplatedControl
    {
        public static readonly StyledProperty<string> ValueProperty =
            AvaloniaProperty.Register<Icon, string>(nameof(Value));

        public static readonly StyledProperty<IconAnimation> AnimationProperty =
            AvaloniaProperty.Register<Icon, IconAnimation>(nameof(Animation));

        internal static readonly DirectProperty<Icon, CanvasViewModel> CanvasProperty =
                            AvaloniaProperty.RegisterDirect<Icon, CanvasViewModel>(nameof(Canvas), o => o.Canvas);

        internal static readonly DirectProperty<Icon, PathViewModel> PathProperty =
            AvaloniaProperty.RegisterDirect<Icon, PathViewModel>(nameof(Path), o => o.Path);

        private CanvasViewModel _canvas;
        private PathViewModel _path;

        static Icon()
        {
            AffectsRender<Icon>(PathProperty, CanvasProperty);
            ValueProperty.Changed.Subscribe(e => HandleValueChanged(e));
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

        internal CanvasViewModel Canvas
        {
            get => _canvas;
            private set => SetAndRaise(CanvasProperty, ref _canvas, value);
        }

        internal PathViewModel Path
        {
            get => _path;
            private set => SetAndRaise(PathProperty, ref _path, value);
        }

        private static void HandleValueChanged(AvaloniaPropertyChangedEventArgs<string> args)
        {
            if (args.Sender is not Icon icon)
            {
                return;
            }

            var value = args.NewValue.GetValueOrDefault(string.Empty);
            var iconModel = IconProvider.Current.GetIcon(value);

            icon.Canvas = new CanvasViewModel(
                width: iconModel.ViewBox.Width,
                height: iconModel.ViewBox.Height);

            icon.Path = new PathViewModel(
                left: iconModel.ViewBox.X * -1,
                right: iconModel.ViewBox.Y * -1,
                data: StreamGeometry.Parse(iconModel.Path));
        }
    }
}
