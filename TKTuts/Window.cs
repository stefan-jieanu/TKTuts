using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace TKTuts
{
    public class Window : GameWindow
    {
        public static int WIDTH = 800;
        public static int HEIGHT = 600;


        private readonly float[] _vertices =
        {
             0.5f,  0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f,
        };

        private readonly ushort[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private Shader _shader;
        private Camera camera;

        public Window(int width, int height, string title)
            : base(width, height, GraphicsMode.Default, title)
        {
        }

        // Now, we start initializing OpenGL.
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);



            _shader = new Shader("E:\\Work\\TKTuts\\TKTuts\\Shaders\\shader.vert",
                "E:\\Work\\TKTuts\\TKTuts\\Shaders\\shader.frag");

            var vertexLocation = _shader.GetAttribLocation("position");
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(vertexLocation);

            _shader.Use();


            //camera = new Camera(new Vector2(Window.WIDTH, Window.HEIGHT) / 2, 1f);
            camera = new Camera(Vector2.Zero, 1f);

            base.OnLoad(e);
        }

        // Now that initialization is done, let's create our render loop.
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Buffers.VertexArray sprite1 = new Buffers.VertexArray();
            Buffers.Buffer vbo = new Buffers.Buffer(_vertices, 3);
            Buffers.IndexBuffer ibo = new Buffers.IndexBuffer(_indices);
            sprite1.AddBuffer(vbo, 0);

            Buffers.VertexArray sprite2 = new Buffers.VertexArray();
            vbo = new Buffers.Buffer(_vertices, 3);
            sprite2.AddBuffer(vbo, 0);

            Vector2 position = new Vector2(300f, 300f);
            Vector2 scale = new Vector2(300f, 200f);
            float rotation = MathF.PI / 4f;

            Matrix4 trans = Matrix4.Identity * Matrix4.CreateTranslation(position.X, position.Y, 1);
            Matrix4 sca = Matrix4.Identity * Matrix4.CreateScale(scale.X, scale.Y, 1);
            Matrix4 rot = Matrix4.CreateRotationZ(rotation);

            var view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);

            var model = Matrix4.Identity;
            model *= sca;
            model *= trans;
            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", view);
            _shader.SetMatrix4("projection", camera.GetProjectionMatrix());

            Color color1 = new Color(100, 150, 250, 255);

            int location = GL.GetUniformLocation(_shader.Handle, "color");
            GL.Uniform4(location, color1);

            sprite1.Bind();
            ibo.Bind();
            GL.DrawElements(PrimitiveType.Triangles, ibo.Count, DrawElementsType.UnsignedShort, 0);
            sprite1.Unbind();
            ibo.Unbind();

            Color color2 = new Color(250, 100, 150, 255);
            model = Matrix4.Identity * Matrix4.CreateScale(100, 100, 1) * Matrix4.CreateTranslation(120, 200, 1);
            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", view);
            _shader.SetMatrix4("projection", camera.GetProjectionMatrix());

            GL.Uniform4(location, color2);

            sprite2.Bind();
            ibo.Bind();
            GL.DrawElements(PrimitiveType.Triangles, ibo.Count, DrawElementsType.UnsignedShort, 0);
            sprite2.Unbind();
            ibo.Unbind();

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


            GL.DeleteProgram(_shader.Handle);
            base.OnUnload(e);
        }
    }
}