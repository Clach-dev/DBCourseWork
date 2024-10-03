using for_db7.Services;
using spp3.Data.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace for_db7.Pages
{
    /// <summary>
    /// Логика взаимодействия для BonusCardPage.xaml
    /// </summary>
    public partial class BonusCardsPage : Page
    {
        private readonly BonusCardsService _bonusCardService;
        private readonly CustomersService _customerService;

        public BonusCard _selectedBonusCard;

        public ObservableCollection<BonusCard> _bonusCards = new ObservableCollection<BonusCard>();
        public ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        public BonusCardsPage()
        {
            InitializeComponent();

            _customerService = new CustomersService();
            _bonusCardService = new BonusCardsService();

            _selectedBonusCard = new BonusCard();

            BonusCardDataGrid.ItemsSource = _bonusCards;
            CustomerComboBox.ItemsSource = _customers;
            NewCustomerComboBox.ItemsSource = _customers;

            HideElements();
            LoadDataAsync();
        }

        public void ShowElements()
        {
            DeleteBonusCardButton.Visibility = Visibility.Visible;
            DiscountLabel.Visibility = Visibility.Visible;
            PhoneLabel.Visibility = Visibility.Visible;
            NumberLabel.Visibility = Visibility.Visible;
            DiscountTextLabel.Visibility = Visibility.Visible;
            PhoneTextLabel.Visibility = Visibility.Visible;
            NumberTextLabel.Visibility = Visibility.Visible;
            NewCustomerTextLabel.Visibility = Visibility.Visible;
            NewDiscountTextLabel.Visibility = Visibility.Visible;
            NewCustomerComboBox.Visibility = Visibility.Visible;
            NewDiscountComboBox.Visibility = Visibility.Visible;
            NewNumberTextBox.Visibility = Visibility.Visible;
            NewNumberTextLabel.Visibility = Visibility.Visible;
            UpdateBonusCardButton.Visibility = Visibility.Visible;
        }

        public void HideElements()
        {
            DeleteBonusCardButton.Visibility = Visibility.Hidden;
            DiscountLabel.Visibility = Visibility.Hidden;
            PhoneLabel.Visibility = Visibility.Hidden;
            NumberLabel.Visibility = Visibility.Hidden;
            DiscountTextLabel.Visibility = Visibility.Hidden;
            PhoneTextLabel.Visibility = Visibility.Hidden;
            NumberTextLabel.Visibility = Visibility.Hidden;
            NewCustomerTextLabel.Visibility = Visibility.Hidden;
            NewDiscountTextLabel.Visibility = Visibility.Hidden;
            NewCustomerComboBox.Visibility = Visibility.Hidden;
            NewDiscountComboBox.Visibility = Visibility.Hidden;
            NewNumberTextBox.Visibility = Visibility.Hidden;
            NewNumberTextLabel.Visibility = Visibility.Hidden;
            UpdateBonusCardButton.Visibility= Visibility.Hidden;
        }

        public async Task LoadDataAsync()
        {
            _bonusCards = await _bonusCardService.GetBonusCardsAsync();
            _customers = await _customerService.GetCustomersAsync();

            BonusCardDataGrid.ItemsSource = _bonusCards;
            CustomerComboBox.ItemsSource = _customers;
            NewCustomerComboBox.ItemsSource = _customers;

        }

        private async void AddBonusCardButton_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = CustomerComboBox.SelectedItem as Customer;
            float discount = (float)Convert.ToDecimal(DiscountComboBox.Text);
            await _bonusCardService.AddBonusCardAsync(discount, customer);
            LoadDataAsync();
        }

        private async void DeleteBonusCardButton_Click(object sender, RoutedEventArgs e)
        {
            await _bonusCardService.DeleteBonusCardAsync(_selectedBonusCard.bcId);
            LoadDataAsync();
            HideElements();
        }

        private async void UpdateBonusCardButton_Click(object sender, RoutedEventArgs e)
        {
            var customer = NewCustomerComboBox.SelectedItem as Customer;
            _selectedBonusCard.cuId = customer.cuId;
            _selectedBonusCard.discount = (float)Convert.ToDecimal(NewDiscountComboBox.Text);
            _selectedBonusCard.number = NewNumberTextBox.Text;
            await _bonusCardService.UpdateBonusCardAsync(_selectedBonusCard);
            LoadDataAsync();
            HideElements();
        }

        private void BonusCardDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBonusCard = BonusCardDataGrid.SelectedItem as BonusCard;
            if (_selectedBonusCard is not null)
            {
                DiscountLabel.Content = _selectedBonusCard.discount.ToString();
                PhoneLabel.Content = _selectedBonusCard.Customer.phoneNumber.ToString();
                NumberLabel.Content = _selectedBonusCard.number;

                ShowElements();
            }
        }
    }
}
