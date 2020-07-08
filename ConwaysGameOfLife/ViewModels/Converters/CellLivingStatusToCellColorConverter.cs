using ConwaysGameOfLife.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace ConwaysGameOfLife.ViewModels.Converters
{
    public class CellLivingStatusToCellColorConverter : INotifyPropertyChanged, IValueConverter
    {
        //---IValueConverter---
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush();

            if (value != null)
            {
                if ((bool)value == true)
                {
                    solidColorBrush.Color = System.Windows.Media.Color.FromRgb(0, 0, 0);
                }
                else
                {
                    solidColorBrush.Color = System.Windows.Media.Color.FromRgb(255, 255, 255);
                }

                return solidColorBrush;
            }
            else
            {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        //---INotifyPropertyChanged---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
