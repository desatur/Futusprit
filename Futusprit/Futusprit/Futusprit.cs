using Futusprit.Extensions;
using Futusprit.Graphics;
using Raylib_cs;

namespace Futusprit
{
    public class Futusprit : IDisposable
    {
        public static float DeltaTime
        {
            get
            {
                return _deltaTime;
            }
        }

        public ApplicationInitArgs ApplicationInitArgs
        {
            get
            {
                return _launchArgs;
            }
            set
            {
                if (_isRunning)
                {
                    throw new FutuspritException("You cannot change ApplicationInitArgs during runtime", 1);
                }
                else
                {
                    _launchArgs = value;
                }
            }
        }

        private static float _deltaTime;
        private ApplicationInitArgs _launchArgs;

        private bool _isRunning = false;
        private StartupSplashScreen _startupSplashScreen;

        private bool _shouldRender = false;

        public Futusprit()
        {
            _startupSplashScreen = new();
        }

        public void Launch()
        {
            if (_launchArgs == null) throw new FutuspritException("ApplicationInitArgs is null, please set it before launching the Engine.", 0);
            Raylib.InitWindow(1920, 1080, _launchArgs.WindowTitle);

            _isRunning = true;
            MainRenderingLoop();
        }

        public virtual void ApplicationLoop()
        {
        }

        private void MainRenderingLoop()
        {
            _startupSplashScreen.BeginDisplay();
            _ = new Camera();

            while (!_isRunning || !Raylib.WindowShouldClose())
            {
                _deltaTime = Raylib.GetFrameTime();
                ApplicationLoop();

                // Rendering code on the main thread
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Raylib.BeginMode2D(Camera.Default.Base);

#if DEBUG
                DrawDebug();
#endif

                Raylib.EndDrawing();
            }
        }

#if DEBUG
        private void DrawDebug()
        {
            Raylib.EndMode2D();
            var debugInfo = new List<string>
            {
                $"Application Name: {_launchArgs.ApplicationName}",
                $"Delta Time: {_deltaTime:F4}s",
                $"FPS: {Raylib.GetFPS()}",
                $"Screen Width: {Raylib.GetScreenWidth()}px",
                $"Screen Height: {Raylib.GetScreenHeight()}px"
            };
            Raylib.DrawText("Debug:\n   " + string.Join("\n   ", debugInfo), 10, 10, 2, Color.Green);
        }
#endif

        public void Dispose()
        {
            Raylib.CloseWindow();
            _isRunning = false;
            GC.SuppressFinalize(this);
        }
    }
}
