using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.Classes
{
    class PlayingField
    {
        Cell[,] FieldSize = new Cell[100, 100];

        public int Height;

        public int Length;
    }
}
