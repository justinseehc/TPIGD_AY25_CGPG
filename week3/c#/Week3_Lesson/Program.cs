using System.Diagnostics;

namespace Week3_Lesson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Vec3 Tests ===");

            Vec3 a = new Vec3(1, 2, 3);
            Vec3 b = new Vec3(4, 5, 6);
            Vec3 c = new Vec3(2, 7, 1);

            Console.WriteLine("A: " + a);
            Console.WriteLine("B: " + b);
            Console.WriteLine("C (Normalization): " + c);

            // Test: Addition
            //Vec3 add = a + b;
            //Console.WriteLine($"a + b = {add} (Expected: 5,7,9)");
            //Debug.Assert(add == new Vec3(5, 7, 9), "Addition failed");

            // Test: Subtraction
            //Vec3 sub = a - b;
            //Console.WriteLine($"a - b = {sub} (Expected: -3,-3,-3)");
            //Debug.Assert(sub == new Vec3(-3, -3, -3), "Subtraction failed");

            // Test: Normalization
            Vec3 norm = c / c;
            Console.WriteLine($"c = {norm} (Expected: -3,-3,-3)");
            //Debug.Assert(sub == new Vec3(-3, -3, -3), "Subtraction failed");
        }
    }
}
