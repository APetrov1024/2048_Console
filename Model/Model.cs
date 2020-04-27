using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game2048_Model
{
    public class Model
    {
        public enum Directions { Left, Right, Up, Down };
        private Field field;
        private Random rndNum;
        private bool isMoved;


        public int Size
        {
            get
            {
                return this.field.HSize;
            }
        }


        public Model(int fieldSize)
        {
            rndNum = new Random();
            this.field = new Field(fieldSize);
            this.field.Clear();
            GenerateNewTile();
            GenerateNewTile();
        }

        public void ClearField()
        {
            this.field.Clear();
            GenerateNewTile();
            GenerateNewTile();
        }

        public int Get(Coordinates coords)
        {
            return this.field.Get(coords);
        }

        /*public void Set(Coordinates coords, int value)
        {
            this.field.Set(coords, value);
        }*/

        public void Action(Directions direction)
        {
            this.isMoved = false;
            MoveTiles(direction);
            MergeTiles(direction);
            if (this.isMoved && this.field.HaveEmptyCells())
                GenerateNewTile();
        }

        public bool IsHaveValue(int value)
        {
            return this.field.HaveCellWithValue(value);
        }

        public bool IsHasNotMoves()
        {
            if (this.field.HaveEmptyCells())
                return false;
            for (int i = 0; i < this.field.HSize; i++)
                for (int j = 0; j < this.field.VSize; j++)
                {
                    int curValue = this.field.Get(new Coordinates(i, j));
                    for (int k = i - 1; k <= i + 1; k++)
                        for (int n = j - 1; n <= j + 1; n++)
                            if (k != n)
                            {
                                Coordinates neighborCoord = new Coordinates(n, k);
                                if (this.field.IsOnField(neighborCoord) && (this.field.Get(neighborCoord) == curValue))
                                    return false;
                            }
                }
            return true;
        }

        private void MergeTiles(Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    MergeTilesUp();
                    break;
                case Directions.Down:
                    MergeTilesDown();
                    break;
                case Directions.Left:
                    MergeTilesLeft();
                    break;
                case Directions.Right:
                    MergeTilesRight();
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private void MergeTilesUp()
        {
            for (int i = 0; i < this.field.HSize; i++)
                for (int j = 0; j < this.field.VSize - 1; j++)
                {
                    Coordinates curCoords = new Coordinates(i, j);
                    Coordinates nextCoords = new Coordinates(i, j + 1);
                    int curValue = this.field.Get(curCoords);
                    int nextValue = this.field.Get(nextCoords);
                    if (curValue != 0 && curValue == nextValue)
                    {
                        this.field.Set(curCoords, 2 * curValue);
                        this.field.Set(nextCoords, 0);
                    }
                }
            // при объединении появились нули между значениями
            MoveTilesUp();
        }

        private void MergeTilesDown()
        {
            for (int i = 0; i < this.field.HSize; i++)
                for (int j = this.field.VSize - 1; j > 0; j--)
                {
                    Coordinates curCoords = new Coordinates(i, j);
                    Coordinates nextCoords = new Coordinates(i, j - 1);
                    int curValue = this.field.Get(curCoords);
                    int nextValue = this.field.Get(nextCoords);
                    if (curValue != 0 && curValue == nextValue)
                    {
                        this.field.Set(curCoords, 2 * curValue);
                        this.field.Set(nextCoords, 0);
                    }
                }
            // при объединении появились нули между значениями
            MoveTilesDown();
        }

        private void MergeTilesLeft()
        {
            for (int i = 0; i < this.field.HSize - 1; i++)
                for (int j = 0; j < this.field.VSize; j++)
                {
                    Coordinates curCoords = new Coordinates(i, j);
                    Coordinates nextCoords = new Coordinates(i + 1, j);
                    int curValue = this.field.Get(curCoords);
                    int nextValue = this.field.Get(nextCoords);
                    if (curValue != 0 && curValue == nextValue)
                    {
                        this.field.Set(curCoords, 2 * curValue);
                        this.field.Set(nextCoords, 0);
                    }
                }
            // при объединении появились нули между значениями
            MoveTilesLeft();
        }

        private void MergeTilesRight()
        {
            for (int i = this.field.HSize - 1; i > 0; i--)
                for (int j = 0; j < this.field.VSize; j++)
                {
                    Coordinates curCoords = new Coordinates(i, j);
                    Coordinates nextCoords = new Coordinates(i - 1, j);
                    int curValue = this.field.Get(curCoords);
                    int nextValue = this.field.Get(nextCoords);
                    if (curValue != 0 && curValue == nextValue)
                    {
                        this.field.Set(curCoords, 2 * curValue);
                        this.field.Set(nextCoords, 0);
                    }
                }
            // при объединении появились нули между значениями
            MoveTilesRight();
        }

        private void MoveTiles(Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    MoveTilesUp();
                    break;
                case Directions.Down:
                    MoveTilesDown();
                    break;
                case Directions.Left:
                    MoveTilesLeft();
                    break;
                case Directions.Right:
                    MoveTilesRight();
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private void MoveTilesUp()
        {
            for (int i = 0; i < this.field.HSize; i++)
                for (int j = 1; j < this.field.VSize; j++)
                {
                    if (this.field.Get(new Coordinates(i, j)) != 0)
                    {
                        Coordinates newCoords = new Coordinates(i, j - 1);
                        Coordinates curCoords = new Coordinates(i, j);
                        while (this.field.IsOnField(newCoords) && (this.field.Get(newCoords) == 0))
                        {
                            this.field.Set(newCoords, this.field.Get(curCoords));
                            this.field.Set(curCoords, 0);
                            curCoords.Set(newCoords);
                            newCoords.Vertical--;
                            this.isMoved = true;
                        }
                    }
                }
        }

        private void MoveTilesDown()
        {
            for (int i = 0; i < this.field.HSize; i++)
                for (int j = this.field.VSize - 2; j >= 0; j--)
                {
                    if (this.field.Get(new Coordinates(i, j)) != 0)
                    {
                        Coordinates newCoords = new Coordinates(i, j + 1);
                        Coordinates curCoords = new Coordinates(i, j);
                        while (this.field.IsOnField(newCoords) && (this.field.Get(newCoords) == 0))
                        {
                            this.field.Set(newCoords, this.field.Get(curCoords));
                            this.field.Set(curCoords, 0);
                            curCoords.Set(newCoords);
                            newCoords.Vertical++;
                            this.isMoved = true;
                        }
                    }
                }
        }

        private void MoveTilesRight()
        {
            for (int j = 0; j < this.field.VSize; j++)
                for (int i = this.field.HSize - 2; i >= 0; i--)
                {
                    if (this.field.Get(new Coordinates(i, j)) != 0)
                    {
                        Coordinates newCoords = new Coordinates(i + 1, j);
                        Coordinates curCoords = new Coordinates(i, j);
                        while (this.field.IsOnField(newCoords) && (this.field.Get(newCoords) == 0))
                        {
                            this.field.Set(newCoords, this.field.Get(curCoords));
                            this.field.Set(curCoords, 0);
                            curCoords.Set(newCoords);
                            newCoords.Horizontal++;
                            this.isMoved = true;
                        }
                    }
                }
        }

        private void MoveTilesLeft()
        {
            for (int j = 0; j < this.field.VSize; j++)
                for (int i = 1; i < this.field.HSize; i++)
                {
                    if (this.field.Get(new Coordinates(i, j)) != 0)
                    {
                        Coordinates newCoords = new Coordinates(i - 1, j);
                        Coordinates curCoords = new Coordinates(i, j);
                        while (this.field.IsOnField(newCoords) && (this.field.Get(newCoords) == 0))
                        {
                            this.field.Set(newCoords, this.field.Get(curCoords));
                            this.field.Set(curCoords, 0);
                            curCoords.Set(newCoords);
                            newCoords.Horizontal--;
                            this.isMoved = true;
                        }
                    }
                }
        }

        private void GenerateNewTile()
        {
            bool isSuccess = false;
            int horCoord, vertCoord;
            int newValue;
            if (rndNum.Next(0, 100) <= 70)
                newValue = 2;
            else
                newValue = 4;
            while (!isSuccess)
            {
                horCoord = rndNum.Next(0, this.Size);
                vertCoord = rndNum.Next(0, this.Size);
                if (this.field.Get(new Coordinates(horCoord, vertCoord)) == 0)
                {
                    this.field.Set(new Coordinates(horCoord, vertCoord), newValue);
                    isSuccess = true;
                }
            }
        }

    }
}
