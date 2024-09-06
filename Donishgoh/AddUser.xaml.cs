using Donishgoh.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
using System.Windows.Shapes;

namespace Donishgoh
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Устанавливаем размеры окна
            this.Width = screenWidth;
            this.Height = screenHeight;

            if (Application.Current.Properties.Contains("Fio"))
            {
                var fioValue = Application.Current.Properties["Fio"];
                if (fioValue is string fio)
                {
                    FioUserLabel.Content = fio;
                }
            }

            if (Application.Current.Properties.Contains("Position"))
            {
                var PositionValue = Application.Current.Properties["Position"];
                if (PositionValue is string Position)
                {
                    PositionLabelFirst.Content = Position;
                }
            }

            GenderComboBox.Items.Add("Выберите пол");
            GenderComboBox.Items.Add("Мужской");
            GenderComboBox.Items.Add("Женский");
            GenderComboBox.SelectedIndex = 0;
            GenderBox.Text = GenderComboBox.Text;

            PositionComboBox.Items.Add("Выберите должность");
            PositionComboBox.Items.Add("Лаборант");
            PositionComboBox.Items.Add("Старший лаборант");
            PositionComboBox.Items.Add("Преподаватель");
            PositionComboBox.Items.Add("Старший преподаватель");
            PositionComboBox.Items.Add("Ассистент");
            PositionComboBox.Items.Add("Доцент");
            PositionComboBox.Items.Add("Профессор");
            PositionComboBox.Items.Add("Зав. кафедрой");
            PositionComboBox.Items.Add("Декан");
            PositionComboBox.Items.Add("Проректор");
            PositionComboBox.Items.Add("Ректор");
            PositionComboBox.SelectedIndex = 0;
            PositionBox.Text = PositionComboBox.Text;

            RoleComboBox.Items.Add("Выберите роль");
            RoleComboBox.Items.Add("Администратор");
            RoleComboBox.Items.Add("Подтверждающий");
            RoleComboBox.Items.Add("Пользователь");
            RoleComboBox.SelectedIndex = 0;
            RoleBox.Text = RoleComboBox.Text;




            Loaded += Window_LoadedAsync;
        }


        private async Task<Authorize> GetAuthorizeDataAsync(string apiUrl, string login, string password, int Id)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&Id={Id}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {

                    string json = await response.Content.ReadAsStringAsync();
                    Authorize authorize = JsonConvert.DeserializeObject<Authorize>(json);
                    return authorize;

                }
                else
                {
                    // Handle error
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }


        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Users users = new Users();
            users.Show();
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void StatisticButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StatisticButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }
        private void StatisticButton_MouseLeave(object sender, MouseEventArgs e)
        {
            StatisticButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }
        private void UsersButton_MouseEnter(object sender, MouseEventArgs e)
        {
            UsersButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }

        private void UsersButton_MouseLeave(object sender, MouseEventArgs e)
        {
            UsersButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }

        private void DesktopButton_MouseEnter(object sender, MouseEventArgs e)
        {
            DesktopButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }

        private void DesktopButton_MouseLeave(object sender, MouseEventArgs e)
        {
            DesktopButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }

        private void Logs_MouseEnter(object sender, MouseEventArgs e)
        {
            Logs.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }

        private void Logs_MouseLeave(object sender, MouseEventArgs e)
        {
            Logs.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }

        private void RaportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            RaportsButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }

        private void RaportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            RaportsButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }

        private void Settings_MouseEnter(object sender, MouseEventArgs e)
        {
            Settings.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }

        private void Settings_MouseLeave(object sender, MouseEventArgs e)
        {
            Settings.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            Users users = new Users();
            users.Show();
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void DesktopButton_Click(object sender, RoutedEventArgs e)
        {
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

        private void Logs_Click(object sender, RoutedEventArgs e)
        {
            LogsForm logsForm = new LogsForm();
            logsForm.Show();
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void RaportsButton_Click(object sender, RoutedEventArgs e)
        {
            RaportsForm raportsForm = new RaportsForm();
            raportsForm.Show();
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenderComboBox.SelectedItem != null)
            {
                string selectedGender = GenderComboBox.SelectedItem.ToString();
                GenderBox.Text = selectedGender;
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



        public static async Task<bool> AddEventsAssync(LogsDiplom logsDiplom, string loginHeaderValue, string passwordHeaderValue)
        {
            string UrlResp = Application.Current.Properties["UrlServer"].ToString();
            try
            {
                string apiUrl = $"{UrlResp}/api/Authorize/AddEvents?Login={loginHeaderValue}&Password={passwordHeaderValue}";
                string jsonData = JsonConvert.SerializeObject(logsDiplom);
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

        private async void Window_LoadedAsync(object sender, RoutedEventArgs e)
        {

            if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
            {
                StatisticButton.Content = "           Статистика на год";
                UniverNameCation.Content = "Таджикский национальный \n           университет";
                UsersButton.Content = "           Сотрудники";
                DesktopButton.Content = "           Рабочее окно";
                Logs.Content = "           Журнал";
                RaportsButton.Content = "           Отчеты";
                Settings.Content = "           Настройки";
                FirstLabel.Content = "Новый сотрудник";
                QuitButton.Content = "          Выход";
                InfoLabel.Content = "Личная информация";
                NameLabel.Content = "Имя";
                SurnameLabel.Content = "Фамилия";
                PatronimicLabel.Content = "Отчество";
                birthdateLabel.Content = "Дата рождения";
                GenderLabel.Content = "Пол";
                emailLabel.Content = "Электронная почта";
                PhoneNumberLabel.Content = "Номер телефона";
                PositionLabel.Content = "Должность";
                RolesLabel.Content = "Роль";

            }
            else
            {
                StatisticButton.Content = "           Омор барои сол";
                UniverNameCation.Content = "Донишгоҳи Миллии \n       Тоҷикистон";
                UsersButton.Content = "           Коргарон";
                DesktopButton.Content = "           Равзанаи корӣ";
                Logs.Content = "           Амалиётҳо";
                RaportsButton.Content = "           Ҳисобот";
                Settings.Content = "           Танзимотҳо";
                FirstLabel.Content = "Коргари нав";
                QuitButton.Content = "          Баромадгоҳ";
                InfoLabel.Content = "Маълумоти шахсӣ";
                NameLabel.Content = "Ном";
                SurnameLabel.Content = "Насаб";
                PatronimicLabel.Content = "Номи падарӣ";
                birthdateLabel.Content = "Санаи таваллуд";
                GenderLabel.Content = "Ҷинс";
                emailLabel.Content = "Почтаи электронӣ";
                PhoneNumberLabel.Content = "Рақами телефон";
                PositionLabel.Content = "Вазифа";
                RolesLabel.Content = "Нақш";

            }


            string RoleResponse = Application.Current.Properties["Roles"].ToString();
            if (RoleResponse == "Подтверждающий")
            {
                UsersButton.IsEnabled = false;
                Logs.IsEnabled = false;
                Settings.IsEnabled = false;
            }
            else if (RoleResponse == "Администратор")
            {
            }
            else if (RoleResponse == "Пользователь")
            {
                UsersButton.IsEnabled = false;
                Logs.IsEnabled = false;
                Settings.IsEnabled = false;
            }
            if (Application.Current.Properties["SelectedUserId"].ToString() == "")
            {
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    FirstLabel.Content = "Новый сотрудник";
                }
                else
                {
                    FirstLabel.Content = "Коргари нав";
                }

            }
            else
            {
                Authorize PersonList = null;
                try
                {
                    PersonList = await GetAuthorizeDataAsync(""+ Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetUserById", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Convert.ToInt32(Application.Current.Properties["SelectedUserId"]));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                }

                if (PersonList != null)
                {
                    loginBox.Text = PersonList.Login;
                    passwordBox.Password = PersonList.Password;
                    RoleBox.Text = PersonList.Roles;
                    string[] names = PersonList.Fio.Split(' ');
                    if (names.Length > 0)
                    {
                        // Первое имя
                        SurnameBox.Text = names[0];
                    }
                    if (names.Length > 1)
                    {
                        // Второе имя
                        NameBox.Text = names[1];
                    }
                    if (names.Length > 2)
                    {
                        // Второе имя
                        PatronimicBox.Text = names[2];
                    }
                    emailBox.Text = PersonList.Email;
                    phonenumberBox.Text = PersonList.PhoneNumber;
                    GenderBox.Text = PersonList.Gender;
                    PositionBox.Text = PersonList.Position;
                    Birhtdate.Text = PersonList.Birthdate;


                    if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                    {
                        FirstLabel.Content = "Существующий сотрудник";
                    }
                    else
                    {
                        FirstLabel.Content = "Корманди мавҷуда";
                    }
                    
                }
                else
                {
                    MessageBox.Show("Список пуст.");
                }

            }


        }
        private async void AddUserButton_Click(object sender, RoutedEventArgs e)
        {

            if (AddUserButton.Content.ToString() == "Изменить")
            {
                NameBox.IsEnabled = true;
                SurnameBox.IsEnabled = true;
                PatronimicBox.IsEnabled = true;
                Birhtdate.IsEnabled = true;
                GenderBox.IsEnabled = true;
                emailBox.IsEnabled = true;
                GenderComboBox.IsEnabled = true;
                phonenumberBox.IsEnabled = true;
                PositionBox.IsEnabled = true;
                PositionComboBox.IsEnabled = true;
                RoleBox.IsEnabled = true;
                RoleComboBox.IsEnabled = true;
                loginBox.IsEnabled = true;
                passwordBox.IsEnabled = true;

                AddUserButton.Content = "Сохранить";
            }
            else if (AddUserButton.Content.ToString() == "Сохранить" | AddUserButton.Content.ToString() == "Добавить сотрудника")
            {

                if (loginBox.Text != "" & RoleBox.Text != "" & PositionBox.Text != "" & NameBox.Text != "" & SurnameBox.Text != "" & GenderBox.Text != "" & Birhtdate.Text != "" & emailBox.Text != "" & phonenumberBox.Text != "")
                {
                    var userData = new Authorize
                    {
                        Login = loginBox.Text,
                        Password = "12345",
                        Roles = RoleBox.Text,
                        Position = PositionBox.Text,
                        Fio = SurnameBox.Text + " " + NameBox.Text + " " + PatronimicBox.Text,
                        Gender = GenderBox.Text,
                        Birthdate = Birhtdate.Text,
                        Email = emailBox.Text,
                        PhoneNumber = phonenumberBox.Text
                    };

                    // Логин и пароль для URL
                    string loginHeaderValue = (string)Application.Current.Properties["Login"];
                    string passwordHeaderValue = (string)Application.Current.Properties["Password"];

                    // Вызов метода для отправки POST-запроса с параметрами в URL
                    bool isSuccess = await AddUserAsync(userData, loginHeaderValue, passwordHeaderValue);

                    if (isSuccess)
                    {
                        ErrorLabel.Content = " ";
                        Users users = new Users();
                        users.Show();
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
                    ErrorLabel.Content = "Не все поля заполнены !";
                }


               




               
            }
        }

        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleComboBox.SelectedItem != null)
            {
                string selectedRole = RoleComboBox.SelectedItem.ToString();
                RoleBox.Text = selectedRole;
            }
        }

        private void NameBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (NameBox.Text == "Введите имя")
            {
                NameBox.Text = "";
            }
        }

        private void NameBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (NameBox.Text == "")
            {
                NameBox.Text = "Введите имя";
            }
        }

        private void SurnameBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (SurnameBox.Text == "Введите фамилию")
            {
                SurnameBox.Text = "";
            }
        }

        private void SurnameBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (SurnameBox.Text == "")
            {
                SurnameBox.Text = "Введите фамилию";
            }
        }

        private void PatronimicBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PatronimicBox.Text == "Введите отчество")
            {
                PatronimicBox.Text = "";
            }

        }

        private void PatronimicBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PatronimicBox.Text == "")
            {
                PatronimicBox.Text = "Введите отчество";
            }
        }

        private void PositionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionComboBox.SelectedItem != null)
            {
                string selectedPosition = PositionComboBox.SelectedItem.ToString();
                PositionBox.Text = selectedPosition;
            }
        }

        private void aprovalBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void emailBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (emailBox.Text == "Введите email")
            {
                emailBox.Text = "";
            }
        }

        private void emailBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (emailBox.Text == "")
            {
                emailBox.Text = "Введите email";
            }
        }

        private void phonenumberBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (phonenumberBox.Text == "Введите телефон")
            {
                phonenumberBox.Text = "";
            }

        }

        private void phonenumberBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (phonenumberBox.Text == "")
            {
                phonenumberBox.Text = "Введите телефон";
            }
        }

        private void loginBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (loginBox.Text == "Введите логин")
            {
                loginBox.Text = "";
            }

        }

        private void loginBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (loginBox.Text == "")
            {
                loginBox.Text = "Введите логин";
            }
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SetBlackoutDates()
{
    // Определите текущую дату
    DateTime currentDate = DateTime.Now.Date;

            // Удалите все предыдущие чёрные дни, если есть
            Birhtdate.BlackoutDates.Clear();

    // Заблокируйте дни, которые ещё не наступили
    for (DateTime date = currentDate.AddDays(1); date < currentDate.AddDays(365); date = date.AddDays(1))
    {
                Birhtdate.BlackoutDates.Add(new CalendarDateRange(date));
    }
}

        private async void RessetPassword_ClickAsync(object sender, RoutedEventArgs e)
        {
            var userData = new Authorize
            {
                Login = loginBox.Text,
                Password = "12345",
                Roles = RoleBox.Text,
                Position = PositionBox.Text,
                Fio = SurnameBox.Text + " " + NameBox.Text + " " + PatronimicBox.Text,
                Gender = GenderBox.Text,
                Birthdate = Birhtdate.Text,
                Email = emailBox.Text,
                PhoneNumber = phonenumberBox.Text
            };
            // Логин и пароль для URL
            string loginHeaderValue = (string)Application.Current.Properties["Login"];
            string passwordHeaderValue = (string)Application.Current.Properties["Password"];

            // Вызов метода для отправки POST-запроса с параметрами в URL
            bool isSuccess = await AddUserAsync(userData, loginHeaderValue, passwordHeaderValue);

            if (isSuccess)
            {
                SuccessLebelFields.Visibility = Visibility.Visible;
            }
            else
            {
                Console.WriteLine("Ошибка при добавлении пользователя.");
            }
        }

        private void TajikButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PatronimicBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Birhtdate_Loaded(object sender, RoutedEventArgs e)
        {
            SetBlackoutDates();
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
