using System;
using System.Text;
using OpenTK.Mathematics;

namespace CGPG
{
    public class Mat4
    {
        // 4x4 matrix (corrected your comment: it's 4x4, not 3x3)
        public float[,] matrix =
        {
            {1.0f, 0.0f, 0.0f, 0.0f},
            {0.0f, 1.0f, 0.0f, 0.0f},
            {0.0f, 0.0f, 1.0f, 0.0f},
            {0.0f, 0.0f, 0.0f, 1.0f},
        };

        // Default constructor
        public Mat4() { }

        // Copy constructor
        public Mat4(Mat4 other)
        {
            // TODO: Implement this
        }

        // Debug string
        public override string ToString()
        {
            string result = "";
            // TODO: Implement this

            return null;
        }

        #region Scale
        public void MakeScale(float sx, float sy, float sz)
        {
            matrix = new float[4, 4];

            matrix[0, 0] = sx;
            matrix[0, 1] = 0;
            matrix[0, 2] = 0;
            matrix[0, 3] = 0;

            matrix[1, 0] = 0;
            matrix[1, 1] = sy;
            matrix[1, 2] = 0;
            matrix[1, 3] = 0;

            matrix[2, 0] = 0;
            matrix[2, 1] = 0;
            matrix[2, 2] = sz;
            matrix[2, 3] = 0;

            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 0;
            matrix[3, 3] = 1;
        }

        #endregion

        #region Translate
        public void MakeTranslate(float tx, float ty, float tz)
        {
            matrix = new float[4, 4];

            matrix[0, 0] = 1;
            matrix[0, 1] = 0;
            matrix[0, 2] = 0;
            matrix[0, 3] = tx;

            matrix[1, 0] = 0;
            matrix[1, 1] = 1;
            matrix[1, 2] = 0;
            matrix[1, 3] = ty;

            matrix[2, 0] = 0;
            matrix[2, 1] = 0;
            matrix[2, 2] = 1;
            matrix[2, 3] = tz;

            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 0;
            matrix[3, 3] = 1;
        }

        #endregion

        // HELPER FUNCTION DO NOT TOUCH
        public static Matrix4 ConvertToMatrix4(Mat4 mat)
        {
            return new Matrix4(
                mat.matrix[0, 0], mat.matrix[1, 0], mat.matrix[2, 0], mat.matrix[3, 0],
                mat.matrix[0, 1], mat.matrix[1, 1], mat.matrix[2, 1], mat.matrix[3, 1],
                mat.matrix[0, 2], mat.matrix[1, 2], mat.matrix[2, 2], mat.matrix[3, 2],
                mat.matrix[0, 3], mat.matrix[1, 3], mat.matrix[2, 3], mat.matrix[3, 3]
            );
        }

    }
}
