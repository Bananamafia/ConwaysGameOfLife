using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media;

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

        public SolidColorBrush CellColor
        {
            get
            {
                SolidColorBrush _solidColorBrush = new SolidColorBrush();

                if (IsAlive == true)
                {
                    _solidColorBrush.Color = System.Windows.Media.Color.FromRgb(0, 0, 0);
                }
                else
                {
                    _solidColorBrush.Color = System.Windows.Media.Color.FromRgb(255, 255, 255);
                }

                return _solidColorBrush;
            }
        }



        public int[] PositionOfCell;

        List<Cell> NeighbourCells = new List<Cell>();

        public int LivingNeighbourCells { get; set; }



        public Brush CProp { get; set; }

        public String CString { get; set; }

    }


}
