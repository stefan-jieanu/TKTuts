using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace TKTuts.Buffers
{
    class VertexArray
    {
        private int ArrayID;
        private List<Buffer> Buffers;

        public VertexArray()
        {
            ArrayID = GL.GenVertexArray();
            Buffers = new List<Buffer>();
        }

        public void AddBuffer(Buffer buffer, int index)
        {
            Bind();

            buffer.Bind();
            GL.VertexAttribPointer(index, buffer.ComponentCount, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(index);
            Buffers.Add(buffer);

            buffer.Unbind();
            Unbind();
        }

        public void Bind()
        {
            GL.BindVertexArray(ArrayID);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public void Delete()
        {
            foreach(Buffer b in Buffers)
            {
                b.Delete();
            }
            GL.DeleteVertexArray(ArrayID);
        }
    }
}
