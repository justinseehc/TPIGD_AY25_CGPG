using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using StbImageSharp;
using System;
using System.IO;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CGPG
{
    internal class Renderer : GameWindow
    {
        private int _vao, _vbo, _ebo;
        private int _shaderProgram;
        private int _texture;
        private Matrix4 _projection;
        private Mat4 _customMatrix = new Mat4();

        private float[] _vertices;
        private uint[] _indices;

        public Renderer(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        public void SetVertexArray(float[] vertices) => _vertices = vertices;
        public void SetIndexArray(uint[] indices) => _indices = indices;
        public void SetMatrix(Mat4 transformationMatrix) => _customMatrix = new Mat4(transformationMatrix);


        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.CornflowerBlue);

            // Create shader program
            _shaderProgram = CreateShaderProgram();

            // Load texture
            _texture = LoadTexture("texture.jpg");

            // Setup buffers
            _vao = GL.GenVertexArray();
            _vbo = GL.GenBuffer();
            _ebo = GL.GenBuffer();

            GL.BindVertexArray(_vao);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            // Position attribute (location = 0)
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Texture coord attribute (location = 1)
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0);
            OnResize(new ResizeEventArgs(ClientSize));
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(_shaderProgram);

            // int projLoc = GL.GetUniformLocation(_shaderProgram, "uProjection");
            // GL.UniformMatrix4(projLoc, false, ref _projection);
            int projLoc = GL.GetUniformLocation(_shaderProgram, "uProjection");
            Matrix4 custom = Mat4.ConvertToMatrix4(_customMatrix);
            Matrix4 final = custom * _projection; // Apply transform, then projection
            GL.UniformMatrix4(projLoc, false, ref final);


            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.BindVertexArray(_vao);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GL.DeleteTexture(_texture);
            GL.DeleteProgram(_shaderProgram);
            GL.DeleteBuffer(_vbo);
            GL.DeleteBuffer(_ebo);
            GL.DeleteVertexArray(_vao);
        }

        private int CreateShaderProgram()
        {
            string vertexShaderSource = @"
                #version 330 core
                layout(location = 0) in vec3 aPosition;
                layout(location = 1) in vec2 aTexCoord;

                uniform mat4 uProjection;

                out vec2 TexCoord;

                void main()
                {
                    gl_Position = uProjection * vec4(aPosition, 1.0);
                    TexCoord = aTexCoord;
                }
            ";

            string fragmentShaderSource = @"
                #version 330 core
                out vec4 FragColor;

                in vec2 TexCoord;
                uniform sampler2D texture0;

                void main()
                {
                    FragColor = texture(texture0, TexCoord);
                }
            ";

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
                throw new Exception("Vertex shader compile error: " + GL.GetShaderInfoLog(vertexShader));

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
                throw new Exception("Fragment shader compile error: " + GL.GetShaderInfoLog(fragmentShader));

            int program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
                throw new Exception("Shader linking error: " + GL.GetProgramInfoLog(program));

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            return program;
        }

        private int LoadTexture(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Texture file not found: {path}");

            StbImage.stbi_set_flip_vertically_on_load(1); // Flip image to match OpenGL

            using var stream = File.OpenRead(path);
            var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            int texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return texture;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

            float aspectRatio = ClientSize.X / (float)ClientSize.Y;
            // _projection = Matrix4.CreateOrthographicOffCenter(-aspectRatio, aspectRatio, -1, 1, -1, 1);
            _projection = Matrix4.CreateOrthographic(aspectRatio, 1, -1, 1);
        }


        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }
    }
}
