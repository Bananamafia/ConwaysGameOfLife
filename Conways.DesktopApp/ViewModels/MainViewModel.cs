using Conways.DesktopApp.Models;
using Conways.DesktopApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Conways.DesktopApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        #region MyConwayCells Property
        private List<ConwayCell> myConwayCells;
        public List<ConwayCell> MyConwayCells
        {
            get { return myConwayCells; }
            set
            {
                myConwayCells = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public PopulateCommand PopulateCommand { get; set; }

        public MainViewModel()
        {
            InstantiateConwayCells(250, 130);
            //InstantiateConwayCells(10, 10);
            AddNeighboursOfConwayCellsAsync();
            PopulateCommand = new(DoPopulation);
        }

        private void InstantiateConwayCells(int columnsOfGameBoard, int rowsOfGameBoard)
        {
            MyConwayCells = new();

            for (int y = 0; y < rowsOfGameBoard; y++)
            {
                for (int x = 0; x < columnsOfGameBoard; x++)
                {
                    MyConwayCells.Add(new(x, y));
                }
            }
        }
        private void AddNeighboursOfConwayCellsAsync()
        {
            int maxXCoordinate = MyConwayCells.Last().PositionOnXAxis;
            int maxYCoordninate = MyConwayCells.Last().PositionOnYAxis;

            List<Task> tasks = new();

            foreach (var conwayCell in MyConwayCells)
            {
                tasks.Add(Task.Run(() =>
                {
                    int topRow, sameRow, bottomRow;
                    int leftColumn, sameColumn, rightColumn;

                    if (conwayCell.PositionOnYAxis != 0)
                    { topRow = conwayCell.PositionOnYAxis - 1; }
                    else
                    { topRow = maxYCoordninate; }

                    sameRow = conwayCell.PositionOnYAxis;

                    if (conwayCell.PositionOnYAxis != maxYCoordninate)
                    { bottomRow = conwayCell.PositionOnYAxis + 1; }
                    else
                    { bottomRow = 0; }


                    if (conwayCell.PositionOnXAxis != 0)
                    { leftColumn = conwayCell.PositionOnXAxis - 1; }
                    else
                    { leftColumn = maxXCoordinate; }

                    sameColumn = conwayCell.PositionOnXAxis;

                    if (conwayCell.PositionOnXAxis != maxXCoordinate)
                    { rightColumn = conwayCell.PositionOnXAxis + 1; }
                    else
                    { rightColumn = 0; }

                    conwayCell.NeighbourCells.Add(MyConwayCells.Find(cell =>
                        cell.PositionOnXAxis == leftColumn &&
                        cell.PositionOnYAxis == topRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.Find(cell =>
                          cell.PositionOnXAxis == sameColumn &&
                          cell.PositionOnYAxis == topRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.Find(cell =>
                        cell.PositionOnXAxis == rightColumn &&
                        cell.PositionOnYAxis == topRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.Find(cell =>
                        cell.PositionOnXAxis == leftColumn &&
                        cell.PositionOnYAxis == sameRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.Find(cell =>
                        cell.PositionOnXAxis == rightColumn &&
                        cell.PositionOnYAxis == sameRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.Find(cell =>
                        cell.PositionOnXAxis == leftColumn &&
                        cell.PositionOnYAxis == bottomRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.Find(cell =>
                        cell.PositionOnXAxis == sameColumn &&
                        cell.PositionOnYAxis == bottomRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.Find(cell =>
                        cell.PositionOnXAxis == rightColumn &&
                        cell.PositionOnYAxis == bottomRow));
                }));
            }
        }
        private void DoPopulation()
        {
            foreach (var conwayCell in MyConwayCells)
            {
                int livingNeighbours = conwayCell.NeighbourCells.Where(cell => cell.IsAlive).Count();

                if (conwayCell.IsAlive)
                {
                    conwayCell.IsAliveInNextTurn = livingNeighbours is < 2 or > 4 ? false : conwayCell.IsAlive;
                }
                else
                {
                    conwayCell.IsAliveInNextTurn = livingNeighbours is 3 ? true : conwayCell.IsAlive;
                }
            }

            foreach (var conwayCell in MyConwayCells)
            {
                conwayCell.IsAlive = conwayCell.IsAliveInNextTurn;

            }
        }

    }
}
