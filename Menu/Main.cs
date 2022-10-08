using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardMenuDemo
{
    class Program
    {
        public static void Main()
        {
            Console.CursorVisible = false;
            Game myGame = new Game();
            Settings mySettings = new Settings();
            myGame.Start();
        }
    }
}
