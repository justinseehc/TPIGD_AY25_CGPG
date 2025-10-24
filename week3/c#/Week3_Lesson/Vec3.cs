using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3_Lesson
{
    public class Vec3
    {
        private float x = 0f;
        private float y = 0f;
        private float z = 0f;
        public Vec3() { }
        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        // Operators
        public static Vec3 operator +(Vec3 a, Vec3 b) // ADD OPERATOR
        {
            return new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        // TODO: Subtraction
        public static Vec3 operator -(Vec3 a, Vec3 b) // SUBTRACT OPERATOR
        {
            return new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        // TODO: Normalisation
        public static Vec3 operator /(Vec3 c) // NORMALISATION OPERATOR
        {
            private float mag = Math.Sqrt(  )
            return new Vec3();
        }
        // TODO: Scalar Multiplication

        public override string ToString() // To make it easier to print out stuff
        {
            return string.Format("({0:F3}, {1:F3}, {2:F3})", this.x, this.y, this.z);
        }

        // Equal Check
        public static bool operator ==(Vec3 a, Vec3 b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;

            return a.x == b.x && a.y == b.y && a.z == b.z;
        }
        public static bool operator !=(Vec3 a, Vec3 b) => !(a == b);
    }
}
