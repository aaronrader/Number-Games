using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Classes
{
    public class Board
    {
        private readonly ushort _nRows;
        private readonly ushort _nColumns;

        public Cell[][] Values { get; private set; }
        public uint NumBombs { get; private set; } = 0;

        internal Board(Cell[][] values)
        {
            Values = values;
            _nRows = (ushort)Values.Length;
            _nColumns = (ushort)Values[0].Length;

            for (uint i = 0; i < _nRows; ++i)
            {
                for (uint j = 0; j < _nColumns; ++j)
                {
                    if (values[i][j].IsBomb) ++NumBombs; //count the bombs
                }
            }
        }
    }
}
