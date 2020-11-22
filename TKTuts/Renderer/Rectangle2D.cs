using System.Collections.Generic;
using System.Text;
using TKTuts.Buffers;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace TKTuts.Renderer
{
    class Rectangle2D
    {
        private VertexArray vertexArray;
        public static IndexBuffer FillIndexBuffer { get; set; }
        private Buffers.Buffer vertexBuffer;

        public Matrix4 Position;
        public Matrix4 Scale;
        public Matrix4 Rotation;
        public Color Color;
        public bool Fill = true;

        private readonly float[] Vertices =
        {
             0.0f,  0.0f, 0.0f,
             1f,  0.0f, 0.0f,
             1f,  1f, 0.0f,
             0.0f,  1f, 0.0f,
        };

        private static readonly ushort[] FillIndices =
        {
            0, 1, 2,
            2, 3, 0
        };

        public Rectangle2D()
        {

        }

        public Rectangle2D(Vector3 position, Vector3 scale, float rotation, Color color)
        {
            vertexArray = new VertexArray();
            FillIndexBuffer = new IndexBuffer(FillIndices);
            vertexBuffer = new Buffers.Buffer(Vertices, 3);

            vertexArray.AddBuffer(vertexBuffer, 0);

            Position = Matrix4.CreateTranslation(position);
            Scale = Matrix4.CreateScale(scale);
            Rotation = Matrix4.CreateRotationZ(rotation);
            Color = color;
        }

        public void Delete()
        {
            vertexArray.Delete();
        }

        public void Bind()
        {
            vertexArray.Bind();
            FillIndexBuffer.Bind();
        }

        public void Unbind()
        {
            vertexArray.Unbind();
            FillIndexBuffer.Unbind();
        }
    }
}
