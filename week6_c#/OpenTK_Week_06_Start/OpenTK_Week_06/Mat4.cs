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
            matrix = new float[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrix[i, j] = other.matrix[i, j];
                }
            }
        }

        // Debug string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                sb.Append("| ");
                for (int j = 0; j < 4; j++)
                {
                    sb.Append(matrix[i, j].ToString("0.00")).Append(" ");
                }
                sb.AppendLine("|");
            }
            return sb.ToString();
        }

        #region Scale
        public void MakeScale(float sx, float sy, float sz)
        {
            matrix = new float[,]
            {
                {sx, 0.0f, 0.0f, 0.0f},
                {0.0f, sy, 0.0f, 0.0f},
                {0.0f, 0.0f, sz, 0.0f},
                {0.0f, 0.0f, 0.0f, 1.0f}
            };
        }
        #endregion

        #region Translate
        public void MakeTranslate(float tx, float ty, float tz)
        {
            matrix = new float[,]
            {
                {1.0f, 0.0f, 0.0f, tx},
                {0.0f, 1.0f, 0.0f, ty},
                {0.0f, 0.0f, 1.0f, tz},
                {0.0f, 0.0f, 0.0f, 1.0f}
            };
        }
        #endregion

        public static Matrix4 ConvertToMatrix4(Mat4 mat)
        {
            return new Matrix4(
                mat.matrix[0, 0], mat.matrix[1, 0], mat.matrix[2, 0], mat.matrix[3, 0],
                mat.matrix[0, 1], mat.matrix[1, 1], mat.matrix[2, 1], mat.matrix[3, 1],
                mat.matrix[0, 2], mat.matrix[1, 2], mat.matrix[2, 2], mat.matrix[3, 2],
                mat.matrix[0, 3], mat.matrix[1, 3], mat.matrix[2, 3], mat.matrix[3, 3]
            );
        }

        // DO NOT TOUCH ANYTHING ABOVE
        // DO THE TODOs BELOW

        private float INNER_PRODUCT(Mat4 a, Mat4 b, int r, int c)
        {
            return
            (a.matrix[r, 0] * b.matrix[0, c]) +
            (a.matrix[r, 1] * b.matrix[1, c]) +
            (a.matrix[r, 2] * b.matrix[2, c]) +
            (a.matrix[r, 3] * b.matrix[3, c]);
        }

        public void PreMult(Mat4 other)
        {
            float[] t = { 0.0f, 0.0f, 0.0f, 0.0f };
            for (int col = 0; col < 4; ++col)
            {
                t[0] = INNER_PRODUCT(other, this, 0, col);
                t[1] = INNER_PRODUCT(other, this, 1, col);
                t[2] = INNER_PRODUCT(other, this, 2, col);
                t[3] = INNER_PRODUCT(other, this, 3, col);
                matrix[0, col] = t[0];
                matrix[1, col] = t[1];
                matrix[2, col] = t[2];
                matrix[3, col] = t[3];
            }
        }

        public void SetRow(int row, float v1, float v2, float v3, float v4)
        {
            matrix[row, 0] = v1;
            matrix[row, 1] = v2;
            matrix[row, 2] = v3;
            matrix[row, 3] = v4;
        }

        public void PostMult(Mat4 other)
        {
            float[] t = { 0.0f, 0.0f, 0.0f, 0.0f };
            for (int row = 0; row < 4; ++row)
            {
                t[0] = INNER_PRODUCT(this, other, row, 0);
                t[1] = INNER_PRODUCT(this, other, row, 1);
                t[2] = INNER_PRODUCT(this, other, row, 2);
                t[3] = INNER_PRODUCT(this, other, row, 3);
                SetRow(row, t[0], t[1], t[2], t[3]);
            }
        }

        public void Mult(Mat4 lhs, Mat4 rhs)
        {
            if (lhs == this)
            {
                PostMult(rhs);
                return;
            }
            if (rhs == this)
            {
                PreMult(lhs);
                return;
            }
            // PRECONDITION: We assume neither lhs nor rhs == this
            // if it did, use preMult or postMult instead
            matrix[0, 0] = INNER_PRODUCT(lhs, rhs, 0, 0);
            matrix[0, 1] = INNER_PRODUCT(lhs, rhs, 0, 1);
            matrix[0, 2] = INNER_PRODUCT(lhs, rhs, 0, 2);
            matrix[0, 3] = INNER_PRODUCT(lhs, rhs, 0, 3);
            matrix[1, 0] = INNER_PRODUCT(lhs, rhs, 1, 0);
            matrix[1, 1] = INNER_PRODUCT(lhs, rhs, 1, 1);
            matrix[1, 2] = INNER_PRODUCT(lhs, rhs, 1, 2);
            matrix[1, 3] = INNER_PRODUCT(lhs, rhs, 1, 3);
            matrix[2, 0] = INNER_PRODUCT(lhs, rhs, 2, 0);
            matrix[2, 1] = INNER_PRODUCT(lhs, rhs, 2, 1);
            matrix[2, 2] = INNER_PRODUCT(lhs, rhs, 2, 2);
            matrix[2, 3] = INNER_PRODUCT(lhs, rhs, 2, 3);
            matrix[3, 0] = INNER_PRODUCT(lhs, rhs, 3, 0);
            matrix[3, 1] = INNER_PRODUCT(lhs, rhs, 3, 1);
            matrix[3, 2] = INNER_PRODUCT(lhs, rhs, 3, 2);
            matrix[3, 3] = INNER_PRODUCT(lhs, rhs, 3, 3);
        }

        public void MakeRotate(float angleX, float angleY, float angleZ)
        {
            float radX = angleX * (float)Math.PI / 180.0f;
            float radY = angleY * (float)Math.PI / 180.0f;
            float radZ = angleZ * (float)Math.PI / 180.0f;
            Mat4 rotationX = new Mat4();
            Mat4 rotationY = new Mat4();
            Mat4 rotationZ = new Mat4();

            rotationX.matrix[1,1] = (float)Math.Cos(radX);
            rotationX.matrix[1,2] = -(float)Math.Sin(radX);
            rotationX.matrix[2,1] = (float)Math.Sin(radX);
            rotationX.matrix[2,2] = (float)Math.Cos(radX);

            rotationY.matrix[0,0] = (float)Math.Cos(radY);
            rotationY.matrix[0,2] = -(float)Math.Sin(radY);
            rotationY.matrix[2,0] = (float)Math.Sin(radY);
            rotationY.matrix[2,2] = (float)Math.Cos(radY);

            rotationZ.matrix[0,0] = (float)Math.Cos(radZ);
            rotationZ.matrix[0,1] = -(float)Math.Sin(radZ);
            rotationZ.matrix[1,0] = (float)Math.Sin(radZ);
            rotationZ.matrix[1,1] = (float)Math.Cos(radZ);

            Mat4 result = new Mat4();
            result.Mult(rotationX, rotationY);
            result.Mult(result, rotationZ);
            this.matrix = result.matrix;
        }
    }
}
