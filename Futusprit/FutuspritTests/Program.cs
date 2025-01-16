using Raylib_cs;

namespace FutuspritTests
{
    internal class Program
    {
        static void Main()
        {
            var engine = new TestGame
            {
                ApplicationInitArgs = new Futusprit.ApplicationInitArgs()
                {
                    ApplicationName = "FutuspritTestsApp",
                    WindowTitle = "FutuspritTests"
                }
            };
            engine.Launch();
        }
    }

    internal class TestGame : Futusprit.Futusprit
    {
        private float rectX = 100;
        private float rectY = 100;
        private const float rectSize = 50;

        public override void ApplicationLoop()
        {
            // Update rectangle position
            if (Raylib.IsKeyDown(KeyboardKey.Right)) rectX += 5 * DeltaTime * 25;  // Move right
            if (Raylib.IsKeyDown(KeyboardKey.Left)) rectX -= 5 * DeltaTime * 25;   // Move left
            if (Raylib.IsKeyDown(KeyboardKey.Up)) rectY -= 5 * DeltaTime * 25;     // Move up
            if (Raylib.IsKeyDown(KeyboardKey.Down)) rectY += 5 * DeltaTime * 25;   // Move down
            Raylib.DrawRectangle((int)rectX, (int)rectY, (int)rectSize, (int)rectSize, Color.Red);
        }
    }
}
