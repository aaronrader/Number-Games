using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSums.Classes
{
    public class BoardFactory
    {
        private ushort _nRows = 8;
        private ushort _nCols = 8;
        private float _density = 0.3f; //the approximate proportion of non-zero values in the board
        public ushort NRows
        {
            get => _nRows;
            set => _nRows = value > 0 && value <= 15
                ? value
                : throw new ArgumentOutOfRangeException(nameof(NRows), value, "Number of rows must be a positive integer less than 15.");
        }
        public ushort NColumns
        {
            get => _nCols;
            set => _nCols = value > 0 && value <= 15
                ? value
                : throw new ArgumentOutOfRangeException(nameof(NColumns), value, "Number of columns must be a positive integer less than 15.");
        }
        public float Density
        {
            get => _density;
            set => _density = (value >= 0 && value <= 1)
                ? value
                : throw new ArgumentOutOfRangeException(nameof(Density), value, "Density must be between 0.0 and 1.0.");
        }

        public Board Generate()
        {
            Cell[][] values = new Cell[_nRows][];
            for (uint i = 0; i < _nRows; ++i)
            {
                values[i] = new Cell[_nCols];
            }
            ushort[] rowTotals = new ushort[_nRows];
            ushort[] colTotals = new ushort[_nCols];

            //Fill the board will random values between 0 and 10
            Random rng = new();
            for (uint i = 0; i < _nRows; ++i)
            {
                for (uint j = 0; j < _nCols; ++j)
                {
                    values[i][j] = new Cell((ushort)rng.Next(1, 10), _density);
                }
            }

            //Ensure no row or column is 0
            for (uint i = 0; i < _nRows; ++i)
            {
                if (rowTotals[i] == 0)
                {
                    var randomCell = values[i][rng.Next(_nCols)];
                    var randomVal = (ushort)rng.Next(1, 10);

                    randomCell.Value = randomVal;
                    randomCell.IsCorrect = true;
                }
            }
            for (uint i = 0; i < _nCols; ++i)
            {
                if (colTotals[i] == 0)
                {
                    var randomCell = values[rng.Next(_nRows)][i];
                    var randomVal = (ushort)rng.Next(1, 10);

                    randomCell.Value = randomVal;
                    randomCell.IsCorrect = true;
                }
            }

            //Calculate totals
            for (uint i = 0; i < _nRows; ++i)
            {
                for (uint j = 0; j < _nCols; ++j)
                {
                    if (values[i][j].IsCorrect)
                    {
                        rowTotals[i] += values[i][j].Value;
                        colTotals[j] += values[i][j].Value;
                    }
                }
            }

            return new Board(values, rowTotals, colTotals);
        }
    }
}
