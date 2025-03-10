using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Classes
{
    public class Cell
    {
        public bool IsBomb { get; private set; }

        public Cell(float density)
        {
            var rng = new Random();
            IsBomb = rng.NextDouble() < density;
        }

        private Cell() {}
    }
}
