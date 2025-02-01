using Futusprit.Extensions;
using Raylib_cs;

namespace Futusprit.Windowing
{
    public class PresentationWindow : IDisposable
    {
        public static PresentationWindow Singleton { get; private set; }
        public static List<WindowResolution> SupportedResolutions { get; private set; }
        public static WindowResolution DefaultSelectedResolution { get; private set; }
        public static WindowResolution PrimaryMonitorResolution { get; private set; }

        public string Title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_suffix))
                {
                    return FutuspritApplication.Singleton.ApplicationInitArgs.WindowTitle;
                }
                else
                {
                    return FutuspritApplication.Singleton.ApplicationInitArgs.WindowTitle + " - " + Suffix;
                }
            }
        }
        public string Suffix
        {
            get
            {
                return _suffix;
            }
            set
            {
                if (value.Length < 32)
                {
                    throw new FutuspritException("Window suffix cannot be larger than 32 characters", 3);
                }
                _suffix = value;
                UpdateWindowTitle();
            }
        }
        public ushort Width
        {
            get
            {
                return 512;
            }
        }
        public ushort Height
        {
            get
            {
                return 512;
            }
        }
        public bool IsMainWindow => this == Singleton;

        private string _suffix;
        private bool _isInitialized = false;

        public PresentationWindow(WindowResolution default_res, bool setAsMainWindow = true)
        {
            DefaultSelectedResolution = default_res;
            if (setAsMainWindow || Singleton == null)
            {
                Singleton = this;
                Raylib.InitWindow(Width, Height, Title);
                _isInitialized = true;
            }
        }

        private void UpdateWindowTitle()
        {
            Raylib.SetWindowTitle(Title);
        }

        public void SetFullscreen(bool enable = true)
        {
            if (IsMainWindow)
            {
                if (enable)
                {
                    Raylib.ToggleFullscreen();
                }
                else
                {
                    Raylib.SetWindowSize(Width, Height);
                }
            }
        }

        public void Dispose()
        {
            if (IsMainWindow && _isInitialized)
            {
                Raylib.CloseWindow();
                _isInitialized = false;
            }

            if (IsMainWindow)
            {
                Singleton = null;
            }
            GC.SuppressFinalize(this);
        }
        //public static void DisposeAll()
        //{
        //    foreach (PresentationWindow w in Windows)
        //    {
        //        w.Dispose();
        //    }
        //}
    }
}
