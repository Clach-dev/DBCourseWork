using API.Data.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace for_db7.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        UsersService _usersService;

        ObservableCollection<User> _users = new ObservableCollection<User>();
        public LoginWindow()
        {
            _usersService = new UsersService();

            InitializeComponent();
            LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            _users = await _usersService.GetUsersAsync();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                login = LoginTextBox.Text,
                password = PasswordTextBox.Text
            };

            var foundedUser = _users.FirstOrDefault(us => us.login == user.login);
            if (foundedUser is not null && foundedUser.password == user.password)
            {
                bool isAdmin;
                if (foundedUser.UserRole.role == "Admin")
                {
                    isAdmin = true;
                }
                else
                {
                    isAdmin = false;
                }
                DataWindow dataWindow = new DataWindow(isAdmin);
                dataWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid login or password");
            }


        }
    }
}
