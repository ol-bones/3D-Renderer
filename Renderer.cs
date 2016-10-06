using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;


namespace 3DRenderer
{
    class Renderer : GameWindow
    {
        public Renderer() : base 
        (
            800, // Width
            600, // Height
            GraphicsMode.Default,
            "Renderer Test",
            GameWindowFlags.Default,
            DisplayDevice.Default,
            3, // major
            3, // minor
            GraphicsContextFlags.ForwardCompatible
        )
        {
        }

        public ShaderUtility TextureShader, CurrentShader;
        private EKeyboard mKeyboard;

        private static TcpListener listener;
        List<Scene> Scenes;
        Dictionary<int, Camera> Cameras;

        protected override void OnLoad(EventArgs e)
        {
            listener = new TcpListener(IPAddress.Any, 43);
            listener.Start();
            GL.ClearColor(Color4.Gray);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            TextureShader = new ShaderUtility(@"Shaders/TextureVert.vert", @"Shaders/TextureFrag.frag");
            CurrentShader = TextureShader;
            GL.UseProgram(CurrentShader.ShaderProgramID);

            Scenes = new List<Scene>();

            Scene Scene1 = new Scene();
            Scenes.Add(Scene1);

            mKeyboard = new EKeyboard();

            CreateColumnScene(Scene1);
            Cameras = new Dictionary<int, Camera>();
            Vector3 CameraPos = new Vector3(-5.0f, -5.0f, -1.0f);

            Cameras.Add(0, CreateFixedCamera(CameraPos));
            Cameras.Add(1, CreateFlyingCamera(CameraPos));
            Cameras.Add(2, CreatePathCamera(CameraPos));
            Cameras.Add(3, CreateFollowCamera(CameraPos));

            foreach (Camera c in Cameras.Values)
            {
                Scene1.Add(c);
            }
            Cameras[1].Activate();

            foreach (Scene scene in Scenes)
            {
                scene.Init();

                int uDirectionalLightCount = GL.GetUniformLocation(CurrentShader.ShaderProgramID, "uDirectionalLightCount");
                GL.Uniform1(uDirectionalLightCount, scene.num_DirLights);
                int uPositionalLightCount = GL.GetUniformLocation(CurrentShader.ShaderProgramID, "uPositionalLightCount");
                GL.Uniform1(uPositionalLightCount, scene.num_PosLights);
                int uAmbientLightCount = GL.GetUniformLocation(CurrentShader.ShaderProgramID, "uAmbientLightCount");
                GL.Uniform1(uAmbientLightCount, scene.num_Ambientights);
            }

            base.OnLoad(e);
        }

        FixedCamera CreateFixedCamera(Vector3 Pos)
        {
            FixedCamera f = new FixedCamera(ref Pos, ref mKeyboard, ref CurrentShader);
            return f;
        }

        FlyingCamera CreateFlyingCamera(Vector3 Pos)
        {
            FlyingCamera f = new FlyingCamera(ref Pos, ref mKeyboard, ref CurrentShader);
            f.MoveSpeed = 2.5f;
            return f;
        }

        PathCamera CreatePathCamera(Vector3 Pos)
        {
            PathCamera c = new PathCamera(ref Pos, ref mKeyboard, ref CurrentShader);
            c.MoveSpeed = 10.0f;
            
            c.AddPath(new Vector3(-5.0f, -5.0f, -1.0f));
            c.AddPath(new Vector3(-5.0f, -4.0f, -1.0f));
            c.AddPath(new Vector3(-5.0f, -3.0f, -1.0f));
            c.AddPath(new Vector3(-5.0f, -2.0f, -1.0f));
            c.AddPath(new Vector3(-5.0f, -1.0f, -1.0f));

            return c;
        }

        FollowCamera CreateFollowCamera(Vector3 Pos)
        {
            FollowCamera f = new FollowCamera(ref Pos, ref mKeyboard, ref CurrentShader);
            f.MoveSpeed = 10.0f;
            return f;
        }

        public void CreateColumnScene(Scene Scene)
        {
            Scene.Add(new EmitterBox(new Vector3(5.0f, 5.0f, 0.0f)));

            Scene.Add(new GridBox1(new Vector3(5.0f, 4.0f, 0.0f)));
            Scene.Add(new Cylinder(new Vector3(5.0f - 0.17f, 4.0f, 0.0f), 0.15f));
            Scene.Add(new Cylinder(new Vector3(5.0f - 0.17f + 0.43f, 4.0f, 0.0f), 0.075f));

            Cylinder c3 = new Cylinder(new Vector3(5.0f - 0.0f, 4.0f, -0.25f), 0.075f);
            Scene.Add(c3);
            c3.Rotate(new Vector3(0.0f, (float)(Math.PI / 2), 0.0f));

            Cylinder c4 = new Cylinder(new Vector3(5.0f + 0.0f, 4.0f, 0.25f), 0.075f);
            Scene.Add(c4);
            c4.Rotate(new Vector3(0.0f, (float)(Math.PI / 2), 0.0f));

            Scene.Add(new GridBox2(new Vector3(5.0f, 3.0f, 0.0f)));

            Cylinder c5 = new Cylinder(new Vector3(5.0f + 0.0f, 3.0f, 0.0f), 0.15f);
            Scene.Add(c5);
            c5.Rotate(new Vector3(0.0f, (float)(Math.PI / 4), 0.0f));

            Cylinder c6 = new Cylinder(new Vector3(5.0f + 0.0f, 3.0f, 0.0f), 0.10f);
            Scene.Add(c6);
            c6.Rotate(new Vector3(0.0f, (float)-(Math.PI / 4), 0.0f));

            Scene.Add(new DoomBox(new Vector3(5.0f, 2.0f, 0.0f)));
            Scene.Add(new BallOfDoom(new Vector3(5.0f, 2.0f, 0.0f), 0.35f));

            Scene.Add(new PortalEntranceBox(new Vector3(5.0f, 1.0f, 0.0f)));
            PortalExit exit = new PortalExit(new Vector3(5.01f, 5.0f, 0.0f));
            Scene.Add(exit);

            Scene.Add(new PortalEntrance(new Vector3(5.0f, 1.001f, 0.0f), exit));

            Scene.Add(new PositionalLight(
                        new Vector4(5.0f, 2.0f, 0.0f, 1.0f),
                        new Vector3(0.0f, 1.0f, 0.0f),
                        0.5f,
                        CurrentShader.ShaderProgramID
            ));

            Scene.Add(new PositionalLight(
                        new Vector4(5.0f, 5f, 0.0f, 1.0f),
                        new Vector3(1.0f, 0.0f, 0.0f),
                        0.5f,
                        CurrentShader.ShaderProgramID
            ));

            Scene.Add(new PositionalLight(
                        new Vector4(5.0f, 3.5f, 0.0f, 1.0f),
                        new Vector3(0.0f, 0.0f, 1.0f),
                        0.5f,
                        CurrentShader.ShaderProgramID
            ));
            Scene.Add(new AmbientLight(
                new Vector3(1.0f, 1.0f, 1.0f),
                0.1f,
                CurrentShader.ShaderProgramID
            ));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(this.ClientRectangle);
            if (CurrentShader != null)
            {
                int uProjectionLocation = GL.GetUniformLocation(CurrentShader.ShaderProgramID, "uProjection");
                Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(1, (float)ClientRectangle.Width / ClientRectangle.Height, 0.5f, 25);
                GL.UniformMatrix4(uProjectionLocation, true, ref projection);
            }
        }

        void DeactivateAllCameras()
        {
            foreach (Camera c in Cameras.Values)
            {
                c.Deactivate();
            }
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            string key = e.Key.ToString();
            mKeyboard.OnKeyDown(key);
            foreach (Scene s in Scenes) { s.OnKeyDown(key); }
            switch(key)
            {
                case "Number1":
                    DeactivateAllCameras();
                    Cameras[0].Activate();
                    break;
                case "Number2":
                    DeactivateAllCameras();
                    Cameras[1].Activate();
                    break;
                case "Number3":
                    DeactivateAllCameras();
                    Cameras[2].Activate();
                    break;
                case "Number4":
                    DeactivateAllCameras();
                    Cameras[3].Activate();
                    break;
            }
        }

        protected override void OnKeyUp(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            foreach (Scene s in Scenes) { s.OnKeyUp(e.Key.ToString()); }
            mKeyboard.OnKeyUp(e.Key.ToString());
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            float gameTime = (float)e.Time;
            Socket socket = listener.AcceptSocket();

            Console.WriteLine("{0} - - [{1}] Client connected", socket.RemoteEndPoint, DateTime.Now);

            foreach (Scene scene in Scenes)
            {
                scene.Update(gameTime);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            {
                foreach (Scene scene in Scenes)
                {
                    scene.Draw();
                }
            }
            this.SwapBuffers();
        }

        protected override void OnUnload(EventArgs e)
        {
            foreach (Scene s in Scenes) { s.Delete(); }
            Scenes.Clear();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
            CurrentShader.Delete();
            base.OnUnload(e);
        }
    }
}
