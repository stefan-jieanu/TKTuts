using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using TKTuts.Renderer;

namespace TKTuts.Editor
{
    class BrushTool
    {
        public Vector3 BrushSize { get; set; }

        public BrushTool()
        {
            BrushSize = new Vector3(2f, 2f, 1f);
        }

        public Rectangle2D Draw(Vector3 pos, Color color)
        {
            return new Rectangle2D(pos, BrushSize, 0, color);
        }
    }
}
