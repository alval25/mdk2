using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp17.Windows
{
    public partial class CaptchaWindow : Window
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private System.Timers.Timer captchaTimer;
        public bool IsCaptchaValid = false;

        public CaptchaWindow()
        {
            InitializeComponent();
            StartCaptchaTimer();
            LoadCaptchaText();
        }

        private void StartCaptchaTimer()
        {
            captchaTimer = new System.Timers.Timer(1000000); 
            captchaTimer.Elapsed += async (sender, e) => await ReloadCaptcha();
            captchaTimer.AutoReset = true;
            captchaTimer.Start();
        }

        private async Task ReloadCaptcha()
        {
            await Dispatcher.Invoke(async () => await LoadCaptchaText());
        }

        private async Task LoadCaptchaText()
        {
            try
            {
                var response = await httpClient.GetAsync("http://localhost:5056/api/captcha/text");
                response.EnsureSuccessStatusCode();
                var captchaData = await response.Content.ReadFromJsonAsync<CaptchaResponse>();
                DrawCaptchaText(captchaData.captchaText);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void DrawCaptchaText(string captchaText)
        {
            CaptchaCanvas.Children.Clear();

            var textBlock = new TextBlock
            {
                Text = captchaText,
                FontSize = 32,
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Canvas.SetLeft(textBlock, (CaptchaCanvas.ActualWidth - textBlock.ActualWidth) / 2);
            Canvas.SetTop(textBlock, (CaptchaCanvas.ActualHeight - textBlock.ActualHeight) / 2);

            CaptchaCanvas.Children.Add(textBlock);
        }

        private async void VerifyClick(object sender, RoutedEventArgs e)
        {
            var captchaInput = CaptchaTextBox.Text;

            try
            {
                var response = await httpClient.PostAsJsonAsync("http://localhost:5056/api/captcha/verify", captchaInput);

                if (response.IsSuccessStatusCode)
                {
                    IsCaptchaValid = true;
                    MessageBox.Show("Каптча верна!");

                    PurchaseWindow purchaseWindow = new PurchaseWindow(); 
                    purchaseWindow.Show();
                    this.Close();
                }
                else
                {
                    IsCaptchaValid = false;
                    MessageBox.Show("Каптча неверна, попробуйте еще раз");
                    await LoadCaptchaText();
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }
    }

    public class CaptchaResponse
    {
        public string captchaText { get; set; }
    }

}
