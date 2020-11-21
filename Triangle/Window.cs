using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;


namespace Triangle
{
    // Be warned, there is a LOT of stuff here. It might seem complicated, but just take it slow and you'll be fine.
    // OpenGL's initial hurdle is quite large, but once you get past that, things will start making more sense.
    public class Window : GameWindow
    {
        float[] _vertices = {
             0.5f,  0.5f, 0.0f,  // top right
             0.5f, -0.5f, 0.0f,  // bottom right
            -0.5f, -0.5f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f   // top left
        };

        uint[] _indices = {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        private int _vertexBufferObject;

        private int _vertexArrayObject;
        private int _elementBufferObject;

        private Shader _shader;

        public Window(int width, int height, string title)
            : base(width, height, GraphicsMode.Default, title)
        {
        }

        // Now, we start initializing OpenGL.
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            // We create/bind the EBO the same way as the VBO, just with a different BufferTarget.
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);

            // We also buffer data to the EBO the same way.
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            _shader = new Shader("E:/Work/TKTuts/Triangle/shader.vert", "E:/Work/TKTuts/Triangle/shader.frag");
            _shader.Use();

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            // We bind the EBO here too, just like with the VBO in the previous tutorial.
            // Now, the EBO will be bound when we bind the VAO.
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);

            // The EBO has now been properly setup. Go to the Render function to see how we draw our rectangle now!

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            base.OnLoad(e);
        }

        // Now that initialization is done, let's create our render loop.
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Matrix4 model = Matrix4.Identity * Matrix4.CreateScale(new Vector3(400, 300, 1)) *
                Matrix4.CreateTranslation(new Vector3(100, 100, 1));

            _shader.Use();
            _shader.SetMatrix4("pr_matrix", Matrix4.CreateOrthographicOffCenter(0.0f, 800.0f, 0.0f, 600.0f, 0.1f, 100.0f));
            //_shader.SetMatrix4("ml_matrix", model);

            // Bind the VAO
            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            // And that's all you have to do for rendering! You should now see a yellow triangle on a black screen.
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
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            GL.DeleteProgram(_shader.Handle);
            base.OnUnload(e);
        }
    }
}