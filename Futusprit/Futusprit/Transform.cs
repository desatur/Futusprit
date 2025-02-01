using System.Numerics;

namespace Futusprit
{
    public struct Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Quaternion Rotation { get; set; }

        // Default constructor
        public Transform()
        {
            Position = Vector2.Zero;
            Size = Vector2.One;
            Rotation = Quaternion.Identity;
        }

        public Transform(Vector2 position)
        {
            Position = position;
            Size = Vector2.One;
            Rotation = Quaternion.Identity;
        }

        public Transform(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
            Rotation = Quaternion.Identity;
        }

        public Transform(Vector2 position, Vector2 size, Quaternion rotation)
        {
            Position = position;
            Size = size;
            Rotation = rotation;
        }

        public void Translate(Vector2 offset)
        {
            Position += offset;
        }

        public void Scale(Vector2 factor)
        {
            Size *= factor;
        }

        public void Rotate(Quaternion deltaRotation)
        {
            Rotation *= deltaRotation;
        }
    }
}
