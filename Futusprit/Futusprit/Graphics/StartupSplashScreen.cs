using Raylib_cs;

namespace Futusprit.Graphics
{
    public class StartupSplashScreen
    {
        public bool Lock { get; set; } = false;

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

        public void BeginDisplay()
        {
            if (!_isDisplaying)
            {
                _isDisplaying = true;
                new Thread(DisplaySplashScreen).Start();
            }
        }

        private void DisplaySplashScreen()
        {
            while (_elapsedTime < _minSplashDuration || Lock)
            {
                _elapsedTime += Raylib.GetFrameTime();
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                if (Lock)
                {
                    string text = "Loading...";
                    int textWidth = Raylib.MeasureText(text, 40);
                    int textHeight = 40;
                    int x = (Raylib.GetScreenWidth() - textWidth) / 2;
                    int y = (Raylib.GetScreenHeight() - textHeight) / 2 + 50;

                    Raylib.DrawText(text, x, y, 40, Color.White);
                }

                Raylib.EndDrawing();
                Thread.Sleep(16);
            }

            _isDisplaying = false;
        }
    }
}
