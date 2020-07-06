using ConwaysGameOfLife.Classes;
using ConwaysGameOfLife.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.ViewModels
{
    public class PlayingFieldViewModel
    {
        public PlayingFieldViewModel()
        {
            CreatingCells();

            playGameCommand = new PlayGameCommand(PlayGame);
            pauseGameCommand = new PauseGameCommand(PauseGame);
        }

        public PlayGameCommand playGameCommand { get; private set; }
        public PauseGameCommand pauseGameCommand { get; private set; }

        public static Cell[,] Field = new Cell[60, 60];
        public int FieldLength = Field.GetLength(0);
        public int FieldHeight = Field.GetLength(1);

        public bool PopulationIsRunning;

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

                await Task.Delay(500);
            }
        }

        private void PauseGame()
        {
            PopulationIsRunning = false;
        }

        private void RestartGame()
        {
            
        }

    }
}
