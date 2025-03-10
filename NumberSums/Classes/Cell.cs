using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSums.Classes
{
    public class Cell
    {
        public bool IsCorrect { get; internal set; }
        public ushort Value { get; internal set; }

        internal Cell(ushort value, float density)
        {
            var rng = new Random();

            Value = value;
            IsCorrect = rng.NextDouble() < density;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
