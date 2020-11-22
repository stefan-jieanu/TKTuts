using System;
using System.Collections.Generic;
using System.Text;
using TKTuts.Renderer;
using OpenTK;
using OpenTK.Input;

namespace TKTuts.Editor
{
    class EditorManager
    {
        public Vector3 MousePos { get; set; }

        private Renderer2D CanvasRenderer;
        private ToolsManager Toolbox;
        private Color SelectedColor;
        RectTool RectTool;
        BrushTool BrushTool;


        public EditorManager()
        {
            CanvasRenderer = new Renderer2D();
            Toolbox = new ToolsManager();
            BrushTool = new BrushTool();
            SelectedColor = Color.Blue;
        }

        public void Draw()
        {
            CanvasRenderer.Render();

            if (RectTool != null)
                RectTool.DrawShape(MousePos);
        }

        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (Toolbox.ActiveTool)
            {
                case Tools.Rect:
                    Vector3 pos = MousePos;
                    RectTool = new RectTool(pos, pos, SelectedColor);
                    CanvasRenderer.AddRenderable(RectTool.Rect);
                    break;
                case Tools.Brush:
                    break;
                case Tools.None:
                    break;
                default:
                    break;
            }            
        }

        public void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            MousePos = new Vector3(e.X, e.Y, 1);

            if (Toolbox.ActiveTool == Tools.Brush)
                CanvasRenderer.AddRenderable(BrushTool.Draw(MousePos, SelectedColor));
        }

        public void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            RectTool.CompleteShape();
        }

        public void OnKeyDown(object sender, KeyPressEventArgs e)
        {
            Toolbox.HandleInput(e.KeyChar);
        }

        public void OnKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            
        }
    }
}
