using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using OpenTK;
using System.Drawing;

namespace OGLTest
{
    public class WInput
    {
        public bool NoClip = true;
        public float DefaultCameraSpeed = 1000;
        public Vector3 CamPos = new Vector3(440, -90, 140);
        public float CRX = 2.4f;
        public float CRZ = -0.2f;
        public Matrix4 ModelViewMatrix { get; set; }
        public bool CaptureInput;
        public GameWindow Window;

        public WInput()
        {
            this.Window = WScene.Current.Window;
            Window.KeyDown += Keyboard_KeyDown;
            UpdateModelViewMatrix();
        }

        public void EnableInputCapture(bool p)
        {
            if (p)
                System.Windows.Forms.Cursor.Hide();
            else
                System.Windows.Forms.Cursor.Show();
            CaptureInput = p;
        }

        public void ResetMouseCursor()
        {
            Point Cursor = new Point(Window.Width / 2, Window.Height / 2);
            Cursor = Window.PointToScreen(Cursor);
            System.Windows.Forms.Cursor.Position = Cursor;
        }


        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.N && CaptureInput)
                NoClip = !NoClip;

            var keyboard = Keyboard.GetState();
            if (e.Key == Key.Enter && keyboard[Key.AltRight])
            {
                if (Window.WindowState != OpenTK.WindowState.Fullscreen)
                    Window.WindowState = OpenTK.WindowState.Fullscreen;
                else if (Window.WindowState == OpenTK.WindowState.Fullscreen)
                    Window.WindowState = OpenTK.WindowState.Normal;
                if (!CaptureInput)
                {
                    EnableInputCapture(true);
                    ResetMouseCursor();
                }
            }
        }

        public void Update(double Time)
        {
            var mouse = Mouse.GetState();
            var keyboard = Keyboard.GetState();
            if (mouse[MouseButton.Left] && !CaptureInput)
            {
                EnableInputCapture(true);
                ResetMouseCursor();
            }

            if (keyboard[Key.Escape])
                Window.Exit();

            if (CaptureInput)
            {
                float DY;
                if (NoClip)
                    DY = (float)Math.Sin(CRZ);
                else
                    DY = 0;
                float CameraSpeed = DefaultCameraSpeed;
                if (keyboard[Key.ShiftLeft])
                    CameraSpeed /= 10;

                Vector3 LookAtVector = new Vector3(
                    (float)Math.Cos(CRX) * (float)Math.Cos(CRZ) * CameraSpeed * (float)Time,
                    (float)Math.Sin(CRX) * (float)Math.Cos(CRZ) * CameraSpeed * (float)Time,
                    DY * CameraSpeed * (float)Time);

                Vector3 SidewaysVector = new Vector3(
                    (float)Math.Sin(CRX) * CameraSpeed * (float)Time,
                    (float)Math.Cos(CRX + MathHelper.Pi) * CameraSpeed * (float)Time,
                    0);

                bool NeedToUpdateMatrix = false;
                Vector3 PreCamPos = CamPos;
                if (keyboard[Key.W])
                    CamPos += LookAtVector;
                if (keyboard[Key.S])
                    CamPos -= LookAtVector;
                if (keyboard[Key.A])
                    CamPos -= SidewaysVector;
                if (keyboard[Key.D])
                    CamPos += SidewaysVector;
                if (keyboard[Key.Space])
                    CamPos += new Vector3(0.0f, 0.0f, CameraSpeed * (float)Time);
                if (keyboard[Key.C])
                    CamPos -= new Vector3(0.0f, 0.0f, CameraSpeed * (float)Time);

                if (CamPos != PreCamPos)
                    NeedToUpdateMatrix = true;
                Point Cursor = Window.PointToClient(System.Windows.Forms.Cursor.Position);
                // Mouse state has changed
                int XDelta = Window.Width / 2 - Cursor.X;
                int YDelta = Window.Height / 2 - Cursor.Y;

                if (XDelta != 0)
                {
                    CRX = CRX + (float)XDelta / 1000f;
                    if (CRX > 2f * (float)Math.PI)
                        CRX -= 2f * (float)Math.PI;
                    if (CRX < -2f * (float)Math.PI)
                        CRX += 2f * (float)Math.PI;
                    NeedToUpdateMatrix = true;
                }
                if (YDelta != 0)
                {
                    CRZ = CRZ + (float)YDelta / 1000f;
                    if (CRZ >= (float)Math.PI / 2f)
                        CRZ = (float)Math.PI / 2f;
                    if (CRZ <= (float)Math.PI / -2f)
                        CRZ = (float)Math.PI / -2f;
                    NeedToUpdateMatrix = true;
                }
                ResetMouseCursor();
                if (NeedToUpdateMatrix)
                    UpdateModelViewMatrix();

                if (keyboard[Key.ControlLeft] && keyboard[Key.AltLeft])
                {
                    EnableInputCapture(false);
                }
            }
        }
        private void UpdateModelViewMatrix()
        {
            Vector3 LookAtVector = new Vector3(
                (float)(Math.Cos(CRX) * Math.Cos(CRZ)),
                (float)(Math.Sin(CRX) * Math.Cos(CRZ)),
                (float)Math.Sin(CRZ));
            //CRZ += (float)Math.PI / 4f;
            Vector3 UpVector = new Vector3(
                (float)(Math.Cos(CRX) * Math.Cos(CRZ + (float)Math.PI / 2f)),
                (float)(Math.Sin(CRX) * Math.Cos(CRZ + (float)Math.PI / 2f)),
                (float)Math.Sin(CRZ + (float)Math.PI / 2f));
            ModelViewMatrix = Matrix4.LookAt(CamPos, CamPos + LookAtVector, UpVector);
        }
    }
}
