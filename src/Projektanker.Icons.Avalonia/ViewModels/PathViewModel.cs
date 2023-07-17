using Avalonia.Media;

namespace Projektanker.Icons.Avalonia.ViewModels
{
    internal class PathViewModel
    {
        public PathViewModel(int left, int right, StreamGeometry data)
        {
            Left = left;
            Right = right;
            Data = data;
        }

        public int Left { get; }
        public int Right { get; }
        public StreamGeometry Data { get; }
    }
}
