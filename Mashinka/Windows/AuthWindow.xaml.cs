using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Threading.Tasks;
//using WpfApp17.Classes;
using System.Windows.Controls;

namespace WpfApp17.Windows
{
    public partial class AuthWindow : Window
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public AuthWindow()
        {
            InitializeComponent();
        }

        private async void EnterClick(object sender, RoutedEventArgs e)
        {
            var captchaWindow = new CaptchaWindow();
            captchaWindow.ShowDialog(); 

            if (captchaWindow.IsCaptchaValid)
            {
                await AuthorizeUser();
            }
            else
            {
                MessageBox.Show("Не удалось пройти проверку капчи.");
            }
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            bool hasPassword = passwordBox.Password != null && passwordBox.Password != "";
            if (hasPassword)
            {
                PasswordBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                PasswordBlock.Visibility = Visibility.Visible;    
            }
        }
        private async Task AuthorizeUser()
        {
            var login = LoginTextBox.Text;
            var password = PasswordTextBox.Password;
            var loginModel = new { Login = login, Password = password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5056/api/auth/login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Вы успешно зашли!");
                }
                else
                {
                    MessageBox.Show($"Ошибка: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private void RegClick(object sender, RoutedEventArgs e)
        {
           AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }
    }
}
