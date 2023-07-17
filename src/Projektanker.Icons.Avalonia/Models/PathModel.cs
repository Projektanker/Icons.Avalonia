namespace Projektanker.Icons.Avalonia.Models
{
    public readonly record struct PathModel
    {
        private readonly string _path;

        public PathModel(string path)
        {
            _path = path;
        }

        public static implicit operator string(PathModel path) => path._path;

        public override string ToString() => _path;
    }
}
