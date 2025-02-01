using System.Numerics;
using Futusprit.Windowing;
using Raylib_cs;

namespace Futusprit.Graphics
{
    public class StartupSplashScreen
    {
        public bool Lock { get; set; } = false;
        public Color BackgroundColor { get; set; } = Color.Black;

        public float MinimumStartupSplashScreenDuration
        {
            set
            {
                _minSplashDuration = value;
            }
            get
            {
                return _minSplashDuration;
            }
        }

        private float _minSplashDuration = 5.0f;
        private float _elapsedTime = 0.0f;
        private bool _isDisplaying = false;
        private Sprite _vignetteSplash;

        public void BeginDisplay()
        {
            _vignetteSplash = new Sprite(Internal.vignette, Sprite.TextureFileType.PNG);
            if (!_isDisplaying)
            {
                _isDisplaying = true;
                DisplaySplashScreen();
            }
        }

        private void DisplaySplashScreen()
        {
            while (_elapsedTime < _minSplashDuration || Lock)
            {
                _elapsedTime += Raylib.GetFrameTime();
                Raylib.BeginDrawing();
                Raylib.ClearBackground(BackgroundColor);

                Vector2 centerPosition = new(
                    (PresentationWindow.Singleton.Width - _vignetteSplash.Size.Y) / 2,
                    (PresentationWindow.Singleton.Height - _vignetteSplash.Size.X) / 2
                );

                //_vignetteSplash.Draw(centerPosition, Color.White);

#if DEBUG
                if (true)
#else
                if (Lock)
#endif
                {
                    string text = "Loading...";
                    int textWidth = Raylib.MeasureText(text, 40);
                    int textHeight = 80;
                    int x = (Raylib.GetScreenWidth() - textWidth) / 2;
                    int y = (Raylib.GetScreenHeight() - textHeight) / 2 + 250;

                    Raylib.DrawText(text, x, y, 40, Color.White);
                }

                Raylib.EndDrawing();
                Thread.Sleep(16);
            }

            _isDisplaying = false;
        }
    }
}
