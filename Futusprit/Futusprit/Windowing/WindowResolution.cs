using System.Numerics;

namespace Futusprit.Windowing
{
    public struct WindowResolution
    {
        public int Width { get; }
        public int Height { get; }

        public float AspectRatio
        {
            get
            {
                return Width / Height;
            }
        }

        public WindowResolution(int x, int y)
        {
            Width = x;
            Height = y;
        }

        public WindowResolution(Vector2 vec) : this((int)vec.X, (int)vec.Y) { }
    }
}
