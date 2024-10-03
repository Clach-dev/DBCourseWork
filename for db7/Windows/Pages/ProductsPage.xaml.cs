using for_db7.Services;
using spp3.Data.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        ProductsService _productsService;
        ObservableCollection<Product> _products;
        Product _selectedProduct;

        ProductTypesService _productTypesService;
        ObservableCollection<ProductType> _productTypes;

        DealsService _dealsService;
        
        public ProductsPage()
        {
            InitializeComponent();

            _productsService = new ProductsService();
            _products = new ObservableCollection<Product>();
            _selectedProduct = new Product();
            _productTypesService = new ProductTypesService();
            _productTypes = new ObservableCollection<ProductType>();
            _dealsService = new DealsService(); 


            ProductsDataGrid.ItemsSource = _products;
            ProductsDataGrid.IsReadOnly = true;
            LoadDataAsync();
            HideElements();
            HideFilters();
        }

        public async Task LoadDataAsync()
        {
            _products = await _productsService.GetProductsAsync();
            ProductsDataGrid.ItemsSource = _products;

            _productTypes = await _productTypesService.GetProductTypesAsync();
            AddProductTypeComboBox.ItemsSource = _productTypes;
            EditProductTypeComboBox.ItemsSource = _productTypes;

        }

        public void HideElements()
        {
            CurrentNameLabel.Visibility = Visibility.Hidden;
            CurrentPriceLabel.Visibility = Visibility.Hidden;
            CurrentQuantityLabel.Visibility = Visibility.Hidden;
            CurrentTypeLabel.Visibility = Visibility.Hidden;

            OldNameLabel.Visibility = Visibility.Hidden;
            OldPriceLabel.Visibility = Visibility.Hidden;
            OldQuantityLabel.Visibility = Visibility.Hidden;
            OldTypeLabel.Visibility = Visibility.Hidden;

            NewNameLabel.Visibility= Visibility.Hidden;
            NewPriceLabel.Visibility = Visibility.Hidden;
            NewQuantityLabel.Visibility = Visibility.Hidden;
            NewTypeLabel.Visibility= Visibility.Hidden;

            EditNameTextBox.Visibility = Visibility.Hidden;
            EditPriceTextBox.Visibility = Visibility.Hidden;
            EditQuantityTextBox.Visibility = Visibility.Hidden;
            EditProductTypeComboBox.Visibility = Visibility.Hidden;

            EditProductButton.Visibility = Visibility.Hidden;
            DeleteProductButton.Visibility= Visibility.Hidden;
        }

        public void ShowElements()
        {
            CurrentNameLabel.Visibility = Visibility.Visible;
            CurrentPriceLabel.Visibility = Visibility.Visible;
            CurrentQuantityLabel.Visibility = Visibility.Visible;
            CurrentTypeLabel.Visibility = Visibility.Visible;

            OldNameLabel.Visibility = Visibility.Visible;
            OldPriceLabel.Visibility = Visibility.Visible;
            OldQuantityLabel.Visibility = Visibility.Visible;
            OldTypeLabel.Visibility = Visibility.Visible;

            NewNameLabel.Visibility = Visibility.Visible;
            NewPriceLabel.Visibility = Visibility.Visible;
            NewQuantityLabel.Visibility = Visibility.Visible;
            NewTypeLabel.Visibility = Visibility.Visible;

            EditNameTextBox.Visibility = Visibility.Visible;
            EditPriceTextBox.Visibility = Visibility.Visible;
            EditQuantityTextBox.Visibility = Visibility.Visible;
            EditProductTypeComboBox.Visibility = Visibility.Visible;

            EditProductButton.Visibility = Visibility.Visible;
            DeleteProductButton.Visibility = Visibility.Visible;
        }

        public void HideFilters()
        {
            FilterButton.Visibility = Visibility.Hidden;
            FilteredDataGrid.Visibility = Visibility.Hidden;
            FilterTextBox.Visibility = Visibility.Hidden;
            FilterOutletLabel.Visibility = Visibility.Hidden;
            FilterOutletTextBox.Visibility = Visibility.Hidden;
            FilterProductLabel.Visibility = Visibility.Hidden;
        }

        public void ShowFilters()
        {
            FilterButton.Visibility = Visibility.Visible;
            FilteredDataGrid.Visibility = Visibility.Visible;
            FilterTextBox.Visibility = Visibility.Visible;
            FilterOutletLabel.Visibility = Visibility.Visible;
        }

        private void ProductsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedProduct = ProductsDataGrid.SelectedItem as Product;
            if (_selectedProduct != null)
            {
                CurrentNameLabel.Content = _selectedProduct.name;
                CurrentPriceLabel.Content = _selectedProduct.price;
                CurrentQuantityLabel.Content = _selectedProduct.quantity;
                CurrentTypeLabel.Content = _selectedProduct.ProductType.name;
                ShowElements();
            }
        }

        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product()
            {
                name = AddNameTextBox.Text,
                price = Convert.ToDouble(AddPriceTextBox.Text),
                quantity = Convert.ToInt32(AddQuantityTextBox.Text),
                ptId = (AddProductTypeComboBox.SelectedItem as ProductType).ptId
            };
            if(product is not null)
            {
                await _productsService.AddProductAsync(product);
                await LoadDataAsync();
            }
        }

        private async void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedProduct is not null)
            {
                await _productsService.DeleteProductAsync(_selectedProduct.prId);
                await LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no that product");
            }
            HideElements();
        }

        private async void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedProduct != null)
            {
                _selectedProduct.price = Convert.ToDouble(EditPriceTextBox.Text);
                _selectedProduct.quantity = Convert.ToInt32(EditQuantityTextBox.Text);
                _selectedProduct.name = EditNameTextBox.Text;
                _selectedProduct.ptId = (EditProductTypeComboBox.SelectedItem as ProductType).ptId;

                await _productsService.UpdateProductAsync(_selectedProduct);
                await LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no that product");
            }
            HideElements();
        }

        private async void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if(FilterComboBox.SelectedIndex == 0)
            {
                var deals = await _dealsService.GetDealsAsync();
                FilteredDataGrid.ItemsSource = deals.ToList().FindAll(dl => dl.Product.name == FilterTextBox.Text);
            }
            else if(FilterComboBox.SelectedIndex == 1)
            {
                var deals = await _dealsService.GetDealsAsync();
                var filteredDeals = deals.ToList().FindAll(dl => dl.Product.name == FilterTextBox.Text);
                FilteredDataGrid.ItemsSource = filteredDeals.FindAll(dl => dl.Seller.OutletSection.TradeOutlet.outletName == FilterOutletTextBox.Text);
            }
            else if(FilterComboBox.SelectedIndex == 2)
            {
                var deals = await _dealsService.GetDealsAsync();
                var filteredDeals = deals.ToList().FindAll(dl => dl.Product.name == FilterTextBox.Text);
                FilteredDataGrid.ItemsSource = filteredDeals.FindAll(dl => dl.Seller.OutletSection.TradeOutlet.outletType == FilterOutletTextBox.Text);
            }
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterProductLabel.Visibility = Visibility.Visible;
            if (FilterComboBox.SelectedIndex == 1)
            {
                ShowFilters();
                FilterOutletLabel.Content = "Enter outlet name:";
                FilterOutletTextBox.Visibility = Visibility.Visible;
            }
            else if (FilterComboBox.SelectedIndex == 2)
            {
                ShowFilters();
                FilterOutletLabel.Content = "Enter outlet type:";
                FilterOutletTextBox.Visibility = Visibility.Visible;
            }
            else if(FilterComboBox.SelectedIndex == 0)
            {
                ShowFilters();
                FilterOutletLabel.Visibility = Visibility.Hidden;
                FilterProductLabel.Content = "Enter product:";
            }
            else
            {
                HideFilters();
            }
        }
    }
}
