// Released to the public domain. Use, modify and relicense at will.

using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using OpenTK.Input;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
using MdxLib.Model;
using MdxLib.ModelFormats;
using System.IO;
using System.Linq;
using MdxLib.Primitives;
using System.Diagnostics;

namespace OGLTest
{
    class TestGameWindow : GameWindow
    {
        public WScene Scene;
        public bool IsActive = true;
        Stopwatch UpdateStopwatch = new Stopwatch();

        public TestGameWindow()
            : base(640, 480, GraphicsMode.Default, "OpenTK Test", GameWindowFlags.Default, DisplayDevice.Default, 3, 1, GraphicsContextFlags.Default)
        {
            VSync = VSyncMode.Off;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Scene = new WScene(this);
            Scene.Initialize();
        }

        protected override void OnUnload(EventArgs e)
        {
            Scene.Dispose();
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            if (!Focused && Scene.Input.CaptureInput)
                Scene.Input.EnableInputCapture(false);

            if (IsActive != Focused)
                IsActive = Focused;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            Scene.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1f, 30000.0f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            UpdateStopwatch.Start();
            double Time;
            if (IsActive)
            {
                base.OnUpdateFrame(e);
                UpdateStopwatch.Stop();
                Time = UpdateStopwatch.Elapsed.TotalSeconds;
                UpdateStopwatch.Reset();
                UpdateStopwatch.Start();
                Scene.Update(Time);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            if (IsActive)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                Scene.Render();               
                SwapBuffers();
            }
        }

        [STAThread]
        static void Main()
        {
            using (TestGameWindow game = new TestGameWindow())
            {
                game.Run();
            }
        }
    }
}