namespace Projektanker.Icons.Avalonia.Models
{
    public record ViewBoxModel(int X, int Y, int Width, int Height)
    {
        public static ViewBoxModel Parse(string viewBox)
        {
            var parts = viewBox.Split(' ');
            return new ViewBoxModel(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                int.Parse(parts[2]),
                int.Parse(parts[3]));
        }
    }
}
