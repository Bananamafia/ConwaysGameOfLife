using Conways.DesktopApp.Models;
using Conways.DesktopApp.ViewModels;
using Conways.DesktopApp.ViewModels.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        #region MyConwayCells DependencyProperty
        public List<ConwayCell> MyConwayCells
        {
            get { return (List<ConwayCell>)GetValue(MyConwayCellsProperty); }
            set { SetValue(MyConwayCellsProperty, value); }
        }
        public static readonly DependencyProperty MyConwayCellsProperty =
            DependencyProperty.Register("MyConwayCells", typeof(List<ConwayCell>), typeof(GameBoard), new PropertyMetadata(new List<ConwayCell>()));
        #endregion

        public GameBoard()
        {
            InitializeComponent();
        }
        public void DrawGameBoard()
        {
            int gridColumns = MyConwayCells.Last().PositionOnXAxis;
            int gridRows = MyConwayCells.Last().PositionOnYAxis;

            Grid gameBoard = new();

            for (int x = 0; x < gridColumns; x++)
            {
                ColumnDefinition columnDefinition = new() { Width = new GridLength(5) };
                gameBoard.ColumnDefinitions.Add(columnDefinition);
            }

            for (int y = 0; y < gridRows; y++)
            {
                RowDefinition rowDefinition = new() { Height = new GridLength(5) };
                gameBoard.RowDefinitions.Add(rowDefinition);
            }

            RegisterName("GameBoardGrid", gameBoard);
            BoardMainGrid.Children.Add(gameBoard);
        }
        public void DrawBorderAroundGridCells()
        {
            Grid gameBoardGrid = FindName("GameBoardGrid") as Grid;
            int gridColoumns = gameBoardGrid.ColumnDefinitions.Count;
            int gridRows = gameBoardGrid.RowDefinitions.Count;

            for (int y = 0; y < gridRows; y++)
            {
                for (int x = 0; x < gridColoumns; x++)
                {
                    Border border = new() { BorderBrush = Brushes.LightGray, BorderThickness = new Thickness(0.5) };
                    Grid.SetColumn(border, x);
                    Grid.SetRow(border, y);

                    border.DataContext = MyConwayCells.Find(cell => cell.PositionOnXAxis == x && cell.PositionOnYAxis == y);

                    Binding binding = new("IsAlive");
                    ConwayCellIsAliveToBrushConverter converter = new();
                    binding.Converter = converter;

                    border.SetBinding(BackgroundProperty, binding);

                    gameBoardGrid.Children.Add(border);
                }
            }
        }

        #region Event Methods
        //private void UserControl_Initialized(object sender, EventArgs e)
        //{
        //    if (!DesignerProperties.GetIsInDesignMode(this))
        //    {
        //        DrawGameBoard();
        //        DrawBorderAroundGridCells();
        //    }
        //}
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                DrawGameBoard();
                DrawBorderAroundGridCells();
            }
        }
        #endregion

    }
}
