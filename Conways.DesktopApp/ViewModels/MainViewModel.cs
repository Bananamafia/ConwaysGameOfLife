using Conways.DesktopApp.Models;
using Conways.DesktopApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public bool GameIsRunning { get; set; } = false;

        public PopulateCommand PopulateCommand { get; set; }
        public RestartCommand RestartCommand { get; set; }

        public MainViewModel()
        {
            //InstantiateConwayCells(137, 67);
            InstantiateConwayCells(30, 30);
            AddNeighboursOfConwayCellsAsync();
            PopulateCommand = new(DoPopulation);
            RestartCommand = new(RestartGame);
        }

        #region Methods
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

                    conwayCell.NeighbourCells.Add(MyConwayCells.First(cell =>
                        cell.PositionOnXAxis == leftColumn &&
                        cell.PositionOnYAxis == topRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.First(cell =>
                          cell.PositionOnXAxis == sameColumn &&
                          cell.PositionOnYAxis == topRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.First(cell =>
                        cell.PositionOnXAxis == rightColumn &&
                        cell.PositionOnYAxis == topRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.First(cell =>
                        cell.PositionOnXAxis == leftColumn &&
                        cell.PositionOnYAxis == sameRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.First(cell =>
                        cell.PositionOnXAxis == rightColumn &&
                        cell.PositionOnYAxis == sameRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.First(cell =>
                        cell.PositionOnXAxis == leftColumn &&
                        cell.PositionOnYAxis == bottomRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.First(cell =>
                        cell.PositionOnXAxis == sameColumn &&
                        cell.PositionOnYAxis == bottomRow));

                    conwayCell.NeighbourCells.Add(MyConwayCells.First(cell =>
                        cell.PositionOnXAxis == rightColumn &&
                        cell.PositionOnYAxis == bottomRow));
                }));
            }
        }
        private async void DoPopulation()
        {
            GameIsRunning = !GameIsRunning;

            while (GameIsRunning)
            {
                List<Task> tasks = new();
                foreach (var conwayCell in MyConwayCells)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        int livingNeighbours = conwayCell.NeighbourCells.Where(cell => cell.IsAlive).Count();
                        conwayCell.IsAliveInNextTurn = livingNeighbours switch
                        {
                            < 2 or > 3 => false,
                            3 => true,
                            _ => conwayCell.IsAlive
                        };
                    }));
                }

                await Task.WhenAll(tasks);
                tasks = new();

                foreach (var conwayCell in MyConwayCells)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        conwayCell.IsAlive = conwayCell.IsAliveInNextTurn;
                    }));
                }

                OnPropertyChanged("MyConwayCells");
                await Task.WhenAll(tasks);

                //todo: find a cleaner way to invoke the dependencyproperty changed callback in GameBoard
                var tempConwayCells = MyConwayCells;
                MyConwayCells = new List<ConwayCell>();
                MyConwayCells = tempConwayCells;

                await Task.Delay(75);
            }
        }
        private void RestartGame()
        {
            GameIsRunning = false;
            Random random = new();
            int initialLivingChanceInPercent = 18;

            foreach (var conwayCell in MyConwayCells)
            {
                conwayCell.IsAlive = random.Next(0, 100) <= initialLivingChanceInPercent;
            }

            OnPropertyChanged("MyConwayCells");
        }
        #endregion
    }
}
