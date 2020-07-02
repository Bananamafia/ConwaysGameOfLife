using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConwaysGameOfLife.Classes
{
    class GameManager
    {
        private static bool _gameIsRunning = false;

        public static void StartGame()
        {
            Grid playingFieldGrid = MainWindow.AppWindow.PlayingFieldGrid;

            Classes.PlayingField.SettingUpPlayingFieldGrid(playingFieldGrid);
            Classes.PlayingField.InstantiateCellsOnPlayingField(playingFieldGrid);

            MainWindow.AppWindow.GenerationCounterTxtBlock.DataContext = Classes.PlayingField.Field[0, 0];

            MainWindow.AppWindow.PopulationSpeedSlider.DataContext = Classes.PlayingField.Field[0, 0];

            //Classes.PlayingField.ShowCellPosition(playingFieldGrid);     
            //Classes.PlayingField.ShowNumberOfLivingCellNeighbours(playingFieldGrid);
        }

        public async static void PlayGame()
        {
            if (_gameIsRunning == false)
            {
                Classes.Cell.PupulationCycleActivated = true;
                Classes.Cell.SimulatePoplulation();
                _gameIsRunning = true;
                await Task.Delay(500);
                MainWindow.AppWindow.MenuBar.Opacity = 0.15;
            }
        }

        public static void PauseGame()
        {
            Classes.Cell.PupulationCycleActivated = false;
            MainWindow.AppWindow.MenuBar.Opacity = 1;
            _gameIsRunning = false;
        }

        public static void RestartGame()
        {
            Grid playingFieldGrid = MainWindow.AppWindow.PlayingFieldGrid;

            playingFieldGrid.Children.Clear();
            Classes.PlayingField.InstantiateCellsOnPlayingField(playingFieldGrid);

            MainWindow.AppWindow.GenerationCounterTxtBlock.DataContext = Classes.PlayingField.Field[0, 0];
        }


    }
}
