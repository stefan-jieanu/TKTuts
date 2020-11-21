using OpenTK.Graphics.OpenGL4;

namespace TKTuts.Buffers
{
    class Buffer
    {
        public int ComponentCount { get; private set; }
        private int BufferID;

        public Buffer(float[] data, int componentCount)
        {
            ComponentCount = componentCount;

            BufferID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
