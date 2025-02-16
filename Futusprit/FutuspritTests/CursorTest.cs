using Futusprit;
using Futusprit.Windowing;

namespace FutuspritTests
{
    public class CursorTest : FutuspritApplication
    {
        public CursorTest()
        {
            Cursor.Bound = true;
            Cursor.Visible = false;
        }

        //public override void ApplicationRenderingLoop() {}
    }
}
