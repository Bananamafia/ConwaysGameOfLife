using Conways.DesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways.DesktopApp.Models
{
    public class ConwayCell : BaseViewModel
    {
        public ConwayCell(int xCoordinate, int yCoordinate)
        {
            Random random = new();

            PositionOnXAxis = xCoordinate;
            PositionOnYAxis = yCoordinate;

            IsAlive = random.Next(0, 100) <= initialLivingChanceInPercent;

            NeighbourCells = new();
        }

        public List<ConwayCell> NeighbourCells { get; init; }

        private readonly double initialLivingChanceInPercent = 30;


        private bool isAlive;

        public bool IsAlive
        {
            get { return isAlive; }
            set
            {
                isAlive = value;
                OnPropertyChanged();
            }
        }



        //public bool IsAlive { get; set; }
        public bool IsAliveInNextTurn { get; set; }

        public int PositionOnXAxis { get; init; }
        public int PositionOnYAxis { get; init; }

        public void CalculateLivingStatusOfNextTurn()
        {
            int livingNeighbours = NeighbourCells.Where(cell => cell.IsAlive).Count();

            switch (livingNeighbours)
            {
                case < 2:
                    IsAliveInNextTurn = false;
                    break;
                case 3:
                    IsAliveInNextTurn = true;
                    break;
                case > 4:
                    IsAliveInNextTurn = false;
                    break;
                default:
                    break;
            }
        }

        public void DoPopulation()
        {
            IsAlive = IsAliveInNextTurn;
        }

    }
}
