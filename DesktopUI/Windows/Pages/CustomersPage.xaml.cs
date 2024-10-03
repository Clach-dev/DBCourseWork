using for_db7.Services;
using spp3.Data.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace for_db7.Pages
{
    /// <summary>
    /// Логика взаимодействия для CustomerPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        private readonly CustomersService _customerService;
        public ObservableCollection<Customer> _customers;
        public Customer _selectedCustomer;

        public ObservableCollection<Customer> _filteredCustomers;
        public ObservableCollection<Product> _products;
        public ProductsService _productsService;

        public CustomersPage()
        {
            InitializeComponent();

            _customerService = new CustomersService();
            _customers = new ObservableCollection<Customer>();
            _selectedCustomer = new Customer();

            _filteredCustomers = new ObservableCollection<Customer>();
            FilteredCustomersDataGrid.ItemsSource = _filteredCustomers;
            _products = new ObservableCollection<Product>();
            _productsService = new ProductsService();

            AllCustomersDataGrid.ItemsSource = _customers;
            LoadDataAsync();

            FilterButton.Visibility = Visibility.Visible;
            FilteredCustomersDataGrid.Visibility = Visibility.Hidden;
            ProductsComboBox.Visibility = Visibility.Hidden;
            FilterCountTextBox.Visibility = Visibility.Hidden;
            FilterLable.Visibility = Visibility.Hidden;
            HideElements();
        }

        public void HideElements()
        {
            UpdateFirstNameTextBox.Visibility = Visibility.Hidden;
            UpdateSecondNameTextBox.Visibility = Visibility.Hidden;
            UpdatePatrynomicTextBox.Visibility = Visibility.Hidden;
            UpdateAgeTextBox.Visibility = Visibility.Hidden;
            UpdateGenderTextBox.Visibility = Visibility.Hidden;
            UpdatePhoneTextBox.Visibility = Visibility.Hidden;
            UpdateAdressTextBox.Visibility = Visibility.Hidden;
            ChangeCustomerButton.Visibility = Visibility.Hidden;
            DeleteCustomerButton.Visibility = Visibility.Hidden;

        }

        public void ShowElements()
        {
            UpdateFirstNameTextBox.Visibility = Visibility.Visible;
            UpdateSecondNameTextBox.Visibility = Visibility.Visible;
            UpdatePatrynomicTextBox.Visibility = Visibility.Visible;
            UpdateAgeTextBox.Visibility = Visibility.Visible;
            UpdateGenderTextBox.Visibility = Visibility.Visible;
            UpdatePhoneTextBox.Visibility = Visibility.Visible;
            UpdateAdressTextBox.Visibility = Visibility.Visible;
            DeleteCustomerButton.Visibility = Visibility.Visible;
            ChangeCustomerButton.Visibility = Visibility.Visible;
        }

        public async Task LoadDataAsync()
        {
            _customers = await _customerService.GetCustomersAsync();
            AllCustomersDataGrid.ItemsSource = _customers;

            _products = await _productsService.GetProductsAsync();
            ProductsComboBox.ItemsSource = _products;
        }

        private async void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer()
            {
                firstName = AddCustomerFirstNameTextBox.Text,
                secondName = AddCustomerSecondNameTextBox.Text,
                patrynomic = AddCustomerPatrynomicTextBox.Text,
                gender = AddCustomerGenderTextBox.Text,
                phoneNumber = AddCustomerPhoneTextBox.Text,
                age = Convert.ToInt32(AddCustomerAgeTextBox.Text),
                adress = AddCustomerAdressTextBox.Text
            };
            await _customerService.AddCustomerAsync(customer);
            LoadDataAsync();
        }

        private async void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            await _customerService.DeleteCustomerAsync(_selectedCustomer.cuId);
            LoadDataAsync();
            HideElements();
        }

        private async void ChangeCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedCustomer.firstName = UpdateFirstNameTextBox.Text;
            _selectedCustomer.secondName = UpdateSecondNameTextBox.Text;
            _selectedCustomer.patrynomic = UpdatePatrynomicTextBox.Text;
            _selectedCustomer.age = Convert.ToInt32(UpdateAgeTextBox.Text);
            _selectedCustomer.gender = UpdateGenderTextBox.Text;
            _selectedCustomer.phoneNumber = UpdatePhoneTextBox.Text;
            _selectedCustomer.adress = UpdateAdressTextBox.Text;
            await _customerService.UpdateCustomerAsync(_selectedCustomer);
            LoadDataAsync();
            HideElements();

        }

        private void AllCustomersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCustomer = AllCustomersDataGrid.SelectedItem as Customer;
            if (_selectedCustomer is not null)
            {
                UpdateFirstNameTextBox.Text = _selectedCustomer.firstName;
                UpdateSecondNameTextBox.Text = _selectedCustomer.secondName;
                UpdatePatrynomicTextBox.Text = _selectedCustomer.patrynomic;
                UpdateGenderTextBox.Text = _selectedCustomer.gender;
                UpdateAgeTextBox.Text = _selectedCustomer.age.ToString();
                UpdatePhoneTextBox.Text = _selectedCustomer.phoneNumber;
                UpdateAdressTextBox.Text = _selectedCustomer.adress;
                ShowElements();
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    FilterLable.Content = "Choose product";
                    FilterCountTextBox.Text = "";

                    Product findProduct = ProductsComboBox.SelectedItem as Product;

                    var foundedCustomers = _customers.ToList().FindAll(cu => cu.Deals.ToList().Any(dl => dl.Product.name == findProduct.name));

                    FilteredCustomersDataGrid.ItemsSource = foundedCustomers;

                    break;
                case 1:
                    FilterLable.Content = "Choose product and enter min value of products";

                    Product foundProduct = ProductsComboBox.SelectedItem as Product;
                    int minValue = 0;
                    minValue = Convert.ToInt32(FilterCountTextBox.Text);


                    var foundedCustomerss = _customers.ToList().FindAll(cu => cu.Deals.ToList().FindAll(dl => dl.Product.name == foundProduct.name).Count >= minValue);

                    FilteredCustomersDataGrid.ItemsSource = foundedCustomerss;

                    break;

            }

            FilteredCustomersDataGrid.Visibility = Visibility.Visible;

        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterComboBox.SelectedIndex == 0)
            {
                FilterLable.Visibility = Visibility.Visible;
                FilterCountTextBox.Visibility = Visibility.Hidden;
                ProductsComboBox.Visibility = Visibility.Visible;
                FilterButton.Visibility = Visibility.Visible;
            }
            if (FilterComboBox.SelectedIndex == 1)
            {
                FilterLable.Visibility = Visibility.Visible;
                FilterCountTextBox.Visibility = Visibility.Visible;
                ProductsComboBox.Visibility = Visibility.Visible;
                FilterButton.Visibility = Visibility.Visible;

            }
        }
    }
}
