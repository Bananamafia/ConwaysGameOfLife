using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ConwaysGameOfLife.Classes
{


    class Cell : INotifyPropertyChanged
    {
        static bool _startLivingCondition()
        {
            Random rnd = new Random();
            const int _maxValue = 100;
            const int _livingChance = 18;

            return rnd.Next(_maxValue) <= _livingChance;
        }
        public bool IsAlive { get; set; } = _startLivingCondition();
        public bool IsAliveInNextTurn { get; set; }

        private SolidColorBrush cellColor;
        public SolidColorBrush CellColor
        {
            get
            {
                SolidColorBrush solidColorBrush = new SolidColorBrush();

                if (IsAlive)
                {
                    solidColorBrush.Color = System.Windows.Media.Color.FromRgb(0, 0, 0);
                }
                else
                {
                    solidColorBrush.Color = System.Windows.Media.Color.FromRgb(255, 255, 255);
                }

                cellColor = solidColorBrush;

                return cellColor;
            }
            set
            {
                cellColor = value;
                OnPropertyChanged("CellColor");
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


        public void SetLivingStatusOfNextTurn()
        {
            if (!IsAlive && LivingNeighbourCells == 3)
            {
                IsAliveInNextTurn = true;
            }

            if (IsAlive)
            {
                if (LivingNeighbourCells < 2)
                {
                    IsAliveInNextTurn = false;
                }
                else if (LivingNeighbourCells > 3)
                {
                    IsAliveInNextTurn = false;
                }
            }
        }

        public void Populate()
        {
            IsAlive = IsAliveInNextTurn;

            SolidColorBrush solidColorBrush = new SolidColorBrush();

            if (IsAlive)
            {
                solidColorBrush.Color = System.Windows.Media.Color.FromRgb(0, 0, 0);
            }
            else
            {
                solidColorBrush.Color = System.Windows.Media.Color.FromRgb(255, 255, 255);
            }

            CellColor = solidColorBrush;
        }

        public static async void SimulatePoplulation(bool activated)
        {
            while (activated)
            {
                foreach (Cell cell in Classes.PlayingField.Field)
                {
                    cell.SetLivingStatusOfNextTurn();
                }
                foreach (Cell cell in Classes.PlayingField.Field)
                {
                    cell.Populate();
                }

                await Task.Delay(100);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }

        }

    }
}
