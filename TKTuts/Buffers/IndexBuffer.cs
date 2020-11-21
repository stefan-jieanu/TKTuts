using OpenTK.Graphics.OpenGL4;

namespace TKTuts.Buffers
{
    class IndexBuffer
    {
        public int Count { get; private set; }
        private int BufferID;

        // Using a ushort for storing the indeces to save some memory 
        // This gives us a total of 2^16 indeces 
        // If in the future we will need more than that, so in a future if we get a weird error
        // this might be the cause of it, so change it to an uint.
        public IndexBuffer(ushort[] data)
        {
            Count = data.Length;

            BufferID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, BufferID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(short), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, BufferID);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
    }
}
