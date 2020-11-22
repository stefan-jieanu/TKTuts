using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TKTuts.Renderer
{
    class Renderer2D
    {
        private List<Rectangle2D> Renderables;
        private Shader shader;
        private Camera camera;

        public Renderer2D()
        {
            Renderables = new List<Rectangle2D>();
            shader = new Shader("E:/ActualWork/TkTuts/TKTuts/Shaders/shader.vert",
                "E:/ActualWork/TkTuts/TKTuts/Shaders/shader.frag");
            shader.Use();

            camera = new Camera(new Vector2(Window.WIDTH, Window.HEIGHT) / 2, 1f);

            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetMatrix4("view", Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f));

            GL.LineWidth(2f);
        }

        public void Render()
        {
            foreach(Rectangle2D rend in Renderables)
            {

                shader.SetMatrix4("model", rend.Scale * rend.Rotation * rend.Position);
                int location = GL.GetUniformLocation(shader.Handle, "color");
                GL.Uniform4(location, rend.Color);

                rend.Bind();
                if (rend.Fill)
                    GL.DrawElements(PrimitiveType.Triangles, Rectangle2D.FillIndexBuffer.Count, DrawElementsType.UnsignedShort, 0);
                
                rend.Unbind();
            }
        }

        public void AddRenderable(Rectangle2D rend)
        {
            Renderables.Add(rend);
        }

        public void Cleanup()
        {
            foreach(Rectangle2D rend in Renderables)
            {
                rend.Delete();
            }
            GL.UseProgram(0);
            GL.DeleteProgram(shader.Handle);
        }
    }
}
