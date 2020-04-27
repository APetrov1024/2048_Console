using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048_Controller
{
    class InputHandler
    {
        public Actions Listen()
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    return Actions.Up;
                    break;
                case ConsoleKey.DownArrow:
                    return Actions.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    return Actions.Left;
                    break;
                case ConsoleKey.RightArrow:
                    return Actions.Right;
                    break;
                case ConsoleKey.Escape:
                    return Actions.Exit;
                    break;
                case ConsoleKey.F5:
                    return Actions.Restart;
                default:
                    return Actions.None;
            }
        } 
    }
}
