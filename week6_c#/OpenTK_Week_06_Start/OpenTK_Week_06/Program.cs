using CGPG;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace CGPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("If the square is off-centered. Please move the window slightly. It's a slight platform specific bug.");
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "CGPG - Creating a Window using OpenTK",
                Flags = ContextFlags.ForwardCompatible,
            };

            var renderer = new Renderer(GameWindowSettings.Default, nativeWindowSettings);

            float[] vertices = {
                // Front face
                -0.5f, -0.5f,  0.5f,  0f, 0f,
                0.5f, -0.5f,  0.5f,  1f, 0f,
                0.5f,  0.5f,  0.5f,  1f, 1f,
                -0.5f,  0.5f,  0.5f,  0f, 1f,
    
                // Back face
                0.5f, -0.5f, -0.5f,  0f, 0f,
                -0.5f, -0.5f, -0.5f,  1f, 0f,
                -0.5f,  0.5f, -0.5f,  1f, 1f,
                0.5f,  0.5f, -0.5f,  0f, 1f,
    
                // Left face
                -0.5f, -0.5f, -0.5f,  0f, 0f,
                -0.5f, -0.5f,  0.5f,  1f, 0f,
                -0.5f,  0.5f,  0.5f,  1f, 1f,
                -0.5f,  0.5f, -0.5f,  0f, 1f,
    
                // Right face
                0.5f, -0.5f,  0.5f,  0f, 0f,
                0.5f, -0.5f, -0.5f,  1f, 0f,
                0.5f,  0.5f, -0.5f,  1f, 1f,
                0.5f,  0.5f,  0.5f,  0f, 1f,
    
                // Top face
                -0.5f,  0.5f,  0.5f,  0f, 0f,
                0.5f,  0.5f,  0.5f,  1f, 0f,
                0.5f,  0.5f, -0.5f,  1f, 1f,
                -0.5f,  0.5f, -0.5f,  0f, 1f,
    
                // Bottom face
                -0.5f, -0.5f, -0.5f,  0f, 0f,
                0.5f, -0.5f, -0.5f,  1f, 0f,
                0.5f, -0.5f,  0.5f,  1f, 1f,
                -0.5f, -0.5f,  0.5f,  0f, 1f,
            };

            uint[] indices = {
                0, 1, 2, 2, 3, 0,         // Front
                4, 5, 6, 6, 7, 4,         // Back
                8, 9,10,10,11, 8,         // Left
                12,13,14,14,15,12,         // Right
                16,17,18,18,19,16,         // Top
                20,21,22,22,23,20          // Bottom
            };



            //Set the vertex and index array to the renderer.
            renderer.SetVertexArray(vertices);
            renderer.SetIndexArray(indices);

            // Uncomment the following segments to test out various things.
            //----- Test out the matrix rotation -------//
            Mat4 mymat_rotz = new Mat4();
            mymat_rotz.MakeRotate(0f, 0f, -45.0f);
            Console.WriteLine("mymat_scale");
            Console.Write(mymat_rotz.ToString());
            renderer.SetMatrix(mymat_rotz);

            //----- Test out the matrix scale -------//
            Mat4 mymat_scale = new Mat4();
            mymat_scale.MakeScale(1.0f, 2.0f, 1.0f);
            Console.WriteLine("mymat_scale");
            Console.Write(mymat_scale.ToString());
            renderer.SetMatrix(mymat_scale);

            //----- Test out the matrix translation -------//
            Mat4 mymat_translate = new Mat4();
            mymat_translate.MakeTranslate(2.0f, 1.75f, 0f);
            Console.WriteLine("my_translate");
            Console.Write(mymat_translate.ToString());
            renderer.SetMatrix(mymat_translate);

            //----- Test out the combined transformations matrix -------//
            Mat4 combined_mat = new Mat4();
            combined_mat.Mult(mymat_rotz, mymat_scale);
            combined_mat.Mult(mymat_translate, combined_mat);
            Console.WriteLine("combiend_mat");
            Console.Write(combined_mat.ToString());
            renderer.SetMatrix(combined_mat);

            renderer.Run();
        }
    }
}

