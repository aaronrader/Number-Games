using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Classes
{
    public class BoardFactory
    {
        private ushort _nRows = 8;
        private ushort _nCols = 8;
        private float _density = 0.3f; //the approximate proportion of non-zero values in the board
        public ushort NRows
        {
            get => _nRows;
            set => _nRows = value > 0 && value <= 25
                ? value
                : throw new ArgumentOutOfRangeException(nameof(NRows), value, "Number of rows must be a positive integer less than 25.");
        }
        public ushort NColumns
        {
            get => _nCols;
            set => _nCols = value > 0 && value <= 25
                ? value
                : throw new ArgumentOutOfRangeException(nameof(NColumns), value, "Number of columns must be a positive integer less than 25.");
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

            for (uint i = 0; i < _nRows; ++i)
            {
                for (uint j = 0; j < _nCols; ++j)
                {
                    values[i][j] = new Cell(_density);
                }
            }

            return new Board(values);
        }
    }
}
