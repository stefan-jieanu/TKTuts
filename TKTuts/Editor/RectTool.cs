using System;
using System.Collections.Generic;
using System.Text;
using TKTuts.Renderer;
using OpenTK;

namespace TKTuts.Editor
{
    class RectTool
    {
        private Vector3 StartPoint;
        private Color RectColor;
        private bool Drawing { get; set; }

        public Rectangle2D Rect { get; set; }

        public RectTool(Vector3 startPoint, Vector3 endPoint, Color color)
        {
            StartPoint = startPoint;
            RectColor = color;

            Rect = new Rectangle2D(new Vector3(StartPoint), endPoint - StartPoint, 0, RectColor);
            Rect.Fill = true;
            Drawing = true;
        }

        public void DrawShape(Vector3 endPoint)
        {
            if (Drawing)
            {
                Rect.Scale = Matrix4.CreateScale(endPoint - StartPoint);
            }
        }

        public void CompleteShape()
        {
            Drawing = false;
        }
    }
}
