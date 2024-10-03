using API.Data.Models;
using for_db7.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для SellersPage.xaml
    /// </summary>
    public partial class SellersPage : Page
    {
        SellersService _sellersService;
        OutletSectionsService _outletSectionService;
        ObservableCollection<Seller> _sellers;
        ObservableCollection<OutletSection> _outletSections;

        Seller _selectedSeller;
        public SellersPage()
        {
            InitializeComponent();
            HideElements();

            _sellersService = new SellersService();
            _outletSectionService = new OutletSectionsService();
            _sellers = new ObservableCollection<Seller>();
            _outletSections = new ObservableCollection<OutletSection>();
            _selectedSeller = new Seller();

            AddOutletSectionComboBox.ItemsSource = _outletSections;
            AddOutletSectionComboBox.DisplayMemberPath = "sectionName";
            AddOutletSectionComboBox.SelectedValuePath = "osId";
            SellersDataGrid.ItemsSource = _sellers;
            SellersDataGrid.IsReadOnly = true;

            LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            _sellers = await _sellersService.GetSellersAsync();
            SellersDataGrid.ItemsSource = _sellers;
            _outletSections = await _outletSectionService.GetOutletSectionsAsync();
            AddOutletSectionComboBox.ItemsSource = _outletSections;
        }

        public void HideElements()
        {

            CurrentFirstNameLabel.Visibility = Visibility.Hidden;
            CurrentSecondNameLabel.Visibility = Visibility.Hidden;
            CurrentPatrynomicLabel.Visibility = Visibility.Hidden;
            CurrentPhoneNumberLabel.Visibility = Visibility.Hidden;
            CurrentSalaryLabel.Visibility = Visibility.Hidden;
            CurrentEndOfContractLabel.Visibility = Visibility.Hidden;

            OldFirstNameLabel.Visibility = Visibility.Hidden;
            OldSecondNameLabel.Visibility = Visibility.Hidden;
            OldPatrynomicLabel.Visibility = Visibility.Hidden;
            OldPhoneNumberLabel.Visibility = Visibility.Hidden;
            OldSalaryLabel.Visibility = Visibility.Hidden;
            OldEndOfContractLabel.Visibility = Visibility.Hidden;

            NewFirstNameLabel.Visibility = Visibility.Hidden;
            NewSecondNameLabel.Visibility = Visibility.Hidden;
            NewPatrynomicLabel.Visibility = Visibility.Hidden;
            NewPhoneNumberLabel.Visibility = Visibility.Hidden;
            NewSalaryLabel.Visibility = Visibility.Hidden;
            NewEndOfContractLabel.Visibility = Visibility.Hidden;

            EditFirstNameTextBox.Visibility = Visibility.Hidden;
            EditSecondNameTextBox.Visibility = Visibility.Hidden;
            EditPatrynomicTextBox.Visibility = Visibility.Hidden;
            EditPhoneNumberTextBox.Visibility = Visibility.Hidden;
            EditSalaryTextBox.Visibility = Visibility.Hidden;
            EditEndOfContractDatePicker.Visibility = Visibility.Hidden;

            DeleteSellerButton.Visibility = Visibility.Hidden;
            EditSellerButton.Visibility = Visibility.Hidden;
        }
        public void ShowElements()
        {

            CurrentFirstNameLabel.Visibility = Visibility.Visible;
            CurrentSecondNameLabel.Visibility = Visibility.Visible;
            CurrentPatrynomicLabel.Visibility = Visibility.Visible;
            CurrentPhoneNumberLabel.Visibility = Visibility.Visible;
            CurrentSalaryLabel.Visibility = Visibility.Visible;
            CurrentEndOfContractLabel.Visibility = Visibility.Visible;

            OldFirstNameLabel.Visibility = Visibility.Visible;
            OldSecondNameLabel.Visibility = Visibility.Visible;
            OldPatrynomicLabel.Visibility = Visibility.Visible;
            OldPhoneNumberLabel.Visibility = Visibility.Visible;
            OldSalaryLabel.Visibility = Visibility.Visible;
            OldEndOfContractLabel.Visibility = Visibility.Visible;

            NewFirstNameLabel.Visibility = Visibility.Visible;
            NewSecondNameLabel.Visibility = Visibility.Visible;
            NewPatrynomicLabel.Visibility = Visibility.Visible;
            NewPhoneNumberLabel.Visibility = Visibility.Visible;
            NewSalaryLabel.Visibility = Visibility.Visible;
            NewEndOfContractLabel.Visibility = Visibility.Visible;

            EditFirstNameTextBox.Visibility = Visibility.Visible;
            EditSecondNameTextBox.Visibility = Visibility.Visible;
            EditPatrynomicTextBox.Visibility = Visibility.Visible;
            EditPhoneNumberTextBox.Visibility = Visibility.Visible;
            EditSalaryTextBox.Visibility = Visibility.Visible;
            EditEndOfContractDatePicker.Visibility = Visibility.Visible;

            DeleteSellerButton.Visibility = Visibility.Visible;
            EditSellerButton.Visibility = Visibility.Visible;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterComboBox.SelectedIndex == 0)
            {
                FilterOutletLabel.Visibility = Visibility.Hidden;
                FilterTextBox.Visibility = Visibility.Hidden;
                FilterButton.Visibility = Visibility.Hidden;
                double sumOfAllSelary = 0;
                foreach (Seller seller in _sellers)
                {
                    sumOfAllSelary += seller.salary;
                }
                MessageBox.Show($"Summary sellers' salary: {Math.Round(sumOfAllSelary)}");
            }
            else if (FilterComboBox.SelectedIndex == 1)
            {
                FilterOutletLabel.Content = "Enter Outlet type:";
                FilterOutletLabel.Visibility = Visibility.Visible;
                FilterTextBox.Visibility = Visibility.Visible;
                FilterButton.Visibility = Visibility.Visible;
            }
            else if (FilterComboBox.SelectedIndex == 2)
            {
                FilterOutletLabel.Content = "Enter Outlet name:";
                FilterOutletLabel.Visibility = Visibility.Visible;
                FilterTextBox.Visibility = Visibility.Visible;
                FilterButton.Visibility = Visibility.Visible;
            }
            else if (FilterComboBox.SelectedIndex == 3)
            {
                FilterOutletLabel.Content = "Enter seller phone number:";
                FilterOutletLabel.Visibility = Visibility.Visible;
                FilterTextBox.Visibility = Visibility.Visible;
                FilterButton.Visibility = Visibility.Visible;
            }
        }


        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterComboBox.SelectedIndex == 1)
            {
                double sumOfOutletTypeSalary = 0;
                foreach (Seller seller in _sellers)
                {
                    if (seller.OutletSection.TradeOutlet.outletType == FilterTextBox.Text)
                    {
                        sumOfOutletTypeSalary += seller.salary;
                    }
                }
                MessageBox.Show($"Summary sellers' salary: {Math.Round(sumOfOutletTypeSalary * 100) / 100}");
            }
            else if (FilterComboBox.SelectedIndex == 2)
            {
                double sumOfOutletNameSalary = 0;
                foreach (Seller seller in _sellers)
                {
                    if (seller.OutletSection is not null && seller.OutletSection.TradeOutlet.outletName == FilterTextBox.Text)
                    {
                        sumOfOutletNameSalary += seller.salary;
                    }
                }
                MessageBox.Show($"Summary sellers' salary: {Math.Round(sumOfOutletNameSalary * 100) / 100}");
            }
            else if (FilterComboBox.SelectedIndex == 3)
            {
                double sumOfSelledProduct = 0;
                var seller = _sellers.FirstOrDefault(sel => sel.phoneNumber == FilterTextBox.Text);
                if (seller is not null && seller.Deals is not null)
                {
                    foreach (Deal deal in seller.Deals)
                    {
                        if (deal.Product.price is not null)
                        {
                            sumOfSelledProduct += (double)deal.Product.price;
                        }
                    }
                }
                var profit = sumOfSelledProduct / seller.salary;
                MessageBox.Show($"Profit from your seller: {Math.Round(profit * 100) / 100}");
            }


        }

        private async void AddSellerButton_Click(object sender, RoutedEventArgs e)
        {
            var outletSection = AddOutletSectionComboBox.SelectedItem as OutletSection;

            DateOnly? dateOnly;
            if (AddEndOfContractDatePicker.SelectedDate is not null)
            {
                DateTime? dateTime = AddEndOfContractDatePicker.SelectedDate;
                dateOnly = DateOnly.FromDateTime((DateTime)dateTime);
            }
            else
            {
                dateOnly = DateOnly.FromDateTime(DateTime.Now);
            }

            Seller seller = new Seller
            {
                firstName = AddFirstNameTextBox.Text,
                secondName = AddSecondNameTextBox.Text,
                patrynomic = AddPatrynomicTextBox.Text,
                salary = Convert.ToDouble(AddSalaryTextBox.Text),
                phoneNumber = AddPhoneNumberTextBox.Text,
                endOfContract = dateOnly,
                osId = outletSection.osId
            };
            await _sellersService.AddSellerAsync(seller);
            LoadDataAsync();
        }

        private void SellersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedSeller = SellersDataGrid.SelectedItem as Seller;
            if (SellersDataGrid.SelectedItem is not null)
            {
                CurrentFirstNameLabel.Content = _selectedSeller.firstName;
                CurrentSecondNameLabel.Content = _selectedSeller.secondName;
                CurrentPatrynomicLabel.Content = _selectedSeller.patrynomic;
                CurrentPhoneNumberLabel.Content = _selectedSeller.phoneNumber;
                CurrentSalaryLabel.Content = _selectedSeller.salary;
                CurrentEndOfContractLabel.Content = _selectedSeller.endOfContract;
                ShowElements();
            }
        }

        private async void DeleteSellerButton_Click(object sender, RoutedEventArgs e)
        {
            {
                //Seller seller = new Seller
                //{
                //    firstName = CurrentFirstNameLabel.Content.ToString(),
                //    secondName = CurrentSecondNameLabel.Content.ToString(),
                //    phoneNumber = CurrentPhoneNumberLabel.Content.ToString(),
                //    salary = Convert.ToDouble(CurrentSalaryLabel.Content)
                //};

                //var deletedSeller = _sellers.FirstOrDefault(sel => sel.phoneNumber == sel.phoneNumber);


                if (_selectedSeller is not null)
                {
                    await _sellersService.DeleteSellerAsync(_selectedSeller.selId);
                    LoadDataAsync();
                }
                else
                {
                    MessageBox.Show("There's no seller with this info");

                }

                HideElements();
            }
        }

        private async void EditSellerButton_Click(object sender, RoutedEventArgs e)
        {
            //Seller newSeller = new Seller
            //{
            //    firstName = EditFirstNameTextBox.Text,
            //    secondName = EditSecondNameTextBox.Text,
            //    patrynomic = EditPatrynomicTextBox.Text,
            //    phoneNumber = EditPhoneNumberTextBox.Text,
            //    salary = Convert.ToDouble(EditSalaryTextBox.Text),
            //    endOfContract = DateOnly.FromDateTime(EditEndOfContractDatePicker.SelectedDate.Value)
            //};

            //Seller oldSeller = new Seller
            //{
            //    firstName = CurrentFirstNameLabel.Content.ToString(),
            //    secondName = CurrentSecondNameLabel.Content.ToString(),
            //    patrynomic = CurrentPatrynomicLabel.Content.ToString(),
            //    phoneNumber = CurrentPhoneNumberLabel.Content.ToString(),
            //    salary = Convert.ToDouble(CurrentSalaryLabel.Content),
            //    endOfContract = DateOnly.FromDateTime(Convert.ToDateTime(CurrentEndOfContractLabel.Content.ToString()))
            //};

            //var seller = _sellers.FirstOrDefault(sel => sel.phoneNumber == oldSeller.phoneNumber);
            //if (seller is not null)
            //{
            //    seller.firstName = newSeller.firstName;
            //    seller.secondName = newSeller.secondName;
            //    seller.patrynomic = newSeller.patrynomic;
            //    seller.phoneNumber = newSeller.phoneNumber;
            //    seller.salary = newSeller.salary;
            //    seller.endOfContract = newSeller.endOfContract;
            //    await _sellersService.UpdateSellerAsync(seller);
            //    LoadDataAsync();
            //}
            if(_selectedSeller is not null)
            {
                _selectedSeller.firstName = EditFirstNameTextBox.Text;
                _selectedSeller.secondName = EditSecondNameTextBox.Text;
                _selectedSeller.patrynomic = EditPatrynomicTextBox.Text;
                _selectedSeller.phoneNumber = EditPhoneNumberTextBox.Text;
                _selectedSeller.salary = Convert.ToDouble(EditSalaryTextBox.Text);
                _selectedSeller.endOfContract = DateOnly.FromDateTime(EditEndOfContractDatePicker.SelectedDate.Value);
                await _sellersService.UpdateSellerAsync(_selectedSeller);
                LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no that seller");
            }
            HideElements();
        }


    }
}
