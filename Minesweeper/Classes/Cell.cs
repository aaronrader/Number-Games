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
        public ushort Adjacent { get; internal set; } = 0;
        public bool IsRevealed { get; set; } = false;

        public ushort Row { get; private set; }
        public ushort Column { get; private set; }

        public Cell(float density, ushort row, ushort column)
        {
            var rng = new Random();
            IsBomb = rng.NextDouble() < density;
            Row = row;
            Column = column;
        }

        private Cell() {}
    }
}
