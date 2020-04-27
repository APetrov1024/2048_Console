using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048_Model
{
    public class Coordinates
    {
        public int Horizontal { get; set; }
        public int Vertical { get; set; }

        public Coordinates() { }

        public Coordinates(int horizontal, int vertical)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        public void Set(Coordinates newCoodinates)
        {
            this.Horizontal = newCoodinates.Horizontal;
            this.Vertical = newCoodinates.Vertical;
        }

        public override string ToString()
        {
            return "H: " + this.Horizontal + "; V: " + this.Vertical;
        }
    }
}
