using System.Numerics;
using System.Runtime.InteropServices;
using Futusprit.Extensions;
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

        public Vector2 Size
        {
            get
            {
                return new(_height, _width);
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
        private int _width;
        private int _height;

        public enum TextureFileType : byte
        {
            PNG,
            JPG
        }

        public Sprite(object textureSource, TextureFileType fileType)
        {
            _id = _idCounter++;
            if (_sprites.Count == 0)
                _default = this;

            byte[] textureBytes = GetTextureBytes(textureSource);

            string extension = TextureFileTypeExtensions.GetExtension(fileType);
            IntPtr extensionPtr = Marshal.StringToHGlobalAnsi(extension);

            try
            {
                unsafe
                {
                    fixed (byte* ptr = textureBytes)
                    {
                        Image image = Raylib.LoadImageFromMemory((sbyte*)extensionPtr, ptr, textureBytes.Length);
                        if (image.Height == 0 || image.Width == 0)
                        {
                            throw new InvalidOperationException("Failed to load image from memory.");
                        }
                        _width = image.Width;
                        _height = image.Height;

                        _baseTexture = Raylib.LoadTextureFromImage(image);
                        Raylib.UnloadImage(image);
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(extensionPtr);
            }

            _sprites.Add(this);
        }

        private static byte[] GetTextureBytes(object textureSource)
        {
            if (textureSource is byte[] byteArray)
            {
                return byteArray;
            }
            else if (textureSource is Stream stream)
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            else
            {
                throw new ArgumentException("Invalid texture source type. Expected byte[] or Stream.");
            }
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
