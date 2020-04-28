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
            this.messageY = vCellSize * model.VSize + 1;
        }
        public void Display()
        {
            Console.Clear();
            ShowGrid(this.model.HSize, this.model.VSize);
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

        private void ShowGrid(int hSize, int vSize)
        {
            for (int i = 0; i < hSize; i++)
                for (int j = 0; j < vSize; j++)
                {
                    ShowGridCell(i, j);
                }
        }

        private void ShowGridCell(int left, int top)
        {
            for (int i = 0; i < this.hCellSize; i++)
                for (int j = 0; j < this.vCellSize; j++)
                {
                    if (i == 0 || j == 0 || i == this.hCellSize - 1 || j == this.vCellSize - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(left * (this.hCellSize - 1) + i, top * (this.vCellSize - 1) + j);
                        Console.Write("*");
                        if (this.model.Get(new Coordinates(left, top)) > 0)
                        {
                            Coordinates displayedValueCoords = new Coordinates(left, top);
                            Console.ForegroundColor = ConsoleColor.Green;
                            if (this.model.LastGeneratedTileCoords.Equals(displayedValueCoords))
                                Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(left * (this.hCellSize - 1) + 2, top * (this.vCellSize - 1) + 2);
                            Console.Write(this.model.Get(displayedValueCoords));
                        }
                    }
                }
        }

    }
}
