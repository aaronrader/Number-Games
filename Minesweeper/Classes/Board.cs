using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Classes
{
    public class Board
    {
        public Cell[][] Values { get; private set; }
        public ushort NumRows { get; private set; }
        public ushort NumColumns { get; private set; }
        public uint NumBombs { get; private set; } = 0;

        internal Board(Cell[][] values)
        {
            Values = values;
            NumRows = (ushort)Values.Length;
            NumColumns = (ushort)Values[0].Length;

            for (uint i = 0; i < NumRows; ++i)
            {
                for (uint j = 0; j < NumColumns; ++j)
                {
                    if (values[i][j].IsBomb) ++NumBombs; //count the bombs
                }
            }
        }

        internal void PopulateCellValues()
        {
            for(uint i = 0; i < NumRows; ++i)
            {
                for (uint j = 0; j < NumColumns; ++j)
                {
                    if (!Values[i][j].IsBomb)
                        Values[i][j].Adjacent = CalculateCellValue((int)i, (int)j);
                }
            }
        }

        private ushort CalculateCellValue(int row, int col)
        {
            int value = 0;
            value += IsBomb(row - 1, col - 1) ? 1 : 0;
            value += IsBomb(row - 1, col) ? 1 : 0;
            value += IsBomb(row - 1, col + 1) ? 1 : 0;
            value += IsBomb(row, col - 1) ? 1 : 0;
            value += IsBomb(row, col + 1) ? 1 : 0;
            value += IsBomb(row + 1, col - 1) ? 1 : 0;
            value += IsBomb(row + 1, col) ? 1 : 0;
            value += IsBomb(row + 1, col + 1) ? 1 : 0;
            return (ushort)value;
        }

        private bool IsBomb(int row, int col)
        {
            if (row < 0 || col < 0 || row >= NumRows || col >= NumColumns) return false;
            return Values[row][col].IsBomb;
        }
    }
}
