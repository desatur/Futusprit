namespace FutuspritTests
{
    internal class Program
    {
        static void Main()
        {
            var pong = new Pong
            {
                ApplicationInitArgs = new()
                {
                    ApplicationName = "FutuspritTestsApp_pong",
                    WindowTitle = "Pong"
                }
            };
            var cursor = new CursorTest
            {
                ApplicationInitArgs = new()
                {
                    ApplicationName = "FutuspritTestsApp_cursor",
                    WindowTitle = "Cursor Test"
                }
            };
            //pong.Launch();
            cursor.Launch();
        }
    }
}
