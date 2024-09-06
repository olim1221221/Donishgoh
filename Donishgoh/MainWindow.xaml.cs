using Donishgoh.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Donishgoh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ImageBrush ShowPassword = new ImageBrush();
            ShowPassword.ImageSource = (ImageSource)Resources["ShowPassword"];
            ShowPassword.Stretch = Stretch.Uniform;

            ImageBrush HidePassword = new ImageBrush();
            HidePassword.ImageSource = (ImageSource)Resources["HidePassword"];
            HidePassword.Stretch = Stretch.Uniform;

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Устанавливаем размеры окна
            this.Width = screenWidth;
            this.Height = screenHeight;
            OpenPasswordBox.Visibility = Visibility.Hidden;

            if (OpenPasswordBox.Visibility == Visibility.Visible)
            {
                HidePasswordButton.Background = ShowPassword;
            }
            else
            {
                HidePasswordButton.Background = HidePassword;
            }


            if (screenWidth == 1920 & screenHeight == 1080)
            {
                ErrorLabel.Margin = new Thickness(760, 817, 0, 0);

                EnterButton.Height = 63;
                EnterButton.Width = 465;
                EnterButton.Margin = new Thickness(734, 744, 0, 0);
                EnterButton.FontSize = 18;

                OpenPasswordBox.Height = 59;
                OpenPasswordBox.Width = 466;
                OpenPasswordBox.Margin = new Thickness(734, 642, 0, 0);
                OpenPasswordBox.FontSize = 18;

                PasswordBox.Height = 59;
                PasswordBox.Width = 466;
                PasswordBox.Margin = new Thickness(734, 642, 0, 0);
                PasswordBox.FontSize = 18;

                RepeatPassword.Height = 59;
                RepeatPassword.Width = 466;
                RepeatPassword.Margin = new Thickness(734, 642, 0, 0);
                RepeatPassword.FontSize = 18;

                PasswordBackLabel.Height = 49;
                PasswordBackLabel.Width = 93;
                PasswordBackLabel.Margin = new Thickness(747, 647, 0, 0);
                PasswordBackLabel.FontSize = 18;

                HidePasswordButton.Height = 35;
                HidePasswordButton.Width = 32;
                HidePasswordButton.Margin = new Thickness(1156, 654, 0, 0);
                HidePasswordButton.FontSize = 18;

                HidePasswordButton.Height = 35;
                HidePasswordButton.Width = 32;
                HidePasswordButton.Margin = new Thickness(1156, 654, 0, 0);
                HidePasswordButton.FontSize = 18;

                PasswordLabel.Height = 49;
                PasswordLabel.Width = 458;
                PasswordLabel.Margin = new Thickness(734, 596, 0, 0);
                PasswordLabel.FontSize = 18;

                LoginBox.Height = 59;
                LoginBox.Width = 465;
                LoginBox.Margin = new Thickness(734, 540, 0, 0);
                LoginBox.FontSize = 18;

                NewPassword.Height = 59;
                NewPassword.Width = 465;
                NewPassword.Margin = new Thickness(734, 540, 0, 0);
                NewPassword.FontSize = 18;

                LoginBackLabel.Height = 49;
                LoginBackLabel.Width = 168;
                LoginBackLabel.Margin = new Thickness(747, 545, 0, 0);
                LoginBackLabel.FontSize = 18;

                LoginLabel.Height = 49;
                LoginLabel.Width = 458;
                LoginLabel.Margin = new Thickness(734, 491, 0, 0);
                LoginLabel.FontSize = 18;

                HeaderDon.Height = 129;
                HeaderDon.Width = 343;
                HeaderDon.Margin = new Thickness(0, 357, 0, 0);
                HeaderDon.FontSize = 22;

                ImageBrush RussianButtonShow = new ImageBrush();
                RussianButtonShow.ImageSource = (ImageSource)Resources["RussianButtonShow"];
                RussianButton.Background = RussianButtonShow;
                ImageBrush TajikButtonShow = new ImageBrush();
                TajikButtonShow.ImageSource = (ImageSource)Resources["TajikButton"];
                TajikButton.Background = TajikButtonShow;

                Application.Current.Properties["UrlServer"] =  // "http://localhost:9090"; 
                Application.Current.Properties["FormLanguage"] = "Russian";
            }

            else if (screenWidth == 1366 & screenHeight == 768)
            {




                ErrorLabel.Margin = new Thickness(508, 569, 0, 0);

                EnterButton.Height = 48;
                EnterButton.Width = 353;
                EnterButton.Margin = new Thickness(508, 532, 0, 0);
                EnterButton.FontSize = 14;

                OpenPasswordBox.Height = 42;
                OpenPasswordBox.Width = 354;
                OpenPasswordBox.Margin = new Thickness(507, 462, 0, 0);
                OpenPasswordBox.FontSize = 14;

                PasswordBox.Height = 42;
                PasswordBox.Width = 354;
                PasswordBox.Margin = new Thickness(507, 462, 0, 0);
                PasswordBox.FontSize = 14;

                PasswordBackLabel.Height = 48;
                PasswordBackLabel.Width = 92;
                PasswordBackLabel.Margin = new Thickness(519, 459, 0, 0);
                PasswordBackLabel.FontSize = 14;

                HidePasswordButton.Height = 27;
                HidePasswordButton.Width = 26;
                HidePasswordButton.Margin = new Thickness(826, 470, 0, 0);
                HidePasswordButton.FontSize = 14;

                PasswordLabel.Height = 50;
                PasswordLabel.Width = 359;
                PasswordLabel.Margin = new Thickness(505, 421, 0, 0);
                PasswordLabel.FontSize = 14;

                LoginBox.Height = 42;
                LoginBox.Width = 364;
                LoginBox.Margin = new Thickness(500, 373, 0, 0);
                LoginBox.FontSize = 14;

                LoginBackLabel.Height = 38;
                LoginBackLabel.Width = 168;
                LoginBackLabel.Margin = new Thickness(519, 373, 0, 0);
                LoginBackLabel.FontSize = 14;

                LoginLabel.Height = 50;
                LoginLabel.Width = 352;
                LoginLabel.Margin = new Thickness(505, 333, 0, 0);
                LoginLabel.FontSize = 14;

                HeaderDon.Height = 84;
                HeaderDon.Width = 314;
                HeaderDon.Margin = new Thickness(0, 244, 0, 0);
                HeaderDon.FontSize = 18;



            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginBox.Text == "")
            {
                LoginBackLabel.Content = "Введите логин";
            }
            else
            {
                LoginBackLabel.Content = "";
            }


        }

        private void PasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LoginBackLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            LoginBackLabel.Visibility = Visibility.Hidden;
        }

        private void LoginBox_MouseLeave(object sender, MouseEventArgs e)
        {
            LoginBackLabel.Visibility = Visibility.Visible;
        }





        private void PasswordBox_MouseLeave(object sender, MouseEventArgs e)
        {
            PasswordBackLabel.Visibility = Visibility.Visible;
        }

        private void PasswordBox_MouseEnter(object sender, MouseEventArgs e)
        {
            PasswordBackLabel.Visibility = Visibility.Hidden;
        }

      
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            OpenPasswordBox.Text = PasswordBox.Password;
            if (PasswordBox.Password == "")
            {
                PasswordBackLabel.Content = "**********";
            }
            else
            {
                PasswordBackLabel.Content = " ";
            }
        }


        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {

            //Users users = new Users();
            //users.Show();
            //this.Close();
        }

        private void OpenPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (OpenPasswordBox.Text != PasswordBox.Password)
            {
                PasswordBox.Password = OpenPasswordBox.Text;
            }

            if (OpenPasswordBox.Text == "")
            {
                PasswordBackLabel.Content = "**********";
            }
            else
            {
                PasswordBackLabel.Content = " ";
            }
        }

        private void OpenPasswordBox_MouseEnter(object sender, MouseEventArgs e)
        {
            PasswordBackLabel.Visibility = Visibility.Hidden;
        }

        private void OpenPasswordBox_MouseLeave(object sender, MouseEventArgs e)
        {
            PasswordBackLabel.Visibility = Visibility.Visible;
        }

        private void HidePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush ShowPassword = new ImageBrush();
            ShowPassword.ImageSource = (ImageSource)Resources["ShowPassword"];
            ShowPassword.Stretch = Stretch.Uniform;

            ImageBrush HidePassword = new ImageBrush();
            HidePassword.ImageSource = (ImageSource)Resources["HidePassword"];
            HidePassword.Stretch = Stretch.Uniform;




            if (OpenPasswordBox.Visibility == Visibility.Hidden)
            {
                OpenPasswordBox.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Hidden;
            }
            else
            {
                OpenPasswordBox.Visibility = Visibility.Hidden;
                PasswordBox.Visibility = Visibility.Visible;
            }

            if (OpenPasswordBox.Visibility == Visibility.Visible)
            {
                HidePasswordButton.Background = ShowPassword;
            }
            else
            {
                HidePasswordButton.Background = HidePassword;
            }
        }



        private async Task<Authorize> GetAuthorizeDataAsync(string apiUrl, string login, string password)
        {
            using (var httpClient = new HttpClient())
            {
                // Выполняем GET-запрос, добавляя логин и пароль в параметры запроса
                string requestUrl = $"{apiUrl}?login={login}&password={password}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Считываем и десериализуем JSON-ответ в объект Authorize
                    string json = await response.Content.ReadAsStringAsync();
                    Authorize authorize = JsonConvert.DeserializeObject<Authorize>(json);
                    return authorize;
                }
                else
                {
                    // Обработка ошибок, например, вывод сообщения об ошибке
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {errorMessage}");
                }
            }
        }

        public static string GenerateSHA512Hash(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha512.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
        private static readonly HttpClient client = new HttpClient();
        public static async Task<bool> AddUserAsync(Authorize authorize, string loginHeaderValue, string passwordHeaderValue)
        {
            string UrlResp = Application.Current.Properties["UrlServer"].ToString();
            try
            {
                string apiUrl = $"{UrlResp}/api/Authorize/AddUsers?Login={loginHeaderValue}&Password={passwordHeaderValue}";
                string jsonData = JsonConvert.SerializeObject(authorize);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return false;
            }
        }



       



        private async void EnterButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                // Замените "https://api.example.com" на адрес вашего API
                // и "/api/getAuthorizeData" на путь к вашему методу API
                string apiUrl = "" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/CheckLogin";
                string pass = GenerateSHA512Hash(PasswordBox.Password);

                // Получаем данные с API, передавая логин и пароль из текстовых полей
                var JsonResponse = await GetAuthorizeDataAsync(apiUrl, LoginBox.Text, GenerateSHA512Hash(PasswordBox.Password));

                // Обработка полученных данных, например, вывод на форму
                if (JsonResponse != null)
                {
                    if (EnterButton.Content.ToString() == "Сохранить")
                    {
                        if (NewPassword.Text == RepeatPassword.Text)
                        {
                            var userData = new Authorize
                            {
                                Login = Application.Current.Properties["Login"].ToString(),
                                Password = RepeatPassword.Text,
                                Roles = Application.Current.Properties["Roles"].ToString(),
                                Position = Application.Current.Properties["Position"].ToString(),
                                Fio = Application.Current.Properties["Fio"].ToString(),
                                Gender = Application.Current.Properties["Gender"].ToString(),
                                Birthdate = Application.Current.Properties["Birthdate"].ToString(),
                                Email = Application.Current.Properties["Email"].ToString(),
                                PhoneNumber = Application.Current.Properties["PhoneNumber"].ToString()
                            };
                            // Логин и пароль для URL
                            string loginHeaderValue = (string)Application.Current.Properties["Login"];
                            string passwordHeaderValue = (string)Application.Current.Properties["Password"];

                            // Вызов метода для отправки POST-запроса с параметрами в URL
                            bool isSuccess = await AddUserAsync(userData, loginHeaderValue, passwordHeaderValue);

                            if (isSuccess)
                            {
                                Application.Current.Properties["Password"] = GenerateSHA512Hash(RepeatPassword.Text);
                                EnterButton.Content = "Войти";
                                Desktop desktop = new Desktop();
                                desktop.Show();
                                var fadeOutAnimation = new DoubleAnimation
                                {
                                    From = 1,
                                    To = 0,
                                    Duration = TimeSpan.FromSeconds(0.3)
                                };

                                fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

                                this.BeginAnimation(OpacityProperty, fadeOutAnimation);
                            }
                            else
                            {
                                Console.WriteLine("Ошибка при добавлении пользователя.");
                            }
                        }
                        else
                        {
                            ErrorLabel.Content = "Пароли не совпадают";
                        }
                    }

                    else if (EnterButton.Content.ToString() == "Войти" || EnterButton.Content.ToString() == "Даромад")
                    {
                        if (GenerateSHA512Hash(PasswordBox.Password) == GenerateSHA512Hash("12345"))
                        {
                            Application.Current.Properties["Login"] = $"{JsonResponse.Login}";
                            Application.Current.Properties["Password"] = $"{JsonResponse.Password}";
                            Application.Current.Properties["Roles"] = $"{JsonResponse.Roles}";
                            Application.Current.Properties["Position"] = $"{JsonResponse.Position}";
                            Application.Current.Properties["Fio"] = $"{JsonResponse.Fio}";
                            Application.Current.Properties["Gender"] = $"{JsonResponse.Gender}";
                            Application.Current.Properties["Birthdate"] = $"{JsonResponse.Birthdate}";
                            Application.Current.Properties["Email"] = $"{JsonResponse.Email}";
                            Application.Current.Properties["PhoneNumber"] = $"{JsonResponse.PhoneNumber}";
                            LoginBackLabel.Visibility = Visibility.Hidden;
                            LoginBox.Visibility = Visibility.Hidden;
                            HidePasswordButton.Visibility = Visibility.Hidden;
                            PasswordBackLabel.Visibility = Visibility.Hidden;
                            PasswordBox.Visibility = Visibility.Hidden;
                            OpenPasswordBox.Visibility = Visibility.Hidden;
                            NewPassword.Visibility = Visibility.Visible;
                            RepeatPassword.Visibility = Visibility.Visible;
                            EnterButton.Content = "Сохранить";
                            LoginLabel.Content = "Введите новый пароль";
                            PasswordLabel.Content = "Повторите пароль";
                        }
                        else
                        {
                            // Пример вывода данных в TextBox
                            Application.Current.Properties["Login"] = $"{JsonResponse.Login}";
                            Application.Current.Properties["Password"] = $"{JsonResponse.Password}";
                            Application.Current.Properties["Roles"] = $"{JsonResponse.Roles}";
                            Application.Current.Properties["Position"] = $"{JsonResponse.Position}";
                            Application.Current.Properties["Fio"] = $"{JsonResponse.Fio}";
                            ErrorLabel.Content = "";
                            LoginBackLabel.Visibility = Visibility.Visible;
                            HidePasswordButton.Visibility = Visibility.Visible;
                            LoginBox.Visibility = Visibility.Visible;
                            PasswordBackLabel.Visibility = Visibility.Visible;
                            PasswordBox.Visibility = Visibility.Visible;
                            OpenPasswordBox.Visibility = Visibility.Visible;
                            NewPassword.Visibility = Visibility.Hidden;
                            RepeatPassword.Visibility = Visibility.Hidden;
                            EnterButton.Content = "Войти";
                            LoginLabel.Content = "Введите логин";
                            PasswordLabel.Content = "Введите пароль";
                            Desktop desktop = new Desktop();
                            desktop.Show();
                            var fadeOutAnimation = new DoubleAnimation
                            {
                                From = 1,
                                To = 0,
                                Duration = TimeSpan.FromSeconds(0.3)
                            };

                            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

                            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
                        }
                    }
                }
                else
                {
                    ErrorLabel.Content = "Нет данных в базе";

                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Сделана попытка выполнить операцию на сокете для недоступного хоста.")
                {
                    ErrorLabel.Content = "Нет соединения с интернетом !";
                }
                else if (ex.Message == "Error: Login and password failed")
                {
                    ErrorLabel.Content = "Введен не правильный логин или пароль !";
                }

            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush RussianButtonShow = new ImageBrush();
            RussianButtonShow.ImageSource = (ImageSource)Resources["RussianButtonShow"];
            RussianButton.Background = RussianButtonShow;
            ImageBrush TajikButtonShow = new ImageBrush();
            TajikButtonShow.ImageSource = (ImageSource)Resources["TajikButton"];
            TajikButton.Background = TajikButtonShow;
            Application.Current.Properties["FormLanguage"] = "Russian";

            EnterButton.Content = "Войти";
            LoginLabel.Content = "Введите логин";
            PasswordLabel.Content = "Введите пароль";
            QuitButton.Content = "          Выход";
            HeaderDon.Content = "ТАДЖИКСКИЙ НАЦИОНАЛЬНЫЙ \n                   УНИВЕРСИТЕТ \n                              1948";
        }

        private void TajikButton_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush TajikButtonShow = new ImageBrush();
            TajikButtonShow.ImageSource = (ImageSource)Resources["TajikButtonShow"];
            TajikButton.Background = TajikButtonShow;
            ImageBrush RussianButtonShow = new ImageBrush();
            RussianButtonShow.ImageSource = (ImageSource)Resources["RussianButton"];
            RussianButton.Background = RussianButtonShow;
            Application.Current.Properties["FormLanguage"] = "Tajik";

            EnterButton.Content = "Даромад";
            LoginLabel.Content = "Логинро ворид кунед";
            PasswordLabel.Content = "Рамзро ворид кунед";
            QuitButton.Content = "          Баромадгоҳ";
            HeaderDon.Content = "ДОНИШГОҲИ МИЛЛИИ \n          ТОҶИКИСТОН \n                    1948";
        }

        private async void LoginBox_KeyDownAsync(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // Замените "https://api.example.com" на адрес вашего API
                    // и "/api/getAuthorizeData" на путь к вашему методу API
                    string apiUrl = "" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/CheckLogin";

                    // Получаем данные с API, передавая логин и пароль из текстовых полей
                    var JsonResponse = await GetAuthorizeDataAsync(apiUrl, LoginBox.Text, GenerateSHA512Hash(PasswordBox.Password));

                    // Обработка полученных данных, например, вывод на форму
                    if (JsonResponse != null)
                    {
                        if (EnterButton.Content.ToString() == "Сохранить")
                        {
                            if (NewPassword.Text == RepeatPassword.Text)
                            {
                                var userData = new Authorize
                                {
                                    Login = Application.Current.Properties["Login"].ToString(),
                                    Password = RepeatPassword.Text,
                                    Roles = Application.Current.Properties["Roles"].ToString(),
                                    Position = Application.Current.Properties["Position"].ToString(),
                                    Fio = Application.Current.Properties["Fio"].ToString(),
                                    Gender = Application.Current.Properties["Gender"].ToString(),
                                    Birthdate = Application.Current.Properties["Birthdate"].ToString(),
                                    Email = Application.Current.Properties["Email"].ToString(),
                                    PhoneNumber = Application.Current.Properties["PhoneNumber"].ToString()
                                };
                                // Логин и пароль для URL
                                string loginHeaderValue = (string)Application.Current.Properties["Login"];
                                string passwordHeaderValue = (string)Application.Current.Properties["Password"];

                                // Вызов метода для отправки POST-запроса с параметрами в URL
                                bool isSuccess = await AddUserAsync(userData, loginHeaderValue, passwordHeaderValue);

                                if (isSuccess)
                                {
                                    Application.Current.Properties["Password"] = GenerateSHA512Hash(RepeatPassword.Text);
                                    EnterButton.Content = "Войти";
                                    Desktop desktop = new Desktop();
                                    desktop.Show();
                                    var fadeOutAnimation = new DoubleAnimation
                                    {
                                        From = 1,
                                        To = 0,
                                        Duration = TimeSpan.FromSeconds(0.3)
                                    };

                                    fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

                                    this.BeginAnimation(OpacityProperty, fadeOutAnimation);
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка при добавлении пользователя.");
                                }
                            }
                            else
                            {
                                ErrorLabel.Content = "Пароли не совпадают";
                            }
                        }

                        else if (EnterButton.Content.ToString() == "Войти" || EnterButton.Content.ToString() == "Даромад")
                        {
                            if (GenerateSHA512Hash(PasswordBox.Password) == GenerateSHA512Hash("12345"))
                            {
                                Application.Current.Properties["Login"] = $"{JsonResponse.Login}";
                                Application.Current.Properties["Password"] = $"{JsonResponse.Password}";
                                Application.Current.Properties["Roles"] = $"{JsonResponse.Roles}";
                                Application.Current.Properties["Position"] = $"{JsonResponse.Position}";
                                Application.Current.Properties["Fio"] = $"{JsonResponse.Fio}";
                                Application.Current.Properties["Gender"] = $"{JsonResponse.Gender}";
                                Application.Current.Properties["Birthdate"] = $"{JsonResponse.Birthdate}";
                                Application.Current.Properties["Email"] = $"{JsonResponse.Email}";
                                Application.Current.Properties["PhoneNumber"] = $"{JsonResponse.PhoneNumber}";
                                LoginBackLabel.Visibility = Visibility.Hidden;
                                LoginBox.Visibility = Visibility.Hidden;
                                PasswordBackLabel.Visibility = Visibility.Hidden;
                                PasswordBox.Visibility = Visibility.Hidden;
                                OpenPasswordBox.Visibility = Visibility.Hidden;
                                NewPassword.Visibility = Visibility.Visible;
                                RepeatPassword.Visibility = Visibility.Visible;
                                EnterButton.Content = "Сохранить";
                                LoginLabel.Content = "Введите новый пароль";
                                PasswordLabel.Content = "Повторите пароль";
                            }
                            else
                            {
                                // Пример вывода данных в TextBox
                                Application.Current.Properties["Login"] = $"{JsonResponse.Login}";
                                Application.Current.Properties["Password"] = $"{JsonResponse.Password}";
                                Application.Current.Properties["Roles"] = $"{JsonResponse.Roles}";
                                Application.Current.Properties["Position"] = $"{JsonResponse.Position}";
                                Application.Current.Properties["Fio"] = $"{JsonResponse.Fio}";
                                ErrorLabel.Content = "";
                                LoginBackLabel.Visibility = Visibility.Visible;
                                LoginBox.Visibility = Visibility.Visible;
                                PasswordBackLabel.Visibility = Visibility.Visible;
                                PasswordBox.Visibility = Visibility.Visible;
                                OpenPasswordBox.Visibility = Visibility.Visible;
                                NewPassword.Visibility = Visibility.Hidden;
                                RepeatPassword.Visibility = Visibility.Hidden;
                                EnterButton.Content = "Войти";
                                LoginLabel.Content = "Введите логин";
                                PasswordLabel.Content = "Введите пароль";
                                Desktop desktop = new Desktop();
                                desktop.Show();
                                var fadeOutAnimation = new DoubleAnimation
                                {
                                    From = 1,
                                    To = 0,
                                    Duration = TimeSpan.FromSeconds(0.3)
                                };

                                fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

                                this.BeginAnimation(OpacityProperty, fadeOutAnimation);
                            }
                        }







                    }
                    else
                    {
                        ErrorLabel.Content = "Нет данных в базе";

                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Сделана попытка выполнить операцию на сокете для недоступного хоста.")
                    {
                        ErrorLabel.Content = "Нет соединения с интернетом !";
                    }
                    else if (ex.Message == "Error: Login and password failed")
                    {
                        ErrorLabel.Content = "Введен не правильный логин или пароль !";
                    }

                }
            }

        }

        private async void PasswordBox_KeyDownAsync(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // Замените "https://api.example.com" на адрес вашего API
                    // и "/api/getAuthorizeData" на путь к вашему методу API
                    string apiUrl = "" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/CheckLogin";

                    // Получаем данные с API, передавая логин и пароль из текстовых полей
                    var JsonResponse = await GetAuthorizeDataAsync(apiUrl, LoginBox.Text, GenerateSHA512Hash(PasswordBox.Password));

                    // Обработка полученных данных, например, вывод на форму
                    if (JsonResponse != null)
                    {
                        if (EnterButton.Content.ToString() == "Сохранить")
                        {
                            if (NewPassword.Text == RepeatPassword.Text)
                            {
                                var userData = new Authorize
                                {
                                    Login = Application.Current.Properties["Login"].ToString(),
                                    Password = RepeatPassword.Text,
                                    Roles = Application.Current.Properties["Roles"].ToString(),
                                    Position = Application.Current.Properties["Position"].ToString(),
                                    Fio = Application.Current.Properties["Fio"].ToString(),
                                    Gender = Application.Current.Properties["Gender"].ToString(),
                                    Birthdate = Application.Current.Properties["Birthdate"].ToString(),
                                    Email = Application.Current.Properties["Email"].ToString(),
                                    PhoneNumber = Application.Current.Properties["PhoneNumber"].ToString()
                                };
                                // Логин и пароль для URL
                                string loginHeaderValue = (string)Application.Current.Properties["Login"];
                                string passwordHeaderValue = (string)Application.Current.Properties["Password"];

                                // Вызов метода для отправки POST-запроса с параметрами в URL
                                bool isSuccess = await AddUserAsync(userData, loginHeaderValue, passwordHeaderValue);

                                if (isSuccess)
                                {
                                    Application.Current.Properties["Password"] = GenerateSHA512Hash(RepeatPassword.Text);
                                    EnterButton.Content = "Войти";
                                    Desktop desktop = new Desktop();
                                    desktop.Show();
                                    var fadeOutAnimation = new DoubleAnimation
                                    {
                                        From = 1,
                                        To = 0,
                                        Duration = TimeSpan.FromSeconds(0.3)
                                    };

                                    fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

                                    this.BeginAnimation(OpacityProperty, fadeOutAnimation);
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка при добавлении пользователя.");
                                }
                            }
                            else
                            {
                                ErrorLabel.Content = "Пароли не совпадают";
                            }
                        }

                        else if (EnterButton.Content.ToString() == "Войти" || EnterButton.Content.ToString() == "Даромад")
                        {
                            if (GenerateSHA512Hash(PasswordBox.Password) == GenerateSHA512Hash("12345"))
                            {
                                Application.Current.Properties["Login"] = $"{JsonResponse.Login}";
                                Application.Current.Properties["Password"] = $"{JsonResponse.Password}";
                                Application.Current.Properties["Roles"] = $"{JsonResponse.Roles}";
                                Application.Current.Properties["Position"] = $"{JsonResponse.Position}";
                                Application.Current.Properties["Fio"] = $"{JsonResponse.Fio}";
                                Application.Current.Properties["Gender"] = $"{JsonResponse.Gender}";
                                Application.Current.Properties["Birthdate"] = $"{JsonResponse.Birthdate}";
                                Application.Current.Properties["Email"] = $"{JsonResponse.Email}";
                                Application.Current.Properties["PhoneNumber"] = $"{JsonResponse.PhoneNumber}";
                                LoginBackLabel.Visibility = Visibility.Hidden;
                                LoginBox.Visibility = Visibility.Hidden;
                                PasswordBackLabel.Visibility = Visibility.Hidden;
                                PasswordBox.Visibility = Visibility.Hidden;
                                OpenPasswordBox.Visibility = Visibility.Hidden;
                                NewPassword.Visibility = Visibility.Visible;
                                RepeatPassword.Visibility = Visibility.Visible;
                                EnterButton.Content = "Сохранить";
                                LoginLabel.Content = "Введите новый пароль";
                                PasswordLabel.Content = "Повторите пароль";
                            }
                            else
                            {
                                // Пример вывода данных в TextBox
                                Application.Current.Properties["Login"] = $"{JsonResponse.Login}";
                                Application.Current.Properties["Password"] = $"{JsonResponse.Password}";
                                Application.Current.Properties["Roles"] = $"{JsonResponse.Roles}";
                                Application.Current.Properties["Position"] = $"{JsonResponse.Position}";
                                Application.Current.Properties["Fio"] = $"{JsonResponse.Fio}";
                                ErrorLabel.Content = "";
                                LoginBackLabel.Visibility = Visibility.Visible;
                                LoginBox.Visibility = Visibility.Visible;
                                PasswordBackLabel.Visibility = Visibility.Visible;
                                PasswordBox.Visibility = Visibility.Visible;
                                OpenPasswordBox.Visibility = Visibility.Visible;
                                NewPassword.Visibility = Visibility.Hidden;
                                RepeatPassword.Visibility = Visibility.Hidden;
                                EnterButton.Content = "Войти";
                                LoginLabel.Content = "Введите логин";
                                PasswordLabel.Content = "Введите пароль";
                                Desktop desktop = new Desktop();
                                desktop.Show();
                                var fadeOutAnimation = new DoubleAnimation
                                {
                                    From = 1,
                                    To = 0,
                                    Duration = TimeSpan.FromSeconds(0.3)
                                };

                                fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

                                this.BeginAnimation(OpacityProperty, fadeOutAnimation);
                            }
                        }







                    }
                    else
                    {
                        ErrorLabel.Content = "Нет данных в базе";

                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Сделана попытка выполнить операцию на сокете для недоступного хоста.")
                    {
                        ErrorLabel.Content = "Нет соединения с интернетом !";
                    }
                    else if (ex.Message == "Error: Login and password failed")
                    {
                        ErrorLabel.Content = "Введен не правильный логин или пароль !";
                    }

                }
            }
        }

        private async void OpenPasswordBox_KeyDownAsync(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // Замените "https://api.example.com" на адрес вашего API
                    // и "/api/getAuthorizeData" на путь к вашему методу API
                    string apiUrl = "" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/CheckLogin";

                    // Получаем данные с API, передавая логин и пароль из текстовых полей
                    var JsonResponse = await GetAuthorizeDataAsync(apiUrl, LoginBox.Text, GenerateSHA512Hash(PasswordBox.Password));

                    // Обработка полученных данных, например, вывод на форму
                    if (JsonResponse != null)
                    {
                        if (EnterButton.Content.ToString() == "Сохранить")
                        {
                            if (NewPassword.Text == RepeatPassword.Text)
                            {
                                var userData = new Authorize
                                {
                                    Login = Application.Current.Properties["Login"].ToString(),
                                    Password = RepeatPassword.Text,
                                    Roles = Application.Current.Properties["Roles"].ToString(),
                                    Position = Application.Current.Properties["Position"].ToString(),
                                    Fio = Application.Current.Properties["Fio"].ToString(),
                                    Gender = Application.Current.Properties["Gender"].ToString(),
                                    Birthdate = Application.Current.Properties["Birthdate"].ToString(),
                                    Email = Application.Current.Properties["Email"].ToString(),
                                    PhoneNumber = Application.Current.Properties["PhoneNumber"].ToString()
                                };
                                // Логин и пароль для URL
                                string loginHeaderValue = (string)Application.Current.Properties["Login"];
                                string passwordHeaderValue = (string)Application.Current.Properties["Password"];

                                // Вызов метода для отправки POST-запроса с параметрами в URL
                                bool isSuccess = await AddUserAsync(userData, loginHeaderValue, passwordHeaderValue);

                                if (isSuccess)
                                {
                                    Application.Current.Properties["Password"] = GenerateSHA512Hash(RepeatPassword.Text);
                                    EnterButton.Content = "Войти";
                                    Desktop desktop = new Desktop();
                                    desktop.Show();
                                    var fadeOutAnimation = new DoubleAnimation
                                    {
                                        From = 1,
                                        To = 0,
                                        Duration = TimeSpan.FromSeconds(0.3)
                                    };

                                    fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

                                    this.BeginAnimation(OpacityProperty, fadeOutAnimation);
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка при добавлении пользователя.");
                                }
                            }
                            else
                            {
                                ErrorLabel.Content = "Пароли не совпадают";
                            }
                        }

                        else if (EnterButton.Content.ToString() == "Войти" || EnterButton.Content.ToString() == "Даромад")
                        {
                            if (GenerateSHA512Hash(PasswordBox.Password) == GenerateSHA512Hash("12345"))
                            {
                                Application.Current.Properties["Login"] = $"{JsonResponse.Login}";
                                Application.Current.Properties["Password"] = $"{JsonResponse.Password}";
                                Application.Current.Properties["Roles"] = $"{JsonResponse.Roles}";
                                Application.Current.Properties["Position"] = $"{JsonResponse.Position}";
                                Application.Current.Properties["Fio"] = $"{JsonResponse.Fio}";
                                Application.Current.Properties["Gender"] = $"{JsonResponse.Gender}";
                                Application.Current.Properties["Birthdate"] = $"{JsonResponse.Birthdate}";
                                Application.Current.Properties["Email"] = $"{JsonResponse.Email}";
                                Application.Current.Properties["PhoneNumber"] = $"{JsonResponse.PhoneNumber}";
                                LoginBackLabel.Visibility = Visibility.Hidden;
                                LoginBox.Visibility = Visibility.Hidden;
                                PasswordBackLabel.Visibility = Visibility.Hidden;
                                PasswordBox.Visibility = Visibility.Hidden;
                                OpenPasswordBox.Visibility = Visibility.Hidden;
                                NewPassword.Visibility = Visibility.Visible;
                                RepeatPassword.Visibility = Visibility.Visible;
                                EnterButton.Content = "Сохранить";
                                LoginLabel.Content = "Введите новый пароль";
                                PasswordLabel.Content = "Повторите пароль";
                            }
                            else
                            {
                                // Пример вывода данных в TextBox
                                Application.Current.Properties["Login"] = $"{JsonResponse.Login}";
                                Application.Current.Properties["Password"] = $"{JsonResponse.Password}";
                                Application.Current.Properties["Roles"] = $"{JsonResponse.Roles}";
                                Application.Current.Properties["Position"] = $"{JsonResponse.Position}";
                                Application.Current.Properties["Fio"] = $"{JsonResponse.Fio}";
                                ErrorLabel.Content = "";
                                LoginBackLabel.Visibility = Visibility.Visible;
                                LoginBox.Visibility = Visibility.Visible;
                                PasswordBackLabel.Visibility = Visibility.Visible;
                                PasswordBox.Visibility = Visibility.Visible;
                                OpenPasswordBox.Visibility = Visibility.Visible;
                                NewPassword.Visibility = Visibility.Hidden;
                                RepeatPassword.Visibility = Visibility.Hidden;
                                EnterButton.Content = "Войти";
                                LoginLabel.Content = "Введите логин";
                                PasswordLabel.Content = "Введите пароль";
                                Desktop desktop = new Desktop();
                                desktop.Show();
                                var fadeOutAnimation = new DoubleAnimation
                                {
                                    From = 1,
                                    To = 0,
                                    Duration = TimeSpan.FromSeconds(0.3)
                                };

                                fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

                                this.BeginAnimation(OpacityProperty, fadeOutAnimation);
                            }
                        }







                    }
                    else
                    {
                        ErrorLabel.Content = "Нет данных в базе";

                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Сделана попытка выполнить операцию на сокете для недоступного хоста.")
                    {
                        ErrorLabel.Content = "Нет соединения с интернетом !";
                    }
                    else if (ex.Message == "Error: Login and password failed")
                    {
                        ErrorLabel.Content = "Введен не правильный логин или пароль !";
                    }

                }
            }
        }

        private void Sver_Click(object sender, RoutedEventArgs e)
        {
           WindowState = WindowState.Minimized;
        }

        private void Sver_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
