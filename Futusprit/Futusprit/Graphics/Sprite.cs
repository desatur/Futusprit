using System.Numerics;
using Raylib_cs;

namespace Futusprit.Graphics
{
    public class Sprite : IDisposable
    {
        public Texture2D Base
        {
            get
            {
                return _baseTexture;
            }
        }

        public static List<Sprite> Sprites
        {
            get
            {
                return _sprites;
            }
        }

        public static Sprite Default
        {
            get
            {
                return _default;
            }
            set
            {
                _default = value;
            }
        }

        public ushort ID
        {
            get
            {
                return _id;
            }
        }

        private static List<Sprite> _sprites = [];
        private static Sprite _default;
        private Texture2D _baseTexture;
        private static ushort _idCounter = 0;
        private ushort _id;

        public Sprite(string texturePath) // TODO: Replace with MemoryStream
        {
            _id = _idCounter++;
            if (_sprites.Count == 0) _default = this;
            _baseTexture = Raylib.LoadTexture(texturePath);
            _sprites.Add(this);
        }

        public void Dispose()
        {
            Raylib.UnloadTexture(_baseTexture);
            _sprites.Remove(this);

            if (_default == this)
            {
                _default = _sprites.Count > 0 ? _sprites[0] : null;
            }

            GC.SuppressFinalize(this);
        }

        public static Sprite FindByID(ushort id)
        {
            return _sprites.FirstOrDefault(sprite => sprite.ID == id);
        }

        public void Draw(Vector2 position, Color color)
        {
            Raylib.DrawTexture(_baseTexture, (int)position.X, (int)position.Y, color);
        }
    }
}
