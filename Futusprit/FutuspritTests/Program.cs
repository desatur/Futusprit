using Raylib_cs;

namespace FutuspritTests
{
    internal class Program
    {
        static void Main()
        {
            var engine = new Test
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

    internal class Test : Futusprit.Futusprit
    {
        private float rectX = 100;
        private float rectY = 100;
        private const float rectSize = 50;

        public override void ApplicationLoop()
        {
            if (Raylib.IsKeyDown(KeyboardKey.Right)) rectX += 5 * DeltaTime * 100;  // Move right
            if (Raylib.IsKeyDown(KeyboardKey.Left)) rectX -= 5 * DeltaTime * 100;   // Move left
            if (Raylib.IsKeyDown(KeyboardKey.Up)) rectY -= 5 * DeltaTime * 100;     // Move up
            if (Raylib.IsKeyDown(KeyboardKey.Down)) rectY += 5 * DeltaTime * 100;   // Move down
            Raylib.DrawRectangle((int)rectX, (int)rectY, (int)rectSize, (int)rectSize, Color.Red);
        }
    }
}
