using ConwaysGameOfLife.Classes;
using ConwaysGameOfLife.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConwaysGameOfLife.ViewModels
{
    public class PlayingFieldViewModel : INotifyPropertyChanged
    {
        public PlayingFieldViewModel()
        {
            CreatingCells();
            IdentifyNeighbours();


            playGameCommand = new PlayGameCommand(PlayGame);
            pauseGameCommand = new PauseGameCommand(PauseGame);
        }

        public PlayGameCommand playGameCommand { get; private set; }
        public PauseGameCommand pauseGameCommand { get; private set; }

        public static Cell[,] Field = new Cell[60, 60];

        public int FieldLength = Field.GetLength(0);
        public int FieldHeight = Field.GetLength(1);

        public static bool PopulationIsRunning;

        private int _generationCounter;
        public int GenerationCounter
        {
            get
            {
                return _generationCounter;
            }
            set
            {
                _generationCounter = value;
                OnPropertyChanged("GenerationCounter");
            }
        }

        private int _populationSizeCounter;

        public int PopulationSizeCounter
        {
            get
            {
                return _populationSizeCounter;
            }
            set
            {
                _populationSizeCounter = value;
                OnPropertyChanged("PopulationSizeCounter");
            }
        }


        private void CreatingCells()
        {
            for (int i = 0; i < FieldHeight; i++)
            {
                for (int j = 0; j < FieldLength; j++)
                {
                    Field[j, i] = new Cell(new int[] { j, i });
                }
            }
        }

        private void IdentifyNeighbours()
        {
            foreach (Cell cell in Field)
            {
                int leftColumn = cell.PositionOfCell[0] - 1;
                int rightColumn = cell.PositionOfCell[0] + 1;

                int upperRow = cell.PositionOfCell[1] - 1;
                int lowerRow = cell.PositionOfCell[1] + 1;

                if (leftColumn < 0)
                {
                    leftColumn = FieldLength - 1;
                }

                if (rightColumn > FieldLength - 1)
                {
                    rightColumn = 0;
                }


                if (upperRow < 0)
                {
                    upperRow = FieldHeight - 1;
                }

                if (lowerRow > FieldHeight - 1)
                {
                    lowerRow = 0;
                }

                cell.NeighbourCells.Add(Field[leftColumn, upperRow]);
                cell.NeighbourCells.Add(Field[cell.PositionOfCell[0], upperRow]);
                cell.NeighbourCells.Add(Field[rightColumn, upperRow]);

                cell.NeighbourCells.Add(Field[leftColumn, cell.PositionOfCell[1]]);
                cell.NeighbourCells.Add(Field[rightColumn, cell.PositionOfCell[1]]);

                cell.NeighbourCells.Add(Field[leftColumn, lowerRow]);
                cell.NeighbourCells.Add(Field[cell.PositionOfCell[0], lowerRow]);
                cell.NeighbourCells.Add(Field[rightColumn, lowerRow]);
            }
        }

        private int LivingCellsOnPlayingField()
        {
            int totalLivingCells = 0;

            foreach (Cell cell in Field)
            {
                if (cell.IsAlive)
                {
                    totalLivingCells++;
                }
            }

            return totalLivingCells;
        }


        //---Commands---
        private async void PlayGame()
        {
            PopulationIsRunning = true;

            while (PopulationIsRunning)
            {
                foreach (Cell cell in Field)
                {
                    cell.GetNumberOfLivingNeighbourCells();
                    cell.DetermineLivingStatusOfNextTurn();
                }
                foreach (Cell cell in Field)
                {
                    cell.UpdateLivingStatus();
                }

                GenerationCounter++;
                PopulationSizeCounter = LivingCellsOnPlayingField();

                await Task.Delay(100);
            }
        }

        private void PauseGame()
        {
            PopulationIsRunning = false;
        }

        private void RestartGame()
        {
            MessageBox.Show("Restart Game");
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
