using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ConwaysGameOfLife.Classes
{


    class Cell
    {
        static bool _startLivingCondition()
        {
            Random rnd = new Random();
            const int _maxValue = 100;
            const int _livingChance = 18;

            return rnd.Next(_maxValue) <= _livingChance;
        }
        public bool IsAlive { get; set; } = _startLivingCondition();
        public bool IsAliveInNextRound { get; set; }
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

        public List<Cell> NeighbourCells
        {
            get
            {
                List<Cell> _neighbourCells = new List<Cell>();

                int leftColumn = this.PositionOfCell[0] - 1;
                int rightColumn = this.PositionOfCell[0] + 1;

                int upperRow = this.PositionOfCell[1] - 1;
                int lowerRow = this.PositionOfCell[1] + 1;

                if (leftColumn < 0)
                {
                    leftColumn = PlayingField.Field.GetLength(0) - 1;
                }

                if (rightColumn > PlayingField.Field.GetLength(0) - 1)
                {
                    rightColumn = 0;
                }


                if (upperRow < 0)
                {
                    upperRow = PlayingField.Field.GetLength(1) - 1;
                }

                if (lowerRow > PlayingField.Field.GetLength(1) - 1)
                {
                    lowerRow = 0;
                }

                _neighbourCells.Add(PlayingField.Field[leftColumn, upperRow]);
                _neighbourCells.Add(PlayingField.Field[PositionOfCell[0], upperRow]);
                _neighbourCells.Add(PlayingField.Field[rightColumn, upperRow]);

                _neighbourCells.Add(PlayingField.Field[leftColumn, PositionOfCell[1]]);
                _neighbourCells.Add(PlayingField.Field[rightColumn, PositionOfCell[1]]);

                _neighbourCells.Add(PlayingField.Field[leftColumn, lowerRow]);
                _neighbourCells.Add(PlayingField.Field[PositionOfCell[0], lowerRow]);
                _neighbourCells.Add(PlayingField.Field[rightColumn, lowerRow]);

                return _neighbourCells;
            }
        }
        public int LivingNeighbourCells
        {
            get
            {
                int counter = 0;

                foreach (Cell cell in this.NeighbourCells)
                {
                    if (cell.IsAlive)
                    {
                        counter++;
                    }
                }

                return counter;
            }
        }


        public static void SetLivingStatusForNextRound(Cell cell)
        {
            if (!cell.IsAlive && cell.LivingNeighbourCells == 3)
            {
                cell.IsAliveInNextRound = true;
            }

            if (cell.IsAlive)
            {
                if (cell.LivingNeighbourCells < 2)
                {
                    cell.IsAliveInNextRound = false;
                }
                else if (cell.LivingNeighbourCells > 3)
                {
                    cell.IsAliveInNextRound = false;
                }
            }
        }



    }


}
