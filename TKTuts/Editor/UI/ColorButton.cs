using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;


namespace TKTuts.Editor.UI
{
    class ColorButton : TKTuts.Renderer.Rectangle2D
    {
        public ColorButton(Vector3 position, Vector3 scale, Color color)
            : base(position, scale, 0, color)
        {

        }
    }
}
