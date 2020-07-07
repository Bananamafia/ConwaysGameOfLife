using ConwaysGameOfLife.Classes;
using ConwaysGameOfLife.ViewModels;
using ConwaysGameOfLife.ViewModels.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConwaysGameOfLife.Views
{
    /// <summary>
    /// Interaktionslogik für PlayingFieldView.xaml
    /// </summary>
    public partial class PlayingFieldView : Window
    {
        public PlayingFieldView()
        {
            InitializeComponent();
            _viewModel = new PlayingFieldViewModel();
            this.DataContext = _viewModel;
            SettingUpPlayingFieldGridControl();
            SettingUpPlayingFieldGridDataContext();
        }

        private PlayingFieldViewModel _viewModel;

        private void SettingUpPlayingFieldGridControl()
        {
            for (int i = 0; i < _viewModel.FieldHeight; i++)
            {
                CellPlayingFieldGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < _viewModel.FieldLength; i++)
            {
                CellPlayingFieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void SettingUpPlayingFieldGridDataContext()
        {
            for (int i = 0; i < _viewModel.FieldHeight; i++)
            {
                for (int j = 0; j < _viewModel.FieldLength; j++)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.DataContext = PlayingFieldViewModel.Field[j, i];

                    Binding binding = new Binding("IsAlive");
                    CellLivingStatusToCellColorConverter cellLivingStatusToCellColorConverter = new CellLivingStatusToCellColorConverter();
                    binding.Converter = cellLivingStatusToCellColorConverter;

                    textBlock.SetBinding(TextBlock.BackgroundProperty, binding);

                    Grid.SetColumn(textBlock, j);
                    Grid.SetRow(textBlock, i);
                    CellPlayingFieldGrid.Children.Add(textBlock);

                }
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
