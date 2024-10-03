using API.Data.Models;
using for_db7.Services;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для OutletSectionsPage.xaml
    /// </summary>
    public partial class OutletSectionsPage : Page
    {
        OutletSectionsService _outletSectionsService;
        ObservableCollection<OutletSection> _outletSections;
        TradeOutletsService _tradeOutletsService;
        ObservableCollection<TradeOutlet> _tradeOutlets;
        OutletSection _selectedOutletSection;

        public OutletSectionsPage()
        {
            InitializeComponent();

            _tradeOutletsService = new TradeOutletsService();
            _tradeOutlets = new ObservableCollection<TradeOutlet>();
            _outletSectionsService = new OutletSectionsService();
            _outletSections = new ObservableCollection<OutletSection>();
            _selectedOutletSection = new OutletSection();

            AddTradeOutletComboBox.ItemsSource = _tradeOutlets;
            OutletSectionsDataGrid.ItemsSource = _outletSections;
            OutletSectionsDataGrid.IsReadOnly = true;
            LoadDataAsync();
            HideElements();
        }

        public async void LoadDataAsync()
        {
            _outletSections = await _outletSectionsService.GetOutletSectionsAsync();
            OutletSectionsDataGrid.ItemsSource = _outletSections;
            _tradeOutlets = await _tradeOutletsService.GetTradeOutletsAsync();
            AddTradeOutletComboBox.ItemsSource = _tradeOutlets;
        }

        public void HideElements()
        {
            CurrentNameLabel.Visibility = Visibility.Hidden;
            CurrentHallLabel.Visibility = Visibility.Hidden;
            CurrentFloorLabel.Visibility =Visibility.Hidden;

            OldFloorLabel.Visibility = Visibility.Hidden;
            OldHallLabel.Visibility = Visibility.Hidden;
            OldNameLabel.Visibility= Visibility.Hidden;

            NewNameLabel.Visibility = Visibility.Hidden;
            NewFloorLabel.Visibility = Visibility.Hidden;
            NewHallLabel.Visibility = Visibility.Hidden;

            EditFloorTextBox.Visibility = Visibility.Hidden;
            EditHallTextBox.Visibility = Visibility.Hidden;
            EditNameTextBox.Visibility = Visibility.Hidden;

            DeleteOutletSectionButton.Visibility = Visibility.Hidden;
            EditOutletSectionButton.Visibility= Visibility.Hidden;
        }
        public void ShowElements()
        {
            CurrentNameLabel.Visibility = Visibility.Visible;
            CurrentHallLabel.Visibility = Visibility.Visible;
            CurrentFloorLabel.Visibility = Visibility.Visible;

            OldFloorLabel.Visibility = Visibility.Visible;
            OldHallLabel.Visibility = Visibility.Visible;
            OldNameLabel.Visibility = Visibility.Visible;

            NewNameLabel.Visibility = Visibility.Visible;
            NewFloorLabel.Visibility = Visibility.Visible;
            NewHallLabel.Visibility = Visibility.Visible;

            EditFloorTextBox.Visibility = Visibility.Visible;
            EditHallTextBox.Visibility = Visibility.Visible;
            EditNameTextBox.Visibility = Visibility.Visible;

            DeleteOutletSectionButton.Visibility = Visibility.Visible;
            EditOutletSectionButton.Visibility = Visibility.Visible;
        }


        private async void AddOutletSectionButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var tradeOutlet = AddTradeOutletComboBox.SelectedItem as TradeOutlet;

            OutletSection outletSection = new OutletSection()
            {
                sectionName = AddNameTextBox.Text,
                sectionFloor = Convert.ToInt32(AddFloorTextBox.Text),
                sectionHall = AddHallTextBox.Text,
                toId = tradeOutlet.toId
            };
            await _outletSectionsService.AddOutletSectionAsync(outletSection);
            LoadDataAsync();
        }

        private async void EditOutletSectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedOutletSection is not null)
            {
                _selectedOutletSection.sectionFloor = Convert.ToInt32(EditFloorTextBox.Text);
                _selectedOutletSection.sectionHall = EditHallTextBox.Text;
                _selectedOutletSection.sectionName = EditNameTextBox.Text;
                
                await _outletSectionsService.UpdateOutletSectionAsync(_selectedOutletSection);
                LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no that section");
            }
            HideElements();
        }

        private void OutletSectionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedOutletSection = OutletSectionsDataGrid.SelectedItem as OutletSection;

            if(_selectedOutletSection is not null)
            {
                CurrentFloorLabel.Content = _selectedOutletSection.sectionFloor;
                CurrentHallLabel.Content = _selectedOutletSection.sectionHall;
                CurrentNameLabel.Content = _selectedOutletSection.sectionName;
                ShowElements();
            }
        }

        private async void DeleteOutletSectionButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedOutletSection is not null)
            {
                await _outletSectionsService.DeleteOutletSectionAsync(_selectedOutletSection.osId);
                HideElements();
                LoadDataAsync();
            }
        }
    }
}
