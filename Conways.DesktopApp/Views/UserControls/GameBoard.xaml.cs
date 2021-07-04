using Conways.DesktopApp.Models;
using Conways.DesktopApp.ViewModels;
using Conways.DesktopApp.ViewModels.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #region Properties
        #region MyConwayCells DependencyProperty
        public List<ConwayCell> MyConwayCells
        {
            get { return (List<ConwayCell>)GetValue(MyConwayCellsProperty); }
            set { SetValue(MyConwayCellsProperty, value); }
        }
        public static readonly DependencyProperty MyConwayCellsProperty =
            DependencyProperty.Register("MyConwayCells", typeof(List<ConwayCell>), typeof(GameBoard), new PropertyMetadata(new List<ConwayCell>(), new PropertyChangedCallback(CellLivingStatusChangedCallback)));

        #endregion
        #endregion

        private List<Border> cellBorderControls = new();

        public GameBoard()
        {
            InitializeComponent();
        }

        #region Functional Methods
        private void DrawGameBoard()
        {
            int gridColumns = MyConwayCells.Last().PositionOnXAxis;
            int gridRows = MyConwayCells.Last().PositionOnYAxis;

            Grid gameBoard = new();

            for (int x = 0; x < gridColumns; x++)
            {
                ColumnDefinition columnDefinition = new() { Width = new GridLength(10) };
                gameBoard.ColumnDefinitions.Add(columnDefinition);
            }

            for (int y = 0; y < gridRows; y++)
            {
                RowDefinition rowDefinition = new() { Height = new GridLength(10) };
                gameBoard.RowDefinitions.Add(rowDefinition);
            }

            RegisterName("GameBoardGrid", gameBoard);
            BoardMainGrid.Children.Add(gameBoard);
        }
        private void DrawBorderAroundGridCells()
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
                    cellBorderControls.Add(border);
                    gameBoardGrid.Children.Add(border);
                }
            }
        }
        private void FillBorderWithCellLivingStatus()
        {
            var CellDataEnumerator = MyConwayCells.GetEnumerator();

            foreach (var borderControl in cellBorderControls)
            {
                if (CellDataEnumerator.MoveNext())
                {
                    borderControl.Background = CellDataEnumerator.Current.IsAlive switch
                    {
                        true => Brushes.Black,
                        false => Brushes.White,
                    };
                }
            }
        }

        #endregion

        private static void CellLivingStatusChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GameBoard gameBoardUserControl)
            {
                gameBoardUserControl.FillBorderWithCellLivingStatus();
            }
        }

        #region Event Methods
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            DrawGameBoard();
            DrawBorderAroundGridCells();
            FillBorderWithCellLivingStatus();
        }
        #endregion

    }
}
