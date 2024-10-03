using API.Data.Models;
using for_db7.Services;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для TradeOutletsPage.xaml
    /// </summary>
    public partial class TradeOutletsPage : Page
    {
        TradeOutletsService _tradeOutletsService;
        ObservableCollection<TradeOutlet> _tradeOutlets;
        CommercialOrganizationsService _organizationsService;
        ObservableCollection<CommercialOrganization> _commercialOrganizations;
        TradeOutlet _selectedOutlet;

        public TradeOutletsPage()
        {
            InitializeComponent();


            _tradeOutletsService = new TradeOutletsService();
            _tradeOutlets = new ObservableCollection<TradeOutlet>();
            _organizationsService = new CommercialOrganizationsService();
            _commercialOrganizations = new ObservableCollection<CommercialOrganization>();
            _selectedOutlet = new TradeOutlet();

            TradeOutletsDataGrid.ItemsSource = _tradeOutlets;
            TradeOutletsDataGrid.IsReadOnly = true;

            LoadDataAsync();
            HideElements();
            HideFilters();
        }

        public async Task LoadDataAsync()
        {
            _tradeOutlets = await _tradeOutletsService.GetTradeOutletsAsync();
            TradeOutletsDataGrid.ItemsSource = _tradeOutlets;
            _commercialOrganizations = await _organizationsService.GetCommercialOrganizationsAsync();
            AddCommercialOrganizationComboBox.ItemsSource = _commercialOrganizations;

        }

        public void ShowElements()
        {
            OldCountersLabel.Visibility = Visibility.Visible;
            OldNameLabel.Visibility = Visibility.Visible;
            OldRentLabel.Visibility = Visibility.Visible;
            OldSizeLabel.Visibility = Visibility.Visible;
            OldTypeLabel.Visibility = Visibility.Visible;

            CurrentCountersLabel.Visibility = Visibility.Visible;
            CurrentNameLabel.Visibility = Visibility.Visible;
            CurrentTypeLabel.Visibility = Visibility.Visible;
            CurrentRentLabel.Visibility = Visibility.Visible;
            CurrentSizeLabel.Visibility = Visibility.Visible;
            
            OldTypeLabel.Visibility = Visibility.Visible;
            OldSizeLabel.Visibility = Visibility.Visible;
            OldRentLabel.Visibility = Visibility.Visible;
            OldNameLabel.Visibility = Visibility.Visible;
            OldCountersLabel.Visibility = Visibility.Visible;

            DeleteTradeOutletButton.Visibility = Visibility.Visible;
            EditTradeOutletButton.Visibility = Visibility.Visible;
        }

        public void HideElements()
        {
            OldCountersLabel.Visibility = Visibility.Hidden;
            OldNameLabel.Visibility = Visibility.Hidden;
            OldRentLabel.Visibility = Visibility.Hidden;
            OldSizeLabel.Visibility = Visibility.Hidden;
            OldTypeLabel.Visibility = Visibility.Hidden;

            CurrentCountersLabel.Visibility = Visibility.Hidden;
            CurrentNameLabel.Visibility = Visibility.Hidden;
            CurrentTypeLabel.Visibility = Visibility.Hidden;
            CurrentRentLabel.Visibility = Visibility.Hidden;
            CurrentSizeLabel.Visibility = Visibility.Hidden;

            OldTypeLabel.Visibility = Visibility.Hidden;
            OldSizeLabel.Visibility = Visibility.Hidden;
            OldRentLabel.Visibility = Visibility.Hidden;
            OldNameLabel.Visibility = Visibility.Hidden;
            OldCountersLabel.Visibility = Visibility.Hidden;

            DeleteTradeOutletButton.Visibility = Visibility.Hidden;
            EditTradeOutletButton.Visibility = Visibility.Hidden;
        }

        public void ShowFilters()
        {
            FilterOutletLabel.Visibility = Visibility.Visible;
            FilterTextBox.Visibility = Visibility.Visible;
            FilteredButton.Visibility = Visibility.Visible;
            //
        }

        public void HideFilters()
        {
            FilterOutletLabel.Visibility = Visibility.Hidden;
            FilteredButton.Visibility = Visibility.Hidden;
            FilterTextBox.Visibility = Visibility.Hidden;


            FilterComboBox.Visibility = Visibility.Hidden;
            
            //
        }

        private void TradeOutletsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedOutlet = TradeOutletsDataGrid.SelectedItem as TradeOutlet;
            if(_selectedOutlet is not null)
            {
                CurrentCountersLabel.Content = _selectedOutlet.counters;
                CurrentNameLabel.Content = _selectedOutlet.outletName;
                CurrentTypeLabel.Content = _selectedOutlet.outletType;
                CurrentSizeLabel.Content = _selectedOutlet.size;
                CurrentRentLabel.Content = _selectedOutlet.rent;
                ShowElements();
            }
        }

        private async void AddTradeOutletButton_Click(object sender, RoutedEventArgs e)
        {
            TradeOutlet tradeOutlet = new TradeOutlet()
            {
                outletName = AddNameTextBox.Text,
                outletType = AddTypeTextBox.Text,
                rent = Convert.ToDouble(AddRentTextBox.Text),
                size = Convert.ToDouble(AddSizeTextBox.Text),
                counters = Convert.ToInt32(AddCountersTextBox.Text),
                coId = (AddCommercialOrganizationComboBox.SelectedItem as CommercialOrganization).coId
            };

            if(tradeOutlet is not null )
            {
                await _tradeOutletsService.AddTradeOutletAsync(tradeOutlet);
                await LoadDataAsync();
            }
        }

        private async void DeleteTradeOutletButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedOutlet is not null)
            {
                await _tradeOutletsService.DeleteTradeOutletAsync(_selectedOutlet.toId);
                await LoadDataAsync();
            }
        }

        private async void EditTradeOutletButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedOutlet is not null )
            {
                _selectedOutlet.counters = Convert.ToInt32(EditCountersTextBox.Text);
                _selectedOutlet.rent = Convert.ToDouble(EditRentTextBox.Text);
                _selectedOutlet.size = Convert.ToDouble(EditSizeTextBox.Text);
                _selectedOutlet.outletType = EditTypeTextBox.Text;
                _selectedOutlet.outletName = EditNameTextBox.Text;

                await _tradeOutletsService.UpdateTradeOutletAsync(_selectedOutlet);
                await LoadDataAsync();
            }
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterComboBox.SelectedIndex == 1)
            {
                ShowFilters();
                FilterOutletLabel.Content = "Enter outlet name:";
            }
            else if (FilterComboBox.SelectedIndex == 2)
            {
                ShowFilters();
                FilterOutletLabel.Content = "Enter outlet type:";
            }
            else if (FilterComboBox.SelectedIndex == 0)
            {
                ShowFilters();
                FilterTextBox.Visibility = Visibility.Hidden;
            }
            else
            {
                HideFilters();
            }
        }
    }
}
