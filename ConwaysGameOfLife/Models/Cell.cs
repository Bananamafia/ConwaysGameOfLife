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

namespace ConwaysGameOfLife.Models
{
    public class Cell : INotifyPropertyChanged
    {
        //---Constructor---
        public Cell(int[] cellPosition)
        {
            IsAlive = StartLivingCondition();
            _isAliveInNextTurn = IsAlive;
            PositionOfCell = cellPosition;
        }

        //---Propertys and Variables---
        public int[] PositionOfCell;

        public List<Cell> NeighbourCells = new List<Cell>(); //todo: Define Neighbours

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
        public static bool StartLivingCondition()
        {
            Random rnd = new Random();
            const int _maxValue = 100;
            const int _livingChance = 18;

            return rnd.Next(_maxValue) <= _livingChance;
        }

        public void GetNumberOfLivingNeighbourCells()
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

        public void DetermineLivingStatusOfNextTurn()
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

        public void UpdateLivingStatus()
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

    }
}
