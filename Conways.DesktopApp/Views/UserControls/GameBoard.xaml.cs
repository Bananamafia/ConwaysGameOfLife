using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Conways.DesktopApp.Views.UserControls
{
    /// <summary>
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoard : UserControl
    {
        public GameBoard()
        {
            InitializeComponent();
            DrawGameBoard(273, 152);

            DrawBorderAroundGridCells();
        }

        public void DrawGameBoard(int columns, int rows)
        {
            Grid gameBoard = new();

            for (int i = 0; i < columns; i++)
            {
                //ColumnDefinition columnDefinition = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
                ColumnDefinition columnDefinition = new() { Width = new GridLength(5) };
                gameBoard.ColumnDefinitions.Add(columnDefinition);
            }

            for (int i = 0; i < rows; i++)
            {
                //RowDefinition rowDefinition = new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) };
                RowDefinition rowDefinition = new() { Height = new GridLength(5) };
                gameBoard.RowDefinitions.Add(rowDefinition);
            }

            RegisterName("GameBoardGrid", gameBoard);
            BoardMainGrid.Children.Add(gameBoard);
        }

        public void DrawBorderAroundGridCells()
        {
            Grid gameBoardGrid = FindName("GameBoardGrid") as Grid;
            int columns = gameBoardGrid.ColumnDefinitions.Count;
            int rows = gameBoardGrid.RowDefinitions.Count;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Border border = new() { BorderBrush = Brushes.LightGray, BorderThickness = new Thickness(1) };
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    gameBoardGrid.Children.Add(border);
                }
            }
        }
    }
}
