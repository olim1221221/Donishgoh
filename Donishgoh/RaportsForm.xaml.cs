using Donishgoh.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для RaportsForm.xaml
    /// </summary>
    public partial class RaportsForm : Window
    {
        public RaportsForm()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Устанавливаем размеры окна
            this.Width = screenWidth;
            this.Height = screenHeight;
            Loaded += Window_LoadedAsync;
        }

        private async Task<DiplomResponseData> GetDiplomDataAsync(string apiUrl, string login, string password, string Status, int page, string searchFIO, string searchEducationLevel, string searchSpeciality, string searchDirection, string startDate, string endDate)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&Status={Status}&Page={page}&searchFIO={searchFIO}&searchEducationLevel={searchEducationLevel}&searchSpeciality={searchSpeciality}&searchDirection={searchDirection}&startDate={startDate}&endDate={endDate}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    DiplomResponseData diplomResponseData = JsonConvert.DeserializeObject<DiplomResponseData>(json);
                    return diplomResponseData;
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
            int totalSize = 0;

            Button1.Width = 280;
            Button1.Height = 90;
            ImageBrush AcceptButton = new ImageBrush();
            AcceptButton.ImageSource = (ImageSource)Resources["ButtonRapportShow"];
            Button1.Background = AcceptButton;
            Button2.Width = 260;
            Button2.Height = 80;
            ImageBrush PrintButtonShow = new ImageBrush();
            PrintButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button2.Background = PrintButtonShow;
            Button3.Width = 260;
            Button3.Height = 80;
            ImageBrush RedirectButtonShow = new ImageBrush();
            RedirectButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button3.Background = RedirectButtonShow;
            Button4.Width = 260;
            Button4.Height = 80;
            ImageBrush RejectButtonShow = new ImageBrush();
            RejectButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button4.Background = RejectButtonShow;
            Button1.Margin = new Thickness(496, 410, 0, 0);
            Button2.Margin = new Thickness(780, 420, 0, 0);
            Button3.Margin = new Thickness(1055, 420, 0, 0);
            Button4.Margin = new Thickness(1325, 420, 0, 0);


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

            if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
            {
                StatisticButton.Content = "           Статистика на год";
                UniverNameCation.Content = "Таджикский национальный \n           университет";
                UsersButton.Content = "           Сотрудники";
                DesktopButton.Content = "           Рабочее окно";
                Logs.Content = "           Журнал";
                RaportsButton.Content = "           Отчеты";
                Settings.Content = "           Настройки";
                FirstLabel.Content = "Отчеты";
                QuitButton.Content = "          Выход";
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
                FirstLabel.Content = "Ҳисобот";
                QuitButton.Content = "          Баромадгоҳ";
            }

            


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

        private void StatisticButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StatisticButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }
        private void StatisticButton_MouseLeave(object sender, MouseEventArgs e)
        {
            StatisticButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
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

        private void Logs_Click(object sender, RoutedEventArgs e)
        {
            LogsForm logs = new LogsForm();
            logs.Show();
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
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

   


        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TajikButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchPopupButton_Click(object sender, RoutedEventArgs e)
        {
            SearchGridField.Visibility = Visibility.Visible;
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReportsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchButton_ClickAsync(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SearchGridField.Visibility = Visibility.Hidden;
        }

        private void RaportsButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private async Task<List<ReportItem>> GetYearDataAsync(string apiUrl, string login, string password)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<ReportItem> yearResponseData = JsonConvert.DeserializeObject<List<ReportItem>>(json);
                    return yearResponseData;
                }
                else
                {
                    // Handle error
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }

        public class ReportData : List<ReportItem>
        {
        }



        private async void Button1_ClickAsync(object sender, RoutedEventArgs e)
        {
            Button1.Width = 280;
            Button1.Height = 90;
            ImageBrush AcceptButton = new ImageBrush();
            AcceptButton.ImageSource = (ImageSource)Resources["ButtonRapportShow"];
            Button1.Background = AcceptButton;
            Button2.Width = 260;
            Button2.Height = 80;
            ImageBrush PrintButtonShow = new ImageBrush();
            PrintButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button2.Background = PrintButtonShow;
            Button3.Width = 260;
            Button3.Height = 80;
            ImageBrush RedirectButtonShow = new ImageBrush();
            RedirectButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button3.Background = RedirectButtonShow;
            Button4.Width = 260;
            Button4.Height = 80;
            ImageBrush RejectButtonShow = new ImageBrush();
            RejectButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button4.Background = RejectButtonShow;
            Button1.Margin = new Thickness(496, 410, 0, 0);
            Button2.Margin = new Thickness(780, 420, 0, 0);
            Button3.Margin = new Thickness(1055, 420, 0, 0);
            Button4.Margin = new Thickness(1325, 420, 0, 0);

            try
            {
                List<ReportItem> YearResponseData = await GetYearDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetRapport", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString());

                // People = PersonList;
                ReportsDataGrid.ItemsSource = YearResponseData;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }


            Style centerHeaderStyle = new Style(typeof(DataGridColumnHeader));
            centerHeaderStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center));
            centerHeaderStyle.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
            centerHeaderStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.Transparent));

            ReportsDataGrid.Columns.Clear();
            ReportsDataGrid.Columns.Add(new DataGridTextColumn
            {
                HeaderStyle = centerHeaderStyle,
                Header = "ID",
                Binding = new Binding("Id"),
                Width = new DataGridLength(60),
                ElementStyle = (Style)FindResource("WrapText") // Замените на ваш ресурс стиля
            });

            ReportsDataGrid.Columns.Add(new DataGridTextColumn
            {
                HeaderStyle = centerHeaderStyle,
                Header = "Факультет",
                Binding = new Binding("TargetSpeciality"),
                Width = new DataGridLength(330),
                ElementStyle = (Style)FindResource("WrapText") // Замените на ваш ресурс стиля
            });

            ReportsDataGrid.Columns.Add(new DataGridTextColumn
            {
                HeaderStyle = centerHeaderStyle,
                Header = "Бакалавр \n красный",
                Binding = new Binding("CountRedDiplomBakalavr"),
                Width = new DataGridLength(108),
                ElementStyle = (Style)FindResource("WrapText") // Замените на ваш ресурс стиля
            });

            ReportsDataGrid.Columns.Add(new DataGridTextColumn
            {
                HeaderStyle = centerHeaderStyle,
                Header = "Бакалавр \n синий",
                Binding = new Binding("CountBlueDiplomBakalavr"),
                Width = new DataGridLength(108),
                ElementStyle = (Style)FindResource("WrapText") // Замените на ваш ресурс стиля
            });


            ReportsDataGrid.Columns.Add(new DataGridTextColumn
            {
                HeaderStyle = centerHeaderStyle,
                Header = "Магистр \n красный",
                Binding = new Binding("CountRedDiplomMagistr"),
                Width = new DataGridLength(108),
                ElementStyle = (Style)FindResource("WrapText") // Замените на ваш ресурс стиля
            });


            ReportsDataGrid.Columns.Add(new DataGridTextColumn
            {
                HeaderStyle = centerHeaderStyle,
                Header = "Магистр \n синий",
                Binding = new Binding("CountBlueDiplomMagistr"),
                Width = new DataGridLength(108),
                ElementStyle = (Style)FindResource("WrapText") // Замените на ваш ресурс стиля
            });


            ReportsDataGrid.Columns.Add(new DataGridTextColumn
            {
                HeaderStyle = centerHeaderStyle,
                Header = "Специалист \n красный",
                Binding = new Binding("CountRedDiplomSpecialist"),
                Width = new DataGridLength(108),
                ElementStyle = (Style)FindResource("WrapText") // Замените на ваш ресурс стиля
            });


            ReportsDataGrid.Columns.Add(new DataGridTextColumn
            {
                HeaderStyle = centerHeaderStyle,
                Header = "Специалист \n синий",
                Binding = new Binding("CountBlueDiplomSpecialist"),
                Width = new DataGridLength(108),
                ElementStyle = (Style)FindResource("WrapText") // Замените на ваш ресурс стиля
            });

            ReportsDataGrid.Columns.Add(new DataGridTextColumn
            {
                HeaderStyle = centerHeaderStyle,
                Header = "Итого",
                Binding = new Binding("Count"),
                Width = new DataGridLength(108),
                ElementStyle = (Style)FindResource("WrapText") // Замените на ваш ресурс стиля
            });

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button1.Width = 260;
            Button1.Height = 80;
            ImageBrush AcceptButton = new ImageBrush();
            AcceptButton.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button1.Background = AcceptButton;
            Button2.Width = 280;
            Button2.Height = 90;
            ImageBrush PrintButtonShow = new ImageBrush();
            PrintButtonShow.ImageSource = (ImageSource)Resources["ButtonRapportShow"];
            Button2.Background = PrintButtonShow;
            Button3.Width = 260;
            Button3.Height = 80;
            ImageBrush RedirectButtonShow = new ImageBrush();
            RedirectButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button3.Background = RedirectButtonShow;
            Button4.Width = 260;
            Button4.Height = 80;
            ImageBrush RejectButtonShow = new ImageBrush();
            RejectButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button4.Background = RejectButtonShow;
            Button1.Margin = new Thickness(490, 420, 0, 0);
            Button2.Margin = new Thickness(760, 410, 0, 0);
            Button3.Margin = new Thickness(1055, 420, 0, 0);
            Button4.Margin = new Thickness(1325, 420, 0, 0);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button1.Width = 260;
            Button1.Height = 80;
            ImageBrush AcceptButton = new ImageBrush();
            AcceptButton.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button1.Background = AcceptButton;
            Button2.Width = 260;
            Button2.Height = 80;
            ImageBrush PrintButtonShow = new ImageBrush();
            PrintButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button2.Background = PrintButtonShow;
            Button3.Width = 280;
            Button3.Height = 90;
            ImageBrush RedirectButtonShow = new ImageBrush();
            RedirectButtonShow.ImageSource = (ImageSource)Resources["ButtonRapportShow"];
            Button3.Background = RedirectButtonShow;
            Button4.Width = 260;
            Button4.Height = 80;
            ImageBrush RejectButtonShow = new ImageBrush();
            RejectButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button4.Background = RejectButtonShow;
            Button1.Margin = new Thickness(490, 420, 0, 0);
            Button2.Margin = new Thickness(760, 420, 0, 0);
            Button3.Margin = new Thickness(1035, 410, 0, 0);
            Button4.Margin = new Thickness(1325, 420, 0, 0);
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Button1.Width = 260;
            Button1.Height = 80;
            ImageBrush AcceptButton = new ImageBrush();
            AcceptButton.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button1.Background = AcceptButton;
            Button2.Width = 260;
            Button2.Height = 80;
            ImageBrush PrintButtonShow = new ImageBrush();
            PrintButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button2.Background = PrintButtonShow;
            Button3.Width = 260;
            Button3.Height = 80;
            ImageBrush RedirectButtonShow = new ImageBrush();
            RedirectButtonShow.ImageSource = (ImageSource)Resources["ButtonRapport"];
            Button3.Background = RedirectButtonShow;
            Button4.Width = 280;
            Button4.Height = 90;
            ImageBrush RejectButtonShow = new ImageBrush();
            RejectButtonShow.ImageSource = (ImageSource)Resources["ButtonRapportShow"];
            Button4.Background = RejectButtonShow;
            Button1.Margin = new Thickness(490, 420, 0, 0);
            Button2.Margin = new Thickness(760, 420, 0, 0);
            Button3.Margin = new Thickness(1035, 420, 0, 0);
            Button4.Margin = new Thickness(1305, 410, 0, 0);
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
