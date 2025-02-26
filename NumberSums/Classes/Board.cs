using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSums.Classes
{
    public class Board
    {
        private readonly ushort _nRows;
        private readonly ushort _nColumns;

        public Cell[][] Values { get; private set; }
        public ushort[] RowTotals { get; private set; }
        public ushort[] ColumnTotals { get; private set; }

        internal Board(Cell[][] values, ushort[] rowTots, ushort[] colTots)
        {
            Values = values;
            _nRows = (ushort)Values.Length;
            _nColumns = (ushort)Values[0].Length;

            RowTotals = rowTots;
            ColumnTotals = colTots;
        }

        public override string ToString()
        {
            string sValue = "";

            for (uint i = 0; i < _nRows; ++i)
            {
                for (uint j = 0; j < _nColumns; ++j)
                {
                    sValue += Values[i][j].ToString()!.PadLeft(3);
                }
                sValue += " |" + RowTotals[i].ToString().PadLeft(3) + "\n";
            }
            sValue += "-------------------------------\n";
            foreach (var total in ColumnTotals)
            {
                sValue += total.ToString().PadLeft(3);
            }

            return sValue;
        }
    }
}
