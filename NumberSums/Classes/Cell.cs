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

        internal Cell(ushort value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
