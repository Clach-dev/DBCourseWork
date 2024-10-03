using System.Windows;

namespace for_db7
{
    /// <summary>
    /// Логика взаимодействия для DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        public bool isAdmin { get; set; }
        public DataWindow()
        {
            InitializeComponent();
        }

        public DataWindow(bool isAdmin = false)
        {
            InitializeComponent();
            this.isAdmin = isAdmin;
            if (isAdmin)
            {
                UsersPageButton.Visibility = Visibility.Visible;
            }
            else
            {
                UsersPageButton.Visibility = Visibility.Hidden;
            }
        }

        private void CustomersPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/CustomersPage.xaml", UriKind.Relative));
        }

        private void BonusCardsPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/BonusCardsPage.xaml", UriKind.Relative));
        }

        private void UsersPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/UsersPage.xaml", UriKind.Relative));
        }

        private void SellersPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/SellersPage.xaml", UriKind.Relative));
        }

        private void CommercialOrganizationsPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/CommercialOrganizationsPage.xaml", UriKind.Relative));
        }

        private void TradeOutletsPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/TradeOutletsPage.xaml", UriKind.Relative));
        }

        private void OutletSectionsPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/OutletSectionsPage.xaml", UriKind.Relative));
        }

        private void SectionManagesPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/SectionManagersPage.xaml", UriKind.Relative));
        }

        private void DealsPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/DealsPage.xaml", UriKind.Relative));
        }

        private void ProductTypesPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/ProductTypesPage.xaml", UriKind.Relative));
        }

        private void OrdersPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/OrdersPage.xaml", UriKind.Relative));
        }

        private void SuppliersPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/SuppliersPage.xaml", UriKind.Relative));
        }

        private void ProductsPageButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveFrame.Navigate(new Uri("Windows/Pages/ProductsPage.xaml", UriKind.Relative));
        }
    }
}
