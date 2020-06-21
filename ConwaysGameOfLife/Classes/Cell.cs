using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.Classes
{
    class Cell
    {
        public bool IsAlive { get; set; }

        public int[] PositionOfCell { get; set; }

        List<Cell> NeighbourCells = new List<Cell>();

        public int LivingNeighbourCells { get; set; }

    }
}
