using API.Data.Models;
using for_db7.Services;
using spp3.Data.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;

namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для DealsPage.xaml
    /// </summary>
    public partial class DealsPage : Page
    {
        DealsService _dealsService;
        ObservableCollection<Deal> _deals;

        CustomersService _customersService;
        ObservableCollection<Customer> _customers;

        ProductsService _productsService;
        ObservableCollection<Product> _products;

        SellersService _sellersService;
        ObservableCollection<Seller> _sellers;


        Deal _selectedDeal;
        public DealsPage()
        {
            InitializeComponent();

            _dealsService = new DealsService();
            _deals = new ObservableCollection<Deal>();

            _productsService = new ProductsService();
            _products = new ObservableCollection<Product>();

            _customers = new ObservableCollection<Customer>();
            _customersService = new CustomersService();

            _sellers = new ObservableCollection<Seller>();
            _sellersService = new SellersService();

            _selectedDeal = new Deal();

            DealsDataGrid.ItemsSource = _deals;
            DealsDataGrid.IsReadOnly = true;
            LoadDataAsync();
            HideElements();
            HideFilters();
        }

        public void HideFilters()
        {
            FilterButton.Visibility = Visibility.Hidden;
            FilteredDataGrid.Visibility = Visibility.Hidden;
            FilterTextBox.Visibility = Visibility.Hidden;
            FilterOutletLabel.Visibility = Visibility.Hidden;
        }

        public void ShowFilters()
        {
            FilterButton.Visibility = Visibility.Visible;
            FilteredDataGrid.Visibility = Visibility.Visible;
            FilterTextBox.Visibility = Visibility.Visible;
            FilterOutletLabel.Visibility = Visibility.Visible;
        }
        
        public async Task LoadDataAsync()
        {

            _deals = await _dealsService.GetDealsAsync();
            DealsDataGrid.ItemsSource = _deals;

            _sellers = await _sellersService.GetSellersAsync();
            AddSellerComboBox.ItemsSource = _sellers;
            EditSellerComboBox.ItemsSource = _sellers;

            _customers = await _customersService.GetCustomersAsync();
            AddCustomerComboBox.ItemsSource = _customers;
            EditCustomerComboBox.ItemsSource = _customers;

            _products = await _productsService.GetProductsAsync();
            AddProductComboBox.ItemsSource = _products;
            EditProductComboBox.ItemsSource = _products;
        }

        public void HideElements()
        {
            CurrentCustomerPhoneLabel.Visibility = Visibility.Hidden;
            CurrentDealMomentLabel.Visibility = Visibility.Hidden;
            CurrentProductNameLabel.Visibility = Visibility.Hidden;
            CurrentSellerPhoneLabel.Visibility = Visibility.Hidden;

            OldCustomerLabel.Visibility = Visibility.Hidden;
            OldDealMomentLabel.Visibility = Visibility.Hidden;
            OldProductLabel.Visibility = Visibility.Hidden;
            OldSellerLabel.Visibility = Visibility.Hidden;

            NewCustomerLabel.Visibility = Visibility.Hidden;
            NewSellerLabel.Visibility = Visibility.Hidden;
            NewProductLabel.Visibility = Visibility.Hidden;
            NewDealMomentLabel.Visibility = Visibility.Hidden;

            EditCustomerComboBox.Visibility = Visibility.Hidden;
            EditDealMomentDatePicker.Visibility = Visibility.Hidden;
            EditSellerComboBox.Visibility = Visibility.Hidden;
            EditProductComboBox.Visibility = Visibility.Hidden;

            DeleteDealButton.Visibility = Visibility.Hidden;
            EditDealButton.Visibility = Visibility.Hidden;
        }

        public void ShowElements()
        {

            CurrentCustomerPhoneLabel.Visibility = Visibility.Visible;
            CurrentDealMomentLabel.Visibility = Visibility.Visible;
            CurrentProductNameLabel.Visibility = Visibility.Visible;
            CurrentSellerPhoneLabel.Visibility = Visibility.Visible;

            OldCustomerLabel.Visibility = Visibility.Visible;
            OldDealMomentLabel.Visibility = Visibility.Visible;
            OldProductLabel.Visibility = Visibility.Visible;
            OldSellerLabel.Visibility = Visibility.Visible;

            NewCustomerLabel.Visibility = Visibility.Visible;
            NewSellerLabel.Visibility = Visibility.Visible;
            NewProductLabel.Visibility = Visibility.Visible;
            NewDealMomentLabel.Visibility = Visibility.Visible;

            EditCustomerComboBox.Visibility = Visibility.Visible;
            EditDealMomentDatePicker.Visibility = Visibility.Visible;
            EditSellerComboBox.Visibility = Visibility.Visible;
            EditProductComboBox.Visibility = Visibility.Visible;

            DeleteDealButton.Visibility = Visibility.Visible;
            EditDealButton.Visibility = Visibility.Visible;
        }

        private async void AddDealButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Deal deal = new Deal()
            {
                dealMoment = AddDealMomentDatePicker.SelectedDate.Value,
                cuId = (AddCustomerComboBox.SelectedItem as Customer).cuId,
                selId = (AddSellerComboBox.SelectedItem as Seller).selId,
                prId = (AddProductComboBox.SelectedItem as Product).prId
            };

            if(deal is not null)
            {
                await _dealsService.AddDealAsync(deal);
                await LoadDataAsync();
            }

        }

        private void DealsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDeal = DealsDataGrid.SelectedItem as Deal;
            if(_selectedDeal is not null)
            {
                CurrentCustomerPhoneLabel.Content = _selectedDeal.Customer.phoneNumber;
                CurrentDealMomentLabel.Content = _selectedDeal.dealMoment.ToString();
                CurrentProductNameLabel.Content = _selectedDeal.Product.name;
                CurrentSellerPhoneLabel.Content = _selectedDeal.Seller.phoneNumber;
                ShowElements();
            }
        }

        private async void DeleteDealButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedDeal is not null)
            {
                await _dealsService.DeleteSellerAsync(_selectedDeal.dlId);
                await LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no deal with this information");
            }
            HideElements();
        }

        private async void EditDealButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedDeal is not null)
            {
                _selectedDeal.dealMoment = EditDealMomentDatePicker.SelectedDate.Value;
                _selectedDeal.cuId = (EditCustomerComboBox.SelectedItem as Customer).cuId;
                _selectedDeal.selId = (EditSellerComboBox.SelectedItem as Seller).selId;
                _selectedDeal.prId = (EditProductComboBox.SelectedItem as Product).prId;
                await _dealsService.UpdateDealAsync(_selectedDeal);
                await LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no that deal");
            }
            HideElements();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if(FilterComboBox.SelectedIndex == 0)
            {
                var outletDeals = _deals.ToList().FindAll(dl => dl.Seller.OutletSection.TradeOutlet.outletName == FilterTextBox.Text);
                Dictionary<string, double> filteredData = new Dictionary<string, double>();
                foreach (var deal in outletDeals)
                {
                    if (filteredData.ContainsKey(deal.Product.name))
                    {
                        filteredData[deal.Product.name] += (double)deal.Product.price;
                    }
                    else
                    {
                        filteredData.Add(deal.Product.name, (double)deal.Product.price);
                    }
                }
                FilteredDataGrid.ItemsSource = filteredData;
            }
            else if(FilterComboBox.SelectedIndex == 1)
            {
                var outletDeals = _deals.ToList().FindAll(dl => dl.Seller.OutletSection.TradeOutlet.outletType == FilterTextBox.Text);
                Dictionary<string, double> filteredData = new Dictionary<string, double>();
                foreach (var deal in outletDeals)
                {
                    if (filteredData.ContainsKey(deal.Product.name))
                    {
                        filteredData[deal.Product.name] += (double)deal.Product.price;
                    }
                    else
                    {
                        filteredData.Add(deal.Product.name, (double)deal.Product.price);
                    }
                }
                FilteredDataGrid.ItemsSource = filteredData;
            }

        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FilterComboBox.SelectedIndex == 0)
            {
                ShowFilters();
                FilterOutletLabel.Content = "Enter outlet name:";
            }
            else if ( FilterComboBox.SelectedIndex == 1)
            {
                ShowFilters();
                FilterOutletLabel.Content = "Enter outlet type:";
            }
            else
            {
                HideFilters();
            }
        }

    }
}
