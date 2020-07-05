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
    public class Cell : INotifyPropertyChanged
    {
        //---Constructor---
        public Cell(int[] cellPosition)
        {
            IsAlive = _startLivingCondition();
            _isAliveInNextTurn = IsAlive;
            PositionOfCell = cellPosition;
        }

        //---Propertys and Variables---
        public int[] PositionOfCell;

        public List<Cell> NeighbourCells; //todo: Define Neighbours

        private bool _isAlive;
        public bool IsAlive
        {
            get { return _isAlive; }
            set
            {
                _isAlive = value;
                OnPropertyChanged("IsAlive");
            }
        }

        private bool _isAliveInNextTurn;

        private int _livingNeighbourCells;

        public int LivingNeighbourCells
        {
            get { return _livingNeighbourCells; }
            set
            {
                _livingNeighbourCells = value;
                OnPropertyChanged("LivingNeighbourCells");
            }
        }


        //---Methods---
        private static bool _startLivingCondition()
        {
            Random rnd = new Random();
            const int _maxValue = 100;
            const int _livingChance = 18;

            return rnd.Next(_maxValue) <= _livingChance;
        }

        private void GetNumberOfLivingNeighbourCells()
        {
            int counter = 0;

            foreach (Cell neighbour in NeighbourCells)
            {
                if (neighbour.IsAlive)
                {
                    counter++;
                }
            }

            LivingNeighbourCells = counter;
        }

        private void DetermineLivingStatusOfNextTurn()
        {
            if (IsAlive)
            {
                if (LivingNeighbourCells < 2 || LivingNeighbourCells > 3)
                {
                    _isAliveInNextTurn = false;
                }
            }
            else
            {
                if (LivingNeighbourCells == 3)
                {
                    _isAliveInNextTurn = true;
                }
            }
        }

        private void UpdateLivingStatus()
        {
            IsAlive = _isAliveInNextTurn;
        }


        //---INotifyPropertyChanged---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }







        //todo: get rid of this Code (after everything is set up and working fine)

        public List<Cell> NeighbourCellsOld
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
        } //todo: take a look, if we really need all this code to put the NeighbourCells in this list ++ maybe setting neighbours in ViewModel

        public int LivingNeighbourCellsOld
        {
            get
            {
                int counter = 0;

                foreach (Cell cell in this.NeighbourCellsOld)
                {
                    if (cell.IsAlive)
                    {
                        counter++;
                    }
                }

                return counter;
            }
        }





        public static async void SimulatePoplulation()
        {
            while (PupulationCycleActivated)
            {
                SolidColorBrush solidColorBrush = new SolidColorBrush();

                foreach (Cell cell in Classes.PlayingField.Field)
                {
                    if (!cell.IsAlive && cell.LivingNeighbourCellsOld == 3)
                    {
                        cell._isAliveInNextTurn = true;
                        solidColorBrush.Color = System.Windows.Media.Color.FromRgb(0, 0, 0);
                    }

                    if (cell.IsAlive)
                    {
                        if (cell.LivingNeighbourCellsOld < 2 || cell.LivingNeighbourCellsOld > 3)
                        {
                            cell._isAliveInNextTurn = false;
                            solidColorBrush.Color = System.Windows.Media.Color.FromRgb(255, 255, 255);
                        }
                    }

                    cell.CellColor = solidColorBrush;

                }

                foreach (Cell cell in Classes.PlayingField.Field)
                {
                    cell.IsAlive = cell._isAliveInNextTurn;
                }

                Classes.PlayingField.Field[0, 0].GenerationCounter++;
                await Task.Delay(100 * PopulationSpeed);
            }
        } //todo: check if all code is necessary



        public static bool PupulationCycleActivated; //todo: ViewModel Content
        public static int PopulationSpeed { get; set; } = 1; //todo: ViewModel Content









        private int generationCounter = 0; //todo: ViewModel Content
        public int GenerationCounter //todo: ViewModel Content
        {
            get
            { return generationCounter; }
            set
            {
                generationCounter = value;
                OnPropertyChanged("GenerationCounter");
            }
        }




        private SolidColorBrush cellColor; //todo: ViewModel Content
        public SolidColorBrush CellColor //todo: ViewModel Content, maybe better a Converter
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
