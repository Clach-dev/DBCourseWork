using API.Data.Models;
using for_db7.Services;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для SectionManagersPage.xaml
    /// </summary>
    public partial class SectionManagersPage : Page
    {

        SectionManagersService _sectionManagersService;
        ObservableCollection<SectionManager> _sectionManagers;
        OutletSectionsService _outletSectionService;
        ObservableCollection<OutletSection> _outletSections;

        SectionManager _selectedSectionManager;
        public SectionManagersPage()
        {
            InitializeComponent();


            _sectionManagersService = new SectionManagersService();
            _sectionManagers = new ObservableCollection<SectionManager>();
            _outletSections = new ObservableCollection<OutletSection>();
            _outletSectionService = new OutletSectionsService();
            _selectedSectionManager = new SectionManager();

            SectionManagersDataGrid.ItemsSource = _sectionManagers;
            SectionManagersDataGrid.IsReadOnly = true;

            LoadDataAsync();
            HideElements();
        }

        public async void LoadDataAsync()
        {
            _sectionManagers = await _sectionManagersService.GetSectionManagersAsync();
            SectionManagersDataGrid.ItemsSource = _sectionManagers;
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
            CurrentExperienceLabel.Visibility = Visibility.Hidden;

            OldFirstNameLabel.Visibility = Visibility.Hidden;
            OldSecondNameLabel.Visibility = Visibility.Hidden;
            OldPatrynomicLabel.Visibility = Visibility.Hidden;
            OldPhoneNumberLabel.Visibility = Visibility.Hidden;
            OldSalaryLabel.Visibility = Visibility.Hidden;
            OldExperienceLabel.Visibility = Visibility.Hidden;

            NewFirstNameLabel.Visibility = Visibility.Hidden;
            NewSecondNameLabel.Visibility = Visibility.Hidden;
            NewPatrynomicLabel.Visibility = Visibility.Hidden;
            NewPhoneNumberLabel.Visibility = Visibility.Hidden;
            NewSalaryLabel.Visibility = Visibility.Hidden;
            NewExperienceLabel.Visibility = Visibility.Hidden;

            EditFirstNameTextBox.Visibility = Visibility.Hidden;
            EditSecondNameTextBox.Visibility = Visibility.Hidden;
            EditPatrynomicTextBox.Visibility = Visibility.Hidden;
            EditPhoneNumberTextBox.Visibility = Visibility.Hidden;
            EditSalaryTextBox.Visibility = Visibility.Hidden;
            EditExperienceTextBox.Visibility = Visibility.Hidden;

            DeleteSectionManagerButton.Visibility = Visibility.Hidden;
            EditSectionManagerButton.Visibility = Visibility.Hidden;
        }
        public void ShowElements()
        {

            CurrentFirstNameLabel.Visibility = Visibility.Visible;
            CurrentSecondNameLabel.Visibility = Visibility.Visible;
            CurrentPatrynomicLabel.Visibility = Visibility.Visible;
            CurrentPhoneNumberLabel.Visibility = Visibility.Visible;
            CurrentSalaryLabel.Visibility = Visibility.Visible;
            CurrentExperienceLabel.Visibility = Visibility.Visible;

            OldFirstNameLabel.Visibility = Visibility.Visible;
            OldSecondNameLabel.Visibility = Visibility.Visible;
            OldPatrynomicLabel.Visibility = Visibility.Visible;
            OldPhoneNumberLabel.Visibility = Visibility.Visible;
            OldSalaryLabel.Visibility = Visibility.Visible;
            OldExperienceLabel.Visibility = Visibility.Visible;

            NewFirstNameLabel.Visibility = Visibility.Visible;
            NewSecondNameLabel.Visibility = Visibility.Visible;
            NewPatrynomicLabel.Visibility = Visibility.Visible;
            NewPhoneNumberLabel.Visibility = Visibility.Visible;
            NewSalaryLabel.Visibility = Visibility.Visible;
            NewExperienceLabel.Visibility = Visibility.Visible;

            EditFirstNameTextBox.Visibility = Visibility.Visible;
            EditSecondNameTextBox.Visibility = Visibility.Visible;
            EditPatrynomicTextBox.Visibility = Visibility.Visible;
            EditPhoneNumberTextBox.Visibility = Visibility.Visible;
            EditSalaryTextBox.Visibility = Visibility.Visible;
            EditExperienceTextBox.Visibility = Visibility.Visible;

            DeleteSectionManagerButton.Visibility = Visibility.Visible;
            EditSectionManagerButton.Visibility = Visibility.Visible;
        }

        private async void AddSellerButton_Click(object sender, RoutedEventArgs e)
        {
            var outletSection = AddOutletSectionComboBox.SelectedItem as OutletSection;

            SectionManager sectionManager = new SectionManager
            {
                firstName = AddFirstNameTextBox.Text,
                secondName = AddSecondNameTextBox.Text,
                patrynomic = AddPatrynomicTextBox.Text,
                salary = Convert.ToDouble(AddSalaryTextBox.Text),
                phoneNumber = AddPhoneNumberTextBox.Text,
                experience = Convert.ToInt32(AddExperienceTextBox.Text),
                osId = outletSection.osId
            };

            if(_sectionManagers.FirstOrDefault(sm => sm.osId == sectionManager.osId) == null)
            {
                await _sectionManagersService.AddSectionManagerAsync(sectionManager);
            }
            else
            {
                MessageBox.Show("That section is alredy taken");
            }
            LoadDataAsync();
        }

        private void SectionManagersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedSectionManager = SectionManagersDataGrid.SelectedItem as SectionManager;
            if(_selectedSectionManager is not null)
            {
                CurrentFirstNameLabel.Content = _selectedSectionManager.firstName;
                CurrentSecondNameLabel.Content = _selectedSectionManager.secondName;
                CurrentPatrynomicLabel.Content = _selectedSectionManager.patrynomic;
                CurrentExperienceLabel.Content = _selectedSectionManager.experience;
                CurrentPhoneNumberLabel.Content = _selectedSectionManager.phoneNumber;
                CurrentSalaryLabel.Content = _selectedSectionManager.salary;
                ShowElements();
            }
        }

        private async void EditSectionManagerButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSectionManager is not null)
            {
                _selectedSectionManager.firstName = EditFirstNameTextBox.Text;
                _selectedSectionManager.secondName = EditSecondNameTextBox.Text;
                _selectedSectionManager.patrynomic = EditPatrynomicTextBox.Text;
                _selectedSectionManager.phoneNumber = EditPhoneNumberTextBox.Text;
                _selectedSectionManager.salary = Convert.ToDouble(EditSalaryTextBox.Text);
                _selectedSectionManager.experience = Convert.ToInt32(EditExperienceTextBox.Text);
                await _sectionManagersService.UpdateSectionManagerAsync(_selectedSectionManager);
                LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no that section manager");
            }
            HideElements();
        }

        private void DeleteSectionManagerButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSectionManager is not null)
            {
                _sectionManagersService.DeleteSectionManagerAsync(_selectedSectionManager.smId);
                LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no section manager with this info");

            }

            HideElements();
        }
    }
}
