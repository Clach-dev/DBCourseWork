using API.Data.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace for_db7.Windows.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        UsersService _usersService;
        ObservableCollection<User> _users;
        User _selectedUser;
        public UsersPage()
        {
            InitializeComponent();
            _usersService = new UsersService();
            _users = new ObservableCollection<User>();
            _selectedUser = new User();

            HideElements();
            LoadDataAsync();
            UsersDataGrid.ItemsSource = _users;
            UsersDataGrid.IsReadOnly = true;
        }

        public async void LoadDataAsync()
        {
            _users = await _usersService.GetUsersAsync();
            UsersDataGrid.ItemsSource = _users;
        }

        public void HideElements()
        {
            CurrentLoginLabel.Visibility = Visibility.Hidden;
            CurrentPasswordLabel.Visibility = Visibility.Hidden;

            OldLoginLabel.Visibility = Visibility.Hidden;
            OldPasswordLabel.Visibility = Visibility.Hidden;
            NewLoginLabel.Visibility = Visibility.Hidden;
            NewPasswordLabel.Visibility = Visibility.Hidden;

            EditLoginTextBox.Visibility = Visibility.Hidden;
            EditPasswordTextBox.Visibility = Visibility.Hidden;

            DeleteUserButton.Visibility = Visibility.Hidden;
            EditUserButton.Visibility = Visibility.Hidden;
        }
        public void ShowElements()
        {
            CurrentLoginLabel.Visibility = Visibility.Visible;
            CurrentPasswordLabel.Visibility = Visibility.Visible;

            OldLoginLabel.Visibility = Visibility.Visible;
            OldPasswordLabel.Visibility = Visibility.Visible;
            NewLoginLabel.Visibility = Visibility.Visible;
            NewPasswordLabel.Visibility = Visibility.Visible;

            EditLoginTextBox.Visibility = Visibility.Visible;
            EditPasswordTextBox.Visibility = Visibility.Visible;

            DeleteUserButton.Visibility = Visibility.Visible;
            EditUserButton.Visibility = Visibility.Visible;
        }

        private async void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                login = AddLoginTextBox.Text,
                password = AddPasswordTextBox.Text
            };

            await _usersService.AddUserAsync(user);
            LoadDataAsync();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var user = UsersDataGrid.SelectedItem as User;
            if (UsersDataGrid.SelectedItem is not null)
            {
                CurrentLoginLabel.Content = user.login;
                CurrentPasswordLabel.Content = user.password;
                ShowElements();
            }

        }

        private async void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                login = EditLoginTextBox.Text,
                password = EditPasswordTextBox.Text
            };

            User oldUser = new User
            {
                login = CurrentLoginLabel.Content.ToString(),
                password = CurrentPasswordLabel.Content.ToString()
            };

            var user = _users.FirstOrDefault(ur => ur.login == oldUser.login);
            if (user is not null && user.password == oldUser.password)
            {
                user.login = newUser.login;
                user.password = newUser.password;
                await _usersService.UpdateUserAsync(user);
                LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no user with this login or password");
            }
            HideElements();
        }

        private async void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                login = CurrentLoginLabel.Content.ToString(),
                password = CurrentPasswordLabel.Content.ToString()
            };

            var deletedUser = _users.FirstOrDefault(ur => ur.login == user.login);

            if (deletedUser is not null && deletedUser.usId == 1)
            {
                MessageBox.Show("You can't delete administrator");
            }
            else if (deletedUser is not null && deletedUser.password == user.password)
            {
                await _usersService.DeleteUserAsync(deletedUser.usId);
                LoadDataAsync();
            }
            else
            {
                MessageBox.Show("There's no user with this name or password");

            }

            HideElements();
        }

    }
}
