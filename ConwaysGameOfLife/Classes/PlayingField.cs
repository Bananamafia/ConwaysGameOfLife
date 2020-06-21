using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ConwaysGameOfLife.Classes
{
    class PlayingField
    {
        //static Cell[,] Field = new Cell[85, 65];

        static Cell[,] Field = new Cell[50, 30];

        private static int _fieldLength = Field.GetLength(0);

        private static int _fieldHeight = Field.GetLength(1);


        public static void FillingPlayingFieldWithCells()
        {
            for (int i = 0; i < _fieldHeight; i++)
            {
                for (int j = 0; j < _fieldLength; j++)
                {
                    Field[j, i] = new Cell() { PositionOfCell = new int[,] { { j, i } } };
                }
            }
        }

        public static void SettingUpPlayingFieldGrid(Grid grid)
        {
            for (int i = 0; i < _fieldLength; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < _fieldHeight; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
        }


        public static void ColourizePlayingFieldGrid(Grid grid)
        {
            SolidColorBrush livingCellColorBrush = new SolidColorBrush();
            livingCellColorBrush.Color = System.Windows.Media.Color.FromRgb(0, 0, 0);

            SolidColorBrush deadCellColorBrush = new SolidColorBrush();
            deadCellColorBrush.Color = System.Windows.Media.Color.FromRgb(255, 255, 255);



            Cell _chosenCell;
            bool _isCellAllive;

            for (int i = 0; i < _fieldHeight; i++)
            {
                for (int j = 0; j < _fieldLength; j++)
                {


                    _chosenCell = Field[j, i];
                    _isCellAllive = _chosenCell.IsAlive;

                    System.Windows.Shapes.Rectangle _rectangle = new System.Windows.Shapes.Rectangle();


                    if (_isCellAllive == true)
                    {
                        _rectangle.Fill = livingCellColorBrush;
                    }
                    else
                    {
                        _rectangle.Fill = deadCellColorBrush;
                    }

                    Grid.SetColumn(_rectangle, j);
                    Grid.SetRow(_rectangle, i);

                    grid.Children.Add(_rectangle);


                    //ShowPosition of Cell
                    ////TextBlock txt = new TextBlock();
                    ////txt.Text = $"{_chosenCell.PositionOfCell[0, 0]} | {_chosenCell.PositionOfCell[0,1]}";
                    ////Grid.SetColumn(txt, j);
                    ////Grid.SetRow(txt, i);
                    ////grid.Children.Add(txt);

                }

            }
        }


        public static void ShowCellPosition(Grid grid)
        {
            Cell _chosenCell;

            for (int i = 0; i < _fieldHeight; i++)
            {
                for (int j = 0; j < _fieldLength; j++)
                {
                    _chosenCell = Field[j, i];

                    TextBlock txt = new TextBlock();

                    txt.Text = $"{_chosenCell.PositionOfCell[0, 0]} | {_chosenCell.PositionOfCell[0, 1]}";

                    Grid.SetColumn(txt, j);
                    Grid.SetRow(txt, i);
                    grid.Children.Add(txt);

                }

            }
        }

    }
}
