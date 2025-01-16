using System.Numerics;
using Raylib_cs;

namespace Futusprit
{
    public class Camera : IDisposable
    {
        public static List<Camera> Cameras
        {
            get
            {
                return _cameras;
            }
        }

        public static Camera Default
        {
            get
            {
                return _default;
            }
            set
            {
                _default = value;
            }
        }

        public Camera2D Base
        {
            get
            {
                return _baseCamera;
            }
        }

        public ushort ID
        {
            get
            {
                return _id;
            }
        }

        private static List<Camera> _cameras = [];
        private static Camera _default;
        private Camera2D _baseCamera;
        private static ushort _idCounter = 0;
        private ushort _id;

        public Camera()
        {
            _id = _idCounter++;
            if (_cameras.Count == 0) _default = this;
            _baseCamera = new Camera2D
            {
                Offset = new Vector2(400, 300),
                Target = new Vector2(0, 0),
                Rotation = 0.0f,
                Zoom = 1.0f
            };
            _cameras.Add(this);
        }

        public void Dispose()
        {
            _cameras.Remove(this);

            if (_default == this)
            {
                _default = _cameras.Count > 0 ? _cameras[0] : null;
            }

            GC.SuppressFinalize(this);
        }

        public static Camera FindByID(ushort id)
        {
            return _cameras.FirstOrDefault(camera => camera.ID == id);
        }
    }
}
