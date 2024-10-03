using API.Data.Models;
using for_db7.Services;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для SuppliersPage.xaml
    /// </summary>
    public partial class SuppliersPage : Page
    {
        SuppliersService _suppliersService;
        ObservableCollection<Supplier> _suppliers;
        Supplier _selecteddSupplier;

        public SuppliersPage()
        {
            InitializeComponent();

            _suppliersService = new SuppliersService();
            _suppliers = new ObservableCollection<Supplier>();

            _selecteddSupplier = new Supplier();

            SuppliersDataGrid.ItemsSource = _suppliers;
            SuppliersDataGrid.IsReadOnly = true;
            LoadDataAsync();
            HideElements();
        }

        public async void LoadDataAsync()
        {
            _suppliers = await _suppliersService.GetSuppliersAsync();
            SuppliersDataGrid.ItemsSource = _suppliers;
        }


        public void HideElements()
        {
            CurrentNameLabel.Visibility = Visibility.Hidden;
            OldNameLabel.Visibility = Visibility.Hidden;
            NewNameLabel.Visibility = Visibility.Hidden;
            EditNameTextBox.Visibility = Visibility.Hidden;

            CurrentPriceLabel.Visibility = Visibility.Hidden;
            OldPriceLabel.Visibility = Visibility.Hidden;
            NewPriceLabel.Visibility = Visibility.Hidden;
            EditPriceTextBox.Visibility = Visibility.Hidden;

            CurrentQuantityLabel.Visibility = Visibility.Hidden;
            OldQuantityLabel.Visibility = Visibility.Hidden;
            NewQuantityLabel.Visibility = Visibility.Hidden;
            EditQuantityTextBox.Visibility = Visibility.Hidden;

            DeleteSupplierButton.Visibility = Visibility.Hidden;
            EditSupplierButton.Visibility = Visibility.Hidden;
        }
        public void ShowElements()
        {
            CurrentNameLabel.Visibility = Visibility.Visible;
            OldNameLabel.Visibility = Visibility.Visible;
            NewNameLabel.Visibility = Visibility.Visible;
            EditNameTextBox.Visibility = Visibility.Visible;

            CurrentPriceLabel.Visibility = Visibility.Visible;
            OldPriceLabel.Visibility = Visibility.Visible;
            NewPriceLabel.Visibility = Visibility.Visible;
            EditPriceTextBox.Visibility = Visibility.Visible;

            CurrentQuantityLabel.Visibility = Visibility.Visible;
            OldQuantityLabel.Visibility = Visibility.Visible;
            NewQuantityLabel.Visibility = Visibility.Visible;
            EditQuantityTextBox.Visibility = Visibility.Visible;

            DeleteSupplierButton.Visibility = Visibility.Visible;
            EditSupplierButton.Visibility = Visibility.Visible;
        }

        private void SuppliersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selecteddSupplier = SuppliersDataGrid.SelectedItem as Supplier;
            
            if(_selecteddSupplier is not null)
            {
            CurrentNameLabel.Content = _selecteddSupplier.supplierName;
                CurrentPriceLabel.Content = _selecteddSupplier.price;
                CurrentQuantityLabel.Content = _selecteddSupplier.quantity;
                ShowElements();
            }

        }

        private async void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            Supplier supplier = new Supplier()
            {
                supplierName = AddNameTextBox.Text,
                price = Convert.ToDouble(AddPriceTextBox.Text),
                quantity = Convert.ToInt32(AddQuantityTextBox.Text)
            };

            await _suppliersService.AddSupplierAsync(supplier);
            LoadDataAsync();
        }

        private async void DeleteSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            await _suppliersService.DeleteSupplierAsync(_selecteddSupplier.suId);
            LoadDataAsync();
            HideElements();

        }

        private async void EditSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            _selecteddSupplier.supplierName = EditNameTextBox.Text;
            _selecteddSupplier.price = Convert.ToDouble(EditPriceTextBox.Text);
            _selecteddSupplier.quantity = Convert.ToInt32(EditQuantityTextBox.Text);
            await _suppliersService.UpdateSupplierAsync(_selecteddSupplier);
            LoadDataAsync();
            HideElements();
        }
    }
}
