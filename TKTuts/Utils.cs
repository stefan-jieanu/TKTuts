using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace TKTuts
{
    static class Utils
    {
        public static float[] GetMatrixElements(Matrix4 mat)
        {
            return new float[]
            {
                mat.M11, mat.M12, mat.M13, mat.M14,
                mat.M21, mat.M22, mat.M23, mat.M24,
                mat.M31, mat.M32, mat.M33, mat.M34,
                mat.M41, mat.M42, mat.M43, mat.M44
            };
        }
    }
}
