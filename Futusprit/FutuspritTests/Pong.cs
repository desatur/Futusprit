using Futusprit;
using Futusprit.Windowing;
using Raylib_cs;

namespace FutuspritTests
{
    internal class Pong : FutuspritApplication
    {
        private static int ScreenWidth;
        private static int ScreenHeight;

        private const int PaddleWidth = 20;
        private const int PaddleHeight = 100;
        private const float PaddleSpeed = 400;

        private const int BallSize = 20;
        private const float BallSpeed = 300;

        private float player1Y = ScreenHeight / 2 - PaddleHeight / 2;
        private float player2Y = ScreenHeight / 2 - PaddleHeight / 2;

        private float ballX = ScreenWidth / 2 - BallSize / 2;
        private float ballY = ScreenHeight / 2 - BallSize / 2;
        private float ballSpeedX = BallSpeed;
        private float ballSpeedY = BallSpeed / 2;

        public override void ApplicationRenderingLoop()
        {
            ScreenWidth = PresentationWindow.Singleton.Width;
            ScreenHeight = PresentationWindow.Singleton.Height;

            // Player 1 controls (W and S)
            if (Raylib.IsKeyDown(KeyboardKey.W)) player1Y -= PaddleSpeed * DeltaTime;
            if (Raylib.IsKeyDown(KeyboardKey.S)) player1Y += PaddleSpeed * DeltaTime;

            // Player 2 controls (Up and Down arrows)
            if (Raylib.IsKeyDown(KeyboardKey.Up)) player2Y -= PaddleSpeed * DeltaTime;
            if (Raylib.IsKeyDown(KeyboardKey.Down)) player2Y += PaddleSpeed * DeltaTime;

            // Prevent paddles from going out of bounds
            player1Y = Clamp(player1Y, 0, ScreenHeight - PaddleHeight);
            player2Y = Clamp(player2Y, 0, ScreenHeight - PaddleHeight);

            // Ball movement
            ballX += ballSpeedX * DeltaTime;
            ballY += ballSpeedY * DeltaTime;

            // Ball collision with top and bottom walls
            if (ballY <= 0 || ballY + BallSize >= ScreenHeight)
            {
                ballSpeedY *= -1;
            }

            // Ball collision with paddles
            if (ballX <= PaddleWidth &&
                ballY + BallSize >= player1Y &&
                ballY <= player1Y + PaddleHeight)
            {
                ballSpeedX *= -1;
                ballX = PaddleWidth; // Prevent ball from sticking
            }

            if (ballX + BallSize >= ScreenWidth - PaddleWidth &&
                ballY + BallSize >= player2Y &&
                ballY <= player2Y + PaddleHeight)
            {
                ballSpeedX *= -1;
                ballX = ScreenWidth - PaddleWidth - BallSize; // Prevent ball from sticking
            }

            // Reset ball position if it goes off screen
            if (ballX < 0 || ballX > ScreenWidth)
            {
                ballX = ScreenWidth / 2 - BallSize / 2;
                ballY = ScreenHeight / 2 - BallSize / 2;
                ballSpeedX = BallSpeed * (ballSpeedX < 0 ? 1 : -1); // Reverse direction
            }

            // Drawing
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawRectangle(0, (int)player1Y, PaddleWidth, PaddleHeight, Color.Blue);
            Raylib.DrawRectangle(ScreenWidth - PaddleWidth, (int)player2Y, PaddleWidth, PaddleHeight, Color.Green);
            Raylib.DrawRectangle((int)ballX, (int)ballY, BallSize, BallSize, Color.White);

            // Draw center line
            Raylib.DrawLine(ScreenWidth / 2, 0, ScreenWidth / 2, ScreenHeight, Color.Gray);
        }
        private static float Clamp(float value, float min, float max)
        {
            return MathF.Max(min, MathF.Min(value, max));
        }
    }
}
