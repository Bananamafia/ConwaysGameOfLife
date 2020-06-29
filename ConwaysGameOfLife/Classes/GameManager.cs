using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ConwaysGameOfLife.Classes
{
    class GameManager
    {
        public static void StartGame()
        {
            Grid playingFieldGrid = MainWindow.AppWindow.PlayingFieldGrid;

            Classes.PlayingField.SettingUpPlayingFieldGrid(playingFieldGrid);
            Classes.PlayingField.InstantiateCellsOnPlayingField(playingFieldGrid);

            //Classes.PlayingField.ShowCellPosition(playingFieldGrid);     
            //Classes.PlayingField.ShowNumberOfLivingCellNeighbours(playingFieldGrid);
        }

        public static void PlayGame()
        {
            Classes.Cell.PupulationCycleActivated = true;
            Classes.Cell.SimulatePoplulation();
            MainWindow.AppWindow.MenuBar.Opacity = 0.15;
        }

        public static void PauseGame()
        {
            Classes.Cell.PupulationCycleActivated = false;
            MainWindow.AppWindow.MenuBar.Opacity = 1;
        }

        public static void RestartGame()
        {
            Grid playingFieldGrid = MainWindow.AppWindow.PlayingFieldGrid;

            playingFieldGrid.Children.Clear();
            Classes.PlayingField.InstantiateCellsOnPlayingField(playingFieldGrid);
        }


    }
}
