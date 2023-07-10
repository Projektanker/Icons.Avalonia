using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Projektanker.Icons.Avalonia
{
    public class Icon : TemplatedControl
    {
        public static readonly DirectProperty<Icon, Geometry> SvgGeometryProperty =
            AvaloniaProperty.RegisterDirect<Icon, Geometry>(nameof(SvgGeometry), o => o.SvgGeometry);

        public static readonly StyledProperty<string> ValueProperty =
            AvaloniaProperty.Register<Icon, string>(nameof(Value));

        public static readonly StyledProperty<IconAnimation> AnimationProperty =
            AvaloniaProperty.Register<Icon, IconAnimation>(nameof(Animation));

        private Geometry _svgGeometry;

        public Geometry SvgGeometry
        {
            get => _svgGeometry;
            private set => SetAndRaise(SvgGeometryProperty, ref _svgGeometry, value);
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

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (ReferenceEquals(e.Property, ValueProperty))
            {
                SvgGeometry = Geometry.Parse(IconProvider.Current.GetIconPath(Value));
            }
        }
    }
}
