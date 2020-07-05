using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ConwaysGameOfLife.ViewModels.Converters
{
    class CellLivingStatusToCellColorConverter : INotifyPropertyChanged
    {
        
        
        
        
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
