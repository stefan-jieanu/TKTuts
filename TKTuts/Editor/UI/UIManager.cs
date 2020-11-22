using System;
using System.Collections.Generic;
using System.Text;
using TKTuts.Renderer;


namespace TKTuts.Editor.UI
{
    class UIManager
    {
        private Renderer2D Renderer2D;
        private List<Rectangle2D> Buttons;

        public UIManager()
        {
            
        }

        public void Draw()
        {
            Renderer2D.Render();
        }

        public void AddButton(Rectangle2D button)
        {
            Renderer2D.AddRenderable(button);
        }
    }
}
