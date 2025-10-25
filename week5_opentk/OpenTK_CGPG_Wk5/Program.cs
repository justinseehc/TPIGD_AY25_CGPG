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
            Console.WriteLine("You may need to reposition the window after every resize on certain platforms (e.g. mac)");
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "CGPG - Creating a Window using OpenTK",
                Flags = ContextFlags.ForwardCompatible,
            };

            var renderer = new Renderer(GameWindowSettings.Default, nativeWindowSettings);

            float[] vertices =
            {
                // Position         Texture coordinates
                0.2f, 0.2f, 0.0f, 1.0f, 1.0f, // top right
                0.2f, -0.2f, 0.0f, 1.0f, 0.0f, // bottom right
                -0.2f, -0.2f, 0.0f, 0.0f, 0.0f, // bottom left
                -0.2f, 0.2f, 0.0f, 0.0f, 1.0f  // top left
            };

            uint[] indices =
            {
                0, 1, 3,
                1, 2, 3
            };

            //Set the vertex and index array to the renderer.
            renderer.SetVertexArray(vertices);
            renderer.SetIndexArray(indices);

            //----- Test out the matrix translation -------//
            // Mat4 mymat_translate = new Mat4();
            // mymat_translate.MakeTranslate(-0.25f, 0.25f, 0.0f);
            // Console.WriteLine("my_translate");
            // Console.Write(mymat_translate.ToString());
            // renderer.SetMatrix(mymat_translate);

            //----- Test out the matrix scale -------//
            // Mat4 mymat_scale = new Mat4();
            // mymat_scale.MakeScale(2.0f, 0.5f, 1.0f);
            // Console.WriteLine("mymat_scale");
            // Console.Write(mymat_scale.ToString());
            // renderer.SetMatrix(mymat_scale);

            renderer.Run();
        }
    }
}

