using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
//using WpfApp17.Classes;

namespace WpfApp17.Windows
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
           AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show(" Поле email не должен быть пустым.");
                return false;
            }

            try
            {
                email = email.Trim();
                var addr = new System.Net.Mail.MailAddress(email);
                string domainPart = email.Split('@').Last();
                if (!domainPart.Contains(".") || domainPart.LastIndexOf(".") == domainPart.Length - 1)
                {
                    MessageBox.Show("Укажите корректный домен электронной почты");
                    return false;
                }
                return addr.Address == email;
            }
            catch (FormatException)
            {
                MessageBox.Show("Неккоректная почта");
                return false;
            }
        }

        private async void RegButton_Click(object sender, RoutedEventArgs e)
        {
            var login = textboxLogin.Text;
            var email = textboxEmail.Text;
            var passwordOne = PasswordBoxOne.Password;
            var passwordTwo = PasswordBoxTwo.Password;

            if (passwordOne != passwordTwo)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            if (!ValidateEmail(email))
            {
                return;
            }

            using var httpClient = new HttpClient();

            try
            {
                var emailRequest = new EmailRequest
                {
                    ToAddress = email
                };
                var sendResponse = await httpClient.PostAsJsonAsync("http://localhost:5056/api/email/send", emailRequest);

                if (!sendResponse.IsSuccessStatusCode)
                {
                    var errorMessage = await sendResponse.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при отправке кода: {sendResponse.StatusCode}, {errorMessage}");
                    return;
                }
              
                textboxCode.Visibility = Visibility.Visible;
                buttonVerifyCode.Visibility = Visibility.Visible;

            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private async void ButtonVerifyCode_Click(object sender, RoutedEventArgs e)
        {
            var email = textboxEmail.Text;
            var code = textboxCode.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Введите код подтверждения.");
                return;
            }

            using var httpClient = new HttpClient();

            try
            {
                var verificationRequest = new VerificationRequest
                {
                    Email = email,
                    Code = code
                };
                var verifyResponse = await httpClient.PostAsJsonAsync("http://localhost:5056/api/email/verify", verificationRequest);

                if (verifyResponse.IsSuccessStatusCode)
                {
                    var loginModel = new LoginModel
                    {
                        Login = textboxLogin.Text,
                        Password = PasswordBoxOne.Password,
                        Role = "Client",
                        Email = email
                    };

                    var registerResponse = await httpClient.PostAsJsonAsync("http://localhost:5056/api/auth/register", loginModel);

                    if (registerResponse.IsSuccessStatusCode)
                    {
                        var loginResponse = await httpClient.PostAsJsonAsync("http://localhost:5056/api/auth/login", loginModel);

                        if (loginResponse.IsSuccessStatusCode)
                        {
                            var responseData = await loginResponse.Content.ReadFromJsonAsync<dynamic>();
                            MessageBox.Show("Вы зарегистрировались");
                           
                        }
                        else
                        {
                            var errorMessage = await loginResponse.Content.ReadAsStringAsync();
                            MessageBox.Show($"Ошибка при входе: {loginResponse.StatusCode}, {errorMessage}");
                        }
                    }
                    else
                    {
                        var errorMessage = await registerResponse.Content.ReadAsStringAsync();
                        MessageBox.Show($"Ошибка регистрации: {registerResponse.StatusCode}, {errorMessage}");
                    }
                }
                else
                {
                    var errorMessage = await verifyResponse.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка проверки кода: {verifyResponse.StatusCode}, {errorMessage}");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }
    }

    internal class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }

    public class EmailRequest
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class VerificationRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
