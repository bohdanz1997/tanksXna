using Microsoft.Xna.Framework;

namespace Tanks.Tools
{
    public class Camera
    {
        protected float _zoom;
        protected float _rotation;

        Matrix _transform;
        Vector2 _pos;
        public Vector2 Viewport;
        public Vector2 Viewport2;

        public Camera(Vector2 viewPort)
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
            Viewport = viewPort;
            Viewport2 = viewPort / 2;
        }
        
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        public Vector2 Position
        {
            get { return _pos; }
            set { _pos = value; }
        }
        
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }

        public RectangleF GetBounds()
        {
            return new RectangleF(
                Position.X - Viewport2.X - 30, 
                Position.Y - Viewport2.Y - 30, 
                Viewport.X + 60, 
                Viewport.Y + 60);
        }

        public Matrix GetTransformation()
        {
            _transform =
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(Viewport.X * 0.5f, 
                                            Viewport.Y * 0.5f, 0));
            return _transform;
        }
    }
}
