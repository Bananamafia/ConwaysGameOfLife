using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ConwaysGameOfLife.Classes
{
    class PlayingField
    {
        static Cell[,] FieldSize = new Cell[100, 80];

        private static int _length = FieldSize.GetLength(0);

        private static int _height = FieldSize.GetLength(1);
                


        public static void FillingPlayingFieldWithCells()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    Classes.Cell cell = new Cell();
                }
            }            
        }
    }
}
