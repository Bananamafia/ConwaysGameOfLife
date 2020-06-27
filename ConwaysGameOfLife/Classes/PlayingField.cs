using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ConwaysGameOfLife.Classes
{
    class PlayingField
    {
        //public static Cell[,] Field = new Cell[70, 70];
        public static Cell[,] Field = new Cell[60, 60];


        private static int _fieldLength = Field.GetLength(0);

        private static int _fieldHeight = Field.GetLength(1);

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

        public static void FillingPlayingFieldWithCells()
        {
            for (int i = 0; i < _fieldHeight; i++)
            {
                for (int j = 0; j < _fieldLength; j++)
                {
                    Field[j, i] = new Cell() { PositionOfCell = new int[] { j, i } };
                }
            }
        }

        public static void FillingPlayingFieldWithColorBlocks(Grid grid)
        {
            Cell _chosenCell;

            for (int i = 0; i < _fieldHeight; i++)
            {
                for (int j = 0; j < _fieldLength; j++)
                {
                    _chosenCell = Field[j, i];

                    TextBlock _textBlock = new TextBlock();
                    _textBlock.SetBinding(TextBlock.BackgroundProperty, "CellColor");
                    _textBlock.DataContext = _chosenCell;

                    Grid.SetColumn(_textBlock, j);
                    Grid.SetRow(_textBlock, i);

                    grid.Children.Add(_textBlock);
                }
            }
        }



        public static void ColourizePlayingFieldGrid(Grid grid)
        {
            grid.Children.Clear();

            Cell _chosenCell;

            for (int i = 0; i < _fieldHeight; i++)
            {
                for (int j = 0; j < _fieldLength; j++)
                {
                    _chosenCell = Field[j, i];

                    TextBlock _textBlock = new TextBlock();

                    _textBlock.SetBinding(TextBlock.BackgroundProperty, "CellColor");

                    _textBlock.DataContext = _chosenCell;

                    Grid.SetColumn(_textBlock, j);
                    Grid.SetRow(_textBlock, i);

                    grid.Children.Add(_textBlock);
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

                    txt.Text = $"{_chosenCell.PositionOfCell[0]} | {_chosenCell.PositionOfCell[1]}";

                    Grid.SetColumn(txt, j);
                    Grid.SetRow(txt, i);
                    grid.Children.Add(txt);
                }
            }
        }

        public static void ShowNumberOfLivingCellNeighbours(Grid grid)
        {
            Cell _chosenCell;

            for (int i = 0; i < _fieldHeight; i++)
            {
                for (int j = 0; j < _fieldLength; j++)
                {
                    _chosenCell = Field[j, i];

                    TextBlock txt = new TextBlock();

                    txt.Text = $"{_chosenCell.LivingNeighbourCells}";

                    Grid.SetColumn(txt, j);
                    Grid.SetRow(txt, i);
                    grid.Children.Add(txt);
                }
            }
        }


    }
}
