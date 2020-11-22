using System;
using System.Collections.Generic;
using System.Text;
using TKTuts.Editor;

namespace TKTuts.Editor
{
    enum Tools
    {
        Rect,
        Brush,
        None
    }

    class ToolsManager
    {
        public Tools ActiveTool { get; set; }

        public ToolsManager()
        {

        }

        public void HandleInput(char key)
        {
            switch (key)
            {
                case 'r':
                case 'R':
                    ActiveTool = Tools.Rect;
                    break;
                case 'b':
                case 'B':
                    ActiveTool = Tools.Brush;
                    break;
                default:
                    ActiveTool = Tools.None;
                    break;
            }
        }
    }
}
