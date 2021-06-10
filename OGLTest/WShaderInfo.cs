using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLTest
{
    public class WShaderInfo
    {
        public struct Shader1Info
        {
            public int in_Vertex;
            public int in_Normal;
            public int in_TexCoord;
            public int in_BoneIndexes1;
            public int in_BoneIndexes2;
            public int in_Bones;
            public int in_IsAnimated;
            public int in_MVP;
            public int in_ObjectPos;
            public int Handle;
        }

        public struct FragmentShader1Info
        {
            public int Handle;
        }

        public int Program1;
        public Shader1Info Shader1;
        public FragmentShader1Info FragmentShader1;

        private void CheckShaderCompiled(int Shader)
        {
            string info;
            int status_code;
            GL.GetShaderInfoLog(Shader, out info);
            GL.GetShader(Shader, ShaderParameter.CompileStatus, out status_code);
            if (status_code != 1)
                throw new ApplicationException(info);
        }

        public void Initialize()
        {      
            Shader1.Handle = GL.CreateShader(ShaderType.VertexShader);
            FragmentShader1.Handle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(Shader1.Handle, Properties.Resources.VertexShader1);
            GL.ShaderSource(FragmentShader1.Handle, Properties.Resources.FragmentShader1);
            GL.CompileShader(Shader1.Handle);
            GL.CompileShader(FragmentShader1.Handle);
            CheckShaderCompiled(Shader1.Handle);
            CheckShaderCompiled(FragmentShader1.Handle);

            Program1 = GL.CreateProgram();
            GL.AttachShader(Program1, Shader1.Handle);
            GL.AttachShader(Program1, FragmentShader1.Handle);

            GL.LinkProgram(Program1);

            Shader1.in_Vertex = GL.GetAttribLocation(Program1, "in_Vertex");
            Shader1.in_Normal = GL.GetAttribLocation(Program1, "in_Normal");
            Shader1.in_TexCoord = GL.GetAttribLocation(Program1, "in_TexCoord");
            Shader1.in_BoneIndexes1 = GL.GetAttribLocation(Program1, "in_BoneIndexes1");
            Shader1.in_BoneIndexes2 = GL.GetAttribLocation(Program1, "in_BoneIndexes2");

            Shader1.in_Bones = GL.GetUniformLocation(Program1, "in_Bones");
            Shader1.in_IsAnimated = GL.GetUniformLocation(Program1, "in_IsAnimated");
            Shader1.in_MVP = GL.GetUniformLocation(Program1, "in_MVP");
            Shader1.in_ObjectPos = GL.GetUniformLocation(Program1, "in_ObjectPos");
        }
    }
}
