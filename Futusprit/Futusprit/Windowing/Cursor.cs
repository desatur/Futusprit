using Raylib_cs;

namespace Futusprit.Windowing
{
    public class Cursor
    {
        public static Cursor Singleton { get; private set; }
        private static bool _bound = false;
        private static bool _visible = true;

        public static bool Bound
        {
            get => _bound;
            set
            {
                if (value)
                {
                    Raylib.DisableCursor();
                }
                else
                {
                    Raylib.EnableCursor();
                }
                _bound = value;
            }
        }

        public static bool Visible
        {
            get => _visible;
            set
            {
                if (value)
                {
                    Raylib.ShowCursor();
                }
                else
                {
                    Raylib.HideCursor();
                }
                _visible = value;
            }
        }

        public Cursor()
        {
            if (Singleton == null)
            {
                Singleton = this;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void DrawCustomCursor()
        {
        }

        /// <summary>
        /// Note (desatur): Not the best method to make sure everything is okay.
        /// </summary>
        internal static void Apply()
        {
            if (Bound) { Raylib.DisableCursor(); } else { Raylib.EnableCursor(); }
            if (Visible) { Raylib.ShowCursor(); } else { Raylib.HideCursor(); }
        }
    }
}
