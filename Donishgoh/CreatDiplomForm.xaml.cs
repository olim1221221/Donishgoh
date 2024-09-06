using Donishgoh.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для CreatDiplomForm.xaml
    /// </summary>
    public partial class CreatDiplomForm : Window
    {
        public CreatDiplomForm()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            DiplomCreatedDateBox.Text = DateTime.Now.ToString();

            // Устанавливаем размеры окна
            this.Width = screenWidth;
            this.Height = screenHeight;


            Loaded += Window_LoadedAsync;
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

        private async void RussianButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            Lang.Text = "RU";
            ImageBrush RussianButtonShow = new ImageBrush();
            RussianButtonShow.ImageSource = (ImageSource)Resources["RussianButtonShow"];
            RussianButton.Background = RussianButtonShow;
            ImageBrush TajikButtonShow = new ImageBrush();
            TajikButtonShow.ImageSource = (ImageSource)Resources["TajikButton"];
            TajikButton.Background = TajikButtonShow;
            StudyFormComboBox.Items.Clear();
            StudyFormComboBox.Items.Add("Выберите форму образование");
            StudyFormComboBox.Items.Add("Очный");
            StudyFormComboBox.Items.Add("Заочный");
            StudyFormComboBox.Items.Add("Удаленный");
            StudyFormComboBox.SelectedIndex = 0;


            DocumentStudyComboBox.Items.Clear();
            DocumentStudyComboBox.Items.Add("Выберите документ");
            DocumentStudyComboBox.Items.Add("Аттестат");
            DocumentStudyComboBox.Items.Add("Диплом");
            DocumentStudyComboBox.SelectedIndex = 0;
            if (StudentId.Text.Length == 10)
            {

                if (FioRu.Text != "" & LevelRu.Text != "" & SpecialityRu.Text != "" & QualificationRu.Text != "" & DirectionRu.Text != "" & StudyFormRu.Text != "" & InstitutionRu.Text != "" & StudyProgramRu.Text != "" & DocRu.Text != "" & INNBox.Text != "")
                {
                    string[] names = FioRu.Text.ToString().Split(' ');
                    if (names.Length > 0)
                    {
                        SurnameBox.Text = names[0];
                    }
                    if (names.Length > 1)
                    {
                        NameBox.Text = names[1];
                    }
                    if (names.Length > 2)
                    {
                        PatronimicBox.Text = names[2];
                    }
                    LevelBox.Text = LevelRu.Text;
                    SpecialityBox.Text = SpecialityRu.Text;
                    QualificationBox.Text = QualificationRu.Text;
                    DirectionBox.Text = DirectionRu.Text;
                    StudyFormBox.Text = StudyFormRu.Text;
                    InstitutionsIssuedDiplomBox.Text = InstitutionRu.Text;
                    StudyProgramLevelBox.Text = StudyFormRu.Text;
                    DocumentStudyBox.Text = DocRu.Text;
                    DescRufields.Text = DescriptionStudyBlock.Text;
                }





                // Ваш код или вызов функции, который должен выполниться, когда значение равно 10
                var PersonList = await GetAuthorizeDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentById", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), StudentId.Text);

                if (PersonList != null && PersonList.Count > 0)
                {

                    string[] names = PersonList[0].nst.ToString().Split(' ');

                    if (names.Length > 0)
                    {
                        // Первое имя
                        SurnameBox.Text = names[0].Replace("{", "ҳ");
                    }

                    if (names.Length > 1)
                    {
                        // Второе имя
                        NameBox.Text = names[1].Replace("{", "ҳ");
                    }

                    if (names.Length > 2)
                    {
                        // Второе имя
                        PatronimicBox.Text = names[2].Replace("{", "ҳ");
                    }



                    if (PersonList[0].speciality.ksp != null | PersonList[0].fakultet.nfk != null)
                    {


                        SpecialityBaseRu.Text = "1-" + PersonList[0].speciality.ksp;


                        AddSettingResponse SettingsList = null;
                        try
                        {
                            SettingsList = await GetSettingsDDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetSettingsBySpeciality", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), SpecialityBaseRu.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading data: {ex.Message}");
                        }

                        SpecialityBox.Text = SettingsList.SpecialityRu;
                        LevelBox.Text = SettingsList.LevelofEducationRu;
                        QualificationBox.Text = SettingsList.QualificationRu;
                        DirectionBox.Text = SettingsList.DirectionRu;
                        //StudyFormBox.Text = SettingsList.TypeEducationRu;
                        StudyProgramLevelBox.Text = SettingsList.StudyPeriodRu;
                        DescriptionStudyBlock.Text = SettingsList.DescEducProgRu;
                        InstitutionsIssuedDiplomBox.Text = "ТАДЖИКСКИЙ НАЦИОНАЛЬНЫЙ УНИВЕРСИТЕТ";
                        TypeDiplomComboBox.IsEnabled = true;
                        StudyFormComboBox.IsEnabled = true;
                        LanguageComboBox.IsEnabled = true;
                        DesCertCommisionPicker.IsEnabled = true;
                    }
                }

            }

            if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
            {
                SendButton.Content = "Сохранить на русском";
            }
            else
            {
                SendButton.Content = "Сабт бо руссӣ";
            }


            SendButton.IsEnabled = true;
            StudentId.IsEnabled = true;
            NameBox.IsEnabled = true;
            SurnameBox.IsEnabled = true;
            PatronimicBox.IsEnabled = true;
            BirthdatePicker.IsEnabled = true;
            LevelBox.IsEnabled = true;
            SpecialityBox.IsEnabled = true;
            QualificationBox.IsEnabled = true;
            DirectionBox.IsEnabled = true;
            StudyFormBox.IsEnabled = true;
            InstitutionsIssuedDiplomBox.IsEnabled = true;
            StudyProgramLevelBox.IsEnabled = true;
            DocumentStudyBox.IsEnabled = true;
            INNBox.IsEnabled = true;
            DescriptionStudyBlock.IsEnabled = true;

        }


        private async Task<AddSettingResponse> GetSettingsDDataAsync(string apiUrl, string login, string password, string searchKeyword)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&Speciality={searchKeyword}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {

                    string json = await response.Content.ReadAsStringAsync();
                    AddSettingResponse authorize = JsonConvert.DeserializeObject<AddSettingResponse>(json);
                    return authorize;

                }
                else
                {
                    // Handle error
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }


        private async Task<List<AddSettingResponse>> GetAllSettingsDataAsync(string apiUrl, string login, string password, string searchKeyword)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&Speciality={searchKeyword}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<AddSettingResponse> authorize = JsonConvert.DeserializeObject<List<AddSettingResponse>>(json);
                    return authorize;
                }
                else
                {
                    // Handle error
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }


        private async Task<List<Prk>> GetStudentLessonsDataAsync(string apiUrl, string login, string password, string searchKeyword)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&searchString={searchKeyword}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Prk> lessonsList = JsonConvert.DeserializeObject<List<Prk>>(json);
                    return lessonsList;
                }
                else
                {
                    // Handle error
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }




        private async void TajikButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            Lang.Text = "TJ";
            ImageBrush TajikButtonShow = new ImageBrush();
            TajikButtonShow.ImageSource = (ImageSource)Resources["TajikButtonShow"];
            TajikButton.Background = TajikButtonShow;
            ImageBrush RussianButtonShow = new ImageBrush();
            RussianButtonShow.ImageSource = (ImageSource)Resources["RussianButton"];
            RussianButton.Background = RussianButtonShow;
            StudyFormComboBox.Items.Clear();



            StudyFormComboBox.Items.Add("Выберите форму образование");
            StudyFormComboBox.Items.Add("Рӯзона");
            StudyFormComboBox.Items.Add("Ғоибона");
            StudyFormComboBox.Items.Add("Фосилавӣ");
            StudyFormComboBox.SelectedIndex = 0;

            DocumentStudyComboBox.Items.Clear();
            DocumentStudyComboBox.Items.Add("Выберите документ");
            DocumentStudyComboBox.Items.Add("Аттестат");
            DocumentStudyComboBox.Items.Add("Диплом");
            DocumentStudyComboBox.SelectedIndex = 0;

            // Ваш код или вызов функции, который должен выполниться, когда значение равно 10
            if (StudentId.Text.Length == 10)
            {

                if (FioTj.Text != "" & LevelTj.Text != "" & SpecialityTj.Text != "" & QualificationTj.Text != "" & DirecionTj.Text != "" & StudyFormTj.Text != "" & InstitutionTj.Text != "" & StudyProgramTj.Text != "" & DocTj.Text != "" & INNBox.Text != "" & DescRufields.Text != "")
                {
                    string[] names = FioTj.Text.ToString().Split(' ');
                    if (names.Length > 0)
                    {
                        SurnameBox.Text = names[0];
                    }
                    if (names.Length > 1)
                    {
                        NameBox.Text = names[1];
                    }
                    if (names.Length > 2)
                    {
                        PatronimicBox.Text = names[2];
                    }
                    LevelBox.Text = LevelTj.Text;
                    SpecialityBox.Text = SpecialityTj.Text;
                    QualificationBox.Text = QualificationTj.Text;
                    DirectionBox.Text = DirecionTj.Text;
                    StudyFormBox.Text = StudyFormTj.Text;
                    InstitutionsIssuedDiplomBox.Text = InstitutionTj.Text;
                    StudyProgramLevelBox.Text = StudyProgramTj.Text;
                    DocumentStudyBox.Text = DocTj.Text;
                    DescTjfields.Text = DescriptionStudyBlock.Text;


                }

                else
                {

                    var PersonList = await GetAuthorizeDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentById", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), StudentId.Text);

                    if (PersonList != null && PersonList.Count > 0)
                    {

                        string[] names = PersonList[0].nstt.ToString().Split(' ');

                        if (names.Length > 0)
                        {
                            // Первое имя
                            SurnameBox.Text = names[0].Replace("{", "ҳ").Replace("r", "қ").Replace("R", "Қ").Replace("X", "Ҷ").Replace("x", "ҷ").Replace("U", "Ғ").Replace("[", "Ҳ").Replace("b", "ӣ");
                        }

                        if (names.Length > 1)
                        {
                            // Второе имя
                            NameBox.Text = names[1].Replace("{", "ҳ").Replace("r", "қ").Replace("R", "Қ").Replace("X", "Ҷ").Replace("x", "ҷ").Replace("U", "Ғ").Replace("[", "Ҳ").Replace("b", "ӣ");
                        }

                        if (names.Length > 2)
                        {
                            // Второе имя
                            PatronimicBox.Text = names[2].Replace("{", "ҳ").Replace("r", "қ").Replace("R", "Қ").Replace("X", "Ҷ").Replace("x", "ҷ").Replace("U", "Ғ").Replace("[", "Ҳ").Replace("b", "ӣ");
                        }

                        if (PersonList[0].speciality.ksp != null | PersonList[0].fakultet.nfkt != null)
                        {


                            SpecialityBaseTj.Text = "1-" + PersonList[0].speciality.ksp.Replace("{", "ҳ").Replace("r", "қ").Replace("R", "Қ").Replace("X", "Ҷ").Replace("x", "ҷ").Replace("U", "Ғ").Replace("[", "Ҳ").Replace("b", "ӣ");


                            AddSettingResponse SettingsList = null;
                            try
                            {
                                SettingsList = await GetSettingsDDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetSettingsBySpeciality", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), SpecialityBaseTj.Text);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error loading data: {ex.Message}");
                            }

                            SpecialityBox.Text = SettingsList.SpecialityTj;
                            LevelBox.Text = SettingsList.LevelofEducationTj;
                            QualificationBox.Text = SettingsList.QualificationTj;
                            DirectionBox.Text = SettingsList.DirectionTj;
                            StudyProgramLevelBox.Text = SettingsList.StudyPeriodTj;
                            DescriptionStudyBlock.Text = SettingsList.DescEducProgTj;
                            InstitutionsIssuedDiplomBox.Text = "ДОНИШГОҲИ МИЛЛИИ ТОҶИКИСТОН";
                            Fakulty.Text = SettingsList.FakultyRu;

                        }

                        StudentDouble.Text = PersonList[0].StudDuble;
                    }
                }
            }


            if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
            {
                SendButton.Content = "Сохранить на таджикском";
            }
            else
            {
                SendButton.Content = "Сабт бо забони тоҷикӣ";

            }

            SendButton.IsEnabled = true;
            StudentId.IsEnabled = true;
            NameBox.IsEnabled = true;
            SurnameBox.IsEnabled = true;
            PatronimicBox.IsEnabled = true;
            BirthdatePicker.IsEnabled = true;
            LevelBox.IsEnabled = true;
            SpecialityBox.IsEnabled = true;
            QualificationBox.IsEnabled = true;
            DirectionBox.IsEnabled = true;
            StudyFormBox.IsEnabled = true;
            InstitutionsIssuedDiplomBox.IsEnabled = true;
            StudyProgramLevelBox.IsEnabled = true;
            DocumentStudyBox.IsEnabled = true;
            INNBox.IsEnabled = true;
            TypeDiplomComboBox.IsEnabled = true;
            StudyFormComboBox.IsEnabled = true;
            LanguageComboBox.IsEnabled = true;
            DescriptionStudyBlock.IsEnabled = true;
            DocumentStudyComboBox.IsEnabled = true;
            DesCertCommisionPicker.IsEnabled = true;
        }

        private void ApprovalComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
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

        private async Task<List<StudentViewModel>> GetAuthorizeDataAsync(string apiUrl, string login, string password, string Id)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&Id={Id}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {

                    string json = await response.Content.ReadAsStringAsync();
                    List<StudentViewModel> authorizeList = JsonConvert.DeserializeObject<List<StudentViewModel>>(json);
                    return authorizeList;
                }
                else
                {
                    // Handle error
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }



        private async Task<acceptDiplom> GetDiplomDataByIdAsync(string apiUrl, string login, string password, int Id)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&Id={Id}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    acceptDiplom receivedDiplom = JsonConvert.DeserializeObject<acceptDiplom>(json);
                    return receivedDiplom;
                }
                else
                {
                    // Handle error
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }


        private async Task<List<Person>> GetAuthorizePersonAsync(string apiUrl, string login, string password, string Search)
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

        public class PersonListResource : ObservableCollection<Donishgoh.Models.Person>
        {
            public PersonListResource(IEnumerable<Donishgoh.Models.Person> collection) : base(collection)
            {
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
                FirstLabel.Content = "На подтверждение";
                QuitButton.Content = "          Выход";
                StudentIdLabel.Content = "ID Студента";
                SurnameLabel.Content = "Фамилия";
                NameLabel.Content = "Имя";
                PatronimicLabel.Content = "Отчество";
                BirthDateLabel.Content = "Дата рождения";
                LavelLabel.Content = "Уровень образования";
                SpecialityLabel.Content = "Специальность";
                QualificationLabel.Content = "Квалификация";
                DirectionLabel.Content = "Направление";
                StudyFormLabel.Content = "Форма обучения";
                InstitutionsIssuedDiplomLabel.Content = "Название учреждения, выдавшего диплом";
                INNLabel.Content = "ИНН";
                DiplomCreatedDateLabel.Content = "Дата создания диплома";
                StudyProgramLevelLebel.Content = "Срок образовательной программы";
                DocumentStudyLebel.Content = "Документ о предыдущем образовании";
                TypeDiplomResp.Content = "Тип Диплома";
                StudyProgramDescLebel.Content = "Описание образовательной программы";
                FirstApprovalLebel.Content = "Первый подтверждающий";
                SecondApprovalLebel.Content = "Второй подтверждающий";
                ThirdApprovalLebel.Content = "Третий подтверждающий";
                FourthApprovalLebel.Content = "Четвертый подтверждающий";
                CancelButton.Content = "Отмена";

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
                FirstLabel.Content = "Барои тасдиқ";
                QuitButton.Content = "          Баромадгоҳ";
                StudentIdLabel.Content = "ID Донишҷӯ";
                SurnameLabel.Content = "Насаб";
                NameLabel.Content = "Ном";
                PatronimicLabel.Content = "Номи падар";
                BirthDateLabel.Content = "Санаи таваллуд";
                LavelLabel.Content = "Зинаи таҳсил";
                SpecialityLabel.Content = "Ихтисос";
                QualificationLabel.Content = "Тахассус (касб)";
                DirectionLabel.Content = "Самт";
                StudyFormLabel.Content = "Намуди таҳсил";
                InstitutionsIssuedDiplomLabel.Content = "Муассисае, ки диплом медиҳад";
                INNLabel.Content = "РМА";
                DiplomCreatedDateLabel.Content = "Санаи сохтани диплом";
                StudyProgramLevelLebel.Content = "Давомнокии барномаи таълимӣ";
                DocumentStudyLebel.Content = "Ҳуҷҷат дар бораи таҳсилоти қаблӣ";
                TypeDiplomResp.Content = "Намуди диплом";
                StudyProgramDescLebel.Content = "Тавсифи барномаи таълимӣ";
                FirstApprovalLebel.Content = "Якум тасдиқкунанда";
                SecondApprovalLebel.Content = "Дуюм тасдиқкунанда";
                ThirdApprovalLebel.Content = "Сеюм тасдиқкунанда";
                FourthApprovalLebel.Content = "Чорум тасдиқкунанда";
                CancelButton.Content = "Бекор кардан";
            }


            LanguageComboBox.Items.Add("Выберите язык образование");
            LanguageComboBox.Items.Add("Таджикский");
            LanguageComboBox.Items.Add("Русский");
            LanguageComboBox.Items.Add("Английский");
            LanguageComboBox.SelectedIndex = 0;


            TypeDiplomComboBox.Items.Add("Выберите Тип");
            TypeDiplomComboBox.Items.Add("Красный");
            TypeDiplomComboBox.Items.Add("Синий");
            TypeDiplomComboBox.SelectedIndex = 0;

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

            try
            {
                var PersonList = await GetAuthorizePersonAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), "Подтверждающий");

                var personListResource = new PersonListResource(PersonList);
                FirstApprovalComboBox.ItemsSource = personListResource;
                FirstApprovalComboBox.DisplayMemberPath = "FIO";

                SecondApprovalComboBox.ItemsSource = personListResource;
                SecondApprovalComboBox.DisplayMemberPath = "FIO";

                ThirdApprovalComboBox.ItemsSource = personListResource;
                ThirdApprovalComboBox.DisplayMemberPath = "FIO";

                FourthApprovalComboBox.ItemsSource = personListResource;
                FourthApprovalComboBox.DisplayMemberPath = "FIO";
            }


            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

        }

        private static readonly HttpClient client = new HttpClient();

        public class ApiResponse<T>
        {
            public bool IsSuccess { get; set; }
            public HttpStatusCode StatusCode { get; set; }
            public T Content { get; set; }
            public string ResponseBody { get; set; }
        }

        public static async Task<ApiResponse<bool>> AddDiplomAsync(acceptDiplom accept, string loginHeaderValue, string passwordHeaderValue)
        {
            string UrlResp = Application.Current.Properties["UrlServer"].ToString();

            try
            {
                string apiUrl = $"{UrlResp}/api/Authorize/AddDiplom?Login={loginHeaderValue}&Password={passwordHeaderValue}";

                // Serialize the object to JSON
                string jsonData = JsonConvert.SerializeObject(accept);

                // Create content for the POST request
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send the POST request
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Read the response body as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Return ApiResponse with information about the status, content, and response body
                return new ApiResponse<bool>
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = response.StatusCode,
                    Content = response.IsSuccessStatusCode, // Set the actual content here if needed
                    ResponseBody = responseContent
                };
            }
            catch (Exception ex)
            {
                // Handle errors
                Console.WriteLine($"Error: {ex.Message}");

                // Return ApiResponse in case of an error
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError, // Set a specific status code for errors
                    Content = false,
                    ResponseBody = ex.Message
                };
            }
        }


       


        public async Task<List<AddStudentGrade>> GetStudentGradeAsync(string apiUrl, string login, string password, string studentid)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&studentid={studentid}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    // Парсинг JSON-строки и извлечение массива "grades"
                    JObject jsonObject = JObject.Parse(json);
                    JToken gradesToken = jsonObject["grades"];

                    // Десериализация массива "grades" в список объектов AddStudentGrade
                    List<AddStudentGrade> gradeResponseData = gradesToken.ToObject<List<AddStudentGrade>>();

                    return gradeResponseData;
                }
                else
                {
                    // Обработка ошибки
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }




        static async Task<string> GetCreditsResponseAsync(string apiUrl, string login, string password, string studentid)
        {
            using (var httpClient = new HttpClient())
            {
                // Выполнение GET-запроса
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&studentid={studentid}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Чтение содержимого ответа как строки
                    string json = await response.Content.ReadAsStringAsync();

                    // Парсинг JSON-строки
                    JObject jsonObject = JObject.Parse(json);

                    // Извлечение значения по ключу "creditsResponse"
                    string creditsResponse = (string)jsonObject["creditsResponse"];

                    return creditsResponse;
                }
                else
                {
                    // Обработка ошибки, если запрос не успешен
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }




        private async void SendButton_ClickAsync(object sender, RoutedEventArgs e)
        {

            if ((SendButton.Content.ToString() == "Сохранить на таджикском" || SendButton.Content.ToString() == "Сабт бо забони тоҷикӣ") & SendButton.IsEnabled == true)
            {
                FioTj.Text = SurnameBox.Text + " " + NameBox.Text + " " + PatronimicBox.Text;
                LevelTj.Text = LevelBox.Text;
                SpecialityTj.Text = SpecialityBox.Text;
                QualificationTj.Text = QualificationBox.Text;
                DirecionTj.Text = DirectionBox.Text;
                StudyFormTj.Text = StudyFormBox.Text;
                InstitutionTj.Text = InstitutionsIssuedDiplomBox.Text;
                StudyProgramTj.Text = StudyProgramLevelBox.Text;
                DocTj.Text = DocumentStudyBox.Text;
                DescTjfields.Text = DescriptionStudyBlock.Text;

            }


            if ((SendButton.Content.ToString() == "Сохранить на таджикском" || SendButton.Content.ToString() == "Сабт бо забони тоҷикӣ") & SendButton.IsEnabled == true & (FioTj.Text == "" | DesCertCommisionPicker.Text == "" | BirthdatePicker.Text == "" | DocumentStudyComboBox.Text == "Выберите документ" | TypeDiplomComboBox.Text == "Выберите Тип" | StudyFormComboBox.Text == "Выберите форму образование"| LanguageComboBox.Text == "Выберите язык образование" | LevelTj.Text == "" | SpecialityTj.Text == "" | QualificationTj.Text == "" | DirecionTj.Text == "" | StudyFormTj.Text == "" | InstitutionTj.Text == "" | StudyProgramTj.Text == "" | DocTj.Text == "" | INNBox.Text == "" | DescTjfields.Text == ""))
            {
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    SaveSuccess.Content = "Не все поля заполнены !";
                }
                else
                {
                    SaveSuccess.Content = "На ҳама майдонҳо пур карда шудаанд!";
                }


                SaveSuccess.Foreground = new SolidColorBrush(Colors.Red);
            }

            else if ((SendButton.Content.ToString() == "Сохранить на таджикском" || SendButton.Content.ToString() == "Сабт бо забони тоҷикӣ") & SendButton.IsEnabled == true)
            {
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    SaveSuccess.Content = "На таджикском сохранено успешно !";
                    SendButton.Content = "Сохранить на русском";
                    RussianButton.IsEnabled = true;
                }
                else
                {
                    SaveSuccess.Content = "Ба забони тоҷикӣ бомуваффақият сабт шуд!";
                    SendButton.Content = "Сабт бо руссӣ";
                }

                SaveSuccess.Foreground = new SolidColorBrush(Colors.Green);
                SendButton.IsEnabled = false;
            }


            if ((SendButton.Content.ToString() == "Сохранить на русском" || SendButton.Content.ToString() == "Сабт бо руссӣ") & SendButton.IsEnabled == true)
            {
                FioRu.Text = SurnameBox.Text + " " + NameBox.Text + " " + PatronimicBox.Text;
                LevelRu.Text = LevelBox.Text;
                SpecialityRu.Text = SpecialityBox.Text;
                QualificationRu.Text = QualificationBox.Text;
                DirectionRu.Text = DirectionBox.Text;
                StudyFormRu.Text = StudyFormBox.Text;
                InstitutionRu.Text = InstitutionsIssuedDiplomBox.Text;
                StudyProgramRu.Text = StudyProgramLevelBox.Text;
                DocRu.Text = DocumentStudyBox.Text;
                DescRufields.Text = DescriptionStudyBlock.Text;
            }


            if ((SendButton.Content.ToString() == "Сохранить на русском" || SendButton.Content.ToString() == "Сабт бо руссӣ") & SendButton.IsEnabled == true & (FioTj.Text == "" | DesCertCommisionPicker.Text == "" | BirthdatePicker.Text == "" | DocumentStudyComboBox.Text == "Выберите документ" | TypeDiplomComboBox.Text == "Выберите Тип" | StudyFormComboBox.Text == "Выберите форму образование" | LanguageComboBox.Text == "Выберите язык образование" | LevelTj.Text == "" | SpecialityTj.Text == "" | QualificationTj.Text == "" | DirecionTj.Text == "" | StudyFormTj.Text == "" | InstitutionTj.Text == "" | StudyProgramTj.Text == "" | DocTj.Text == "" | INNBox.Text == "" | DescTjfields.Text == ""))
            {
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    SaveSuccess.Content = "Не все поля заполнены !";
                }
                else
                {
                    SaveSuccess.Content = "На ҳама майдонҳо пур карда шудаанд!";
                }
                SaveSuccess.Foreground = new SolidColorBrush(Colors.Red);
            }

            else if ((SendButton.Content.ToString() == "Сохранить на русском" || SendButton.Content.ToString() == "Сабт бо руссӣ") & SendButton.IsEnabled == true)
            {
                SendButton.IsEnabled = false;
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    SendButton.Content = "Сохранить на таджикском";
                    SaveSuccess.Content = "На русском сохранено успешно !";
                }
                else
                {
                    SendButton.Content = "Сабт бо забони тоҷикӣ";
                    SaveSuccess.Content = "Ба забони руссӣ бомуваффақият сабт шуд!";
                }

                SaveSuccess.Foreground = new SolidColorBrush(Colors.Green);
            }



            if (SendButton.Content.ToString() != "Готово" & FioRu.Text != "" & LevelRu.Text != "" & SpecialityRu.Text != "" & QualificationRu.Text != "" & DirectionRu.Text != "" & StudyFormRu.Text != "" & InstitutionRu.Text != "" & StudyProgramRu.Text != "" & DocRu.Text != "" & FioTj.Text != "" & LevelTj.Text != "" & SpecialityTj.Text != "" & QualificationTj.Text != "" & DirecionTj.Text != "" & StudyFormTj.Text != "" & InstitutionTj.Text != "" & StudyProgramTj.Text != "" & DocTj.Text != "" & INNBox.Text != "" & DocumentStudyComboBox.Text != "Выберите документ" & TypeDiplomComboBox.Text != "Выберите Тип" & StudyFormComboBox.Text != "Выберите форму образование"  & LanguageComboBox.Text != "Выберите язык образование"  )
            {
                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    SendButton.Content = "Отправить на согласование";
                }
                else
                {
                    SendButton.Content = "Барои тасдиқ фиристед";
                }

                SendButton.IsEnabled = true;
                FirstApprovalComboBox.IsEnabled = true;
                SecondApprovalComboBox.IsEnabled = true;
                ThirdApprovalComboBox.IsEnabled = true;
                FourthApprovalComboBox.IsEnabled = true;

                FirstAprovalBox.IsEnabled = true;
                SecondAprovalBox.IsEnabled = true;
                ThirdApprovalBox.IsEnabled = true;
                FourthApprovalBox.IsEnabled = true;
            }



            if ((SendButton.Content.ToString() == "Отправить на согласование" || SendButton.Content.ToString() == "Барои тасдиқ фиристед") & FirstAprovalBox.Text != "" & SecondAprovalBox.Text != "")
            {
                var userData = new acceptDiplom
                {
                    FioStudentTj = FioTj.Text,
                    FioStudentRu = FioRu.Text,
                    StudentId = Convert.ToInt32(StudentId.Text),
                    Birthdate = BirthdatePicker.Text,
                    StudyLevel = LevelRu.Text,
                    Speciality = SpecialityRu.Text,
                    SpecialityTj = SpecialityTj.Text,
                    QualificationTj = QualificationTj.Text,
                    QualificationRu = QualificationRu.Text,
                    Direction = DirectionRu.Text,
                    DirectionTj = DirecionTj.Text,
                    TrainingForm = StudyFormRu.Text,
                    TrainingFormTj = StudyFormTj.Text,
                    Donishgoh = InstitutionRu.Text,
                    DonishgohTj = InstitutionTj.Text,
                    inn = INNBox.Text,
                    CreatedDiplomDate = Convert.ToDateTime(DiplomCreatedDateBox.Text),
                    TermStudy = StudyProgramRu.Text,
                    TermStudyTj = StudyProgramTj.Text,
                    Document = DocTj.Text,
                    Firstapp = FirstAprovalBox.Text,
                    Secondapp = SecondAprovalBox.Text,
                    Thirdapp = ThirdApprovalBox.Text,
                    Fourthapp = FourthApprovalBox.Text,
                    DescRu = DescRufields.Text,
                    DescTj = DescTjfields.Text,
                    TypeDiplom = TypeDiplomBox.Text,
                    CommissionDate = DesCertCommisionPicker.Text,
                    Created = Application.Current.Properties["Fio"].ToString(),
                    Fakulty = Fakulty.Text,
                    Dublicate = StudentDouble.Text,
                    Language = Language.Text

                };
                // Логин и пароль для URL
                string loginHeaderValue = (string)Application.Current.Properties["Login"];
                string passwordHeaderValue = (string)Application.Current.Properties["Password"];

                // Вызов метода для отправки POST-запроса с параметрами в URL


                ApiResponse<bool> apiResponse = await AddDiplomAsync(userData, loginHeaderValue, passwordHeaderValue);


                bool isSuccess = apiResponse.IsSuccess;


                if (isSuccess)
                {
                    DiplomColumnId.Text = apiResponse.ResponseBody;
                    SendButton.Content = "Готово";
                    Grades.IsEnabled = true;
                    if (Grades.Visibility == Visibility.Visible)
                    {
                        if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                        {
                            SaveSuccess.Content = "Добавьте оценки студента !";
                        }
                        else
                        {
                            SaveSuccess.Content = "Баҳои донишҷӯро ворид кунед !";
                        }
                    }
                    else
                    {

                        RootObject1 StudentGradeList = await GetStudentGradeByIdAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentGrade1", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), StudentId.Text, DiplomColumnId.Text);

                        SaveSuccess.Content = "Проверьте оценки студента !";

                        DiplomGradeGrid.Visibility = Visibility.Visible;

                        try
                        {

                            CreditsResponseBox.Text = await GetCreditsResponseAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentsGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), DiplomColumnId.Text);


                            List<AddStudentGrade> GradeResponseData = await GetStudentGradeAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentsGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), DiplomColumnId.Text);
                            AddStudentGradeGrid.ItemsSource = GradeResponseData;



                            //        public int id { get; set; }
                            //public string lessonName { get; set; }
                            //public string lessonNameTj { get; set; }

                            //public string credits { get; set; }
                            //public string grades { get; set; }
                            //public string studentId { get; set; }

                            DataGridColumn IDColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "id");
                            DataGridColumn lessonNameColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "lessonName");
                            DataGridColumn lessonNameTjColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "lessonNameTj");
                            DataGridColumn creditsColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "credits");
                            DataGridColumn gradesColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "grades");
                            DataGridColumn studentIdColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "studentId");

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
                                lessonNameColumn.Header = "Название предмета \nна русском";
                                lessonNameTjColumn.Header = "Название предмета \nна таджикском";
                                creditsColumn.Header = "Кредиты и\n аудиторские";
                                gradesColumn.Header = "Оценка\nстудента";
                                studentIdColumn.Header = "ID студента";
                            }
                            else
                            {
                                lessonNameColumn.Header = "Номи фан бо \nзабони руссӣ";
                                lessonNameTjColumn.Header = "Номи фан бо \nзабони тоҷикӣ";
                                creditsColumn.Header = "Миқдори кредитҳо\n ва аудиторӣ";
                                gradesColumn.Header = "Баҳои\nдонишҷӯ";
                                studentIdColumn.Header = "ID донишҷӯ";
                            }


                            IDColumn.Width = new DataGridLength(50);
                            IDColumn.CellStyle = centerCellStyle;
                            IDColumn.Header = "ID";
                            IDColumn.HeaderStyle = centerHeaderStyle;

                            lessonNameColumn.Width = new DataGridLength(235);
                            lessonNameColumn.CellStyle = centerCellStyle;
                            lessonNameColumn.HeaderStyle = centerHeaderStyle;

                            lessonNameTjColumn.Width = new DataGridLength(235);
                            lessonNameTjColumn.CellStyle = centerCellStyle;
                            lessonNameTjColumn.HeaderStyle = centerHeaderStyle;

                            creditsColumn.Width = new DataGridLength(140);
                            creditsColumn.CellStyle = centerCellStyle;
                            creditsColumn.HeaderStyle = centerHeaderStyle;

                            gradesColumn.Width = new DataGridLength(140);
                            gradesColumn.CellStyle = centerCellStyle;
                            gradesColumn.HeaderStyle = centerHeaderStyle;

                            studentIdColumn.Width = new DataGridLength(140);
                            studentIdColumn.CellStyle = centerCellStyle;
                            studentIdColumn.HeaderStyle = centerHeaderStyle;

                        }
                        catch
                        {

                        }

                    }

                }
                else
                {
                    Console.WriteLine("Ошибка при добавлении пользователя.");
                }
            }

            else if ((SendButton.Content.ToString() == "Отправить на согласование" || SendButton.Content.ToString() == "Барои тасдиқ фиристед") & FirstAprovalBox.Text == "" & SecondAprovalBox.Text == "")
            {
                SaveSuccess.Content = "Выберите подтверждающих";
                SaveSuccess.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (SendButton.Content.ToString() == "Готово" && DiplomGradeGrid.Visibility == Visibility.Hidden)
            {
                try
                {
                    List<AddStudentGrade> GradeResponseData = await GetStudentGradeAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentsGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), DiplomColumnId.Text);

                    // People = PersonList;
                    //ReportsDataGrid.ItemsSource = YearResponseData;
                    if (SendButton.Content.ToString() == "Готово" & (GradeResponseData != null && GradeResponseData.Any()))
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
                    else
                    {
                        if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                        {
                            SaveSuccess.Content = "Добавьте оценки студента !";
                        }
                        else
                        {
                            SaveSuccess.Content = "Баҳои донишҷӯро ворид кунед !";
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                    {
                        SaveSuccess.Content = "Добавьте оценки студента !";
                    }
                    else
                    {
                        SaveSuccess.Content = "Баҳои донишҷӯро ворид кунед !";
                    }
                }
            }

        }


        private async Task<RootObject1> GetStudentGradeByIdAsync(string apiUrl, string login, string password, string Id, string NewId)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&StudentId={Id}&StudentIdNew={NewId}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    RootObject1 receivedDiplom = JsonConvert.DeserializeObject<RootObject1>(json);
                    return receivedDiplom;
                }
                else
                {
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }


        private void FirstApprovalComboBox_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {

            if (FirstApprovalComboBox.SelectedItem != null)
            {
                Donishgoh.Models.Person selectedPerson = (Donishgoh.Models.Person)FirstApprovalComboBox.SelectedItem;

                // Предполагается, что у Donishgoh.models.person есть свойство FIO
                string firstApproval = selectedPerson.FIO;

                FirstAprovalBox.Text = firstApproval;
            }
        }

        private void SecondApprovalComboBox_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            if (SecondApprovalComboBox.SelectedItem != null)
            {
                Donishgoh.Models.Person selectedPerson = (Donishgoh.Models.Person)SecondApprovalComboBox.SelectedItem;
                string firstApproval = selectedPerson.FIO;
                SecondAprovalBox.Text = firstApproval;
            }
        }
        private void ThirdApprovalComboBox_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {

            if (ThirdApprovalComboBox.SelectedItem != null)
            {
                Donishgoh.Models.Person selectedPerson = (Donishgoh.Models.Person)ThirdApprovalComboBox.SelectedItem;
                string firstApproval = selectedPerson.FIO;
                ThirdApprovalBox.Text = firstApproval;
            }
        }
        private void FourthApprovalComboBox_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            if (FourthApprovalComboBox.SelectedItem != null)
            {
                Donishgoh.Models.Person selectedPerson = (Donishgoh.Models.Person)FourthApprovalComboBox.SelectedItem;
                string firstApproval = selectedPerson.FIO;
                FourthApprovalBox.Text = firstApproval;
            }
        }

        private void StudentId_TextChangedAsync(object sender, TextChangedEventArgs e)
        {

        }

        private async void SpecialityBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            List<AddSettingResponse> LessonsList = null;
            try
            {
                LessonsList = await GetAllSettingsDataAsync(
                   "" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetAllSettingsBySpeciality",
                   Application.Current.Properties["Login"].ToString(),
                   Application.Current.Properties["Password"].ToString(),
                   SpecialityBox.Text);

                ObservableCollection<AddSettingResponse> lessonsCollection = new ObservableCollection<AddSettingResponse>(LessonsList);
                SpecialityComboBox.ItemsSource = lessonsCollection;


                SpecialityComboBox.IsDropDownOpen = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Предмет не найден");
            }
            //GetAllSettingsDataAsync
        }

        private void TypeDiplomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeDiplomComboBox.SelectedItem != null)
            {
                string selectedRole = TypeDiplomComboBox.SelectedItem.ToString();
                TypeDiplomBox.Text = selectedRole;
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

        private void TajikButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StudyFormComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudyFormComboBox.SelectedItem != null)
            {
                string selectedRole = StudyFormComboBox.SelectedItem.ToString();
                StudyFormBox.Text = selectedRole;
            }
        }

        private void DocumentStudyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DocumentStudyComboBox.SelectedItem != null)
            {
                string selectedRole = DocumentStudyComboBox.SelectedItem.ToString();
                DocumentStudyBox.Text = selectedRole;
            }
        }

        private void SurnameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void StudentId_KeyDownAsync(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Lang.Text = "TJ";
                ImageBrush TajikButtonShow = new ImageBrush();
                TajikButtonShow.ImageSource = (ImageSource)Resources["TajikButtonShow"];
                TajikButton.Background = TajikButtonShow;
                ImageBrush RussianButtonShow = new ImageBrush();
                RussianButtonShow.ImageSource = (ImageSource)Resources["RussianButton"];
                RussianButton.Background = RussianButtonShow;
                StudyFormComboBox.Items.Clear();



                StudyFormComboBox.Items.Add("Выберите форму образование");
                StudyFormComboBox.Items.Add("Рӯзона");
                StudyFormComboBox.Items.Add("Ғоибона");
                StudyFormComboBox.Items.Add("Фосилавӣ");
                StudyFormComboBox.SelectedIndex = 0;

                DocumentStudyComboBox.Items.Clear();
                DocumentStudyComboBox.Items.Add("Выберите документ");
                DocumentStudyComboBox.Items.Add("Аттестат");
                DocumentStudyComboBox.Items.Add("Диплом");
                DocumentStudyComboBox.SelectedIndex = 0;

                // Ваш код или вызов функции, который должен выполниться, когда значение равно 10
                if (StudentId.Text.Length == 10)
                {

                    if (FioTj.Text != "" & LevelTj.Text != "" & SpecialityTj.Text != "" & QualificationTj.Text != "" & DirecionTj.Text != "" & StudyFormTj.Text != "" & InstitutionTj.Text != "" & StudyProgramTj.Text != "" & DocTj.Text != "" & INNBox.Text != "" & DescRufields.Text != "")
                    {
                        string[] names = FioTj.Text.ToString().Split(' ');
                        if (names.Length > 0)
                        {
                            SurnameBox.Text = names[0];
                        }
                        if (names.Length > 1)
                        {
                            NameBox.Text = names[1];
                        }
                        if (names.Length > 2)
                        {
                            PatronimicBox.Text = names[2];
                        }
                        LevelBox.Text = LevelTj.Text;
                        SpecialityBox.Text = SpecialityTj.Text;
                        QualificationBox.Text = QualificationTj.Text;
                        DirectionBox.Text = DirecionTj.Text;
                        StudyFormBox.Text = StudyFormTj.Text;
                        InstitutionsIssuedDiplomBox.Text = InstitutionTj.Text;
                        StudyProgramLevelBox.Text = StudyProgramTj.Text;
                        DocumentStudyBox.Text = DocTj.Text;
                        DescTjfields.Text = DescriptionStudyBlock.Text;
                    }

                    else
                    {

                        var PersonList = await GetAuthorizeDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentById", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), StudentId.Text);

                        if (PersonList != null && PersonList.Count > 0)
                        {

                            string[] names = PersonList[0].nstt.ToString().Split(' ');

                            if (names.Length > 0)
                            {
                                // Первое имя
                                SurnameBox.Text = names[0].Replace("{", "ҳ").Replace("r", "қ").Replace("R", "Қ").Replace("X", "Ҷ").Replace("x", "ҷ").Replace("U", "Ғ").Replace("[", "Ҳ").Replace("b", "ӣ");
                            }

                            if (names.Length > 1)
                            {
                                // Второе имя
                                NameBox.Text = names[1].Replace("{", "ҳ").Replace("r", "қ").Replace("R", "Қ").Replace("X", "Ҷ").Replace("x", "ҷ").Replace("U", "Ғ").Replace("[", "Ҳ").Replace("b", "ӣ");
                            }

                            if (names.Length > 2)
                            {
                                // Второе имя
                                PatronimicBox.Text = names[2].Replace("{", "ҳ").Replace("r", "қ").Replace("R", "Қ").Replace("X", "Ҷ").Replace("x", "ҷ").Replace("U", "Ғ").Replace("[", "Ҳ").Replace("b", "ӣ");
                            }

                            if (PersonList[0].speciality.ksp != null | PersonList[0].fakultet.nfkt != null)
                            {


                                SpecialityBaseTj.Text = "1-" + PersonList[0].speciality.ksp.Replace("{", "ҳ").Replace("r", "қ").Replace("R", "Қ").Replace("X", "Ҷ").Replace("x", "ҷ").Replace("U", "Ғ").Replace("[", "Ҳ").Replace("b", "ӣ");


                                AddSettingResponse SettingsList = null;
                                try
                                
                                
                                {
                                    SettingsList = await GetSettingsDDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetSettingsBySpeciality", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), SpecialityBaseTj.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error loading data: {ex.Message}");
                                }

                                SpecialityBox.Text = SettingsList.SpecialityTj;
                                LevelBox.Text = SettingsList.LevelofEducationTj;
                                QualificationBox.Text = SettingsList.QualificationTj;
                                DirectionBox.Text = SettingsList.DirectionTj;
                                StudyProgramLevelBox.Text = SettingsList.StudyPeriodTj;
                                DescriptionStudyBlock.Text = SettingsList.DescEducProgTj;
                                InstitutionsIssuedDiplomBox.Text = "ДОНИШГОҲИ МИЛЛИИ ТОҶИКИСТОН";
                                Fakulty.Text = SettingsList.FakultyRu;
                            }
                        }

                        if (PersonList.Count != 0)
                        {
                            StudentDouble.Text = PersonList[0].StudDuble;
                        }


                    }
                }


                if (Application.Current.Properties["FormLanguage"].ToString() == "Russian")
                {
                    SendButton.Content = "Сохранить на таджикском";
                }
                else
                {
                    SendButton.Content = "Сабт бо забони тоҷикӣ";

                }

                SendButton.IsEnabled = true;
                StudentId.IsEnabled = true;
                NameBox.IsEnabled = true;
                SurnameBox.IsEnabled = true;
                PatronimicBox.IsEnabled = true;
                BirthdatePicker.IsEnabled = true;
                LevelBox.IsEnabled = true;
                SpecialityBox.IsEnabled = true;
                QualificationBox.IsEnabled = true;
                DirectionBox.IsEnabled = true;
                StudyFormBox.IsEnabled = true;
                InstitutionsIssuedDiplomBox.IsEnabled = true;
                StudyProgramLevelBox.IsEnabled = true;
                DocumentStudyBox.IsEnabled = true;
                INNBox.IsEnabled = true;
                TypeDiplomComboBox.IsEnabled = true;
                StudyFormComboBox.IsEnabled = true;
                DescriptionStudyBlock.IsEnabled = true;
                DocumentStudyComboBox.IsEnabled = true;
                DesCertCommisionPicker.IsEnabled = true;
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

        private void Grades_Click(object sender, RoutedEventArgs e)
        {
            DiplomGradeGrid.Visibility = Visibility.Visible;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            DiplomGradeGrid.Visibility = Visibility.Hidden;
        }

        private async void Save1_ClickAsync(object sender, RoutedEventArgs e)
        {
            GradeGrid.Visibility = Visibility.Hidden;
            string Ocenka = "";
            if (Convert.ToDecimal(GradeStudText.Text) >= 90 & Convert.ToDecimal(GradeStudText.Text) <= 94.99m)
            {
                Ocenka = "аъло / отлично\n (A-) " + GradeStudText.Text;
            }

            else if (Convert.ToDecimal(GradeStudText.Text) >= 95)
            {
                Ocenka = "аъло / отлично\n (A+) " + GradeStudText.Text;
            }

            else if (Convert.ToDecimal(GradeStudText.Text) >= 75 & Convert.ToDecimal(GradeStudText.Text) <= 79.99m)
            {
                Ocenka = "хуб / хорошо\n (B-) " + GradeStudText.Text;
            }
            else if (Convert.ToDecimal(GradeStudText.Text) >= 80 & Convert.ToDecimal(GradeStudText.Text) <= 84.99m)
            {
                Ocenka = "хуб / хорошо\n (B ) " + GradeStudText.Text;
            }

            else if (Convert.ToDecimal(GradeStudText.Text) >= 85 & Convert.ToDecimal(GradeStudText.Text) <= 89.99m)
            {
                Ocenka = "хуб / хорошо\n (B+) " + GradeStudText.Text;
            }


            else if (Convert.ToDecimal(GradeStudText.Text) >= 60 & Convert.ToDecimal(GradeStudText.Text) <= 64.99m )
            {
                Ocenka = "комёб / зачтено\n (С-) " + GradeStudText.Text;
            }

            else if (Convert.ToDecimal(GradeStudText.Text) >= 65 & Convert.ToDecimal(GradeStudText.Text) <= 69.99m)
            {
                Ocenka = "комёб / зачтено\n (С ) " + GradeStudText.Text;
            }

            else if (Convert.ToDecimal(GradeStudText.Text) >= 70 & Convert.ToDecimal(GradeStudText.Text) <= 74.99m)
            {
                Ocenka = "комёб / зачтено\n (С+) " + GradeStudText.Text;
            }

            else if (Convert.ToDecimal(GradeStudText.Text) >= 50 & Convert.ToDecimal(GradeStudText.Text) <= 54.99m)
            {
                Ocenka = "комёб / зачтено\n (D ) " + GradeStudText.Text;
            }

            else if (Convert.ToDecimal(GradeStudText.Text) >= 55 & Convert.ToDecimal(GradeStudText.Text) <= 59.99m)
            {
                Ocenka = "комёб / зачтено\n (D+) " + GradeStudText.Text;
            }

            int id = 0;

            if (GradeId.Text != "")
            {
                id = Convert.ToInt32(GradeId.Text);
            }

            var userData = new AddStudentGrade
            {
                id = id,
                lessonName = LessonNameText.Text,
                lessonNameTj = LessonNameTextTj.Text,
                credits = CreditsText.Text + "/" + (Convert.ToInt32(CreditsText.Text) * 24).ToString() + "(" + (Convert.ToInt32(AuditsText.Text) * 24).ToString() + ")",
                grades = Ocenka,
                studentId = DiplomColumnId.Text,
            };

            // Логин и пароль для URL
            string loginHeaderValue = (string)Application.Current.Properties["Login"];
            string passwordHeaderValue = (string)Application.Current.Properties["Password"];

            // Вызов метода для отправки POST-запроса с параметрами в URL
            bool isSuccess = await AddGradeStudentAsync(userData, loginHeaderValue, passwordHeaderValue);




            try
            {
                CreditsResponseBox.Text = await GetCreditsResponseAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentsGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), DiplomColumnId.Text);
                List<AddStudentGrade> GradeResponseData = await GetStudentGradeAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentsGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), DiplomColumnId.Text);
                AddStudentGradeGrid.ItemsSource = GradeResponseData;



                DataGridColumn IDColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "id");
                DataGridColumn lessonNameColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "lessonName");
                DataGridColumn lessonNameTjColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "lessonNameTj");
                DataGridColumn creditsColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "credits");
                DataGridColumn gradesColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "grades");
                DataGridColumn studentIdColumn = AddStudentGradeGrid.Columns.Single(c => c.Header.ToString() == "studentId");

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
                    lessonNameColumn.Header = "Название предмета \nна русском";
                    lessonNameTjColumn.Header = "Название предмета \nна таджикском";
                    creditsColumn.Header = "Кредиты и\n аудиторские";
                    gradesColumn.Header = "Оценка\nстудента";
                    studentIdColumn.Header = "ID студента";
                }
                else
                {
                    lessonNameColumn.Header = "Номи фан бо \nзабони руссӣ";
                    lessonNameTjColumn.Header = "Номи фан бо \nзабони тоҷикӣ";
                    creditsColumn.Header = "Миқдори кредитҳо\n ва аудиторӣ";
                    gradesColumn.Header = "Баҳои\nдонишҷӯ";
                    studentIdColumn.Header = "ID донишҷӯ";
                }


                IDColumn.Width = new DataGridLength(50);
                IDColumn.CellStyle = centerCellStyle;
                IDColumn.Header = "ID";
                IDColumn.HeaderStyle = centerHeaderStyle;

                lessonNameColumn.Width = new DataGridLength(235);
                lessonNameColumn.CellStyle = centerCellStyle;
                lessonNameColumn.HeaderStyle = centerHeaderStyle;

                lessonNameTjColumn.Width = new DataGridLength(235);
                lessonNameTjColumn.CellStyle = centerCellStyle;
                lessonNameTjColumn.HeaderStyle = centerHeaderStyle;

                creditsColumn.Width = new DataGridLength(140);
                creditsColumn.CellStyle = centerCellStyle;
                creditsColumn.HeaderStyle = centerHeaderStyle;

                gradesColumn.Width = new DataGridLength(140);
                gradesColumn.CellStyle = centerCellStyle;
                gradesColumn.HeaderStyle = centerHeaderStyle;

                studentIdColumn.Width = new DataGridLength(140);
                studentIdColumn.CellStyle = centerCellStyle;
                studentIdColumn.HeaderStyle = centerHeaderStyle;

            }
            catch
            {

            }


        }


        public static async Task<bool> AddGradeStudentAsync(AddStudentGrade studentGrade, string loginHeaderValue, string passwordHeaderValue)
        {
            string UrlResp = Application.Current.Properties["UrlServer"].ToString();
            try
            {
                string apiUrl = $"{UrlResp}/api/Authorize/AddStudentGrade?Login={loginHeaderValue}&Password={passwordHeaderValue}";
                string jsonData = JsonConvert.SerializeObject(studentGrade);
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

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextAllowed(e.Text))
            {
                e.Handled = true;
            }
        }

        private static bool IsTextAllowed(string text)
        {
            // Регулярное выражение, которое разрешает только цифры
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        private void Save_Copy_Click(object sender, RoutedEventArgs e)
        {
            GradeGrid.Visibility = Visibility.Hidden;
        }

        private async void Save_ClickAsync(object sender, RoutedEventArgs e)
        {
            GradeGrid.Visibility = Visibility.Visible;
            GradeId.Text = "";
        }

        private async void LessonNameText_TextChangedAsync(object sender, TextChangedEventArgs e)
        {

            List<Prk> LessonsList = null;
            try
            {
                LessonsList = await GetStudentLessonsDataAsync(
   "" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentsLesson",
   Application.Current.Properties["Login"].ToString(),
   Application.Current.Properties["Password"].ToString(),
   LessonNameText.Text);

                ObservableCollection<Prk> lessonsCollection = new ObservableCollection<Prk>(LessonsList);
                LessonsNameComboBox.ItemsSource = lessonsCollection;


                LessonsNameComboBox.IsDropDownOpen = true;
            }
            catch (Exception ex)
            {
             //   MessageBox.Show($"Предмет не найден");
            }



        }

        private void LessonsNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LessonsNameComboBox.SelectedItem != null)
            {
                Prk selectedPrk = (Prk)LessonsNameComboBox.SelectedItem;

                if (LessonNameText.Text != selectedPrk.Npr)
                {
                    LessonNameText.Text = selectedPrk.Npr;
                    LessonNameTextTj.Text = selectedPrk.Nprt;
                }
            }
        }

        private void AddStudentGradeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Audits = "";
            string GradeStud = "";
            string Credits = "";


            if (AddStudentGradeGrid.SelectedItem != null)
            {
                string input = ((AddStudentGrade)AddStudentGradeGrid.SelectedItem).credits.ToString();
                string[] parts = input.Split('/');
                Credits = parts[0];
                int startIndex = input.IndexOf('(');
                int endIndex = input.IndexOf(')');

                // Проверяем, что обе скобки присутствуют в строке
                if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                {
                    // Извлекаем текст внутри скобок
                    Audits = input.Substring(startIndex + 1, endIndex - startIndex - 1);

                }
                string input1 = ((AddStudentGrade)AddStudentGradeGrid.SelectedItem).grades.ToString();

                string pattern = @"\d+,\d+";

                // Поиск совпадений в строке
                Match match = Regex.Match(input1, pattern);

                // Если найдено совпадение, выводим найденное число
                if (match.Success)
                {
                    GradeStud = match.Value;
                }

                GradeId.Text = ((AddStudentGrade)AddStudentGradeGrid.SelectedItem).id.ToString();

                if (((AddStudentGrade)AddStudentGradeGrid.SelectedItem).lessonName != null)
                {
                    LessonNameText.Text = ((AddStudentGrade)AddStudentGradeGrid.SelectedItem).lessonName.ToString();
                }

                if (((AddStudentGrade)AddStudentGradeGrid.SelectedItem).lessonNameTj != null)
                {
                    LessonNameTextTj.Text = ((AddStudentGrade)AddStudentGradeGrid.SelectedItem).lessonNameTj.ToString();
                }

                CreditsText.Text = Credits;

                if (Audits != "")
                {
                    AuditsText.Text = (Convert.ToInt32(Audits) / 24).ToString();
                }
                GradeStudText.Text = GradeStud;
                GradeGrid.Visibility = Visibility.Visible;
            }






        }

        private void SpecialityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpecialityComboBox.SelectedItem != null)
            {
                AddSettingResponse selectedPrk = (AddSettingResponse)SpecialityComboBox.SelectedItem;

                if (SpecialityBox.Text != selectedPrk.SpecialityTj)
                {
                    if (Lang.Text == "TJ")
                    {
                        SpecialityBox.Text = selectedPrk.SpecialityTj;
                        LevelBox.Text = selectedPrk.LevelofEducationTj;
                        QualificationBox.Text = selectedPrk.QualificationTj;
                        DirectionBox.Text = selectedPrk.DirectionTj;
                        StudyProgramLevelBox.Text = selectedPrk.StudyPeriodTj;
                        DescriptionStudyBlock.Text = selectedPrk.DescEducProgTj;
                        InstitutionsIssuedDiplomBox.Text = "ДОНИШГОҲИ МИЛЛИИ ТОҶИКИСТОН";
                        Fakulty.Text = selectedPrk.FakultyRu;
                    }
                    else if (Lang.Text == "RU")
                    {
                        SpecialityBox.Text = selectedPrk.SpecialityRu;
                        LevelBox.Text = selectedPrk.LevelofEducationRu;
                        QualificationBox.Text = selectedPrk.QualificationRu;
                        DirectionBox.Text = selectedPrk.DirectionRu;
                        //StudyFormBox.Text = SettingsList.TypeEducationRu;
                        StudyProgramLevelBox.Text = selectedPrk.StudyPeriodRu;
                        DescriptionStudyBlock.Text = selectedPrk.DescEducProgRu;
                        InstitutionsIssuedDiplomBox.Text = "ТАДЖИКСКИЙ НАЦИОНАЛЬНЫЙ УНИВЕРСИТЕТ";
                        TypeDiplomComboBox.IsEnabled = true;
                        StudyFormComboBox.IsEnabled = true;
                        DesCertCommisionPicker.IsEnabled = true;
                    }


                    //SpecialityBox.Text = selectedPrk.SpecialityTj;
                    //QualificationBox.Text = selectedPrk.QualificationTj;
                    //LevelBox.
                }
            }
        }

        private void SpecialityBaseTj_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedItem != null)
            {
                string selectedRole = LanguageComboBox.SelectedItem.ToString();
                Language.Text = selectedRole;
            }
        }
    }
}
