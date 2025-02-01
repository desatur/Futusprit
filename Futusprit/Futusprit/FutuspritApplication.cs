using Futusprit.Extensions;
using Futusprit.Graphics;
using Futusprit.Windowing;
using Futusprit.Objects;
using Raylib_cs;

namespace Futusprit
{
    public class FutuspritApplication : IDisposable
    {
        public static FutuspritApplication Singleton { get; private set; }
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

        public static ECS ECS
        {
            get
            {
                return _ecs;
            }
        }

        private static float _deltaTime;
        private ApplicationInitArgs _launchArgs;

        private bool _isRunning = false;
        private StartupSplashScreen _startupSplashScreen;

        private bool _shouldRender = false;
        private static ECS _ecs;

        public FutuspritApplication()
        {

        }

        public void Launch()
        {
            if (_launchArgs == null) throw new FutuspritException("ApplicationInitArgs is null, please set it before launching the Engine.", 0);
            if (Singleton == null)
            {
                Singleton = this;
            }
            else
            {
                throw new FutuspritException("Only single instance of the engine is allowed.", 2);
            }
            _startupSplashScreen = new();
            _ecs = new();
            _ = new PresentationWindow(new(512, 512));

            _isRunning = true;
            MainRenderingLoop();
        }

        public virtual void ApplicationRenderingLoop() { }
        private void MainRenderingLoop()
        {
            _startupSplashScreen.BeginDisplay();
            _ = new Camera();

            while (!_isRunning || !Raylib.WindowShouldClose())
            {
                _deltaTime = Raylib.GetFrameTime();
                if (Cursor.Bound)
                {
                    Raylib.SetMousePosition(PresentationWindow.Singleton.Width / 2, PresentationWindow.Singleton.Width / 2);
                    /*
                     *  This implementation sucks asf
                     * ⠄⣾⣿⡇⢸⣿⣿⣿⠄⠈⣿⣿⣿⣿⠈⣿⡇⢹⣿⣿⣿⡇⡇⢸⣿⣿⡇⣿⣿⣿
                     * ⢠⣿⣿⡇⢸⣿⣿⣿⡇⠄⢹⣿⣿⣿⡀⣿⣧⢸⣿⣿⣿⠁⡇⢸⣿⣿⠁⣿⣿⣿
                     * ⢸⣿⣿⡇⠸⣿⣿⣿⣿⡄⠈⢿⣿⣿⡇⢸⣿⡀⣿⣿⡿⠸⡇⣸⣿⣿⠄⣿⣿⣿
                     * ⢸⣿⡿⠷⠄⠿⠿⠿⠟⠓⠰⠘⠿⣿⣿⡈⣿⡇⢹⡟⠰⠦⠁⠈⠉⠋⠄⠻⢿⣿
                     * ⢨⡑⠶⡏⠛⠐⠋⠓⠲⠶⣭⣤⣴⣦⣭⣥⣮⣾⣬⣴⡮⠝⠒⠂⠂⠘⠉⠿⠖⣬
                     * ⠈⠉⠄⡀⠄⣀⣀⣀⣀⠈⢛⣿⣿⣿⣿⣿⣿⣿⣿⣟⠁⣀⣤⣤⣠⡀⠄⡀⠈⠁
                     * ⠄⠠⣾⡀⣾⣿⣧⣼⣿⡿⢠⣿⣿⣿⣿⣿⣿⣿⣿⣧⣼⣿⣧⣼⣿⣿⢀⣿⡇⠄
                     * ⡀⠄⠻⣷⡘⢿⣿⣿⡿⢣⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣜⢿⣿⣿⡿⢃⣾⠟⢁⠈
                     * ⢃⢻⣶⣬⣿⣶⣬⣥⣶⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣷⣶⣶⣾⣿⣷⣾⣾⢣
                     * ⡄⠈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠘
                     * ⣿⡐⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢠⢃
                     * ⣿⣷⡀⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⡿⠋⢀⠆⣼
                     * ⣿⣿⣷⡀⠄⠈⠛⢿⣿⣿⣿⣿⣷⣶⣶⣶⣶⣶⣿⣿⣿⣿⣿⠿⠋⠠⠂⢀⣾⣿
                     * ⣿⣿⣿⣧⠄⠄⢵⢠⣈⠛⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢋⡁⢰⠏⠄⠄⣼⣿⣿
                     * ⢻⣿⣿⣿⡄⢢⠨⠄⣯⠄⠄⣌⣉⠛⠻⠟⠛⢋⣉⣤⠄⢸⡇⣨⣤⠄⢸⣿⣿⣿
                    */
                }
                ApplicationRenderingLoop();

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
            List<string> debugInfo =
            [
                $"Application Name: {_launchArgs.ApplicationName}",
                $"Delta Time: {_deltaTime} | {_deltaTime:F4}s",
                $"FPS: {Raylib.GetFPS()}",
                $"Screen Width: {Raylib.GetScreenWidth()}px",
                $"Screen Height: {Raylib.GetScreenHeight()}px",
                $"Object Count: {ECS.Singleton.ObjectManager.Count}"
            ];
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
