using Donishgoh.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Threading;

namespace Donishgoh
{
    /// <summary>
    /// Логика взаимодействия для Desktop.xaml
    /// </summary>
    public partial class Desktop : Window
    {
        public ObservableCollection<DiplomList> Diplom { get; }

        private DispatcherTimer timer;
        private int seconds = 0;
        public Desktop()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            this.Width = screenWidth;
            this.Height = screenHeight;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_TickAsync;
            timer.Start();
            Loaded += Window_LoadedAsync;
        }

        private async void Timer_TickAsync(object sender, EventArgs e)
        {
            seconds++;
            labelTimer.Content = seconds.ToString();
            int totalSize = 0;
            if (seconds == 20)
              
            {
                try
                {
                    DiplomResponseData result = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), "InProgress", 1, FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);
                    totalSize = result.TotalCount;
                    DiplomDataGrid.ItemsSource = result.diplomList;


                    if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                    {
                        TodayButton.Content = "   В ожидании\n           " + result.TotalInProgress;
                        AcceptedButton.Content = "     Одобрено\n           " + result.TotalAccepted;
                        PrintedButton.Content = "     Распечатано\n           " + result.TotalPrinted;
                        RedirectButton.Content = "     На дороботку\n           " + result.TotalForFinishing;
                        RejectButton.Content = "        Отклонено\n           " + result.TotalRejected;
                    }
                    else
                    {
                        TodayButton.Content = "   Дар интизорӣ\n           " + result.TotalInProgress;
                        AcceptedButton.Content = "     Тасдиқ шуд\n           " + result.TotalAccepted;
                        PrintedButton.Content = "     Чоп шуд\n           " + result.TotalPrinted;
                        RedirectButton.Content = "     Барои коркард\n           " + result.TotalForFinishing;
                        RejectButton.Content = "        Инкор шуд\n           " + result.TotalRejected;
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                }


                if (DiplomDataGrid != null)
                {
                    UpdatePageVisibility(totalSize, TodayButton, "   В ожидании\n           ", "   Дар интизорӣ\n           ");
                }


                seconds = 0;
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

        private void StatisticButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StatisticButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }
        private void StatisticButton_MouseLeave(object sender, MouseEventArgs e)
        {
            StatisticButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
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
            timer.Stop();
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
            timer.Stop();
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
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
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
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            MainWindow main = new MainWindow();
            main.Show();
            main.EnterButton.Content = "Войти";
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
            timer.Stop();
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
        private void ConfigureDataGridColumn(DataGridColumn column, DataGridLength width, string header)
        {
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

            column.Width = width;
            column.CellStyle = centerCellStyle;
            column.Header = header;
            column.HeaderStyle = centerHeaderStyle;
            //column.ToolTip = toolTip;
        }
        private void ConfigureTextColumn(DataGrid dataGrid, string columnHeader, string propertyName, string stringFormat = "{0}")
        {
            DataGridColumn column = dataGrid.Columns.Single(c => c.Header.ToString() == columnHeader);

            if (column is DataGridTextColumn textColumn)
            {
                textColumn.Binding = new Binding(propertyName)
                {
                    StringFormat = stringFormat.Trim() // This removes spaces before and after the text
                };
            }
        }
        private async void TodayButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["statusFieldsResponse"] = "InProgress";
            ButtonSatus.Text = "InProgress";



            int totalSize = 0;
            TodayButton.Width = 280;
            TodayButton.Height = 90;
            ImageBrush Today = new ImageBrush();
            Today.ImageSource = (ImageSource)Resources["TodayButton"];
            TodayButton.Background = Today;
            ImageBrush AcceptButtonShow = new ImageBrush();
            AcceptButtonShow.ImageSource = (ImageSource)Resources["AcceptButtonShow"];
            AcceptedButton.Background = AcceptButtonShow;
            AcceptedButton.Width = 260;
            AcceptedButton.Height = 80;
            ImageBrush PrintButtonShow = new ImageBrush();
            PrintButtonShow.ImageSource = (ImageSource)Resources["PrintButtonShow"];
            PrintedButton.Background = PrintButtonShow;
            PrintedButton.Width = 260;
            PrintedButton.Height = 80;
            ImageBrush RedirectButtonShow = new ImageBrush();
            RedirectButtonShow.ImageSource = (ImageSource)Resources["RedirectButtonShow"];
            RedirectButton.Background = RedirectButtonShow;
            RedirectButton.Width = 260;
            RedirectButton.Height = 80;
            ImageBrush RejectButtonShow = new ImageBrush();
            RejectButtonShow.ImageSource = (ImageSource)Resources["RejectButtonShow"];
            RejectButton.Background = RejectButtonShow;
            RejectButton.Width = 260;
            RejectButton.Height = 80;


            TodayButton.Margin = new Thickness(495, 245, 0, 0);
            AcceptedButton.Margin = new Thickness(782, 255, 0, 0);
            PrintedButton.Margin = new Thickness(1055, 255, 0, 0);
            RedirectButton.Margin = new Thickness(1325, 255, 0, 0);
            RejectButton.Margin = new Thickness(1595, 255, 0, 0);

            FioStudentSearch.Text = "";
            EducationLevelSearch.Text = "";
            SpecialitySearch.Text = "";
            DirectionSearch.Text = "";
            startDateSearch.Text = "";
            endDateSearch.Text = "";
            
            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), "InProgress", 1, FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);

                // People = PersonList;
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
                totalSize = DiplomList.TotalCount;

                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    TodayButton.Content = "   В ожидании\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Одобрено\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Распечатано\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     На дороботку\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Отклонено\n           " + DiplomList.TotalRejected;
                }
                else
                {
                    TodayButton.Content = "   Дар интизорӣ\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Тасдиқ шуд\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Чоп шуд\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     Барои коркард\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Инкор шуд\n           " + DiplomList.TotalRejected;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (DiplomDataGrid != null)
            {
                UpdatePageVisibility(totalSize, TodayButton, "   В ожидании\n           ", "   Дар интизорӣ\n           ");
            }

            


        }
       
        private async void AcceptedButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["statusFieldsResponse"] = "Accepted";
            ButtonSatus.Text = "Accepted";

            int totalSize = 0;
            AcceptedButton.Width = 280;
            AcceptedButton.Height = 90;
            ImageBrush AcceptButton = new ImageBrush();
            AcceptButton.ImageSource = (ImageSource)Resources["AcceptButton"];
            AcceptedButton.Background = AcceptButton;
            ImageBrush TodayButtonShow = new ImageBrush();
            TodayButtonShow.ImageSource = (ImageSource)Resources["TodayButtonShow"];
            TodayButton.Background = TodayButtonShow;
            TodayButton.Width = 260;
            TodayButton.Height = 80;
            ImageBrush PrintButtonShow = new ImageBrush();
            PrintButtonShow.ImageSource = (ImageSource)Resources["PrintButtonShow"];
            PrintedButton.Background = PrintButtonShow;
            PrintedButton.Width = 260;
            PrintedButton.Height = 80;
            ImageBrush RedirectButtonShow = new ImageBrush();
            RedirectButtonShow.ImageSource = (ImageSource)Resources["RedirectButtonShow"];
            RedirectButton.Background = RedirectButtonShow;
            RedirectButton.Width = 260;
            RedirectButton.Height = 80;
            ImageBrush RejectButtonShow = new ImageBrush();
            RejectButtonShow.ImageSource = (ImageSource)Resources["RejectButtonShow"];
            RejectButton.Background = RejectButtonShow;
            RejectButton.Width = 260;
            RejectButton.Height = 80;

            TodayButton.Margin = new Thickness(495, 255, 0, 0);
            AcceptedButton.Margin = new Thickness(765, 245, 0, 0);
            PrintedButton.Margin = new Thickness(1055, 255, 0, 0);
            RedirectButton.Margin = new Thickness(1325, 255, 0, 0);
            RejectButton.Margin = new Thickness(1595, 255, 0, 0);

            FioStudentSearch.Text = "";
            EducationLevelSearch.Text = "";
            SpecialitySearch.Text = ""; 
            DirectionSearch.Text = "";
            startDateSearch.Text = "";
            endDateSearch.Text = "";
            
            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), "Accepted", 1, FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);

                // People = PersonList;
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
                totalSize = DiplomList.TotalCount;
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    TodayButton.Content = "   В ожидании\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Одобрено\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Распечатано\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     На дороботку\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Отклонено\n           " + DiplomList.TotalRejected;
                }
                else
                {
                    TodayButton.Content = "   Дар интизорӣ\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Тасдиқ шуд\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Чоп шуд\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     Барои коркард\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Инкор шуд\n           " + DiplomList.TotalRejected;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (DiplomDataGrid != null)
            {          
                UpdatePageVisibility(totalSize, AcceptedButton, "     Одобрено\n           ", "     Тасдиқ шуд\n           ");             
            }
        }
        private async void PrintedButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            int totalSize = 0;
            Application.Current.Properties["statusFieldsResponse"] = "Printed";
            ButtonSatus.Text = "Printed";

            PrintedButton.Width = 280;
            PrintedButton.Height = 90;
            ImageBrush PrintButton = new ImageBrush();
            PrintButton.ImageSource = (ImageSource)Resources["PrintButton"];
            PrintedButton.Background = PrintButton;
            ImageBrush TodayButtonShow = new ImageBrush();
            TodayButtonShow.ImageSource = (ImageSource)Resources["TodayButtonShow"];
            TodayButton.Background = TodayButtonShow;
            TodayButton.Width = 260;
            TodayButton.Height = 80;
            ImageBrush AcceptButtonShow = new ImageBrush();
            AcceptButtonShow.ImageSource = (ImageSource)Resources["AcceptButtonShow"];
            AcceptedButton.Background = AcceptButtonShow;
            AcceptedButton.Width = 260;
            AcceptedButton.Height = 80;
            ImageBrush RedirectButtonShow = new ImageBrush();
            RedirectButtonShow.ImageSource = (ImageSource)Resources["RedirectButtonShow"];
            RedirectButton.Background = RedirectButtonShow;
            RedirectButton.Width = 260;
            RedirectButton.Height = 80;
            ImageBrush RejectButtonShow = new ImageBrush();
            RejectButtonShow.ImageSource = (ImageSource)Resources["RejectButtonShow"];
            RejectButton.Background = RejectButtonShow;
            RejectButton.Width = 260;
            RejectButton.Height = 80;

            TodayButton.Margin = new Thickness(495, 255, 0, 0);
            AcceptedButton.Margin = new Thickness(765, 255, 0, 0);
            PrintedButton.Margin = new Thickness(1035, 245, 0, 0);
            RedirectButton.Margin = new Thickness(1325, 255, 0, 0);
            RejectButton.Margin = new Thickness(1595, 255, 0, 0);


           

            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), "Printed", 1, FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);

                // People = PersonList;
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
                totalSize = DiplomList.TotalCount;
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    TodayButton.Content = "   В ожидании\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Одобрено\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Распечатано\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     На дороботку\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Отклонено\n           " + DiplomList.TotalRejected;
                }
                else
                {
                    TodayButton.Content = "   Дар интизорӣ\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Тасдиқ шуд\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Чоп шуд\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     Барои коркард\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Инкор шуд\n           " + DiplomList.TotalRejected;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (DiplomDataGrid != null)
            {
                UpdatePageVisibility(totalSize, PrintedButton, "     Распечатано\n           ", "     Чоп шуд\n           ");

            }
        }
        private async void RedirectButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            int totalSize = 0;
            ButtonSatus.Text = "ForFinishing";
            Application.Current.Properties["statusFieldsResponse"] = "ForFinishing";
            RedirectButton.Width = 280;
            RedirectButton.Height = 90;
            ImageBrush AcceptButton = new ImageBrush();
            AcceptButton.ImageSource = (ImageSource)Resources["RedirectButton"];
            RedirectButton.Background = AcceptButton;
            ImageBrush TodayButtonShow = new ImageBrush();
            TodayButtonShow.ImageSource = (ImageSource)Resources["TodayButtonShow"];
            TodayButton.Background = TodayButtonShow;
            TodayButton.Width = 260;
            TodayButton.Height = 80;
            ImageBrush AcceptButtonShow = new ImageBrush();
            AcceptButtonShow.ImageSource = (ImageSource)Resources["AcceptButtonShow"];
            AcceptedButton.Background = AcceptButtonShow;
            AcceptedButton.Width = 260;
            AcceptedButton.Height = 80;
            ImageBrush PrintButtonShow = new ImageBrush();
            PrintButtonShow.ImageSource = (ImageSource)Resources["PrintButtonShow"];
            PrintedButton.Background = PrintButtonShow;
            PrintedButton.Width = 260;
            PrintedButton.Height = 80;
            ImageBrush RejectButtonShow = new ImageBrush();
            RejectButtonShow.ImageSource = (ImageSource)Resources["RejectButtonShow"];
            RejectButton.Background = RejectButtonShow;
            RejectButton.Width = 260;
            RejectButton.Height = 80;

            TodayButton.Margin = new Thickness(495, 255, 0, 0);
            AcceptedButton.Margin = new Thickness(765, 255, 0, 0);
            PrintedButton.Margin = new Thickness(1035, 255, 0, 0);
            RedirectButton.Margin = new Thickness(1305, 245, 0, 0);
            RejectButton.Margin = new Thickness(1595, 255, 0, 0);

            FioStudentSearch.Text = "";
            EducationLevelSearch.Text = "";
            SpecialitySearch.Text = "";
            DirectionSearch.Text = "";
            startDateSearch.Text = "";
            endDateSearch.Text = "";
            
            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), "ForFinishing", 1, FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);

                // People = PersonList;
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
                totalSize = DiplomList.TotalCount;
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    TodayButton.Content = "   В ожидании\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Одобрено\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Распечатано\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     На дороботку\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Отклонено\n           " + DiplomList.TotalRejected;
                }
                else
                {
                    TodayButton.Content = "   Дар интизорӣ\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Тасдиқ шуд\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Чоп шуд\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     Барои коркард\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Инкор шуд\n           " + DiplomList.TotalRejected;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (DiplomDataGrid != null)
            {
                UpdatePageVisibility(totalSize, RedirectButton, "     На дороботку\n           ", "     Барои коркард\n           ");
            }

        }
        private async void RejectButton_ClickAsync(object sender, RoutedEventArgs e)
        {

            Application.Current.Properties["statusFieldsResponse"] = "Rejected";
            int totalSize = 0;
            ButtonSatus.Text = "Rejected";
            RejectButton.Width = 280;
            RejectButton.Height = 90;
            ImageBrush AcceptButton = new ImageBrush();
            AcceptButton.ImageSource = (ImageSource)Resources["RejectButton"];
            RejectButton.Background = AcceptButton;
            ImageBrush TodayButtonShow = new ImageBrush();
            TodayButtonShow.ImageSource = (ImageSource)Resources["TodayButtonShow"];
            TodayButton.Background = TodayButtonShow;
            TodayButton.Width = 260;
            TodayButton.Height = 80;
            ImageBrush AcceptButtonShow = new ImageBrush();
            AcceptButtonShow.ImageSource = (ImageSource)Resources["AcceptButtonShow"];
            AcceptedButton.Background = AcceptButtonShow;
            AcceptedButton.Width = 260;
            AcceptedButton.Height = 80;
            ImageBrush PrintButtonShow = new ImageBrush();
            PrintButtonShow.ImageSource = (ImageSource)Resources["PrintButtonShow"];
            PrintedButton.Background = PrintButtonShow;
            PrintedButton.Width = 260;
            PrintedButton.Height = 80;
            ImageBrush RedirectButtonShow = new ImageBrush();
            RedirectButtonShow.ImageSource = (ImageSource)Resources["RedirectButtonShow"];
            RedirectButton.Background = RedirectButtonShow;
            RedirectButton.Width = 260;
            RedirectButton.Height = 80;

            TodayButton.Margin = new Thickness(495, 255, 0, 0);
            AcceptedButton.Margin = new Thickness(765, 255, 0, 0);
            PrintedButton.Margin = new Thickness(1035, 255, 0, 0);
            RedirectButton.Margin = new Thickness(1305, 255, 0, 0);
            RejectButton.Margin = new Thickness(1575, 245, 0, 0);

            FioStudentSearch.Text = "";
            EducationLevelSearch.Text = "";
            SpecialitySearch.Text = "";
            DirectionSearch.Text = "";
            startDateSearch.Text = "";
            endDateSearch.Text = "";
           
            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), "Rejected", 1, FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);

                // People = PersonList;
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
                totalSize = DiplomList.TotalCount;
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    TodayButton.Content = "   В ожидании\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Одобрено\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Распечатано\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     На дороботку\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Отклонено\n           " + DiplomList.TotalRejected;
                }
                else
                {
                    TodayButton.Content = "   Дар интизорӣ\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Тасдиқ шуд\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Чоп шуд\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     Барои коркард\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Инкор шуд\n           " + DiplomList.TotalRejected;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (DiplomDataGrid != null)
            {
                UpdatePageVisibility(totalSize, RejectButton, "        Отклонено\n           ", "        Инкор шуд\n           ");
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
                FirstLabel.Content = "Рабочее окно";
                TodayButton.Content = "   В ожидании";
                AcceptedButton.Content = "     Одобрено";
                PrintedButton.Content = "     Распечатано";
                RedirectButton.Content = "     На доработку";
                RejectButton.Content = "     Отклонено";
                GivenDiplomButton.Content = "Выданные дипломы";
                QuitButton.Content = "          Выход";
                AddDiplom.Content = "Создать диплом";
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
                FirstLabel.Content = "Равзанаи корӣ";
                TodayButton.Content = "   Дар интизорӣ";
                AcceptedButton.Content = "     Тасдиқ шуд";
                PrintedButton.Content = "     Чоп шуд";
                RedirectButton.Content = "     Барои коркард";
                RejectButton.Content = "     Инкор шуд";
                GivenDiplomButton.Content = "Дипломҳои додашуда";
                QuitButton.Content = "          Баромадгоҳ";
                AddDiplom.Content = "Сохтани диплом";
            }

            int totalSize = 0;

            Application.Current.Properties["statusFieldsResponse"] = "InProgress";
            ButtonSatus.Text = "InProgress";
            totalSize = 0;
            Page = 0;
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
            FioStudentSearch.Text = "";
            EducationLevelSearch.Text = "";
            SpecialitySearch.Text = "";
            DirectionSearch.Text = "";
            startDateSearch.Text = "";
            endDateSearch.Text = "";
            try
            {
                DiplomResponseData result = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), "InProgress", 1, FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);

                DiplomDataGrid.ItemsSource = result.diplomList;
                totalSize = result.TotalCount;
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    TodayButton.Content = "   В ожидании\n           " + result.TotalInProgress;
                    AcceptedButton.Content = "     Одобрено\n           " + result.TotalAccepted;
                    PrintedButton.Content = "     Распечатано\n           " + result.TotalPrinted;
                    RedirectButton.Content = "     На дороботку\n           " + result.TotalForFinishing;
                    RejectButton.Content = "        Отклонено\n           " + result.TotalRejected;
                }
                else
                {
                    TodayButton.Content = "   Дар интизорӣ\n           " + result.TotalInProgress;
                    AcceptedButton.Content = "     Тасдиқ шуд\n           " + result.TotalAccepted;
                    PrintedButton.Content = "     Чоп шуд\n           " + result.TotalPrinted;
                    RedirectButton.Content =  "     Барои коркард\n           " + result.TotalForFinishing;
                    RejectButton.Content =  "        Инкор шуд\n           " + result.TotalRejected;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (DiplomDataGrid != null)
            {
                UpdatePageVisibility(totalSize, TodayButton, "   В ожидании\n           ", "   Дар интизорӣ\n           ");
            }

            string RoleResponse = Application.Current.Properties["Roles"].ToString();
            if (RoleResponse == "Подтверждающий")

            {
                AddDiplom.Visibility = Visibility.Hidden;
                UsersButton.IsEnabled = false;
                Logs.IsEnabled = false;
                Settings.IsEnabled = false;
            }

            else if (RoleResponse == "Администратор")
            {
                AddDiplom.Visibility = Visibility.Hidden;
            }
            else if (RoleResponse == "Пользователь")
            {
                UsersButton.IsEnabled = false;
                Logs.IsEnabled = false;
                Settings.IsEnabled = false;
            }

          
        }


        private void UpdatePageVisibilitySearch(int totalSize)
        {
            Page = 0;
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

            // Скрываем лишние страницы, если текущая страница уменьшилась
            for (int i = Page; i < allPages.Count; i++)
            {
                allPages[i].Visibility = Visibility.Hidden;
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

            DataGridColumn IDColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ID");
            DataGridColumn FIOColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "FIO");
            DataGridColumn EducationLevelColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "EducationLevel");
            DataGridColumn SpecialityColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Speciality");
            DataGridColumn QualificationColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Qualification");
            DataGridColumn DirectionColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Direction");
            DataGridColumn ApprovalDateColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ApprovalDate");
            DataGridColumn StatusColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Status");

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
                FIOColumn.Header = "ФИО";
                EducationLevelColumn.Header = "    Уровень \nобразования";
                SpecialityColumn.Header = "Специальность";
                QualificationColumn.Header = "Квалификация";
                DirectionColumn.Header = "Направление";
                ApprovalDateColumn.Header = "Дата одобрении";
                StatusColumn.Header = "Статус";
               

            }
            else
            {
                FIOColumn.Header = "Ном ва насаб, номи падарӣ";
                EducationLevelColumn.Header = "    Зинаи таҳсил";
                SpecialityColumn.Header = "Ихтисос";
                QualificationColumn.Header = "Тахассус (касб)";
                DirectionColumn.Header = "Самт";
                ApprovalDateColumn.Header = "Санаи тасдиқ";
                StatusColumn.Header = "Статус";

            }


            IDColumn.Width = new DataGridLength(65);
            IDColumn.CellStyle = centerCellStyle;
            IDColumn.Header = "ID";
            IDColumn.HeaderStyle = centerHeaderStyle;

            FIOColumn.Width = new DataGridLength(280);
            FIOColumn.CellStyle = centerCellStyle;
            FIOColumn.HeaderStyle = centerHeaderStyle;

            EducationLevelColumn.Width = new DataGridLength(140);
            EducationLevelColumn.CellStyle = centerCellStyle;
            EducationLevelColumn.HeaderStyle = centerHeaderStyle;

            SpecialityColumn.Width = new DataGridLength(170);
            SpecialityColumn.CellStyle = centerCellStyle;
            SpecialityColumn.HeaderStyle = centerHeaderStyle;

            QualificationColumn.Width = new DataGridLength(180);
            QualificationColumn.CellStyle = centerCellStyle;
            QualificationColumn.HeaderStyle = centerHeaderStyle;

            DirectionColumn.Width = new DataGridLength(140);
            DirectionColumn.CellStyle = centerCellStyle;
            DirectionColumn.HeaderStyle = centerHeaderStyle;

            ApprovalDateColumn.Width = new DataGridLength(180);
            ApprovalDateColumn.CellStyle = centerCellStyle;
            ApprovalDateColumn.HeaderStyle = centerHeaderStyle;

            StatusColumn.Width = new DataGridLength(180);
            StatusColumn.CellStyle = centerCellStyle;
            StatusColumn.HeaderStyle = centerHeaderStyle;

        }



        private void UpdatePageVisibility(int totalSize, Button button, string Russian, string Tajik)
        {
            Page = 0;
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

            // Скрываем лишние страницы, если текущая страница уменьшилась
            for (int i = Page; i < allPages.Count; i++)
            {
                allPages[i].Visibility = Visibility.Hidden;
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

            DataGridColumn IDColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ID");
            DataGridColumn FIOColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "FIO");
            DataGridColumn EducationLevelColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "EducationLevel");
            DataGridColumn SpecialityColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Speciality");
            DataGridColumn QualificationColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Qualification");
            DataGridColumn DirectionColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Direction");
            DataGridColumn ApprovalDateColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ApprovalDate");
            DataGridColumn StatusColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Status");

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
                FIOColumn.Header = "ФИО";
                EducationLevelColumn.Header = "    Уровень \nобразования";
                SpecialityColumn.Header = "Специальность";
                QualificationColumn.Header = "Квалификация";
                DirectionColumn.Header = "Направление";
                ApprovalDateColumn.Header = "Дата одобрении";
                StatusColumn.Header = "Статус";
                button.Content = Russian + totalSize.ToString();
                
            }
            else
            {
                FIOColumn.Header = "Ном ва насаб, номи падарӣ";
                EducationLevelColumn.Header = "    Зинаи таҳсил";
                SpecialityColumn.Header = "Ихтисос";
                QualificationColumn.Header = "Тахассус (касб)";
                DirectionColumn.Header = "Самт";
                ApprovalDateColumn.Header = "Санаи тасдиқ";
                StatusColumn.Header = "Статус";
                button.Content = Tajik + totalSize.ToString();
            }


            IDColumn.Width = new DataGridLength(65);
            IDColumn.CellStyle = centerCellStyle;
            IDColumn.Header = "ID";
            IDColumn.HeaderStyle = centerHeaderStyle;

            FIOColumn.Width = new DataGridLength(280);
            FIOColumn.CellStyle = centerCellStyle;
            FIOColumn.HeaderStyle = centerHeaderStyle;

            EducationLevelColumn.Width = new DataGridLength(140);
            EducationLevelColumn.CellStyle = centerCellStyle;
            EducationLevelColumn.HeaderStyle = centerHeaderStyle;

            SpecialityColumn.Width = new DataGridLength(170);
            SpecialityColumn.CellStyle = centerCellStyle;
            SpecialityColumn.HeaderStyle = centerHeaderStyle;

            QualificationColumn.Width = new DataGridLength(180);
            QualificationColumn.CellStyle = centerCellStyle;
            QualificationColumn.HeaderStyle = centerHeaderStyle;

            DirectionColumn.Width = new DataGridLength(140);
            DirectionColumn.CellStyle = centerCellStyle;
            DirectionColumn.HeaderStyle = centerHeaderStyle;

            ApprovalDateColumn.Width = new DataGridLength(180);
            ApprovalDateColumn.CellStyle = centerCellStyle;
            ApprovalDateColumn.HeaderStyle = centerHeaderStyle;

            StatusColumn.Width = new DataGridLength(180);
            StatusColumn.CellStyle = centerCellStyle;
            StatusColumn.HeaderStyle = centerHeaderStyle;
            


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


        private async Task<DiplomResponseData> GetDiplomSearchAsync(string apiUrl, string login, string password, string Status, int page, string SearchFields)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&Status={Status}&Page={page}&SearchFields={SearchFields}";
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

        public static class StatusHelper
        {
            public static string GetStatusText(string status)
            {
                if (status.Equals("Accepted", StringComparison.OrdinalIgnoreCase))
                    return "Одобрено";
                else if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
                    return "Отклонено";
                else if (status.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
                    return "В ожидании";
                else if (status.Equals("ForFinishing", StringComparison.OrdinalIgnoreCase))
                    return "На доработку";
                else if (status.Equals("Printed", StringComparison.OrdinalIgnoreCase))
                    return "Распечатано";
                else if (status.Equals("Removed", StringComparison.OrdinalIgnoreCase))
                    return "Выдано";

                return status; // Вернуть оригинальное значение, если не удалось преобразовать
            }
        }


        private void AddDiplom_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            string RoleResponse = Application.Current.Properties["Roles"].ToString();
            if (RoleResponse == "Пользователь")
            {
                CreatDiplomForm creatDiplomForm = new CreatDiplomForm();

                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    creatDiplomForm.SendButton.Content = "Сохранить";
                }
                else
                {
                    creatDiplomForm.SendButton.Content = "Сабт";
                }
                creatDiplomForm.Grades.Visibility = Visibility.Hidden;
                creatDiplomForm.Show();
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
        private void SearchPopupButton_Click(object sender, RoutedEventArgs e)
        {
            SearchGridField.Visibility = Visibility.Visible;
        }
        private async void SearchButton_ClickAsync(object sender, RoutedEventArgs e)
        {
           
            int totalSize = 0;
            Page = 0;
            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), ButtonSatus.Text, 1, FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
                totalSize = DiplomList.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (DiplomDataGrid != null)
            {

                UpdatePageVisibilitySearch(totalSize);
            }

            SearchGridField.Visibility = Visibility.Hidden;
        }
        private void DiplomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string RoleResponse = Application.Current.Properties["Roles"].ToString();
            if (RoleResponse == "Подтверждающий" || RoleResponse == "Пользователь")
            {
                timer.Stop();

                Application.Current.Properties["SelectedDiplomId"] = ((DiplomList)DiplomDataGrid.SelectedItem).ID;
                ToApproveForm toApproveForm = new ToApproveForm();
                toApproveForm.Show();
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
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SearchGridField.Visibility = Visibility.Hidden;
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
        public int Page;
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
        private async void Page1_ClickAsync(object sender, RoutedEventArgs e)
        {

             try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), ButtonSatus.Text, Convert.ToInt32(Page1.Content), FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (DiplomDataGrid != null)
            {
                DataGridColumn IDColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ID");
                DataGridColumn FIOColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "FIO");
                DataGridColumn EducationLevelColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "EducationLevel");
                DataGridColumn SpecialityColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Speciality");
                DataGridColumn QualificationColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Qualification");
                DataGridColumn DirectionColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Direction");
                DataGridColumn ApprovalDateColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ApprovalDate");
                DataGridColumn StatusColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Status");



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

                IDColumn.Width = new DataGridLength(65);
                IDColumn.CellStyle = centerCellStyle;
                IDColumn.Header = "ID";
                IDColumn.HeaderStyle = centerHeaderStyle;


                FIOColumn.Width = new DataGridLength(280);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EducationLevelColumn.Width = new DataGridLength(140);
                EducationLevelColumn.CellStyle = centerCellStyle;
                EducationLevelColumn.Header = "    Уровень \nобразования";
                EducationLevelColumn.HeaderStyle = centerHeaderStyle;

                SpecialityColumn.Width = new DataGridLength(170);
                SpecialityColumn.CellStyle = centerCellStyle;
                SpecialityColumn.Header = "Специальность";
                SpecialityColumn.HeaderStyle = centerHeaderStyle;

                QualificationColumn.Width = new DataGridLength(180);
                QualificationColumn.CellStyle = centerCellStyle;
                QualificationColumn.Header = "Квалификация";
                QualificationColumn.HeaderStyle = centerHeaderStyle;

                DirectionColumn.Width = new DataGridLength(140);
                DirectionColumn.CellStyle = centerCellStyle;
                DirectionColumn.Header = "Направление";
                DirectionColumn.HeaderStyle = centerHeaderStyle;

                ApprovalDateColumn.Width = new DataGridLength(180);
                ApprovalDateColumn.CellStyle = centerCellStyle;
                ApprovalDateColumn.Header = "Дата одобрении";
                ApprovalDateColumn.HeaderStyle = centerHeaderStyle;

                StatusColumn.Width = new DataGridLength(180);
                StatusColumn.CellStyle = centerCellStyle;
                StatusColumn.Header = "Статус";
                StatusColumn.HeaderStyle = centerHeaderStyle;
            }
        }
        private async void Page2_ClickAsync(object sender, RoutedEventArgs e)
        {
           
            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), ButtonSatus.Text, Convert.ToInt32(Page2.Content), FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (DiplomDataGrid != null)
            {
                DataGridColumn IDColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ID");
                DataGridColumn FIOColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "FIO");
                if (FIOColumn is DataGridTextColumn fioColumn)
                {
                    fioColumn.Binding = new Binding("FIO")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn EducationLevelColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "EducationLevel");
                if (EducationLevelColumn is DataGridTextColumn educationLevelColumn)
                {
                    educationLevelColumn.Binding = new Binding("EducationLevel")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn SpecialityColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Speciality");
                if (SpecialityColumn is DataGridTextColumn specialityColumn)
                {
                    specialityColumn.Binding = new Binding("Speciality")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn QualificationColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Qualification");
                if (QualificationColumn is DataGridTextColumn qualificationColumn)
                {
                    qualificationColumn.Binding = new Binding("Qualification")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn DirectionColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Direction");
                if (DirectionColumn is DataGridTextColumn directionColumn)
                {
                    directionColumn.Binding = new Binding("Direction")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn ApprovalDateColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ApprovalDate");
                if (ApprovalDateColumn is DataGridTextColumn approvalDateColumn)
                {
                    approvalDateColumn.Binding = new Binding("ApprovalDate")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn StatusColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Status");
                if (StatusColumn is DataGridTextColumn statusColumn)
                {
                    statusColumn.Binding = new Binding("Status")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }


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

                IDColumn.Width = new DataGridLength(65);
                IDColumn.CellStyle = centerCellStyle;
                IDColumn.Header = "ID";
                IDColumn.HeaderStyle = centerHeaderStyle;


                FIOColumn.Width = new DataGridLength(280);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EducationLevelColumn.Width = new DataGridLength(140);
                EducationLevelColumn.CellStyle = centerCellStyle;
                EducationLevelColumn.Header = "    Уровень \nобразования";
                EducationLevelColumn.HeaderStyle = centerHeaderStyle;

                SpecialityColumn.Width = new DataGridLength(170);
                SpecialityColumn.CellStyle = centerCellStyle;
                SpecialityColumn.Header = "Специальность";
                SpecialityColumn.HeaderStyle = centerHeaderStyle;

                QualificationColumn.Width = new DataGridLength(180);
                QualificationColumn.CellStyle = centerCellStyle;
                QualificationColumn.Header = "Квалификация";
                QualificationColumn.HeaderStyle = centerHeaderStyle;

                DirectionColumn.Width = new DataGridLength(140);
                DirectionColumn.CellStyle = centerCellStyle;
                DirectionColumn.Header = "Направление";
                DirectionColumn.HeaderStyle = centerHeaderStyle;

                ApprovalDateColumn.Width = new DataGridLength(180);
                ApprovalDateColumn.CellStyle = centerCellStyle;
                ApprovalDateColumn.Header = "Дата одобрении";
                ApprovalDateColumn.HeaderStyle = centerHeaderStyle;

                StatusColumn.Width = new DataGridLength(180);
                StatusColumn.CellStyle = centerCellStyle;
                StatusColumn.Header = "Статус";
                StatusColumn.HeaderStyle = centerHeaderStyle;
            }
        }
        private async void Page3_ClickAsync(object sender, RoutedEventArgs e)
        {
            
            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), ButtonSatus.Text, Convert.ToInt32(Page3.Content), FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (DiplomDataGrid != null)
            {
                DataGridColumn IDColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ID");
                DataGridColumn FIOColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "FIO");
                if (FIOColumn is DataGridTextColumn fioColumn)
                {
                    fioColumn.Binding = new Binding("FIO")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn EducationLevelColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "EducationLevel");
                if (EducationLevelColumn is DataGridTextColumn educationLevelColumn)
                {
                    educationLevelColumn.Binding = new Binding("EducationLevel")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn SpecialityColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Speciality");
                if (SpecialityColumn is DataGridTextColumn specialityColumn)
                {
                    specialityColumn.Binding = new Binding("Speciality")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn QualificationColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Qualification");
                if (QualificationColumn is DataGridTextColumn qualificationColumn)
                {
                    qualificationColumn.Binding = new Binding("Qualification")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn DirectionColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Direction");
                if (DirectionColumn is DataGridTextColumn directionColumn)
                {
                    directionColumn.Binding = new Binding("Direction")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn ApprovalDateColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ApprovalDate");
                if (ApprovalDateColumn is DataGridTextColumn approvalDateColumn)
                {
                    approvalDateColumn.Binding = new Binding("ApprovalDate")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn StatusColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Status");
                if (StatusColumn is DataGridTextColumn statusColumn)
                {
                    statusColumn.Binding = new Binding("Status")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }


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

                IDColumn.Width = new DataGridLength(65);
                IDColumn.CellStyle = centerCellStyle;
                IDColumn.Header = "ID";
                IDColumn.HeaderStyle = centerHeaderStyle;


                FIOColumn.Width = new DataGridLength(280);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EducationLevelColumn.Width = new DataGridLength(140);
                EducationLevelColumn.CellStyle = centerCellStyle;
                EducationLevelColumn.Header = "    Уровень \nобразования";
                EducationLevelColumn.HeaderStyle = centerHeaderStyle;

                SpecialityColumn.Width = new DataGridLength(170);
                SpecialityColumn.CellStyle = centerCellStyle;
                SpecialityColumn.Header = "Специальность";
                SpecialityColumn.HeaderStyle = centerHeaderStyle;

                QualificationColumn.Width = new DataGridLength(180);
                QualificationColumn.CellStyle = centerCellStyle;
                QualificationColumn.Header = "Квалификация";
                QualificationColumn.HeaderStyle = centerHeaderStyle;

                DirectionColumn.Width = new DataGridLength(140);
                DirectionColumn.CellStyle = centerCellStyle;
                DirectionColumn.Header = "Направление";
                DirectionColumn.HeaderStyle = centerHeaderStyle;

                ApprovalDateColumn.Width = new DataGridLength(180);
                ApprovalDateColumn.CellStyle = centerCellStyle;
                ApprovalDateColumn.Header = "Дата одобрении";
                ApprovalDateColumn.HeaderStyle = centerHeaderStyle;

                StatusColumn.Width = new DataGridLength(180);
                StatusColumn.CellStyle = centerCellStyle;
                StatusColumn.Header = "Статус";
                StatusColumn.HeaderStyle = centerHeaderStyle;
            }
        }
        private async void Page4_ClickAsync(object sender, RoutedEventArgs e)
        {
            
            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), ButtonSatus.Text, Convert.ToInt32(Page4.Content), FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (DiplomDataGrid != null)
            {
                DataGridColumn IDColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ID");
                DataGridColumn FIOColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "FIO");
                if (FIOColumn is DataGridTextColumn fioColumn)
                {
                    fioColumn.Binding = new Binding("FIO")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn EducationLevelColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "EducationLevel");
                if (EducationLevelColumn is DataGridTextColumn educationLevelColumn)
                {
                    educationLevelColumn.Binding = new Binding("EducationLevel")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn SpecialityColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Speciality");
                if (SpecialityColumn is DataGridTextColumn specialityColumn)
                {
                    specialityColumn.Binding = new Binding("Speciality")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn QualificationColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Qualification");
                if (QualificationColumn is DataGridTextColumn qualificationColumn)
                {
                    qualificationColumn.Binding = new Binding("Qualification")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn DirectionColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Direction");
                if (DirectionColumn is DataGridTextColumn directionColumn)
                {
                    directionColumn.Binding = new Binding("Direction")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn ApprovalDateColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ApprovalDate");
                if (ApprovalDateColumn is DataGridTextColumn approvalDateColumn)
                {
                    approvalDateColumn.Binding = new Binding("ApprovalDate")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn StatusColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Status");
                if (StatusColumn is DataGridTextColumn statusColumn)
                {
                    statusColumn.Binding = new Binding("Status")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }


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

                IDColumn.Width = new DataGridLength(65);
                IDColumn.CellStyle = centerCellStyle;
                IDColumn.Header = "ID";
                IDColumn.HeaderStyle = centerHeaderStyle;


                FIOColumn.Width = new DataGridLength(280);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EducationLevelColumn.Width = new DataGridLength(140);
                EducationLevelColumn.CellStyle = centerCellStyle;
                EducationLevelColumn.Header = "    Уровень \nобразования";
                EducationLevelColumn.HeaderStyle = centerHeaderStyle;

                SpecialityColumn.Width = new DataGridLength(170);
                SpecialityColumn.CellStyle = centerCellStyle;
                SpecialityColumn.Header = "Специальность";
                SpecialityColumn.HeaderStyle = centerHeaderStyle;

                QualificationColumn.Width = new DataGridLength(180);
                QualificationColumn.CellStyle = centerCellStyle;
                QualificationColumn.Header = "Квалификация";
                QualificationColumn.HeaderStyle = centerHeaderStyle;

                DirectionColumn.Width = new DataGridLength(140);
                DirectionColumn.CellStyle = centerCellStyle;
                DirectionColumn.Header = "Направление";
                DirectionColumn.HeaderStyle = centerHeaderStyle;

                ApprovalDateColumn.Width = new DataGridLength(180);
                ApprovalDateColumn.CellStyle = centerCellStyle;
                ApprovalDateColumn.Header = "Дата одобрении";
                ApprovalDateColumn.HeaderStyle = centerHeaderStyle;

                StatusColumn.Width = new DataGridLength(180);
                StatusColumn.CellStyle = centerCellStyle;
                StatusColumn.Header = "Статус";
                StatusColumn.HeaderStyle = centerHeaderStyle;
            }
        }
        private async void Page5_ClickAsync(object sender, RoutedEventArgs e)
        {         
            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), ButtonSatus.Text, Convert.ToInt32(Page5.Content), FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (DiplomDataGrid != null)
            {
                DataGridColumn IDColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ID");
                DataGridColumn FIOColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "FIO");
                if (FIOColumn is DataGridTextColumn fioColumn)
                {
                    fioColumn.Binding = new Binding("FIO")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn EducationLevelColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "EducationLevel");
                if (EducationLevelColumn is DataGridTextColumn educationLevelColumn)
                {
                    educationLevelColumn.Binding = new Binding("EducationLevel")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn SpecialityColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Speciality");
                if (SpecialityColumn is DataGridTextColumn specialityColumn)
                {
                    specialityColumn.Binding = new Binding("Speciality")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn QualificationColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Qualification");
                if (QualificationColumn is DataGridTextColumn qualificationColumn)
                {
                    qualificationColumn.Binding = new Binding("Qualification")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn DirectionColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Direction");
                if (DirectionColumn is DataGridTextColumn directionColumn)
                {
                    directionColumn.Binding = new Binding("Direction")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn ApprovalDateColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "ApprovalDate");
                if (ApprovalDateColumn is DataGridTextColumn approvalDateColumn)
                {
                    approvalDateColumn.Binding = new Binding("ApprovalDate")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }
                DataGridColumn StatusColumn = DiplomDataGrid.Columns.Single(c => c.Header.ToString() == "Status");
                if (StatusColumn is DataGridTextColumn statusColumn)
                {
                    statusColumn.Binding = new Binding("Status")
                    {
                        StringFormat = "{0}".Trim() // Это удалит пробелы перед и после текста
                    };
                }


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

                IDColumn.Width = new DataGridLength(65);
                IDColumn.CellStyle = centerCellStyle;
                IDColumn.Header = "ID";
                IDColumn.HeaderStyle = centerHeaderStyle;


                FIOColumn.Width = new DataGridLength(280);
                FIOColumn.CellStyle = centerCellStyle;
                FIOColumn.Header = "ФИО";
                FIOColumn.HeaderStyle = centerHeaderStyle;

                EducationLevelColumn.Width = new DataGridLength(140);
                EducationLevelColumn.CellStyle = centerCellStyle;
                EducationLevelColumn.Header = "    Уровень \nобразования";
                EducationLevelColumn.HeaderStyle = centerHeaderStyle;

                SpecialityColumn.Width = new DataGridLength(170);
                SpecialityColumn.CellStyle = centerCellStyle;
                SpecialityColumn.Header = "Специальность";
                SpecialityColumn.HeaderStyle = centerHeaderStyle;

                QualificationColumn.Width = new DataGridLength(180);
                QualificationColumn.CellStyle = centerCellStyle;
                QualificationColumn.Header = "Квалификация";
                QualificationColumn.HeaderStyle = centerHeaderStyle;

                DirectionColumn.Width = new DataGridLength(140);
                DirectionColumn.CellStyle = centerCellStyle;
                DirectionColumn.Header = "Направление";
                DirectionColumn.HeaderStyle = centerHeaderStyle;

                ApprovalDateColumn.Width = new DataGridLength(180);
                ApprovalDateColumn.CellStyle = centerCellStyle;
                ApprovalDateColumn.Header = "Дата одобрении";
                ApprovalDateColumn.HeaderStyle = centerHeaderStyle;

                StatusColumn.Width = new DataGridLength(180);
                StatusColumn.CellStyle = centerCellStyle;
                StatusColumn.Header = "Статус";
                StatusColumn.HeaderStyle = centerHeaderStyle;
            }
        }
        private async void SearchBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            
            int totalSize = 0;
            Page = 0;
            try
            {
                DiplomResponseData DiplomList = await GetDiplomSearchAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/SearchDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), ButtonSatus.Text, 1, SearchBox.Text );
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
                totalSize = DiplomList.TotalCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (DiplomDataGrid != null)
            {
                UpdatePageVisibilitySearch(totalSize);
            }
        }

        private void TajikButton_Click(object sender, RoutedEventArgs e)
        {
   
        }



        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DesktopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void GivenDiplomButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["statusFieldsResponse"] = "Removed";
            ButtonSatus.Text = "Removed";

            int totalSize = 0;
            AcceptedButton.Width = 260;
            AcceptedButton.Height = 80;
            ImageBrush AcceptButton = new ImageBrush();
            AcceptButton.ImageSource = (ImageSource)Resources["AcceptButtonShow"];
            AcceptedButton.Background = AcceptButton;
            ImageBrush TodayButtonShow = new ImageBrush();
            TodayButtonShow.ImageSource = (ImageSource)Resources["TodayButtonShow"];
            TodayButton.Background = TodayButtonShow;
            TodayButton.Width = 260;
            TodayButton.Height = 80;
            ImageBrush PrintButtonShow = new ImageBrush();
            PrintButtonShow.ImageSource = (ImageSource)Resources["PrintButtonShow"];
            PrintedButton.Background = PrintButtonShow;
            PrintedButton.Width = 260;
            PrintedButton.Height = 80;
            ImageBrush RedirectButtonShow = new ImageBrush();
            RedirectButtonShow.ImageSource = (ImageSource)Resources["RedirectButtonShow"];
            RedirectButton.Background = RedirectButtonShow;
            RedirectButton.Width = 260;
            RedirectButton.Height = 80;
            ImageBrush RejectButtonShow = new ImageBrush();
            RejectButtonShow.ImageSource = (ImageSource)Resources["RejectButtonShow"];
            RejectButton.Background = RejectButtonShow;
            RejectButton.Width = 260;
            RejectButton.Height = 80;

            TodayButton.Margin = new Thickness(495, 255, 0, 0);
            AcceptedButton.Margin = new Thickness(765, 255, 0, 0);
            PrintedButton.Margin = new Thickness(1035, 255, 0, 0);
            RedirectButton.Margin = new Thickness(1305, 255, 0, 0);
            RejectButton.Margin = new Thickness(1575, 255, 0, 0);

            FioStudentSearch.Text = "";
            EducationLevelSearch.Text = "";
            SpecialitySearch.Text = "";
            DirectionSearch.Text = "";
            startDateSearch.Text = "";
            endDateSearch.Text = "";

            try
            {
                DiplomResponseData DiplomList = await GetDiplomDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllDiplom", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), "Removed", 1, FioStudentSearch.Text, EducationLevelSearch.Text, SpecialitySearch.Text, DirectionSearch.Text, startDateSearch.Text, endDateSearch.Text);

                // People = PersonList;
                DiplomDataGrid.ItemsSource = DiplomList.diplomList;
                totalSize = DiplomList.TotalCount;
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    TodayButton.Content = "   В ожидании\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Одобрено\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Распечатано\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     На дороботку\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Отклонено\n           " + DiplomList.TotalRejected;
                }
                else
                {
                    TodayButton.Content = "   Дар интизорӣ\n           " + DiplomList.TotalInProgress;
                    AcceptedButton.Content = "     Тасдиқ шуд\n           " + DiplomList.TotalAccepted;
                    PrintedButton.Content = "     Чоп шуд\n           " + DiplomList.TotalPrinted;
                    RedirectButton.Content = "     Барои коркард\n           " + DiplomList.TotalForFinishing;
                    RejectButton.Content = "        Инкор шуд\n           " + DiplomList.TotalRejected;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (DiplomDataGrid != null)
            {
                UpdatePageVisibility(totalSize, GivenDiplomButton, "     Выданные дипломы\n           ", "     Дипломҳои додашуда\n           ");
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

        private void OldDiplom_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            string RoleResponse = Application.Current.Properties["Roles"].ToString();
            if (RoleResponse == "Пользователь")
            {
                CreatDiplomForm creatDiplomForm = new CreatDiplomForm();

                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    creatDiplomForm.SendButton.Content = "Сохранить";
                }
                else
                {
                    creatDiplomForm.SendButton.Content = "Сабт";
                }
                creatDiplomForm.Grades.Visibility = Visibility.Visible;
                creatDiplomForm.Show();
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
}
