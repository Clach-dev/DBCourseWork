using for_db7.Services;
using spp3.Data.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductTypesPage.xaml
    /// </summary>
    public partial class ProductTypesPage : Page
    {
        ProductTypesService _productTypesService;
        ObservableCollection<ProductType> _productTypes;
        ProductType _selectedProductType;
        public ProductTypesPage()
        {
            InitializeComponent();

            _productTypesService = new ProductTypesService();
            _productTypes = new ObservableCollection<ProductType>();
            _selectedProductType = new ProductType();
            ProductTypesDataGrid.ItemsSource = _productTypes;
            ProductTypesDataGrid.IsReadOnly = true;
            LoadDataAsync();
            HideElements();
        }

        public async void LoadDataAsync()
        {
            _productTypes = await _productTypesService.GetProductTypesAsync();
            ProductTypesDataGrid.ItemsSource = _productTypes;
        }
        public void HideElements()
        {
            CurrentTypeLabel.Visibility = Visibility.Hidden;
            CurrentAgeLimitLabel.Visibility = Visibility.Hidden;

            OldAgeLimitLabel.Visibility = Visibility.Hidden;
            OldTypeLabel.Visibility = Visibility.Hidden;

            NewAgeLimitLabel.Visibility = Visibility.Hidden;
            NewTypeLabel.Visibility = Visibility.Hidden;

            EditAgeLimitTextBox.Visibility = Visibility.Hidden;
            EditTypeTextBox.Visibility = Visibility.Hidden;

            DeleteProductTypeButton.Visibility = Visibility.Hidden;
            EditProductTypeButton.Visibility = Visibility.Hidden;
        }
        public void ShowElements()
        {

            CurrentTypeLabel.Visibility = Visibility.Visible;
            CurrentAgeLimitLabel.Visibility = Visibility.Visible;

            OldAgeLimitLabel.Visibility = Visibility.Visible;
            OldTypeLabel.Visibility = Visibility.Visible;

            NewAgeLimitLabel.Visibility = Visibility.Visible;
            NewTypeLabel.Visibility = Visibility.Visible;

            EditAgeLimitTextBox.Visibility = Visibility.Visible;
            EditTypeTextBox.Visibility = Visibility.Visible;

            DeleteProductTypeButton.Visibility = Visibility.Visible;
            EditProductTypeButton.Visibility = Visibility.Visible;
        }

        private void ProductTypesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedProductType = ProductTypesDataGrid.SelectedItem as ProductType;
            if( _selectedProductType is not null)
            {
                CurrentAgeLimitLabel.Content = _selectedProductType.ageLimit.ToString();
                CurrentTypeLabel.Content = _selectedProductType.name;
                ShowElements();
            }
        }

        private async void AddProductTypeButton_Click(object sender, RoutedEventArgs e)
        {
            ProductType productType = new ProductType()
            {
                name = AddTypeTextBox.Text,
                ageLimit = Convert.ToInt32(AddAgeLimitTextBox.Text)
            };
            await _productTypesService.AddProductTypeAsync(productType);
            LoadDataAsync();
        }

        private async void DeleteProductTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if( _selectedProductType is not null )
            {
                await _productTypesService.DeleteProductTypeAsync(_selectedProductType.ptId);
                LoadDataAsync();
                HideElements();

            }
        }

        private async void EditProductTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedProductType is not null )
            {
                _selectedProductType.name = EditTypeTextBox.Text;
                _selectedProductType.ageLimit = Convert.ToInt32(EditAgeLimitTextBox.Text);
                
                await _productTypesService.UpdateProductTypeAsync(_selectedProductType);
                LoadDataAsync();
                HideElements();
            }
        }
    }
}
