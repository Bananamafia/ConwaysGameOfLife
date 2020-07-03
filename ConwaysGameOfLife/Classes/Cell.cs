using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ConwaysGameOfLife.Classes
{
    class Cell : INotifyPropertyChanged
    {
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
        } //todo: take a look, if we really need all this code to put the NeighbourCells in this list

        public bool IsAlive { get; set; } = _startLivingCondition(); //todo: instead of setting the IsAlive Status here, maybe better in the constructor?
        public bool IsAliveInNextTurn;

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


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }










        //todo: SimulatePopulation Function maybe better somewhere else, to keep only Props in the Cell Class

        public static async void SimulatePoplulation()
        {
            while (PupulationCycleActivated)
            {
                SolidColorBrush solidColorBrush = new SolidColorBrush();

                foreach (Cell cell in Classes.PlayingField.Field)
                {
                    if (!cell.IsAlive && cell.LivingNeighbourCells == 3)
                    {
                        cell.IsAliveInNextTurn = true;
                        solidColorBrush.Color = System.Windows.Media.Color.FromRgb(0, 0, 0);
                    }

                    if (cell.IsAlive)
                    {
                        if (cell.LivingNeighbourCells < 2 || cell.LivingNeighbourCells > 3)
                        {
                            cell.IsAliveInNextTurn = false;
                            solidColorBrush.Color = System.Windows.Media.Color.FromRgb(255, 255, 255);
                        }
                    }

                    cell.CellColor = solidColorBrush;

                }

                foreach (Cell cell in Classes.PlayingField.Field)
                {
                    cell.IsAlive = cell.IsAliveInNextTurn;
                }

                Classes.PlayingField.Field[0, 0].GenerationCounter++;
                await Task.Delay(100 * PopulationSpeed);
            }
        }


        //todo: PopulationCycleActivated and PopulationSpeed don't fit in the Cell very well... better different Class 
        public static bool PupulationCycleActivated;
        public static int PopulationSpeed { get; set; } = 1;


        //todo: maybe startLivingCondition better in a other Class, to keep Cell class only with Props
        static bool _startLivingCondition()
        {
            Random rnd = new Random();
            const int _maxValue = 100;
            const int _livingChance = 18;

            return rnd.Next(_maxValue) <= _livingChance;
        }



        //todo: put GenerationCounter in a different Class? 

        private int generationCounter = 0;
        public int GenerationCounter
        {
            get
            { return generationCounter; }
            set
            {
                generationCounter = value;
                OnPropertyChanged("GenerationCounter");
            }
        }


        //todo: IsAliveToColorConverter instead of Cell Property

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
    }
}
