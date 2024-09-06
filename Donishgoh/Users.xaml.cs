using Donishgoh.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public ObservableCollection<Person> People { get; set; }

        public Users()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Устанавливаем размеры окна
            this.Width = screenWidth;
            this.Height = screenHeight;
            Loaded += Window_LoadedAsync;
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




        private void Button_Click(object sender, RoutedEventArgs e, Person person)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private async Task<List<Person>> GetAuthorizeDataAsync(string apiUrl, string login, string password, string Search)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&Search={Search}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                   
                    string json = await response.Content.ReadAsStringAsync();
                    List<Person> authorizeList = JsonConvert.DeserializeObject<List<Person>>(json);
                    return authorizeList;
                }
                else
                {
                    // Handle error
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }



        private async void Window_LoadedAsync(object sender, RoutedEventArgs e)
        {
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
                    PositionLabel.Content = Position;
                }
            }

            if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
            {
                StatisticButton.Content = "           Статистика на год";
                UniverNameCation.Content = "Таджикский национальный \n           университет";
                UsersButton.Content = "           Сотрудники";
                DesktopButton.Content = "           Рабочее окно";
                Logs.Content = "           Журнал";
                RaportsButton.Content = "           Отчеты";
                Settings.Content = "           Настройки";
                UserLabelFrame.Content = "Сотрудники";
                QuitButton.Content = "          Выход";
                AddUsers.Content = "Добавить сотрудника";
               
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
                UserLabelFrame.Content = "Коргарон";
                QuitButton.Content = "          Баромадгоҳ";
                AddUsers.Content = "Кормандро илова кунед";
                
            }

            try
            {
                var PersonList = await GetAuthorizeDataAsync(
                    $"{Application.Current.Properties["UrlServer"].ToString()}/api/Authorize",
                    Application.Current.Properties["Login"].ToString(),
                    Application.Current.Properties["Password"].ToString(),
                    SearchBox.Text);

                UsersDataGrid.ItemsSource = PersonList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }



            if (UsersDataGrid != null)
            {
                ImageBrush EditUser = new ImageBrush();
                EditUser.ImageSource = (ImageSource)Resources["EditUser"];
                EditUser.Stretch = Stretch.Uniform;

                Style centerCellStyle = new Style(typeof(DataGridCell));
                centerCellStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center));
                centerCellStyle.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
                centerCellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, Brushes.Transparent));
                centerCellStyle.Setters.Add(new Setter(DataGridCell.BorderBrushProperty, Brushes.Transparent));
                centerCellStyle.Setters.Add(new Setter(DataGridCell.ForegroundProperty, Brushes.Black));

                Style centerHeaderStyle = new Style(typeof(DataGridColumnHeader));
                centerHeaderStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center));
                centerHeaderStyle.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
                centerHeaderStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.Transparent));

                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    SetupColumn("ID", 70, "ID", centerCellStyle, centerHeaderStyle);
                    SetupColumn("FIO", 364, "ФИО", centerCellStyle, centerHeaderStyle);
                    SetupColumn("CreatedDate", 170, "Дата создания", centerCellStyle, centerHeaderStyle);
                    SetupColumn("Position", 170, "Должность", centerCellStyle, centerHeaderStyle);
                    SetupColumn("No_Approval", 190, "№ Согласование", centerCellStyle, centerHeaderStyle);
                    SetupColumn("Roles", 214, "Роль", centerCellStyle, centerHeaderStyle);
                }
                else
                {
                    SetupColumn("ID", 70, "ID", centerCellStyle, centerHeaderStyle);
                    SetupColumn("FIO", 364, "Ном ва насаб, номи падарӣ", centerCellStyle, centerHeaderStyle);
                    SetupColumn("CreatedDate", 170, "   Cанаи \nофариниш", centerCellStyle, centerHeaderStyle);
                    SetupColumn("Position", 170, "Вазифа", centerCellStyle, centerHeaderStyle);
                    SetupColumn("No_Approval", 190, "№ Тасдиқ", centerCellStyle, centerHeaderStyle);
                    SetupColumn("Roles", 214, "Нақш", centerCellStyle, centerHeaderStyle);
                }


                    

                bool actionColumnExists = UsersDataGrid.Columns.Any(c => c.Header.ToString() == "Действие");

                // Если столбец "Действие" существует, удаляем его
                if (actionColumnExists)
                {
                    DataGridColumn existingActionColumn = UsersDataGrid.Columns.Single(c => c.Header.ToString() == "Действие");
                    UsersDataGrid.Columns.Remove(existingActionColumn);
                }

                // Создаем новый столбец "Действие"
                DataGridTemplateColumn actionColumn = new DataGridTemplateColumn() { Header = "Действие" };
                DataTemplate dataTemplate = new DataTemplate();
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Button));
                factory.SetValue(Button.ContentProperty, "");
                factory.SetValue(Button.BackgroundProperty, EditUser);
                factory.SetValue(Button.BorderBrushProperty, Brushes.Transparent);
                factory.SetValue(Button.FocusVisualStyleProperty, null);
                factory.SetValue(Button.BorderThicknessProperty, new Thickness(0));
                factory.SetValue(Button.WidthProperty, 40.0);
                factory.SetValue(Button.IsTabStopProperty, false);
                factory.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click));
                factory.SetValue(Button.StyleProperty, FindResource("CustomButtonStyle"));

                dataTemplate.VisualTree = factory;
                actionColumn.CellTemplate = dataTemplate;

                // Добавляем столбец "Действие" в конец списка столбцов
                UsersDataGrid.Columns.Add(actionColumn);
            }
        }

        private void YourDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Получите текущий объект Person из строки
            Person person = e.Row.Item as Person;

            // Найдите столбец "Действие"
            DataGridTemplateColumn actionColumn = UsersDataGrid.Columns
                .OfType<DataGridTemplateColumn>()
                .FirstOrDefault(column => column.Header.ToString() == "1234");

            if (actionColumn != null)
            {
                // Создайте содержимое ячейки с кнопкой
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Button));
                factory.SetValue(Button.ContentProperty, "Ваш текст кнопки");
                factory.AddHandler(Button.ClickEvent, new RoutedEventHandler((s, args) => Button_Click(s, args, person)));

                // Примените содержимое к столбцу "Действие"
                DataTemplate dataTemplate = new DataTemplate { VisualTree = factory };
                actionColumn.CellTemplate = dataTemplate;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void AddUsers_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["SelectedUserId"] = "";
            AddUser addUser = new AddUser();

            addUser.AddUserButton.Content = "Добавить сотрудника";


            addUser.NameBox.IsEnabled = true;
            addUser.SurnameBox.IsEnabled = true;
            addUser.PatronimicBox.IsEnabled = true;
            addUser.Birhtdate.IsEnabled = true;
            addUser.GenderBox.IsEnabled = true;
            addUser.emailBox.IsEnabled = true;
            addUser.GenderComboBox.IsEnabled = true;
            addUser.phonenumberBox.IsEnabled = true;
            addUser.PositionBox.IsEnabled = true;
            addUser.PositionComboBox.IsEnabled = true;
            addUser.RoleBox.IsEnabled = true;
            addUser.RoleComboBox.IsEnabled = true;
            addUser.loginBox.IsEnabled = true;
            addUser.passwordBox.IsEnabled = true;
            addUser.RessetPassword.Visibility = Visibility.Hidden;
            addUser.Show();
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }
      

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem != null)
            {
                Application.Current.Properties["SelectedUserId"] = ((Person)UsersDataGrid.SelectedItem).ID;
                AddUser addUser = new AddUser();
                addUser.AddUserButton.Content = "Изменить";
                addUser.NameBox.IsEnabled = false;
                addUser.SurnameBox.IsEnabled = false;
                addUser.PatronimicBox.IsEnabled = false;
                addUser.Birhtdate.IsEnabled = false;
                addUser.GenderBox.IsEnabled = false;
                addUser.emailBox.IsEnabled = false;
                addUser.GenderComboBox.IsEnabled = false;
                addUser.phonenumberBox.IsEnabled = false;
                addUser.PositionBox.IsEnabled = false;
                addUser.PositionComboBox.IsEnabled = false;
                addUser.RoleBox.IsEnabled = false;
                addUser.RoleComboBox.IsEnabled = false;
                addUser.loginBox.IsEnabled = false;
                addUser.passwordBox.IsEnabled = false;
                addUser.RessetPassword.Visibility = Visibility.Visible;

                addUser.Show();
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

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {

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


        private void SetupColumn(string columnName, double width, string header, Style cellStyle, Style headerStyle)
        {
            DataGridColumn column = UsersDataGrid.Columns.SingleOrDefault(c => c.Header?.ToString() == columnName);

            if (column != null)
            {



                column.Width = new DataGridLength(width);
                column.CellStyle = cellStyle;
                column.Header = header;
                column.HeaderStyle = headerStyle;
            }
        }

        private async void SearchBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            try
            {
                var PersonList = await GetAuthorizeDataAsync(
                    $"{Application.Current.Properties["UrlServer"].ToString()}/api/Authorize",
                    Application.Current.Properties["Login"].ToString(),
                    Application.Current.Properties["Password"].ToString(),
                    SearchBox.Text);

                UsersDataGrid.ItemsSource = PersonList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }



            if (UsersDataGrid != null)
            {
                ImageBrush EditUser = new ImageBrush();
                EditUser.ImageSource = (ImageSource)Resources["EditUser"];
                EditUser.Stretch = Stretch.Uniform;

                Style centerCellStyle = new Style(typeof(DataGridCell));
                centerCellStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center));
                centerCellStyle.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
                centerCellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, Brushes.Transparent));
                centerCellStyle.Setters.Add(new Setter(DataGridCell.BorderBrushProperty, Brushes.Transparent));
                centerCellStyle.Setters.Add(new Setter(DataGridCell.ForegroundProperty, Brushes.Black));

                Style centerHeaderStyle = new Style(typeof(DataGridColumnHeader));
                centerHeaderStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center));
                centerHeaderStyle.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
                centerHeaderStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.Transparent));

                SetupColumn("ID", 70, "ID", centerCellStyle, centerHeaderStyle);
                SetupColumn("FIO", 364, "ФИО", centerCellStyle, centerHeaderStyle);
                SetupColumn("CreatedDate", 170, "Дата создания", centerCellStyle, centerHeaderStyle);
                SetupColumn("Position", 170, "Должность", centerCellStyle, centerHeaderStyle);
                SetupColumn("No_Approval", 190, "№ Согласование", centerCellStyle, centerHeaderStyle);
                SetupColumn("Roles", 214, "Роль", centerCellStyle, centerHeaderStyle);

                bool actionColumnExists = UsersDataGrid.Columns.Any(c => c.Header.ToString() == "Действие");

                // Если столбец "Действие" существует, удаляем его
                if (actionColumnExists)
                {
                    DataGridColumn existingActionColumn = UsersDataGrid.Columns.Single(c => c.Header.ToString() == "Действие");
                    UsersDataGrid.Columns.Remove(existingActionColumn);
                }

                // Создаем новый столбец "Действие"
                DataGridTemplateColumn actionColumn = new DataGridTemplateColumn() { Header = "Действие" };
                DataTemplate dataTemplate = new DataTemplate();
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Button));
                factory.SetValue(Button.ContentProperty, "");
                factory.SetValue(Button.BackgroundProperty, EditUser);
                factory.SetValue(Button.BorderBrushProperty, Brushes.Transparent);
                factory.SetValue(Button.FocusVisualStyleProperty, null);
                factory.SetValue(Button.BorderThicknessProperty, new Thickness(0));
                factory.SetValue(Button.WidthProperty, 40.0);
                factory.SetValue(Button.IsTabStopProperty, false);
                factory.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click));
                factory.SetValue(Button.StyleProperty, FindResource("CustomButtonStyle"));

                dataTemplate.VisualTree = factory;
                actionColumn.CellTemplate = dataTemplate;

                // Добавляем столбец "Действие" в конец списка столбцов
                UsersDataGrid.Columns.Add(actionColumn);
            }

        }

        private void SearchBox_MouseEnter(object sender, MouseEventArgs e)
        {
          
            
        }

        private void SearchBox_MouseLeave(object sender, MouseEventArgs e)
        {
            
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

        private void TajikButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {

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
