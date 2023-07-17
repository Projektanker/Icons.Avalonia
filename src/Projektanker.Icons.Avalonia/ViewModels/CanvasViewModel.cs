namespace Projektanker.Icons.Avalonia.ViewModels
{
    internal class CanvasViewModel
    {
        public CanvasViewModel(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }
}
