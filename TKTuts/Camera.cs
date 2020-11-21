using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;

namespace TKTuts
{
    class Camera
    {
        public Vector2 FocusPosition { get; set; } 
        public float Zoom { get; set; }

        public Camera(Vector2 focusPosition, float zoom)
        {
            FocusPosition = focusPosition;
            Zoom = zoom;
        }

        public Matrix4 GetProjectionMatrix()
        {
            float left = FocusPosition.X - Window.WIDTH / 2f;
            float right = FocusPosition.X + Window.WIDTH / 2f;
            float top = FocusPosition.Y - Window.HEIGHT / 2f;
            float bottom = FocusPosition.Y + Window.HEIGHT / 2f;

            Matrix4 orthoMatrix = Matrix4.CreateOrthographicOffCenter(0.0f, 800.0f, 0.0f, 600.0f, 0.1f, 100.0f);
            //Matrix4 orthoMatrix = Matrix4.CreateOrthographic(800f, 600f, 0.1f, 100f);
            Matrix4 zoomMatrix = Matrix4.CreateScale(Zoom);

            return orthoMatrix;
        }
    }
}
