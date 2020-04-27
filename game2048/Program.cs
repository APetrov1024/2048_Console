using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048_Model;
using Game2048_Controller;
using Game2048_View;


namespace game2048
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.StartGame();   
        }
    }
}
