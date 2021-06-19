using Conways.DesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways.DesktopApp.Models
{
    public class ConwayCell
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

        private readonly double initialLivingChanceInPercent = 18;

        public bool IsAlive { get; set; }

        public bool IsAliveInNextTurn { get; set; }

        public int PositionOnXAxis { get; init; }
        public int PositionOnYAxis { get; init; }
    }
}
