namespace Futusprit.Windowing
{
    public class ImGuiWindow : IDisposable
    {
        public static List<ImGuiWindow> Windows { get; private set; } = [];
        public ImGuiWindow()
        {
            Windows.Add(this);
        }

        public void Dispose()
        {
            Windows.Remove(this);
        }
        public static void DisposeAll()
        {
            foreach (var win in Windows)
            {
                win.Dispose();
            }
        }
    }
}
