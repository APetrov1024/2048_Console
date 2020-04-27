using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048_Model;

namespace Game2048_View
{
    public class View
    {
        private int hCellSize = 7;
        private int vCellSize = 5;
        private int messageY;
        private Model model;

        public View(Model model)
        {
            this.model = model;
            this.messageY = vCellSize * model.Size + 1;
        }
        public void Display()
        {
            Console.Clear();
            ShowGrid(model.Size);
            Console.SetCursorPosition(0, this.messageY);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Управляйте клавишами стрелок. Для выхода нажмите Esc. Для перезапуска игры нажмите F5.");
        }

        public void DisplayWinMessage()
        {
            Console.SetCursorPosition(0, this.messageY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Победа!!! Для выхода нажмите Esc. Для перезапуска игры нажмите F5.                    ");
        }

        public void DisplayFailMessage()
        {
            Console.SetCursorPosition(0, this.messageY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ходов больше нет! Для выхода нажмите Esc. Для перезапуска игры нажмите F5.             ");
        }

        private void ShowGrid(int size)
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    ShowGridCell(i, j);
                }
        }

        private void ShowGridCell(int left, int top)
        {
            for (int i = 0; i < hCellSize; i++)
                for (int j = 0; j < vCellSize; j++)
                {
                    if (i == 0 || j == 0 || i == hCellSize - 1 || j == vCellSize - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(left * (hCellSize - 1) + i, top * (vCellSize - 1) + j);
                        Console.Write("*");
                        if (model.Get(new Coordinates(left, top)) > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(left * (hCellSize - 1) + 2, top * (vCellSize - 1) + 2);
                            Console.Write(model.Get(new Coordinates(left, top)));
                        }
                    }
                }
        }

    }
}
