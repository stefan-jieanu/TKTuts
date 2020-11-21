using System;

namespace TKTuts
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Window win = new Window(Window.WIDTH, Window.HEIGHT, "LearnOpenTK"))
            {
                win.Run();
            }

            
        }
    }
}
