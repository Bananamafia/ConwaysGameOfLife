using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.Classes
{


    class Cell
    {
        static bool _startLivingCondition()
        {
            Random rnd = new Random();
            const int _maxValue = 100;
            const int _livingChance = 30;

            return rnd.Next(_maxValue) <= _livingChance;
        }

        public bool IsAlive { get; set; } = _startLivingCondition();

        public int[] PositionOfCell;

        List<Cell> NeighbourCells = new List<Cell>();

        public int LivingNeighbourCells { get; set; }

    }


}
