using Raylib_cs;

namespace Futusprit
{
    public class Futusprit
    {
        public static float DeltaTime
        {
            get
            {
                return _deltaTime;
            }
        }

        private static float _deltaTime;

        public Futusprit()
        {
            Raylib.InitWindow(1920, 1080, "");
            
            GameLoop();
        }

        private void GameLoop()
        {
            _ = new Camera();

            while (!Raylib.WindowShouldClose())
            {
                _deltaTime = Raylib.GetFrameTime();
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                Raylib.BeginMode2D(Camera.Default.Base);

                Raylib.DrawText($"Delta Time: {_deltaTime:F4} seconds", 10, 10, 20, Color.Green);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
