using API.Data.Models;
using for_db7.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для CommercialOrganizationPage.xaml
    /// </summary>
    public partial class CommercialOrganizationPage : Page
    {
        CommercialOrganizationsService _commercialOrganizationsService;
        ObservableCollection<CommercialOrganization> _commercialOrganizations;

        CommercialOrganization _selectedCommercialOrganization;

        public CommercialOrganizationPage()
        {
            InitializeComponent();
            HideElements();

            _commercialOrganizationsService = new CommercialOrganizationsService();
            _commercialOrganizations = new ObservableCollection<CommercialOrganization>();

            _selectedCommercialOrganization = new CommercialOrganization();

            CommercialOrganizationDataGrid.ItemsSource = _commercialOrganizations;
            CommercialOrganizationDataGrid.IsReadOnly = true;

            LoadDataAsync();
        }

        public async void LoadDataAsync()
        {
            _commercialOrganizations = await _commercialOrganizationsService.GetCommercialOrganizationsAsync();
            CommercialOrganizationDataGrid.ItemsSource = _commercialOrganizations;
        }

        public void HideElements()
        {

            CurrentCommercialOrganizationNameLabel.Visibility = Visibility.Hidden;

            OldCommercialOrganizationNameLabel.Visibility = Visibility.Hidden;

            NewCommercialOrganizationNameLabel.Visibility = Visibility.Hidden;

            EditCommercialOrganizationNameTextBox.Visibility = Visibility.Hidden;

            DeleteCommerccialOrganizationButton.Visibility = Visibility.Hidden;
            EditCommercialOrganizationButton.Visibility = Visibility.Hidden;
        }
        public void ShowElements()
        {

            CurrentCommercialOrganizationNameLabel.Visibility = Visibility.Visible;
            OldCommercialOrganizationNameLabel.Visibility = Visibility.Visible;
            NewCommercialOrganizationNameLabel.Visibility = Visibility.Visible;

            EditCommercialOrganizationNameTextBox.Visibility = Visibility.Visible;

            DeleteCommerccialOrganizationButton.Visibility = Visibility.Visible;
            EditCommercialOrganizationButton.Visibility = Visibility.Visible;
        }

        private void CommercialOrganizationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCommercialOrganization = CommercialOrganizationDataGrid.SelectedItem as CommercialOrganization;
            if (CommercialOrganizationDataGrid.SelectedItem is not null)
            {
                CurrentCommercialOrganizationNameLabel.Content = _selectedCommercialOrganization.organizationName;
                ShowElements();
            }
        }


        private async void AddCommercialOrganizationButton_Click(object sender, RoutedEventArgs e)
        {
            CommercialOrganization commercialOrganization = new CommercialOrganization
            {
                organizationName = AddCommercialOrganizationNameTextBox.Text
            };

            await _commercialOrganizationsService.AddCommercialOrganizationAsync(commercialOrganization);
            LoadDataAsync();
        }

        private async void DeleteCommerccialOrganizationButton_Click(object sender, RoutedEventArgs e)
        {
            //CommercialOrganization commercialOrganization = new CommercialOrganization
            //{
            //    organizationName = CurrentCommercialOrganizationNameLabel.Content.ToString()
            //};

            //var deletedComOrg = _commercialOrganizations.FirstOrDefault(co => co.organizationName == commercialOrganization.organizationName);

            //if (deletedComOrg is not null)
            //{
            //    await _commercialOrganizationsService.DeleteCommercialOrganizationAsync(deletedComOrg.coId);
            //    LoadDataAsync();
            //}
            //else
            //{
            //    MessageBox.Show("There's no organization with this name or password");

            //}

            if (_selectedCommercialOrganization is not null)
            {
                await _commercialOrganizationsService.DeleteCommercialOrganizationAsync(_selectedCommercialOrganization.coId);
                LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no organization with this name or password");

            }

            HideElements();
        }


        private async void EditCommercialOrganizationButton_Click(object sender, RoutedEventArgs e)
        {
            //CommercialOrganization newCommercialOrganization = new CommercialOrganization
            //{
            //    organizationName = EditCommercialOrganizationNameTextBox.Text
            //};

            //CommercialOrganization oldCommercialOrganization = new CommercialOrganization
            //{
            //    organizationName = CurrentCommercialOrganizationNameLabel.Content.ToString()
            //};

            //var comOrg = _commercialOrganizations.First(co => co.organizationName == _selectedCommercialOrganizationorganizationName)
            //var user = _users.FirstOrDefault(ur => ur.login == oldUser.login);
            //if (user is not null && user.password == oldUser.password)
            //{
            //    user.login = newUser.login;
            //    user.password = newUser.password;
            //    await _usersService.UpdateUserAsync(user);
            //    LoadDataAsync();
            //}
            //else
            //{
            //    MessageBox.Show("There's no user with this login or password");
            //}

            if (_selectedCommercialOrganization != null)
            {
                _selectedCommercialOrganization.organizationName = EditCommercialOrganizationNameTextBox.Text;

                await _commercialOrganizationsService.UpdateCommercialOrganizationAsync(_selectedCommercialOrganization);
                LoadDataAsync();
            }
            HideElements();
        }
    }
}