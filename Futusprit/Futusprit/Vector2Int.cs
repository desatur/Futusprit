namespace Futusprit
{
    public struct Vector2Int
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Operators
        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new Vector2Int(a.X + b.X, a.Y + b.Y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new Vector2Int(a.X - b.X, a.Y - b.Y);
        public static Vector2Int operator *(Vector2Int a, int scalar) => new Vector2Int(a.X * scalar, a.Y * scalar);
        public static Vector2Int operator /(Vector2Int a, int scalar) => new Vector2Int(a.X / scalar, a.Y / scalar);

        // Equality overrides
        public override bool Equals(object? obj) => obj is Vector2Int other && X == other.X && Y == other.Y;
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public static bool operator ==(Vector2Int a, Vector2Int b) => a.Equals(b);
        public static bool operator !=(Vector2Int a, Vector2Int b) => !a.Equals(b);

        // Magnitude and distance calculations
        public double Magnitude() => Math.Sqrt(X * X + Y * Y);
        public static double Distance(Vector2Int a, Vector2Int b) => (a - b).Magnitude();

        // Conversion to and from System.Numerics.Vector2
        public System.Numerics.Vector2 ToVector2() => new System.Numerics.Vector2(X, Y);
        public static Vector2Int FromVector2(System.Numerics.Vector2 vector) => new Vector2Int((int)vector.X, (int)vector.Y);

        public override string ToString() => $"({X}, {Y})";
    }
}
