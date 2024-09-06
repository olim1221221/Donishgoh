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
    /// Логика взаимодействия для SettingsForm.xaml
    /// </summary>
    public partial class SettingsForm : Window
    {
        public SettingsForm()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            this.Width = screenWidth;
            this.Height = screenHeight;
            Loaded += Window_LoadedAsync;
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
                FirstLabel.Content = "Настройки";
                QuitButton.Content = "          Выход";
                AadditionalInformation.Content = "    Данные по специальности для диплома";
                ProfLesson.Content = "   Дополнительные данные";
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
                FirstLabel.Content = "Танзимотҳо";
                QuitButton.Content = "          Баромадгоҳ";
                AadditionalInformation.Content = "    Маълумот дар бораи ихтисос барои диплом";
                ProfLesson.Content = "   Маълумоти иловагӣ";
            }
            AddOtherSettingResponse OtherSettingsList = null;
            try
            {
                OtherSettingsList = await GetOtherSettingsDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetOtherSettingsById", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (OtherSettingsList != null)
            {
                OpportunityTrainingRuBox.Text = OtherSettingsList.OpportunityTrainingRu;
                OpportunityTrainingTjBox.Text = OtherSettingsList.OpportunityTrainingTj;
                InformeducsysruBox.Text = OtherSettingsList.InformEducSysRu;
                InformeducsystjBox.Text = OtherSettingsList.InformEducSysTj;
                ProfessionalStatusRuBox.Text = OtherSettingsList.ProfeccionalStatusRu;
                ProfessionalStatusTjBox.Text = OtherSettingsList.ProfeccionalStatusTj;
                OtherinformasionBox.Text = OtherSettingsList.AdditionalInformation;
                RektorBox.Text = OtherSettingsList.Rektor;
            }

        }
        private async Task<AddOtherSettingResponse> GetOtherSettingsDataAsync(string apiUrl, string login, string password)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    AddOtherSettingResponse getOtherSettings = JsonConvert.DeserializeObject<AddOtherSettingResponse>(json);
                    return getOtherSettings;
                }
                else
                {
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
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
        private void AadditionalInformation_MouseEnter(object sender, MouseEventArgs e)
        {
            AadditionalInformation.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }
        private void AadditionalInformation_MouseLeave(object sender, MouseEventArgs e)
        {
            AadditionalInformation.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }
        private void ProfLesson_MouseEnter(object sender, MouseEventArgs e)
        {
            ProfLesson.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }
        private void ProfLesson_MouseLeave(object sender, MouseEventArgs e)
        {
            ProfLesson.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }
        private async void AadditionalInformation_ClickAsync(object sender, RoutedEventArgs e)
        {
            AddSettingsDataGrid.Visibility = Visibility.Visible;
            SpecialityGrid.Visibility = Visibility.Visible;
            OtherSettingsGrid.Visibility = Visibility.Hidden;
            DescEducProgBoxRu.Visibility = Visibility.Hidden;
            DescEducProgBoxTj.Visibility = Visibility.Hidden;
            AddFakultyGrid.Visibility = Visibility.Hidden;
            // AddFakultyDataGrid.Visibility = Visibility.Hidden;
            try
            {
                List<AddSetting> addSettingsList = await GetSettingsAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetSettings", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString());
                AddSettingsDataGrid.ItemsSource = addSettingsList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (AddSettingsDataGrid != null)
            {
                DataGridColumn SpecialityRuColumn = AddSettingsDataGrid.Columns.Single(c => c.Header.ToString() == "SpecialityRu");
                DataGridColumn DirectionRuColumn = AddSettingsDataGrid.Columns.Single(c => c.Header.ToString() == "DirectionRu");
                DataGridColumn QualificationRuColumn = AddSettingsDataGrid.Columns.Single(c => c.Header.ToString() == "QualificationRu");
                DataGridColumn LevelofEducationRuColumn = AddSettingsDataGrid.Columns.Single(c => c.Header.ToString() == "LevelofEducationRu");
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
                    SpecialityRuColumn.Header = "Специальность";
                    DirectionRuColumn.Header = "Направление";
                    QualificationRuColumn.Header = "Квалификация";
                    LevelofEducationRuColumn.Header = "    Уровень \n образование";
                }
                else
                {
                    SpecialityRuColumn.Header = "Ихтисос";
                    DirectionRuColumn.Header = "Самт";
                    QualificationRuColumn.Header = "Тахассус (касб)";
                    LevelofEducationRuColumn.Header = "Зинаи таҳсил";
                }
                SpecialityRuColumn.Width = new DataGridLength(230);
                SpecialityRuColumn.CellStyle = centerCellStyle;              
                SpecialityRuColumn.HeaderStyle = centerHeaderStyle;
                DirectionRuColumn.Width = new DataGridLength(240);
                DirectionRuColumn.CellStyle = centerCellStyle;               
                DirectionRuColumn.HeaderStyle = centerHeaderStyle;
                QualificationRuColumn.Width = new DataGridLength(180);
                QualificationRuColumn.CellStyle = centerCellStyle;
                QualificationRuColumn.HeaderStyle = centerHeaderStyle;
                LevelofEducationRuColumn.Width = new DataGridLength(180);
                LevelofEducationRuColumn.CellStyle = centerCellStyle;
                LevelofEducationRuColumn.HeaderStyle = centerHeaderStyle;
            }


            try
            {
                var PersonList = await GetFakultyAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetFakulty", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString());

                FakultyRusComboBox.ItemsSource = PersonList.Select(item => item.FakultyRu).ToList();

                // Предполагаем, что FakultyTjComboBox - это комбо-бокс, предназначенный для FakultyTj
                FakultyTjComboBox.ItemsSource = PersonList.Select(item => item.FakultyTj).ToList();
            }


            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }



        }



   
        private async Task<List<AddSetting>> GetSettingsAsync(string apiUrl, string login, string password)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<AddSetting> diplomResponseData = JsonConvert.DeserializeObject<List<AddSetting>>(json);
                    return diplomResponseData;
                }
                else
                {
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }

       
        private async Task<List<AddFakultyResponse>> GetFakultyAsync(string apiUrl, string login, string password)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<AddFakultyResponse> fakultyResponseData = JsonConvert.DeserializeObject<List<AddFakultyResponse>>(json);
                    return fakultyResponseData;
                }
                else
                {
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }

        private static readonly HttpClient client = new HttpClient();
        public static async Task<bool> AddSettingsAsync(AddSettingResponse addSetting, string loginHeaderValue, string passwordHeaderValue)
        {
            string UrlResp = Application.Current.Properties["UrlServer"].ToString();
            try
            {
                string apiUrl = $"{UrlResp}/api/Authorize/AddSettings?Login={loginHeaderValue}&Password={passwordHeaderValue}";
                string jsonData = JsonConvert.SerializeObject(addSetting);
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
        public static async Task<bool> AddOtherSettingsAsync(AddOtherSettingResponse addotherSetting, string loginHeaderValue, string passwordHeaderValue)
        {
            string UrlResp = Application.Current.Properties["UrlServer"].ToString();
            try
            {
                string apiUrl = $"{UrlResp}/api/Authorize/AddOtherSettings?Login={loginHeaderValue}&Password={passwordHeaderValue}";
                string jsonData = JsonConvert.SerializeObject(addotherSetting);
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
        private void AddFields_Click(object sender, RoutedEventArgs e)
        {
            AddSettingsDataGrid.Visibility = Visibility.Hidden;
            CencelFields.Visibility = Visibility.Visible;
            ChangeFileds.Visibility = Visibility.Visible;
            AddFields.Visibility = Visibility.Hidden;
            DescEducProgBoxRu.Visibility = Visibility.Visible;
            DescEducProgBoxTj.Visibility = Visibility.Visible;
            ChangeFileds.Content = "    Сохранить";
            SpecialityBoxRu.Text = "";
            SpecialityBoxTj.Text = "";
            DescEducProgBoxRu.Text = "";
            DescEducProgBoxTj.Text = "";
            DirectionBoxRu.Text = "";
            DirectionBoxTj.Text = "";
            QualificationBoxRu.Text = "";
            QualificationBoxTj.Text = "";
            LevelofEducationBoxRu.Text = "";
            LevelofEducationBoxRu1.Text = "";
            FakultyBoxRu.Text = "";
            FakultyBoxTj.Text = "";
            StudyPeriodBoxRu.Text = "";
            StudyPeriodBoxTj.Text = "";
            TypeEducationBoxRu.Text = "";
            TypeEducationBoxTj.Text = "";
            DekanBox.Text = "";
            SecretarBox.Text = "";
            SpecialityBoxRu.IsEnabled = true;
            SpecialityBoxTj.IsEnabled = true;
            DescEducProgBoxRu.IsEnabled = true;
            DescEducProgBoxTj.IsEnabled = true;
            DirectionBoxRu.IsEnabled = true;
            DirectionBoxTj.IsEnabled = true;
            QualificationBoxRu.IsEnabled = true;
            QualificationBoxTj.IsEnabled = true;
            LevelofEducationBoxRu.IsEnabled = true;
            LevelofEducationBoxRu1.IsEnabled = true;
            FakultyBoxRu.IsEnabled = true;
            FakultyBoxTj.IsEnabled = true;
            StudyPeriodBoxRu.IsEnabled = true;
            StudyPeriodBoxTj.IsEnabled = true;
            DekanBox.IsEnabled = true;
            SecretarBox.IsEnabled = true;
            StudyPeriodBoxRu.IsEnabled = true;
            StudyPeriodBoxTj.IsEnabled = true;
            FakultyTjComboBox.IsEnabled = true;
            FakultyRusComboBox.IsEnabled = true;
        }

        private void CencelFields_Click(object sender, RoutedEventArgs e)
        {
            AddSettingsDataGrid.Visibility = Visibility.Visible;
            CencelFields.Visibility = Visibility.Hidden;
            ChangeFileds.Visibility = Visibility.Hidden;
            AddFields.Visibility = Visibility.Visible;
            ErrorLebel.Visibility = Visibility.Hidden;
            DescEducProgBoxRu.Visibility = Visibility.Hidden;
            DescEducProgBoxTj.Visibility = Visibility.Hidden;

        }
        private async Task<AddSettingResponse> GetSettingsDDataAsync(string apiUrl, string login, string password, string Speciality)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&Speciality={Speciality}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {

                    string json = await response.Content.ReadAsStringAsync();
                    AddSettingResponse authorize = JsonConvert.DeserializeObject<AddSettingResponse>(json);
                    return authorize;
                }
                else
                {
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }


        private async void AddSettingsDataGrid_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            ChangeFileds.Content = "    Изменить";
            AddSettingsDataGrid.Visibility = Visibility.Hidden;
            CencelFields.Visibility = Visibility.Visible;
            ChangeFileds.Visibility = Visibility.Visible;
            AddFields.Visibility = Visibility.Hidden;
            DescEducProgBoxRu.Visibility = Visibility.Visible;
            DescEducProgBoxTj.Visibility = Visibility.Visible;
            AddSettingResponse SettingsList = null;
            try
            {
                SettingsList = await GetSettingsDDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetSettingsBySpeciality", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), ((AddSetting)AddSettingsDataGrid.SelectedItem).SpecialityRu);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (SettingsList != null)
            {
                SpecialityBoxRu.Text = SettingsList.SpecialityRu;
                SpecialityBoxTj.Text = SettingsList.SpecialityTj;
                DescEducProgBoxRu.Text = SettingsList.DescEducProgRu;
                DescEducProgBoxTj.Text = SettingsList.DescEducProgTj;
                DirectionBoxRu.Text = SettingsList.DirectionRu;
                DirectionBoxTj.Text = SettingsList.DirectionTj;
                QualificationBoxRu.Text = SettingsList.QualificationRu;
                QualificationBoxTj.Text = SettingsList.QualificationTj;
                LevelofEducationBoxRu.Text = SettingsList.LevelofEducationRu;
                LevelofEducationBoxRu1.Text = SettingsList.LevelofEducationTj;
                FakultyBoxRu.Text = SettingsList.FakultyRu;
                FakultyBoxTj.Text = SettingsList.FakultyTj;
                StudyPeriodBoxRu.Text = SettingsList.StudyPeriodRu;
                StudyPeriodBoxTj.Text = SettingsList.StudyPeriodTj;
                TypeEducationBoxRu.Text = SettingsList.TypeEducationRu;
                TypeEducationBoxTj.Text = SettingsList.TypeEducationTj;         
                DekanBox.Text = SettingsList.Dekan;
                SecretarBox.Text = SettingsList.Secretar;
            }
            else
            {
                MessageBox.Show("Список пуст.");
            }
            SpecialityBoxRu.IsEnabled = false;
            SpecialityBoxTj.IsEnabled = false;
            DescEducProgBoxRu.IsEnabled = false;
            DescEducProgBoxTj.IsEnabled = false;
            DirectionBoxRu.IsEnabled = false;
            DirectionBoxTj.IsEnabled = false;
            QualificationBoxRu.IsEnabled = false;
            QualificationBoxTj.IsEnabled = false;
            LevelofEducationBoxRu.IsEnabled = false;
            LevelofEducationBoxRu1.IsEnabled = false;
            FakultyBoxRu.IsEnabled = false;
            FakultyBoxTj.IsEnabled = false;
            StudyPeriodBoxRu.IsEnabled = false;
            StudyPeriodBoxTj.IsEnabled = false;
            DekanBox.IsEnabled = false;
            SecretarBox.IsEnabled = false;
            TypeEducationBoxRu.IsEnabled = false;
            TypeEducationBoxTj.IsEnabled = false;
            FakultyRusComboBox.IsEnabled = false;
            FakultyTjComboBox.IsEnabled = false;
        }
        private async void ChangeFileds_ClickAsync(object sender, RoutedEventArgs e)
        {

            if (SpecialityBoxRu.Text == ""
                & SpecialityBoxTj.Text == ""
                & DescEducProgBoxRu.Text == ""
                & DescEducProgBoxTj.Text == ""
                & DirectionBoxRu.Text == ""
                & DirectionBoxTj.Text == ""
                & QualificationBoxRu.Text == ""
                & QualificationBoxTj.Text == ""
                & LevelofEducationBoxRu.Text == ""
                & LevelofEducationBoxRu1.Text == ""
                & FakultyBoxRu.Text == ""
                & FakultyBoxTj.Text == ""
                & StudyPeriodBoxRu.Text == ""
                & StudyPeriodBoxTj.Text == ""
                & TypeEducationBoxRu.Text == ""
                & TypeEducationBoxTj.Text == ""
                & DekanBox.Text == ""
                & SecretarBox.Text == ""
                )
            {
                ErrorLebel.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorLebel.Visibility = Visibility.Hidden;
                if (ChangeFileds.Content.ToString() == "    Сохранить")
                {
                    var addSetting = new AddSettingResponse
                    {
                        SpecialityRu = SpecialityBoxRu.Text,
                        SpecialityTj = SpecialityBoxTj.Text,
                        DescEducProgRu = DescEducProgBoxRu.Text,
                        DescEducProgTj = DescEducProgBoxTj.Text,
                        DirectionRu = DirectionBoxRu.Text,
                        DirectionTj = DirectionBoxTj.Text,
                        QualificationRu = QualificationBoxRu.Text,
                        QualificationTj = QualificationBoxTj.Text,
                        LevelofEducationRu = LevelofEducationBoxRu.Text,
                        LevelofEducationTj = LevelofEducationBoxRu1.Text,
                        FakultyRu = FakultyBoxRu.Text,
                        FakultyTj = FakultyBoxTj.Text,
                        StudyPeriodRu = StudyPeriodBoxRu.Text,
                        StudyPeriodTj = StudyPeriodBoxTj.Text,
                        TypeEducationRu = TypeEducationBoxRu.Text,
                        TypeEducationTj = TypeEducationBoxTj.Text,
                        Dekan = DekanBox.Text,
                        Secretar = SecretarBox.Text
                    };
                    string loginHeaderValue = (string)Application.Current.Properties["Login"];
                    string passwordHeaderValue = (string)Application.Current.Properties["Password"];
                    bool isSuccess = await AddSettingsAsync(addSetting, loginHeaderValue, passwordHeaderValue);
                    if (isSuccess)
                    {
                        AddSettingsDataGrid.Visibility = Visibility.Visible;
                        CencelFields.Visibility = Visibility.Hidden;
                        ChangeFileds.Visibility = Visibility.Hidden;
                        AddFields.Visibility = Visibility.Visible;
                        DescEducProgBoxRu.Visibility = Visibility.Hidden;
                        DescEducProgBoxTj.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка при добавлении пользователя.");
                    }
                }
                else if (ChangeFileds.Content.ToString() == "    Изменить")
                {
                    SpecialityBoxRu.IsEnabled = true;
                    SpecialityBoxTj.IsEnabled = true;
                    DescEducProgBoxRu.IsEnabled = true;
                    DescEducProgBoxTj.IsEnabled = true;
                    DirectionBoxRu.IsEnabled = true;
                    DirectionBoxTj.IsEnabled = true;
                    QualificationBoxRu.IsEnabled = true;
                    QualificationBoxTj.IsEnabled = true;
                    LevelofEducationBoxRu.IsEnabled = true;
                    LevelofEducationBoxRu1.IsEnabled = true;
                    FakultyBoxRu.IsEnabled = true;
                    FakultyBoxTj.IsEnabled = true;
                    StudyPeriodBoxRu.IsEnabled = true;
                    StudyPeriodBoxTj.IsEnabled = true;
                    TypeEducationBoxRu.IsEnabled = true;
                    TypeEducationBoxTj.IsEnabled = true;
                    DekanBox.IsEnabled = true;
                    SecretarBox.IsEnabled = true;
                    FakultyRusComboBox.IsEnabled = true;
                    FakultyTjComboBox.IsEnabled = true;
                    ChangeFileds.Content = "    Сохранить";
                }
            }
        }
        private void ProfLesson_Click(object sender, RoutedEventArgs e)
        {
            OtherSettingsGrid.Visibility = Visibility.Visible;
            SpecialityGrid.Visibility = Visibility.Hidden;
            AddFakultyGrid.Visibility = Visibility.Hidden;
        }
        private async void ChangeFileds1_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (ChangeFileds1.Content.ToString() == "         Изменить данные")
            {
                OpportunityTrainingRuBox.IsEnabled = true;
                OpportunityTrainingTjBox.IsEnabled = true;
                ProfessionalStatusRuBox.IsEnabled = true;
                ProfessionalStatusTjBox.IsEnabled = true;
                OtherinformasionBox.IsEnabled = true;
                InformeducsysruBox.IsEnabled = true;
                InformeducsystjBox.IsEnabled = true;
                RektorBox.IsEnabled = true;
                ChangeFileds1.Content = "         Сохранить";
                ErrorOtherSettingsLebel.Visibility = Visibility.Hidden;
                
            }
            else if (ChangeFileds1.Content.ToString() == "         Сохранить")
            {
                var AddOtherSettingsData = new AddOtherSettingResponse
                {
                    OpportunityTrainingRu = OpportunityTrainingRuBox.Text,
                    OpportunityTrainingTj = OpportunityTrainingTjBox.Text,
                    ProfeccionalStatusRu = ProfessionalStatusRuBox.Text,
                    ProfeccionalStatusTj = ProfessionalStatusTjBox.Text,
                    AdditionalInformation = OtherinformasionBox.Text,
                    InformEducSysRu = InformeducsysruBox.Text,
                    InformEducSysTj = InformeducsystjBox.Text,
                    Rektor = RektorBox.Text
                };
                string loginHeaderValue = (string)Application.Current.Properties["Login"];
                string passwordHeaderValue = (string)Application.Current.Properties["Password"];
                bool isSuccess = await AddOtherSettingsAsync(AddOtherSettingsData, loginHeaderValue, passwordHeaderValue);
                if (isSuccess)
                {
                    ErrorOtherSettingsLebel.Visibility = Visibility.Visible;
                    ChangeFileds1.Content = "         Изменить данные";
                    OpportunityTrainingRuBox.IsEnabled = false;
                    OpportunityTrainingTjBox.IsEnabled = false;
                    ProfessionalStatusRuBox.IsEnabled = false;
                    ProfessionalStatusTjBox.IsEnabled = false;
                    OtherinformasionBox.IsEnabled = false;
                    InformeducsysruBox.IsEnabled = false;
                    InformeducsystjBox.IsEnabled = false;
                    RektorBox.IsEnabled = false;
                }
                else
                {
                    Console.WriteLine("Ошибка при добавлении пользователя.");
                }
            }
        }
        public static async Task<bool> AddDiplomAsync(AddOtherSettingResponse addOtherSetting, string loginHeaderValue, string passwordHeaderValue)
        {
            string UrlResp = Application.Current.Properties["UrlServer"].ToString();
            try
            {
                string apiUrl = $"{UrlResp}/api/Authorize/AddOtherSettings?Login={loginHeaderValue}&Password={passwordHeaderValue}";
                string jsonData = JsonConvert.SerializeObject(addOtherSetting);
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


        public static async Task<bool> AddFakultyAsync(AddFakultyResponse addFakulty, string loginHeaderValue, string passwordHeaderValue, int Id)
        {
            string UrlResp = Application.Current.Properties["UrlServer"].ToString();
            try
            {
                string apiUrl = $"{UrlResp}/api/Authorize/AddFakulty?Login={loginHeaderValue}&Password={passwordHeaderValue}&Id={Id}";
                string jsonData = JsonConvert.SerializeObject(addFakulty);
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

        private async void FakultySettings_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                List<AddFakultyResponse> addfakultyList = await GetFakultyAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetFakulty", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString());
              AddFakultyDataGrid.ItemsSource = addfakultyList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (AddSettingsDataGrid != null)
            {
                DataGridColumn IdColumn = AddFakultyDataGrid.Columns.Single(c => c.Header.ToString() == "Id");
                DataGridColumn FakultyTjColumn  = AddFakultyDataGrid.Columns.Single(c => c.Header.ToString() == "FakultyTj");
                DataGridColumn FakultyRuColumn = AddFakultyDataGrid.Columns.Single(c => c.Header.ToString() == "FakultyRu");
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
                    FakultyTjColumn.Header = "Факультет таджикский";
                    FakultyRuColumn.Header = "Факультет русский";
                }
                else
                {
                    FakultyTjColumn.Header = "Факулта тоҷикӣ";
                    FakultyRuColumn.Header = "Факулта русӣ";
                }
                IdColumn.Width = new DataGridLength(70);
                IdColumn.CellStyle = centerCellStyle;
                IdColumn.HeaderStyle = centerHeaderStyle;
                FakultyTjColumn.Width = new DataGridLength(380);
                FakultyTjColumn.CellStyle = centerCellStyle;
                FakultyTjColumn.HeaderStyle = centerHeaderStyle;
                FakultyRuColumn.Width = new DataGridLength(380);
                FakultyRuColumn.CellStyle = centerCellStyle;
                FakultyRuColumn.HeaderStyle = centerHeaderStyle;             
            }

            AddSettingsDataGrid.Visibility = Visibility.Hidden;
            SpecialityGrid.Visibility = Visibility.Hidden;
            OtherSettingsGrid.Visibility = Visibility.Hidden;
            DescEducProgBoxRu.Visibility = Visibility.Hidden;
            DescEducProgBoxTj.Visibility = Visibility.Hidden;
            AddFakultyGrid.Visibility = Visibility.Visible;
        }

        private void FakultySettings_MouseEnter(object sender, MouseEventArgs e)
        {
            FakultySettings.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }

        private void FakultySettings_MouseLeave(object sender, MouseEventArgs e)
        {
            FakultySettings.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }

        private void AddFakultyButton_Click(object sender, RoutedEventArgs e)
        {
            IdFields.Text = "0";
            GridFakulty.Visibility = Visibility.Visible;
            RevisionButtonPopUp.Content = "Добавить";
        }

        private async void RevisionButtonPopUp_ClickAsync(object sender, RoutedEventArgs e)
        {
            string loginHeaderValue = (string)Application.Current.Properties["Login"];
            string passwordHeaderValue = (string)Application.Current.Properties["Password"];

            var Addfakulty = new AddFakultyResponse
            {
                FakultyTj = FakultyTjButton.Text,
                FakultyRu = FakultyRuButton.Text
            };

            bool isSuccess = await AddFakultyAsync(Addfakulty, loginHeaderValue, passwordHeaderValue, Convert.ToInt32(IdFields.Text));
            if (isSuccess)
            {
                try
                {
                    List<AddFakultyResponse> addfakultyList = await GetFakultyAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetFakulty", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString());
                    AddFakultyDataGrid.ItemsSource = addfakultyList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                }
                if (AddFakultyDataGrid != null)
                {
                    DataGridColumn IdColumn = AddFakultyDataGrid.Columns.Single(c => c.Header.ToString() == "Id");
                    DataGridColumn FakultyTjColumn = AddFakultyDataGrid.Columns.Single(c => c.Header.ToString() == "FakultyTj");
                    DataGridColumn FakultyRuColumn = AddFakultyDataGrid.Columns.Single(c => c.Header.ToString() == "FakultyRu");
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
                        FakultyTjColumn.Header = "Факультет таджикский";
                        FakultyRuColumn.Header = "Факультет русский";
                    }
                    else
                    {
                        FakultyTjColumn.Header = "Факулта тоҷикӣ";
                        FakultyRuColumn.Header = "Факулта русӣ";
                    }
                    IdColumn.Width = new DataGridLength(70);
                    IdColumn.CellStyle = centerCellStyle;
                    IdColumn.HeaderStyle = centerHeaderStyle;
                    FakultyTjColumn.Width = new DataGridLength(380);
                    FakultyTjColumn.CellStyle = centerCellStyle;
                    FakultyTjColumn.HeaderStyle = centerHeaderStyle;
                    FakultyRuColumn.Width = new DataGridLength(380);
                    FakultyRuColumn.CellStyle = centerCellStyle;
                    FakultyRuColumn.HeaderStyle = centerHeaderStyle;
                }
            }

            GridFakulty.Visibility = Visibility.Hidden;

        }

        private void AddFakultyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RevisionButtonPopUp.Content.ToString() == "Сохранить")
            {
                RevisionButtonPopUp.Content = "Добавить";
            }
            else
            {
                IdFields.Text = (((AddFakultyResponse)AddFakultyDataGrid.SelectedItem).Id).ToString();
                FakultyRuButton.Text = ((AddFakultyResponse)AddFakultyDataGrid.SelectedItem).FakultyRu;
                FakultyTjButton.Text = ((AddFakultyResponse)AddFakultyDataGrid.SelectedItem).FakultyTj;
                RevisionButtonPopUp.Content = "Сохранить";         
            }
            GridFakulty.Visibility = Visibility.Visible;

        }

        private void ClosePopUpComment_Click(object sender, RoutedEventArgs e)
        {
            GridFakulty.Visibility = Visibility.Hidden;
        }

        private void FakultyRusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FakultyRusComboBox.SelectedItem != null)
            {
                string selectedRole = FakultyRusComboBox.SelectedItem.ToString();
                FakultyBoxRu.Text = selectedRole;
            }
        }

        private void FakultyTjComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FakultyTjComboBox.SelectedItem != null)
            {
                string selectedRole = FakultyTjComboBox.SelectedItem.ToString();
                FakultyBoxTj.Text = selectedRole;
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
