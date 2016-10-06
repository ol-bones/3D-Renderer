using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace 3DRenderer
{
    static class Program
    {
        public static Renderer e;
        static void Main(string[] args)
        {
            using (e = new Renderer())
            {
                Console.WriteLine("CONTROLS:");
                Console.WriteLine("- Cameras:");
                Console.WriteLine("       - Fixed = 1\n       - UserControlled = 2\n       - Path = 3\n       - FollowObject = 4");
                Console.WriteLine("         - + and - to change which object to follow");
                Console.WriteLine("         - W A S D Space and Alt to move UserControlled camera");
                e.VSync = VSyncMode.Adaptive;
                e.Run(60.0f, 0.0f);
            }
        }
        /*
        public static void CheckForGLErrors(string pMessage = "")
        {

            ErrorCode error = GL.GetError();

            if (error != ErrorCode.NoError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(pMessage + " GL Error Code : " + error.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                //Console.WriteLine(pMessage + ": No error");
            }
        }
        */
    }
}
