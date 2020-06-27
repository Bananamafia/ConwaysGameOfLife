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
        public MainWindow()
        {
            InitializeComponent();

            Classes.PlayingField.SettingUpPlayingFieldGrid(PlayingFieldGrid);

            Classes.PlayingField.FillingPlayingFieldWithCells();

            Classes.PlayingField.ColourizePlayingFieldGrid(PlayingFieldGrid);

            //Classes.PlayingField.ShowCellPosition(PlayingFieldGrid);             

            //Classes.PlayingField.ShowNumberOfLivingCellNeighbours(PlayingFieldGrid);

        }

        private void PopulateCells_Click(object sender, RoutedEventArgs e)
        {
            Classes.PlayingField.DoPopulation(PlayingFieldGrid);
        }
    }
}
