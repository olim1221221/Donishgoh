
using Donishgoh.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для LogsForm.xaml
    /// </summary>
    public partial class LogsForm : Window
    {
        public LogsForm()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Устанавливаем размеры окна
            this.Width = screenWidth;
            this.Height = screenHeight;
            Loaded += Window_LoadedAsync;
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

        private void Logs_Click(object sender, RoutedEventArgs e)
        {

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
        private static readonly HttpClient _httpClient = new HttpClient();

        private async Task<LogsResponseData> GetLogsDataAsync(string apiUrl, string login, string password, string search, int page)
        {
            try
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&Search={search}&Page={page}";
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    LogsResponseData logsResponseData = JsonConvert.DeserializeObject<LogsResponseData>(json);
                    return logsResponseData;
                }
                else
                {
                    // Обработка ошибки
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Обработка ошибки сети
                throw new Exception($"Failed to retrieve data. {ex.Message}");
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                throw new Exception($"Failed to retrieve data. {ex.Message}");
            }
        }


        private async void Window_LoadedAsync(object sender, RoutedEventArgs e)
        {



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
                FirstLabel.Content = "Журнал действий";
                QuitButton.Content = "          Выход";
                Download.Content = "    Скачать";
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
                FirstLabel.Content = "Амалиётҳо";
                QuitButton.Content = "          Баромадгоҳ";
                Download.Content = "    Зеркашӣ";
            }



            int totalSize = 0;
            Page = 0;
            try
            {
                LogsResponseData result = await GetLogsDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetLogs", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), SearchBox.Text, 1 ); 
                LogsDataGrid.ItemsSource = result.LogsResponse;
                 totalSize = result.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }



            if (LogsDataGrid != null)
            {

                DataGridColumn FIOColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "FIO");
                DataGridColumn EventDiplomColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "EventDiplom");
                DataGridColumn CreatedDateColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "CreatedDate");

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

                FIOColumn.Width = new DataGridLength(360);
                FIOColumn.CellStyle = centerCellStyle;

                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    FIOColumn.Header = "ФИО";
                    EventDiplomColumn.Header = "Событие";
                    CreatedDateColumn.Header = "Дата и время";
                }
                else
                {
                    FIOColumn.Header = "Ном ва насаб, номи падарӣ";
                    EventDiplomColumn.Header = "Амалиёт";
                    CreatedDateColumn.Header = "Сана ва вақт";
                }

                   
                FIOColumn.HeaderStyle = centerHeaderStyle;
                EventDiplomColumn.Width = new DataGridLength(730);
                EventDiplomColumn.CellStyle = centerCellStyle;             
                EventDiplomColumn.HeaderStyle = centerHeaderStyle;
                CreatedDateColumn.Width = new DataGridLength(220);
                CreatedDateColumn.CellStyle = centerCellStyle;               
                CreatedDateColumn.HeaderStyle = centerHeaderStyle;

            }
            if (totalSize > 20)
            {
                Page = totalSize / 20;
            }

            if ((Page * 20) < totalSize)
            {
                Page = Page + 1;
            }



            UpdatePageVisibility(Page);

        }

        private void LogsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public int Page;
        private async void SearchBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            int totalSize = 0;
            Page = 0;
            try
            {
                LogsResponseData result = await GetLogsDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetLogs", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), SearchBox.Text, 1);
                LogsDataGrid.ItemsSource = result.LogsResponse;
                totalSize = result.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (LogsDataGrid != null)
            {

                DataGridColumn FIOColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "FIO");
                DataGridColumn EventDiplomColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "EventDiplom");
                DataGridColumn CreatedDateColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "CreatedDate");

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

                FIOColumn.Width = new DataGridLength(360);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EventDiplomColumn.Width = new DataGridLength(730);
                EventDiplomColumn.CellStyle = centerCellStyle;
                EventDiplomColumn.Header = "Событие";
                EventDiplomColumn.HeaderStyle = centerHeaderStyle;

                CreatedDateColumn.Width = new DataGridLength(220);
                CreatedDateColumn.CellStyle = centerCellStyle;
                CreatedDateColumn.Header = "Дата и время";
                CreatedDateColumn.HeaderStyle = centerHeaderStyle;

            }

            UpdatePageVisibility(totalSize);
        }


        private void UpdatePageVisibility(int totalSize)
        {
            if (totalSize > 20)
            {
                Page = totalSize / 20;
            }

            if ((Page * 20) < totalSize)
            {
                Page = Page + 1;
            }


            // Все страницы
            List<UIElement> allPages = new List<UIElement> { Page1, Page2, Page3, Page4, Page5 };

            // Скрываем все страницы
            foreach (var page in allPages)
            {
                page.Visibility = Visibility.Hidden;
            }

            // Показываем нужное количество страниц в зависимости от текущей страницы
            for (int i = 0; i < Math.Min(Page, allPages.Count); i++)
            {
                allPages[i].Visibility = Visibility.Visible;
            }

            // Показываем/скрываем кнопки Next и Last в зависимости от текущей страницы
            if (Page > 5)
            {
                NextPage.Visibility = Visibility.Visible;
                LastPage.Visibility = Visibility.Visible;
            }
            else
            {
                NextPage.Visibility = Visibility.Hidden;
                LastPage.Visibility = Visibility.Hidden;
            }
        }


        private async void Page1_ClickAsync(object sender, RoutedEventArgs e)
        {
            Page1.Width = 46;
            Page1.Height = 46;
            Page2.Width = 40;
            Page2.Height = 40;
            Page3.Width = 40;
            Page3.Height = 40;
            Page4.Width = 40;
            Page4.Height = 40;
            Page5.Width = 40;
            Page5.Height = 40;



            int totalSize = 0;
            Page = 0;
            try
            {
                LogsResponseData result = await GetLogsDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetLogs", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), SearchBox.Text, Convert.ToInt32(Page1.Content));
                LogsDataGrid.ItemsSource = result.LogsResponse;
                totalSize = result.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (LogsDataGrid != null)
            {

                DataGridColumn FIOColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "FIO");
                DataGridColumn EventDiplomColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "EventDiplom");
                DataGridColumn CreatedDateColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "CreatedDate");

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

                FIOColumn.Width = new DataGridLength(360);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EventDiplomColumn.Width = new DataGridLength(730);
                EventDiplomColumn.CellStyle = centerCellStyle;
                EventDiplomColumn.Header = "Событие";
                EventDiplomColumn.HeaderStyle = centerHeaderStyle;

                CreatedDateColumn.Width = new DataGridLength(220);
                CreatedDateColumn.CellStyle = centerCellStyle;
                CreatedDateColumn.Header = "Дата и время";
                CreatedDateColumn.HeaderStyle = centerHeaderStyle;

            }

            UpdatePageVisibility(totalSize);


        }

        private async void Page2_ClickAsync(object sender, RoutedEventArgs e)
        {
            Page1.Width = 40;
            Page1.Height = 40;
            Page2.Width = 46;
            Page2.Height = 46;
            Page3.Width = 40;
            Page3.Height = 40;
            Page4.Width = 40;
            Page4.Height = 40;
            Page5.Width = 40;
            Page5.Height = 40;


            int totalSize = 0;
            Page = 0;
            try
            {
                LogsResponseData result = await GetLogsDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetLogs", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), SearchBox.Text, Convert.ToInt32(Page2.Content));
                LogsDataGrid.ItemsSource = result.LogsResponse;
                totalSize = result.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (LogsDataGrid != null)
            {

                DataGridColumn FIOColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "FIO");
                DataGridColumn EventDiplomColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "EventDiplom");
                DataGridColumn CreatedDateColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "CreatedDate");

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

                FIOColumn.Width = new DataGridLength(360);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EventDiplomColumn.Width = new DataGridLength(730);
                EventDiplomColumn.CellStyle = centerCellStyle;
                EventDiplomColumn.Header = "Событие";
                EventDiplomColumn.HeaderStyle = centerHeaderStyle;

                CreatedDateColumn.Width = new DataGridLength(220);
                CreatedDateColumn.CellStyle = centerCellStyle;
                CreatedDateColumn.Header = "Дата и время";
                CreatedDateColumn.HeaderStyle = centerHeaderStyle;

            }


            UpdatePageVisibility(totalSize);
        }

        private async void Page3_ClickAsync(object sender, RoutedEventArgs e)
        {
            Page1.Width = 40;
            Page1.Height = 40;
            Page2.Width = 40;
            Page2.Height = 40;
            Page3.Width = 46;
            Page3.Height = 46;
            Page4.Width = 40;
            Page4.Height = 40;
            Page5.Width = 40;
            Page5.Height = 40;

            int totalSize = 0;
            Page = 0;
            try
            {
                LogsResponseData result = await GetLogsDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetLogs", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), SearchBox.Text, Convert.ToInt32(Page3.Content));
                LogsDataGrid.ItemsSource = result.LogsResponse;
                totalSize = result.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (LogsDataGrid != null)
            {

                DataGridColumn FIOColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "FIO");
                DataGridColumn EventDiplomColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "EventDiplom");
                DataGridColumn CreatedDateColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "CreatedDate");

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

                FIOColumn.Width = new DataGridLength(360);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EventDiplomColumn.Width = new DataGridLength(730);
                EventDiplomColumn.CellStyle = centerCellStyle;
                EventDiplomColumn.Header = "Событие";
                EventDiplomColumn.HeaderStyle = centerHeaderStyle;

                CreatedDateColumn.Width = new DataGridLength(220);
                CreatedDateColumn.CellStyle = centerCellStyle;
                CreatedDateColumn.Header = "Дата и время";
                CreatedDateColumn.HeaderStyle = centerHeaderStyle;

            }


            UpdatePageVisibility(totalSize);
        }

        private async void Page4_ClickAsync(object sender, RoutedEventArgs e)
        {
            Page1.Width = 40;
            Page1.Height = 40;
            Page2.Width = 40;
            Page2.Height = 40;
            Page3.Width = 40;
            Page3.Height = 40;
            Page4.Width = 46;
            Page4.Height = 46;
            Page5.Width = 40;
            Page5.Height = 40;

            int totalSize = 0;
            Page = 0;
            try
            {
                LogsResponseData result = await GetLogsDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetLogs", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), SearchBox.Text, Convert.ToInt32(Page4.Content));
                LogsDataGrid.ItemsSource = result.LogsResponse;
                totalSize = result.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (LogsDataGrid != null)
            {

                DataGridColumn FIOColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "FIO");
                DataGridColumn EventDiplomColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "EventDiplom");
                DataGridColumn CreatedDateColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "CreatedDate");

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

                FIOColumn.Width = new DataGridLength(360);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EventDiplomColumn.Width = new DataGridLength(730);
                EventDiplomColumn.CellStyle = centerCellStyle;
                EventDiplomColumn.Header = "Событие";
                EventDiplomColumn.HeaderStyle = centerHeaderStyle;

                CreatedDateColumn.Width = new DataGridLength(220);
                CreatedDateColumn.CellStyle = centerCellStyle;
                CreatedDateColumn.Header = "Дата и время";
                CreatedDateColumn.HeaderStyle = centerHeaderStyle;

            }


            UpdatePageVisibility(totalSize);
        }

        private async void Page5_ClickAsync(object sender, RoutedEventArgs e)
        {
            Page1.Width = 40;
            Page1.Height = 40;
            Page2.Width = 40;
            Page2.Height = 40;
            Page3.Width = 40;
            Page3.Height = 40;
            Page4.Width = 40;
            Page4.Height = 40;
            Page5.Width = 46;
            Page5.Height = 46;

            int totalSize = 0;
            Page = 0;
            try
            {
                LogsResponseData result = await GetLogsDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetLogs", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), SearchBox.Text, Convert.ToInt32(Page5.Content));
                LogsDataGrid.ItemsSource = result.LogsResponse;
                totalSize = result.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (LogsDataGrid != null)
            {

                DataGridColumn FIOColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "FIO");
                DataGridColumn EventDiplomColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "EventDiplom");
                DataGridColumn CreatedDateColumn = LogsDataGrid.Columns.SingleOrDefault(c => c.Header.ToString() == "CreatedDate");

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

                FIOColumn.Width = new DataGridLength(360);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EventDiplomColumn.Width = new DataGridLength(730);
                EventDiplomColumn.CellStyle = centerCellStyle;
                EventDiplomColumn.Header = "Событие";
                EventDiplomColumn.HeaderStyle = centerHeaderStyle;

                CreatedDateColumn.Width = new DataGridLength(220);
                CreatedDateColumn.CellStyle = centerCellStyle;
                CreatedDateColumn.Header = "Дата и время";
                CreatedDateColumn.HeaderStyle = centerHeaderStyle;

            }


            UpdatePageVisibility(totalSize);
        }

        private void LastPage_Click(object sender, RoutedEventArgs e)
        {
            Page1.Width = 40;
            Page1.Height = 40;
            Page2.Width = 40;
            Page2.Height = 40;
            Page3.Width = 40;
            Page3.Height = 40;
            Page4.Width = 40;
            Page4.Height = 40;
            Page5.Width = 40;
            Page5.Height = 40;

            int FirstPage = Convert.ToInt32(Page1.Content);


            if (Convert.ToInt32(Page1.Content) > 1)
            {
                Page1.Content = (FirstPage - 1).ToString();
                Page2.Content = FirstPage.ToString();
                Page3.Content = (FirstPage + 1).ToString();
                Page4.Content = (FirstPage + 2).ToString();
                Page5.Content = (FirstPage + 3).ToString();
            }

            else
            {
                NextPage.IsEnabled = true;
                LastPage.IsEnabled = false;
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            Page1.Width = 40;
            Page1.Height = 40;
            Page2.Width = 40;
            Page2.Height = 40;
            Page3.Width = 40;
            Page3.Height = 40;
            Page4.Width = 40;
            Page4.Height = 40;
            Page5.Width = 40;
            Page5.Height = 40;
            int lastPage = Convert.ToInt32(Page5.Content);


            if (lastPage < Page)

            {
                Page1.Content = (lastPage - 3).ToString();
                Page2.Content = (lastPage - 2).ToString();
                Page3.Content = (lastPage - 1).ToString();
                Page4.Content = (lastPage).ToString();
                Page5.Content = (lastPage + 1).ToString();
            }
            else
            {
                NextPage.IsEnabled = false;
                LastPage.IsEnabled = true;               
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
      
        private void Download_Click(object sender, RoutedEventArgs e)
        {
            ExportDataGridToExcel(LogsDataGrid, "File.xlsx");
        }
        private void ExportDataGridToExcel(DataGrid dataGrid, string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                Title = "Save Excel File",
                FileName = "ExcelFile.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    // Добавление данных в ячейки
                    worksheet.Cells["A1"].LoadFromDataTable(DataGridToDataTable(dataGrid), true);

                    // Сохранение книги в файл
                    using (var stream = new MemoryStream())
                    {
                        package.SaveAs(stream);
                        stream.Position = 0;

                        // Сохранение файла по выбранному пути
                        using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
            }
        }

        private void SaveStreamToFile(string fileName, MemoryStream stream)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(fileStream);
            }
        }

       

        private DataTable DataGridToDataTable(DataGrid dataGrid)
        {
            var dataTable = new DataTable();

            foreach (var column in dataGrid.Columns)
            {
                dataTable.Columns.Add(column.Header.ToString());
            }

            foreach (var item in dataGrid.Items)
            {
                var row = dataTable.NewRow();

                foreach (var column in dataGrid.Columns)
                {
                    var binding = (column as DataGridBoundColumn).Binding as System.Windows.Data.Binding;
                    var propertyPath = binding.Path.Path;
                    var cellValue = item.GetType().GetProperty(propertyPath).GetValue(item, null);
                    row[column.Header.ToString()] = cellValue;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TajikButton_Click(object sender, RoutedEventArgs e)
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
