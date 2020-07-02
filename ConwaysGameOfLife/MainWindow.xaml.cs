using ConwaysGameOfLife.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConwaysGameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;

        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;

            Classes.GameManager.StartGame();
        }

        private void PopulateCells_Click(object sender, RoutedEventArgs e)
        {
            Classes.GameManager.PlayGame();
        }

        private void CanclePopulateCells_Click(object sender, RoutedEventArgs e)
        {
            Classes.GameManager.PauseGame();
            MenuBar.Opacity = 1;
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            Classes.GameManager.RestartGame();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (PopulationSpeedStackPanel.Visibility == Visibility.Visible)
            { 
                PopulationSpeedStackPanel.Visibility = Visibility.Hidden; 
            }
            else
            {
                PopulationSpeedStackPanel.Visibility = Visibility.Visible;
            }           
        }

        private void MenuBar_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuBar.Opacity = 1;
        }

        private async void MenuBar_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Classes.Cell.PupulationCycleActivated == true)
            {
                await Task.Delay(500);
                MenuBar.Opacity = 0.15;
            }
        }

        private async void PopulationSpeedStackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            await Task.Delay(500);
            PopulationSpeedStackPanel.Visibility = Visibility.Hidden;
            MenuBar.Opacity = 0.15;
        }


    }
}
