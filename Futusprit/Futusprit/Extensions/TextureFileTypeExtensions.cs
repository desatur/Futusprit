using static Futusprit.Graphics.Sprite;

namespace Futusprit.Extensions
{
    public static class TextureFileTypeExtensions
    {
        public static string GetExtension(this TextureFileType fileType)
        {
            return fileType switch
            {
                TextureFileType.PNG => ".png",
                TextureFileType.JPG => ".jpg",
                _ => throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null)
            };
        }
    }
}
