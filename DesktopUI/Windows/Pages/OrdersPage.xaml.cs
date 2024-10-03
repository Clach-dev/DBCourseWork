using API.Data.Models;
using for_db7.Services;
using spp3.Data.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        OrdersService _ordersService;
        ObservableCollection<Order> _orders;
        Order _selectedOrder;

        ProductsService _productsService;
        ObservableCollection<Product> _products;

        SuppliersService _suppliersService;
        ObservableCollection<Supplier> _suppliers;

        public OrdersPage()
        {
            InitializeComponent();

            _ordersService = new OrdersService();
            _orders = new ObservableCollection<Order>();
            _selectedOrder = new Order();

            _products = new ObservableCollection<Product>();
            _suppliers = new ObservableCollection<Supplier>();
            _productsService = new ProductsService();
            _suppliersService = new SuppliersService();

            OrdersDataGrid.ItemsSource = _orders;
            OrdersDataGrid.IsReadOnly = true;
            LoadDataAsync();
            HideElements();
            HideFilters();
        }

        public async Task LoadDataAsync()
        {
            _orders = await _ordersService.GetOrdersAsync();
            OrdersDataGrid.ItemsSource = _orders;

            _products = await _productsService.GetProductsAsync();
            EditProductComboBox.ItemsSource = _products;

            _suppliers = await _suppliersService.GetSuppliersAsync();
            EditSupplierComboBox.ItemsSource = _suppliers;
        }

        public void HideElements()
        {
            NewOrderNumberLabel.Visibility = Visibility.Hidden;
            NewOrderStatusLabel.Visibility = Visibility.Hidden;
            NewQuantityLabel.Visibility = Visibility.Hidden;
            NewProductLabel.Visibility =Visibility.Hidden;
            NewSupplierLabel.Visibility = Visibility.Hidden;

            EditOrderNumberTextBox.Visibility = Visibility.Hidden;
            EditQuantityTextBox.Visibility = Visibility.Hidden;
            EditOrderStatusTextBox.Visibility = Visibility.Hidden;
            EditProductComboBox.Visibility = Visibility.Hidden;
            EditSupplierComboBox.Visibility = Visibility.Hidden;

            EditOrderButton.Visibility = Visibility.Hidden;
            DeleteOrderButton.Visibility = Visibility.Hidden;
        }

        public void ShowElements()
        {

            NewOrderNumberLabel.Visibility = Visibility.Visible;
            NewOrderStatusLabel.Visibility = Visibility.Visible;
            NewQuantityLabel.Visibility = Visibility.Visible;
            NewProductLabel.Visibility = Visibility.Visible;
            NewSupplierLabel.Visibility = Visibility.Visible;

            EditOrderNumberTextBox.Visibility = Visibility.Visible;
            EditQuantityTextBox.Visibility = Visibility.Visible;
            EditOrderStatusTextBox.Visibility = Visibility.Visible;
            EditProductComboBox.Visibility = Visibility.Visible;
            EditSupplierComboBox.Visibility = Visibility.Visible;

            EditOrderButton.Visibility = Visibility.Visible;
            DeleteOrderButton.Visibility = Visibility.Visible;
        }

        public void HideFilters()
        {
            FilterquntityLabel.Visibility = Visibility.Hidden;
            FilterNameLabel.Visibility = Visibility.Hidden;
            FilteredOrdersDataGrid.Visibility = Visibility.Hidden;
            FilteredSuppliersDataGrid.Visibility = Visibility.Hidden;
            FilterTextBox.Visibility = Visibility.Hidden;
            FilterQuantityTextBox.Visibility = Visibility.Hidden;
            FilterButton.Visibility = Visibility.Hidden;
        }

        public void ShowFilters()
        {
            FilterquntityLabel.Visibility = Visibility.Visible;
            FilterNameLabel.Visibility = Visibility.Visible;
            FilteredOrdersDataGrid.Visibility = Visibility.Visible;
            FilteredSuppliersDataGrid.Visibility = Visibility.Visible;
            FilterTextBox.Visibility = Visibility.Visible;
            FilterQuantityTextBox.Visibility = Visibility.Visible;
            FilterButton.Visibility = Visibility.Visible;
        }

        private void OrdersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedOrder = OrdersDataGrid.SelectedItem as Order;
            if(_selectedOrder is not null)
            {
                EditOrderNumberTextBox.Text = _selectedOrder.orderNumber.ToString();
                EditOrderStatusTextBox.Text = _selectedOrder.orderStatus;
                EditQuantityTextBox.Text = _selectedOrder.quantity.ToString();
                ShowElements();
            } 
        }

        private async void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order()
            {
                orderNumber = Convert.ToInt32(AddOrderNumberTextBox.Text),
                orderStatus = AddOrderStatusTextBox.Text,
                quantity = Convert.ToInt32(AddQuantityTextBox.Text)
            };
            if (order != null)
            {
                await _ordersService.AddOrderAsync(order);
                await LoadDataAsync();
            }
        }

        private async void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedOrder is not null)
            {
                await _ordersService.DeleteOrderAsync(_selectedOrder.orId);
                await LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no that order");
            }
            HideElements();
        }

        private async void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedOrder is not null)
            {
                _selectedOrder.orderNumber = Convert.ToInt32(EditOrderNumberTextBox.Text);
                _selectedOrder.orderStatus = EditOrderStatusTextBox.Text;
                _selectedOrder.quantity = Convert.ToInt32(EditQuantityTextBox.Text);
                if(EditProductComboBox.SelectedItem != null)
                {
                    ProductToOrder prToOr = new ProductToOrder()
                    {
                        orId = _selectedOrder.orId,
                        prId = (EditProductComboBox.SelectedItem as Product).prId
                    };
                    _selectedOrder.ProductsToOrders.Add(prToOr);

                }
                if(EditSupplierComboBox.SelectedItem != null)
                {
                    OrderToSupplier orToSu = new OrderToSupplier()
                    {
                        orId = _selectedOrder.orId,
                        suId = (EditSupplierComboBox.SelectedItem as Supplier).suId
                    };
                    _selectedOrder.OrdersToSuppliers.Add(orToSu);
                }
                await _ordersService.UpdateOrderAsync(_selectedOrder);
                await LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no that order");
            }
            HideElements();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if(FilterComboBox.SelectedIndex == 0)
            {
                FilteredSuppliersDataGrid.ItemsSource = _suppliers.ToList().FindAll(su => su.OrdersToSuppliers == su.OrdersToSuppliers.Where(ots => ots.Order.ProductsToOrders == ots.Order.ProductsToOrders.Where(pto => pto.Product.name == FilterTextBox.Text)));
            }
            else if(FilterComboBox.SelectedIndex == 1)
            {
                var _supplyProducts = _suppliers.ToList().FindAll(su => su.OrdersToSuppliers == su.OrdersToSuppliers.Where(ots => ots.Order.ProductsToOrders == ots.Order.ProductsToOrders.Where(pto => pto.Product.name == FilterTextBox.Text)));

                FilteredSuppliersDataGrid.ItemsSource = _supplyProducts.FindAll(su => su.quantity >= Convert.ToInt32(FilterQuantityTextBox.Text));
            }
            else if(FilterComboBox.SelectedIndex == 2)
            {
                FilteredOrdersDataGrid.ItemsSource = _orders.ToList().FindAll(or => or.orderNumber == Convert.ToInt32(FilterTextBox.Text));
            }

        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FilterComboBox.SelectedIndex == 0 )
            {
                ShowFilters();
                FilterquntityLabel.Visibility = Visibility.Hidden;
                FilterQuantityTextBox.Visibility = Visibility.Hidden;
                FilteredOrdersDataGrid.Visibility = Visibility.Hidden;
                FilterNameLabel.Content = "Enter Product name";
            }
            else if(FilterComboBox.SelectedIndex == 1 )
            {
                ShowFilters();
                FilteredOrdersDataGrid.Visibility = Visibility.Hidden;
                FilterNameLabel.Content = "Enter Product name";
                FilterquntityLabel.Content = "Enter quantity";
            }
            else if(FilterComboBox.SelectedIndex == 2 )
            {
                ShowFilters();
                FilterquntityLabel.Visibility = Visibility.Hidden;
                FilterQuantityTextBox.Visibility = Visibility.Hidden;
                FilteredSuppliersDataGrid.Visibility = Visibility.Hidden;
                FilterNameLabel.Content = "Enter order number";
            }
        }
    }
}
