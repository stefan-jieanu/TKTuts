using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using TKTuts.Editor;
namespace TKTuts
{
    public class Window : GameWindow
    {
        public static int WIDTH = 800;
        public static int HEIGHT = 600;

        private EditorManager editor;

        public Window(int width, int height, string title)
            : base(width, height, GraphicsMode.Default, title)
        {
        }

        // Now, we start initializing OpenGL.
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.WhiteSmoke);

            editor = new EditorManager();
            MouseMove += editor.OnMouseMove;
            MouseDown += editor.OnMouseDown;
            MouseUp += editor.OnMouseUp;
            KeyPress += editor.OnKeyDown;
            KeyUp += editor.OnKeyUp;

            base.OnLoad(e);
        }


        // Now that initialization is done, let's create our render loop.
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            editor.Draw();

            SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            // When the window gets resized, we have to call GL.Viewport to resize OpenGL's viewport to match the new size.
            // If we don't, the NDC will no longer be correct.
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        // Now, for cleanup. This isn't technically necessary since C# and OpenGL will clean up all resources automatically when
        // the program closes, but it's very important to know how anyway.
        protected override void OnUnload(EventArgs e)
        {
            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            // Delete all the resources.
            base.OnUnload(e);
        }
    }
}