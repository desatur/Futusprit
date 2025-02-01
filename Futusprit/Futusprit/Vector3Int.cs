namespace Futusprit
{
    public struct Vector3Int
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3Int(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Operators
        public static Vector3Int operator +(Vector3Int a, Vector3Int b) => new Vector3Int(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vector3Int operator -(Vector3Int a, Vector3Int b) => new Vector3Int(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Vector3Int operator *(Vector3Int a, int scalar) => new Vector3Int(a.X * scalar, a.Y * scalar, a.Z * scalar);
        public static Vector3Int operator /(Vector3Int a, int scalar) => new Vector3Int(a.X / scalar, a.Y / scalar, a.Z / scalar);

        // Equality overrides
        public override bool Equals(object? obj) => obj is Vector3Int other && X == other.X && Y == other.Y && Z == other.Z;
        public override int GetHashCode() => HashCode.Combine(X, Y, Z);
        public static bool operator ==(Vector3Int a, Vector3Int b) => a.Equals(b);
        public static bool operator !=(Vector3Int a, Vector3Int b) => !a.Equals(b);

        // Magnitude and distance calculations
        public double Magnitude() => Math.Sqrt(X * X + Y * Y + Z * Z);
        public static double Distance(Vector3Int a, Vector3Int b) => (a - b).Magnitude();

        // Conversion to and from System.Numerics.Vector3
        public System.Numerics.Vector3 ToVector3() => new System.Numerics.Vector3(X, Y, Z);
        public static Vector3Int FromVector3(System.Numerics.Vector3 vector) => new Vector3Int((int)vector.X, (int)vector.Y, (int)vector.Z);

        public override string ToString() => $"({X}, {Y}, {Z})";
    }
}