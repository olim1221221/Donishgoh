using Donishgoh.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Application = System.Windows.Application;
using Xceed.Words.NET;
using System.Linq;
using WPFPdfViewer;
using System.IO;
using System.Diagnostics;
using System.Printing;
using System.Windows.Controls.Primitives;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Donishgoh
{
    /// <summary>
    /// Логика взаимодействия для ToApproveForm.xaml
    /// </summary>
    public partial class ToApproveForm : System.Windows.Window
    {
       
        public ToApproveForm()
        {
            InitializeComponent();
           
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Устанавливаем размеры окна
            this.Width = screenWidth;
            this.Height = screenHeight;
            Loaded += Window_LoadedAsync;
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

        private async void Window_LoadedAsync(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties.Contains("Fio"))
            {
                var fioValue = Application.Current.Properties["Fio"];
                if (fioValue is string fio)
                {
                    FioUserLabel.Content = fio;
                    TakeName.Text = "Шахси масъул: "+fio;
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
                FirstLabel.Content = "На подтверждение";
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
                FirstLabel.Content = "Барои тасдиқ";
                QuitButton.Content = "          Баромадгоҳ";
            }

            string RoleResponse = Application.Current.Properties["Roles"].ToString();

            if (Application.Current.Properties["statusFieldsResponse"].ToString() == "Rejected")
            {
                ApproveDiplom.Visibility = Visibility.Hidden;
                CencelDiplom.Visibility = Visibility.Hidden;
                RevisionDiplom.Visibility = Visibility.Hidden;
                PrintDiplom.Visibility = Visibility.Hidden;
                PrintDone.Visibility = Visibility.Hidden;
                GiveDiplom.Visibility = Visibility.Hidden;
            }
            else if (Application.Current.Properties["statusFieldsResponse"].ToString() == "InProgress")
            {
                ApproveDiplom.Visibility = Visibility.Visible;
                CencelDiplom.Visibility = Visibility.Visible;
                RevisionDiplom.Visibility = Visibility.Visible;
                PrintDiplom.Visibility = Visibility.Hidden;
                PrintDone.Visibility = Visibility.Hidden;
                GiveDiplom.Visibility = Visibility.Hidden;
            }
            else if (Application.Current.Properties["statusFieldsResponse"].ToString() == "Accepted")
            {
                ApproveDiplom.Visibility = Visibility.Hidden;
                CencelDiplom.Visibility = Visibility.Hidden;
                RevisionDiplom.Visibility = Visibility.Hidden;
                GiveDiplom.Visibility = Visibility.Hidden;
                if (RoleResponse == "Пользователь")
                {
                    PrintDiplom.Visibility = Visibility.Visible;
                    PrintDone.Visibility = Visibility.Visible;
                }
                    
            }
            else if (Application.Current.Properties["statusFieldsResponse"].ToString() == "Printed")
            {
                ApproveDiplom.Visibility = Visibility.Hidden;
                CencelDiplom.Visibility = Visibility.Hidden;
                RevisionDiplom.Visibility = Visibility.Hidden;
                PrintDiplom.Visibility = Visibility.Hidden;
                PrintDone.Visibility = Visibility.Hidden;
                GiveDiplom.Visibility = Visibility.Hidden;
                if (RoleResponse == "Пользователь")
                {
                    GiveDiplom.Visibility = Visibility.Visible;
                }
            }

            else if (Application.Current.Properties["statusFieldsResponse"].ToString() == "ForFinishing")
            {
                ApproveDiplom.Visibility = Visibility.Hidden;
                CencelDiplom.Visibility = Visibility.Hidden;
                RevisionDiplom.Visibility = Visibility.Hidden;
                PrintDiplom.Visibility = Visibility.Hidden;
                PrintDone.Visibility = Visibility.Hidden;
                GiveDiplom.Visibility = Visibility.Hidden;
            }

            else if (Application.Current.Properties["statusFieldsResponse"].ToString() == "Removed")
            {
                ApproveDiplom.Visibility = Visibility.Hidden;
                CencelDiplom.Visibility = Visibility.Hidden;
                RevisionDiplom.Visibility = Visibility.Hidden;
                PrintDiplom.Visibility = Visibility.Hidden;
                PrintDone.Visibility = Visibility.Hidden;
                GiveDiplom.Visibility = Visibility.Hidden;
            }



            if (RoleResponse == "Подтверждающий")

            {
                UsersButton.IsEnabled = false;
                Logs.IsEnabled = false;
                Settings.IsEnabled = false;
                PrintDiplom.Visibility = Visibility.Hidden;
                PrintDone.Visibility = Visibility.Hidden;
            }

            else if (RoleResponse == "Администратор")
            {
                UsersButton.IsEnabled = false;
                Logs.IsEnabled = false;
                Settings.IsEnabled = false;
                PrintDiplom.Visibility = Visibility.Hidden;
                PrintDone.Visibility = Visibility.Hidden;
            }
            else if (RoleResponse == "Пользователь")
            {
                UsersButton.IsEnabled = false;
                Logs.IsEnabled = false;
                Settings.IsEnabled = false;

                ApproveDiplom.Visibility = Visibility.Hidden;
                CencelDiplom.Visibility = Visibility.Hidden;
                RevisionDiplom.Visibility = Visibility.Hidden;
            }

            if (Application.Current.Properties["SelectedDiplomId"].ToString() == "")
            {
                FirstLabel.Content = "На подтверждение";
            }
            else
            {

                acceptDiplom DiplomList = null;
                try
                {
                    DiplomList = await GetDiplomDataByIdAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetDiplomById", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Convert.ToInt32(Application.Current.Properties["SelectedDiplomId"]));
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error loading data: {ex.Message}");
                }

                if (DiplomList.FioStudentTj != null)
                {
                    string[] names = DiplomList.FioStudentTj.ToString().Split(' ');
                    if (names.Length > 0)
                    {
                    }
                    if (names.Length > 1)
                    {
                        SureNameRespLabel1.Content = names[0] + " " + names[1];            
                        Fam3.Content = names[0];
                        Fam4.Content = names[1];
                    }
                    if (names.Length > 2)
                    {
                        NPRespLabel1.Content = names[2];
                        Fam3.Content = names[0];
                        Fam4.Content = names[1] + " " + names[2];
                    }
                }
                if (DiplomList.FioStudentRu != null)
                {
                    string[] names = DiplomList.FioStudentRu.ToString().Split(' ');
                    if (names.Length > 0)
                    {
                        SurnameBox.Text = names[0];
                    }
                    if (names.Length > 1)
                    {
                        SureNameRespLabel.Content = names[0] + " " + names[1];
                        NameBox.Text = names[1];
                        Fam1.Content = names[0];
                        Fam2.Content = names[1];
                    }
                    if (names.Length > 2)
                    {
                        PatronimicBox.Text = names[2];
                        SureNameRespLabel.Content = names[0] + " " + names[1];
                        NPRespLabel.Content = names[2];
                        Fam1.Content = names[0];
                        Fam2.Content = names[1] + " " + names[2];
                    }
                }

                if (DiplomList.Dublicate.Trim() == "No")
                {
                    Double.Visibility = Visibility.Hidden;
                }
                else if (DiplomList.Dublicate.Trim() == "Yes")
                {
                    Double.Visibility = Visibility.Visible;
                }


                    AddSettingResponse SettingsList = null;
                try
                {
                    SettingsList = await GetSettingsDDataAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetSettingsBySpeciality", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), DiplomList.Speciality);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
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

                RootObject StudentGradeList = null;
                RootObject1 StudentGradeList1 = null;
                try
                {
                 //   StudentGradeList = await GetStudentGradeByIdAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), DiplomList.StudentId.ToString());
                    StudentGradeList1 = await GetStudentGradeByIdAsync1("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentGrade1", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), DiplomList.StudentId.ToString(), DiplomList.Id.ToString());
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error loading data: {ex.Message}");
                }

                Application.Current.Properties["GradeRowCount"] = StudentGradeList1.RowCount;

                string Language = "";

                if (DiplomList.Language != "")
                {
                    if ( DiplomList.Language == "Таджикский")
                    {
                        Language = "Тоҷикӣ / Таджикский";
                    }
                    else if ( DiplomList.Language == "Русский")
                    {
                        Language = "Русӣ / Русский";
                    }

                    else if (DiplomList.Language == "Английский")
                    {
                        Language = "Англисӣ / Английский";
                    }
                }
          
                if (DiplomList.StudyLevel == "Бакалавр" | DiplomList.StudyLevel == "Специалист")
                {
                    SetValuesInTextBoxesBakalavr(this, StudentGradeList1);
                    DiplomSrollviewerBakalavr.Visibility = Visibility.Visible;
                    DiplomSrollviewerMagistr.Visibility = Visibility.Hidden;
                }
                else
                {
                    DiplomSrollviewerBakalavr.Visibility = Visibility.Hidden;
                    DiplomSrollviewerMagistr.Visibility = Visibility.Visible;
                }

                string StudyLevelResp = "";

                if (DiplomList.StudyLevel == "Специалист")
                {
                    StudyLevelResp = "Мутахасис / Специалист";
                }    
                else if (DiplomList.StudyLevel == "Бакалавр")
                {
                    StudyLevelResp = "Бакалавр / Бакалавр";
                }
                else if (DiplomList.StudyLevel == "Магистр")
                {
                    StudyLevelResp = "Магистр / Магистр";
                }


                LevelBox.Text = DiplomList.StudyLevel;
                SpecialityBox.Text = DiplomList.Speciality;
                SpecialityResponse.Content = "по специальности \"" + DiplomList.Speciality + "\"";
                SpecialityResponse1.Content = "аз рӯйи ихтисоси \"" + DiplomList.SpecialityTj + "\"";
                Speciality1.Content = DiplomList.SpecialityTj;
                Speciality2.Content = DiplomList.Speciality;  
                StudentIdResponse.Content = DiplomList.StudentId; 
                BirthdateResponse.Content = DiplomList.Birthdate;
                NumbVertDiplomId.Content = DiplomList.FakultyCode;
                NumbVertDiplomId_Copy.Content = DiplomList.FakultyCode;
                NumbVertDiplomId_Copy1.Content = DiplomList.FakultyCode;
                NumbVertDiplomId_Copy2.Content = DiplomList.FakultyCode;
                DipNumber.Content = DiplomList.DiplomCode;
                DipNumber_Copy.Content = DiplomList.DiplomCode;
                GiveRespDiplomDate.Content = DiplomList.CreatedDiplomDate.ToString("dd.MM.yyyy");
                GiveRespDiplomDate_Copy.Content = DiplomList.CreatedDiplomDate.ToString("dd.MM.yyyy");
                GiveRespDiplomDate_Copy1.Content = DiplomList.CreatedDiplomDate.ToString("dd.MM.yyyy");
                GiveRespDiplomDate_Copy2.Content = DiplomList.CreatedDiplomDate.ToString("dd.MM.yyyy");
                QualificationBox.Text = DiplomList.QualificationRu;
                Qualification1.Content = DiplomList.QualificationTj;
                Qualification2.Content = DiplomList.QualificationRu;
                DirectRespDiplom.Content = DiplomList.Direction;
                DirectRespDiplom1.Content = DiplomList.DirectionTj;
                DocumentResponseDip.Content = DiplomList.Document;
                ComissRespDate1.Content = "аз "+ DiplomList.CommissionDate.Trim();
                ComissRespDate.Content ="от "+ DiplomList.CommissionDate.Trim();
                QualificationResponse.Content = "квалификации \"" + DiplomList.QualificationRu + "\"";
                QualificationResponse1.Content = "бо касби \"" + DiplomList.QualificationTj + "\"";
                DirectionBox.Text = DiplomList.Direction;
                StudyFormBox.Text = DiplomList.TrainingForm;
                INNBox.Text = DiplomList.inn;
                InstitutionsIssuedDiplomBox.Text = DiplomList.Donishgoh;
                StudyProgramLevelBox.Text = DiplomList.TermStudy;
                DocumentStudyBox.Text = DiplomList.Document;
                Birthdatebox.Text = DiplomList.Birthdate;
                DiplomCreatedDateBox.Text = DiplomList.CreatedDiplomDate.ToString("dd.MM.yyyy");
                CommentBlock.Text = DiplomList.Comment;
                FIOCertificate.Text = DiplomList.FioStudentTj;
                GiveStudent.Text = "Донишҷӯ: " + DiplomList.FioStudentRu;
                SpecialityCertificate.Text = DiplomList.Speciality;
                QualificationCert.Text = DiplomList.QualificationTj;
                ComissionDateCert.Text = DiplomList.CommissionDate.Trim();
                LevelCertificate.Text = DiplomList.StudyLevel;
                LanguageResp.Content = Language;
                UnivercityResp.Content = DiplomList.DonishgohTj;
                UnivercityResp1.Content = DiplomList.Donishgoh;
                Zina.Content = DiplomList.TermStudyTj + " / " + DiplomList.TermStudy;
                StudyLavel.Content = StudyLevelResp;
                Shakl.Content = DiplomList.TrainingFormTj;
                Shakl1.Content = DiplomList.TrainingForm;
                Descript.Text = "     " + DiplomList.DescTj + "\n\n     " + DiplomList.DescRu;

                if (DiplomList.TypeDiplom == "Синий")
                {
                    TypeDiplomCertificate.Text = "Муқаррарӣ";
                }
                else
                {
                    TypeDiplomCertificate.Text = "Аъло";
                }

                GivenDateCertificate.Text = DateTime.Now.ToString("dd.MM.yyyy");
                FacultyCertificate.Text = SettingsList.FakultyTj;
                DiplomIdCertificate.Text = DiplomList.DiplomCode;
                RegNumDiplomCertificate.Text = DiplomList.FakultyCode;
                DescStudyBlock.Text = DiplomList.DescRu;
                TypeDiplomBox.Text = DiplomList.TypeDiplom;
                FirstAppResponse.Content = DiplomList.Firstapp;
                SecondAppResponse.Content = DiplomList.Secondapp;
                ThirdAppResponse.Content = DiplomList.Thirdapp;
                FourthAppResponse.Content = DiplomList.Fourthapp;
                HeaderDiplom.Content = "Диплом №" + DiplomList.Id;
                SenderResponse.Content = DiplomList.Created;
                CreatedDateResponse.Content = DiplomList.CreatedDiplomDate;
                UpdateUIVisibility(DiplomList.FirstapprovalStatus, DiplomList.Firstapp, CheckSuccess1, CheckReject1, CheckInProgress1, CheckInForFinishing1);
                UpdateUIVisibility(DiplomList.SecondapprovalStatus, DiplomList.Secondapp, CheckSuccess2, CheckReject2, CheckInProgress2, CheckInForFinishing2);
                UpdateUIVisibility(DiplomList.ThirdapprovalStatus, DiplomList.Thirdapp, CheckSuccess3, CheckReject3, CheckInProgress3, CheckInForFinishing3);
                UpdateUIVisibility(DiplomList.FourthapprovalStatus, DiplomList.Fourthapp, CheckSuccess4, CheckReject4, CheckInProgress4, CheckInForFinishing4);
                
                
                
                if (DiplomList.Status.Trim() == "Accepted")
                {
                    CheckSuccess5.Visibility = Visibility.Visible;
                    CheckReject5.Visibility = Visibility.Hidden;
                    CheckInProgress5.Visibility = Visibility.Hidden;
                    CheckInForFinishing5.Visibility = Visibility.Hidden;
                    CheckInPrinting5.Visibility = Visibility.Hidden;
                    ResponseStatusEnd.Content = "Одобрено";
                }
                else if (DiplomList.Status.Trim() == "Rejected")
                {
                    CheckSuccess5.Visibility = Visibility.Hidden;
                    CheckReject5.Visibility = Visibility.Visible;
                    CheckInProgress5.Visibility = Visibility.Hidden;
                    CheckInForFinishing5.Visibility = Visibility.Hidden;
                    CheckInPrinting5.Visibility = Visibility.Hidden;
                    ResponseStatusEnd.Content = "Отклонено";
                }
                else if (DiplomList.Status.Trim() == "InProgress")
                {
                    CheckSuccess5.Visibility = Visibility.Hidden;
                    CheckReject5.Visibility = Visibility.Hidden;
                    CheckInProgress5.Visibility = Visibility.Visible;
                    CheckInForFinishing5.Visibility = Visibility.Hidden;
                    CheckInPrinting5.Visibility = Visibility.Hidden;
                    ResponseStatusEnd.Content = "В процессе";
                }
                else if (DiplomList.Status.Trim() == "Printed")
                {
                    CheckSuccess5.Visibility = Visibility.Hidden;
                    CheckReject5.Visibility = Visibility.Hidden;
                    CheckInProgress5.Visibility = Visibility.Hidden;
                    CheckInForFinishing5.Visibility = Visibility.Hidden;
                    CheckInPrinting5.Visibility = Visibility.Visible;
                    ResponseStatusEnd.Content = "Распечатано";
                }
                else if (DiplomList.Status.Trim() == "ForFinishing")
                {
                    CheckSuccess5.Visibility = Visibility.Hidden;
                    CheckReject5.Visibility = Visibility.Hidden;
                    CheckInProgress5.Visibility = Visibility.Hidden;
                    CheckInForFinishing5.Visibility = Visibility.Visible;
                    CheckInPrinting5.Visibility = Visibility.Hidden;
                    ResponseStatusEnd.Content = "На дороботку";
                }
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

        private void UpdateUIVisibility(string approvalStatus, string Approval, UIElement success, UIElement reject, UIElement inProgress, UIElement ForFinishing)
        {
            success.Visibility = (approvalStatus == "Accepted" &&  Approval !="") ? Visibility.Visible : Visibility.Hidden;
            reject.Visibility = (approvalStatus == "Rejected" &&  Approval != "") ? Visibility.Visible : Visibility.Hidden;
            inProgress.Visibility = (approvalStatus == "InProgress" &&  Approval != "") ? Visibility.Visible : Visibility.Hidden;
            ForFinishing.Visibility = (approvalStatus == "ForFinishing" && Approval != "") ? Visibility.Visible : Visibility.Hidden;
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
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }
        private async Task<RootObject> GetStudentGradeByIdAsync(string apiUrl, string login, string password, string Id)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?Login={login}&Password={password}&StudentId={Id}";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    RootObject receivedDiplom = JsonConvert.DeserializeObject<RootObject>(json);
                    return receivedDiplom;
                }
                else
                {
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }

        private async Task<RootObject1> GetStudentGradeByIdAsync1(string apiUrl, string login, string password, string Id, string NewId)
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
            UsersButton.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }
        private void UsersButton_MouseLeave(object sender, MouseEventArgs e)
        {
            UsersButton.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }
        private void DesktopButton_MouseEnter(object sender, MouseEventArgs e)
        {
            DesktopButton.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }

        private void DesktopButton_MouseLeave(object sender, MouseEventArgs e)
        {
            DesktopButton.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }

        private void Logs_MouseEnter(object sender, MouseEventArgs e)
        {
            Logs.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }
        private void Logs_MouseLeave(object sender, MouseEventArgs e)
        {
            Logs.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }
        private void RaportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            RaportsButton.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }
        private void RaportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            RaportsButton.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
        }
        private void Settings_MouseEnter(object sender, MouseEventArgs e)
        {
            Settings.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }
        private void Settings_MouseLeave(object sender, MouseEventArgs e)
        {
            Settings.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x0F, 0x17, 0x2A));
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
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void RussianButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            ImageBrush RussianButtonShow = new ImageBrush();
            RussianButtonShow.ImageSource = (ImageSource)Resources["RussianButtonShow"];
            RussianButton.Background = RussianButtonShow;
            ImageBrush TajikButtonShow = new ImageBrush();
            TajikButtonShow.ImageSource = (ImageSource)Resources["TajikButton"];
            TajikButton.Background = TajikButtonShow;
            acceptDiplom DiplomList = null;
            try
            {
                DiplomList = await GetDiplomDataByIdAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetDiplomById", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Convert.ToInt32(Application.Current.Properties["SelectedDiplomId"]));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error loading data: {ex.Message}");
            }
            if (DiplomList.FioStudentRu != null)
            {
                string[] names = DiplomList.FioStudentRu.ToString().Split(' ');
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
            }

            LevelBox.Text = DiplomList.StudyLevel;
            SpecialityBox.Text = DiplomList.Speciality;
            QualificationBox.Text = DiplomList.QualificationRu;
            DirectionBox.Text = DiplomList.Direction;
            StudyFormBox.Text = DiplomList.TrainingForm;
            INNBox.Text = DiplomList.inn;
            InstitutionsIssuedDiplomBox.Text = DiplomList.Donishgoh;
            StudyProgramLevelBox.Text = DiplomList.TermStudy;
            DocumentStudyBox.Text = DiplomList.Document;
            Birthdatebox.Text = DiplomList.Birthdate;
            DiplomCreatedDateBox.Text = DiplomList.CreatedDiplomDate.ToString("dd.MM.yyyy");
            CommentBlock.Text = DiplomList.Comment;
            DescStudyBlock.Text = DiplomList.DescRu;
            TypeDiplomBox.Text = DiplomList.TypeDiplom;
        }
        private async void TajikButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            ImageBrush TajikButtonShow = new ImageBrush();
            TajikButtonShow.ImageSource = (ImageSource)Resources["TajikButtonShow"];
            TajikButton.Background = TajikButtonShow;
            ImageBrush RussianButtonShow = new ImageBrush();
            RussianButtonShow.ImageSource = (ImageSource)Resources["RussianButton"];
            RussianButton.Background = RussianButtonShow;
            acceptDiplom DiplomList = null;
            try
            {
                DiplomList = await GetDiplomDataByIdAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetDiplomById", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Convert.ToInt32(Application.Current.Properties["SelectedDiplomId"]));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error loading data: {ex.Message}");
            }

            if (DiplomList.FioStudentTj != null)
            {

                string[] names = DiplomList.FioStudentTj.ToString().Split(' ');

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
            }

            LevelBox.Text = DiplomList.StudyLevel;
            SpecialityBox.Text = DiplomList.SpecialityTj;
            QualificationBox.Text = DiplomList.QualificationTj;
            DirectionBox.Text = DiplomList.DirectionTj;
            StudyFormBox.Text = DiplomList.TrainingFormTj;
            INNBox.Text = DiplomList.inn;
            InstitutionsIssuedDiplomBox.Text = DiplomList.DonishgohTj;
            StudyProgramLevelBox.Text = DiplomList.TermStudyTj;
            DocumentStudyBox.Text = DiplomList.Document;
            Birthdatebox.Text = DiplomList.Birthdate;
            DiplomCreatedDateBox.Text = DiplomList.CreatedDiplomDate.ToString("dd.MM.yyyy");
            CommentBlock.Text = DiplomList.Comment;
            DescStudyBlock.Text = DiplomList.DescTj;
            TypeDiplomBox.Text = DiplomList.TypeDiplom;
        }

        private void ReplaceTextAndSave(string filePath, string targetFile, Dictionary<string, string> replacements)
        {
            try
            {
                using (DocX document = DocX.Load(filePath))
                {
                    foreach (var paragraph in document.Paragraphs)
                    {
                        foreach (var replacement in replacements)
                        {
                            // Заменяем текст в параграфах
                            paragraph.ReplaceText(replacement.Key, replacement.Value);
                        }
                    }

                    // Сохраняем изменения
                    document.SaveAs(targetFile);
                }

                //  MessageBox.Show("Замена и сохранение выполнены успешно.");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private string AdjustDocumentText2(string baseText, string targetText)
        {
            // Определяем разницу в количестве символов
            int difference = baseText.Length - targetText.Length;

            // Если символов в targetText меньше, чем в baseText
            if (difference > 0)
            {
                // Добавляем пробелы до равенства символов
                targetText += new string(' ', difference);
            }

            // Добавляем перевод строки после каждых 90 символов, учитывая слова
            const int maxCharactersPerLine = 80;
            int currentCharacterCount = 0;
            StringBuilder adjustedText = new StringBuilder();

            string[] words = targetText.Split(' ');

            foreach (string word in words)
            {
                if ((currentCharacterCount + word.Length) <= maxCharactersPerLine)
                {
                    adjustedText.Append(word + " ");
                    currentCharacterCount += word.Length + 1;
                }
                else
                {
                    adjustedText.Append("\n" + word + " ");
                    currentCharacterCount = word.Length + 1;
                }
            }

            return adjustedText.ToString().Trim();
        }





        private int CountNewLines(string text)
        {
            return text.Count(c => c == '\n');
        }

        private string AdjustDocumentText(string baseText, string targetText)
        {
            // Определяем разницу в количестве символов
            int difference = baseText.Length - targetText.Length;

            // Если символов в targetText меньше, чем в baseText
            if (difference > 0)
            {
                // Добавляем пробелы до равенства символов
                targetText += new string(' ', difference);
            }

            return targetText;
        }

        static void AdjustDocumentText1(string baseText, string document, ref string response1, ref string response2, ref string response3, ref string response4)
        {
            string[] documents = { baseText, "2_____________________________________________", "3_____________________________________________", "4_____________________________________________" };
            string[] responses = { response1, response2, response3, response4 };

            for (int i = 0; i < documents.Length; i++)
            {
                int lineLength = documents[i].Length;

                if (document.Length <= lineLength)
                {
                    // Если количество символов меньше или равно текущей строке, добавляем данные в текущий response
                    responses[i] = responses[i] + document.PadRight(lineLength, ' ');
                    break;
                }
                else
                {
                    // Если количество символов больше текущей строки, ищем последнее слово в пределах текущей строки
                    int lastSpaceIndex = documents[i].LastIndexOf(' ');

                    if (lastSpaceIndex >= 0)
                    {
                        // Если найдено слово, переношенное на следующую строку
                        responses[i] = responses[i] + document.Substring(0, lastSpaceIndex).PadRight(lineLength, ' ');
                        document = document.Substring(lastSpaceIndex + 1);
                    }
                    else
                    {
                        // Если в текущей строке нет пробелов, просто обрезаем текст
                        responses[i] = responses[i] + document.Substring(0, lineLength).PadRight(lineLength, ' ');
                        document = document.Substring(lineLength);
                    }
                }
            }

            // Обновляем значения переменных, переданных по ссылке
            response1 = responses[0];
            response2 = responses[1];
            response3 = responses[2];
            response4 = responses[3];
        }


        static int GetNextWordLength(string text, int maxLength)
        {
            int length = 0;
            while (length < text.Length && text[length] != ' ' && length < maxLength)
            {
                length++;
            }
            return length;
        }

       
        private async void PrintDiplom_ClickAsync(object sender, RoutedEventArgs e)
        {
            
            acceptDiplom DiplomList = null;
            try
            {
                DiplomList = await GetDiplomDataByIdAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetDiplomById", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Convert.ToInt32(Application.Current.Properties["SelectedDiplomId"]));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error loading data: {ex.Message}");
            }

            //RootObject StudentGradeList = null;
            RootObject1 StudentGradeList1 = null;
            try
            {
               // StudentGradeList = await GetStudentGradeByIdAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), DiplomList.StudentId.ToString());
                StudentGradeList1 = await GetStudentGradeByIdAsync1("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentGrade1", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), DiplomList.StudentId.ToString(), DiplomList.Id.ToString());
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error loading data: {ex.Message}");
            }

            string FiTj = "";
            string PatTj = "";
            string FTj = "";
            string IPTj = "";
            string FiRu = "";
            string PatRu = "";
            string FRu = "";
            string IPRu = "";
            string Birthdate = "";
            string SpecialityTj = "";
            string SpecialityRu = "";
            string Doc1 = "1_____________________________________________";
            string Doc1Response = null;
            string Doc2Response = null;
            string Doc3Response = null;
            string Doc4Response = null;
            string Desc1Response = null;
            string Language = "";

            string StudLanguage = "";

            
                if (DiplomList.Language == "Таджикский")
                {
                    Language = "Тоҷикӣ / Таджикский";
                }
                else if (DiplomList.Language == "Русский")
                {
                    Language = "Русӣ / Русский";
                }
                else if (DiplomList.Language == "Английский")
                {
                    Language = "Англисӣ / Английский";
                }



            int SpaceFiTjIndex = DiplomList.FioStudentTj.IndexOf(' ');
            int SpaceFiRuIndex = DiplomList.FioStudentRu.IndexOf(' ');
            if (SpaceFiTjIndex >= 0)
            {
                FTj = DiplomList.FioStudentTj.Substring(0, SpaceFiTjIndex);
                IPTj = DiplomList.FioStudentTj.Substring(SpaceFiTjIndex + 1);
                SpaceFiTjIndex = DiplomList.FioStudentTj.IndexOf(' ', SpaceFiTjIndex + 1);
                if (SpaceFiTjIndex >= 0)
                {
                    FiTj = DiplomList.FioStudentTj.Substring(0, SpaceFiTjIndex);
                    PatTj = DiplomList.FioStudentTj.Substring(SpaceFiTjIndex + 1);
                }
            }
            if (SpaceFiRuIndex >= 0)
            {
                FRu = DiplomList.FioStudentRu.Substring(0, SpaceFiRuIndex);
                IPRu = DiplomList.FioStudentRu.Substring(SpaceFiRuIndex + 1);
                SpaceFiRuIndex = DiplomList.FioStudentRu.IndexOf(' ', SpaceFiRuIndex + 1);
                if (SpaceFiRuIndex >= 0)
                {
                    FiRu = DiplomList.FioStudentRu.Substring(0, SpaceFiRuIndex);
                    PatRu = DiplomList.FioStudentRu.Substring(SpaceFiRuIndex + 1);
                }
            }


            string FullFITj = "";
            string FullPTj = "";
            string FullFIRu = "";
            string FullPRu = "";

            if (FiTj == "" & PatTj == "")
            {
                FullFITj = FTj;
                FullPTj = IPTj;
                FullFIRu = FRu;
                FullPRu = IPRu;
            }
            else
            {
                FullFITj = FiTj;
                FullPTj = PatTj;
                FullFIRu = FiRu;
                FullPRu = PatRu;
            }

            FTj = AdjustDocumentText("Фамилия                         Tj", FTj);
            IPTj = AdjustDocumentText("Имя Отчество                          Tj", IPTj);
            FRu = AdjustDocumentText("Фамилия                         Ru", FRu);
            IPRu = AdjustDocumentText("Имя Отчество                          Ru", IPRu);
            Birthdate = AdjustDocumentText("Год рождения                  Tj", DiplomList.Birthdate);
            Desc1Response = AdjustDocumentText2("1D_____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________", DiplomList.DescTj + Environment.NewLine + Environment.NewLine + "            " + DiplomList.DescRu + Environment.NewLine);
           // Desc2Response = AdjustDocumentText2("2D______________________________ _________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________", DiplomList.DescRu);
            AdjustDocumentText1(Doc1, DiplomList.Document, ref Doc1Response, ref Doc2Response, ref Doc3Response, ref Doc4Response);
            SpecialityTj = AdjustDocumentText("Специальность                                                            TJ", DiplomList.SpecialityTj);
            SpecialityRu = AdjustDocumentText("Специальность                                                            Ru", DiplomList.Speciality);
            int newLinesCount = CountNewLines(Desc1Response);
            int totalenter = 20 - newLinesCount;
            string textfieldsAdd = "";

            for (int i = 0; i < totalenter; i++)
            {
                textfieldsAdd += "\n";
            }


            string StudyLevelResp = "";

            if (DiplomList.StudyLevel == "Специалист")
            {
                StudyLevelResp = "Мутахасис / Специалист";
            }
            else if (DiplomList.StudyLevel == "Бакалавр")
            {
                StudyLevelResp = "Бакалавр / Бакалавр";
            }
            else if (DiplomList.StudyLevel == "Магистр")
            {
                StudyLevelResp = "Магистр / Магистр";
            }


            Dictionary<string, string> replacements = new Dictionary<string, string>
            
            {
    { "Фамилия имя Tj", FullFITj },
    { "Отчество Tj", FullPTj },
    { "Фамилия имя Ru", FullFIRu },
    { "Отчество Ru", FullPRu },

    { "Название учреждление Tj", "барномаи таълимии таҳсилоти олии касбиро" },
    { "По квалификации Ru", "квалификации \"" + DiplomList.QualificationRu + "\"" },

    { "Специальность Tj", "аз рӯйи ихтисоси \""+ DiplomList.SpecialityTj +"\"" },
    { "Специальность Ru", "по специальности \""+ DiplomList.Speciality +"\"" },
    { "По специальности test test test test test test test test test test test test test test test test test test test test test", "бо касби \""+DiplomList.QualificationTj +"\" \nаз худ намуда, ба ӯ дараҷаи" },
    { "Фамилия                         Tj", FTj },
    { "Имя Отчество                         Tj", IPTj },
    { "Фамилия                         Ru", FRu },
    { "Имя Отчество                         Ru", IPRu},
    { "Год рождения                  Tj", Birthdate },
    { "Код студента                  Id", DiplomList.StudentId.ToString() },
                {"YearTj", DiplomList.TermStudyTj },
                {"YearRu", DiplomList.TermStudy },

    { "1_____________________________________________", Doc1Response },
    { "2_____________________________________________", Doc2Response },
    { "3_____________________________________________", Doc3Response },
    { "4_____________________________________________", Doc4Response },
    { "Специальность                                                            TJ", SpecialityTj },
    { "Специальность                                                            Ru", SpecialityRu },
    { "Квалификация                                                             Tj", DiplomList.QualificationTj },
    { "Квалификация                                                             Ru", DiplomList.QualificationRu },
    { "Направление                                                                 Tj", DiplomList.DirectionTj },
    { "Направление                                                                 Ru", DiplomList.Direction },
    { "StudyLevel                                             ", StudyLevelResp },
    { "TermStudy                       ", DiplomList.TermStudy +" / "+ DiplomList.TermStudyTj},
    { "00001-01", DiplomList.FakultyCode },
    { "000002-02", DiplomList.DiplomCode },
    { "Имя университета                                                                                                Tj", DiplomList.DonishgohTj },
    { "Имя университета                                                                                                Ru", DiplomList.Donishgoh },
    { "Форма обуч Tj", DiplomList.TrainingFormTj },
    { "Форма обуч Ru", DiplomList.TrainingForm },
     { "1D_____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________", Desc1Response + Environment.NewLine + textfieldsAdd},
 
     { "Language Headers", Language },
     {"02.01.2023", DateTime.Now.ToString("dd.MM.yyyy") },
     { "Karor      Ru", "от " + DiplomList.CommissionDate },
     { "ComissionDate", "аз " + DiplomList.CommissionDate },


        };

            int RowCount = Convert.ToInt32(Application.Current.Properties["GradeRowCount"]);
            if (LevelBox.Text == "Бакалавр")
            {
                

                if (RowCount <= 65)
                {
                    ReplaceTextAndSave("Bakalavr1.docx", "1.docx", replacements);
                    Dictionary<string, string> replacements1 = GenerateReplacementsDictionary(StudentGradeList1);
                    ReplaceTextAndSave("1.docx", "1.docx", replacements1);
                    string inputFilePath = @"1.docx";
                    string selectedPrinter = ShowPrintDialog();
                    PrintDocument(inputFilePath, selectedPrinter);
                }
                else 
                {
                    ReplaceTextAndSave("Bakalavr2.docx", "1.docx", replacements);
                    Dictionary<string, string> replacements1 = GenerateReplacementsDictionary(StudentGradeList1);
                    ReplaceTextAndSave("1.docx", "1.docx", replacements1);
                    string inputFilePath = @"1.docx";
                    string selectedPrinter = ShowPrintDialog();
                    PrintDocument(inputFilePath, selectedPrinter);
                }
         
            }
            else if (LevelBox.Text == "Магистр")
            {
                ReplaceTextAndSave("Магистр мукарари.docx", "1.docx", replacements);
                Dictionary<string, string> replacements1 = GenerateReplacementsDictionary(StudentGradeList1);
                ReplaceTextAndSave("1.docx", "1.docx", replacements1);
                string inputFilePath = @"1.docx";
               
                string selectedPrinter = ShowPrintDialog();
                PrintDocument(inputFilePath, selectedPrinter);
            }    
            else if (LevelBox.Text == "Специалист")
            {


                if (RowCount <= 65)
                {
                    ReplaceTextAndSave("Bakalavr1.docx", "1.docx", replacements);
                    Dictionary<string, string> replacements1 = GenerateReplacementsDictionary(StudentGradeList1);
                    ReplaceTextAndSave("1.docx", "1.docx", replacements1);
                    string inputFilePath = @"1.docx";
                    string selectedPrinter = ShowPrintDialog();
                    PrintDocument(inputFilePath, selectedPrinter);
                }
                else
                {
                    ReplaceTextAndSave("Bakalavr2.docx", "1.docx", replacements);
                    Dictionary<string, string> replacements1 = GenerateReplacementsDictionary(StudentGradeList1);
                    ReplaceTextAndSave("1.docx", "1.docx", replacements1);
                    string inputFilePath = @"1.docx";
                    string selectedPrinter = ShowPrintDialog();
                    PrintDocument(inputFilePath, selectedPrinter);
                }
            }
        }

        static string ShowPrintDialog()
        {
            System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog();
            if (printDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return printDialog.PrinterSettings.PrinterName;
            }
            return null;
        }
        static void PrintDocument(string filePath, string printerName)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    Verb = "printto",
                    FileName = filePath,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = true,
                    Arguments = "\"" + printerName + "\""
                };
                using (Process p = new Process { StartInfo = info })
                {
                    p.Start();
                    p.WaitForInputIdle();
                    System.Threading.Thread.Sleep(3000); // Подождите, чтобы убедиться, что процесс успешно запущен
                    if (p.CloseMainWindow() == false)
                        p.Kill(); // Если окно не закрыто, убиваем процесс
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при печати: " + ex.Message);
            }
        }
        public static void SetValuesInTextBoxesBakalavr(ToApproveForm window, RootObject1 studentGradeList)
        {
            
            int counter = 10;
            int totalRowCount = 10;
            int totalKolKr = 0;
            int totalkolKrRes = 0;


            foreach (var gpaData in studentGradeList.gpaDataList)
            {
                if (!string.IsNullOrEmpty(gpaData.lessonNameTj) || !string.IsNullOrEmpty(gpaData.lessonName))
                { 
                
                
                string keyTj = $"{counter}Tj";
                string keyRu = $"{counter}Ru";
                string keyB = $"{counter}B";
                string keyOT = $"{counter}OT";
                string keyOR = $"{counter}OR";
                string valueTj = gpaData.lessonNameTj ;
                string valueRu = gpaData.lessonName;
                string valueB = "";
                valueB = gpaData.credits;
                    // totalKolKr += Convert.ToInt32(vdStKrData.KolKr);
                    TextBox textBox = window.FindName("T" + counter) as TextBox;

                    if (textBox != null)
                    {
                        textBox.Text = valueTj + "\n" + valueRu;
                    }
                    else
                    {
                        Console.WriteLine($"TextBox с именем {"T" + counter} не найден");
                    }

                    TextBox textBox1 = window.FindName("B" + counter) as TextBox;

                    if (textBox1 != null)
                    {
                        textBox1.Text = gpaData.grades;
                    }
                    else
                    {
                        Console.WriteLine($"TextBox с именем {"B" + counter} не найден");
                    }


                    TextBox textBox2 = window.FindName("M" + counter) as TextBox;

                    if (textBox2 != null)
                    {
                        textBox2.Text = valueB;
                    }
                    else
                    {
                        Console.WriteLine($"TextBox с именем {"M" + counter} не найден");
                    }

                    counter++;
                }


                if (counter > 74)
                    break;
            }


            TextBox textBox3 = window.FindName("M75") as TextBox;

            if (textBox3 != null)
            {
                textBox3.Text = studentGradeList.Credits.ToString() + " / " + (studentGradeList.Credits * 24).ToString() + " ("+ (studentGradeList.Audits*24).ToString() + ")";
            }
            else
            {
                Console.WriteLine($"TextBox с именем {"T" + counter} не найден");
            }

            while (counter <= 74)
            {
                TextBox textBox = window.FindName("T" + counter) as TextBox;

                if (textBox != null)
                {
                    textBox.Text = " ";
                }
                else
                {
                    Console.WriteLine($"TextBox с именем {"T" + counter} не найден");
                }

                TextBox textBox1 = window.FindName("B" + counter) as TextBox;

                if (textBox1 != null)
                {
                    textBox1.Text = " ";
                }
                else
                {
                    Console.WriteLine($"TextBox с именем {"B" + counter} не найден");
                }


                TextBox textBox2 = window.FindName("M" + counter) as TextBox;

                if (textBox2 != null)
                {
                    textBox2.Text = " ";
                }
                else
                {
                    Console.WriteLine($"TextBox с именем {"M" + counter} не найден");
                }
                counter++;
            }

           
        }
        public static Dictionary<string, string> GenerateReplacementsDictionary(RootObject1 studentGradeList)
        {

            Dictionary<string, string> replacements = new Dictionary<string, string>();
            int counter = 10;
            int totalRowCount = 10;
            int totalKolKr = 0;
            int totalkolKrRes = 0;

            foreach (var gpaData in studentGradeList.gpaDataList)
            {
                if (!string.IsNullOrEmpty(gpaData.lessonNameTj) || !string.IsNullOrEmpty(gpaData.lessonName))
                {


                    string keyTj = $"{counter}Tj";
                    string keyRu = $"{counter}Ru";
                    string keyB = $"{counter}B";
                    string keyOT = $"{counter}OT";
                    string keyOR = $"{counter}OR";
                    string valueTj = gpaData.lessonNameTj;
                    string valueRu = gpaData.lessonName;
                    string valueB = "";
                    string valueOT = "";
                    string valueOR = "";
                    valueB = gpaData.credits;


                    string[] partsBeforeSlash = gpaData.grades.Split('(');

                    // Concatenate the parts before '/'
                    valueOT = partsBeforeSlash[0].Trim();

                    // Extract the part inside parentheses
                    //string insideParentheses = gpaData.grades.Split('(')[1].Split(')')[0];

                    // Concatenate the part inside parentheses

                    if (partsBeforeSlash.Length > 1)
                    {
                        valueOR = "(" + partsBeforeSlash[1].Trim();
                    }
                    else
                    {
                        // Handle the case where there is no element at index 1
                        // You can set a default value or handle it as needed
                        valueOR = "( "; // Or handle it differently based on your requirement
                    }


                   // valueOR = "("+ partsBeforeSlash[1].Trim();
                    replacements.Add(keyTj, valueTj);
                    replacements.Add(keyRu, valueRu);
                    replacements.Add(keyB, valueB);
                    replacements.Add(keyOT, valueOT);
                    replacements.Add(keyOR, valueOR);
                    counter++;
                }
                if (Convert.ToInt32(Application.Current.Properties["GradeRowCount"]) <= 65 & counter > 74)
                    break;

                if (Convert.ToInt32(Application.Current.Properties["GradeRowCount"]) > 65 & counter > 99)
                    break;
            }


            replacements.Add("OverFields", studentGradeList.Credits.ToString() + " / " + (studentGradeList.Credits*24).ToString()    + " (" + (studentGradeList.Audits * 24).ToString() +   ")");

            // Добавьте пробельные значения для оставшихся ключей, если totalRowCount не достигло значения counter
            while (counter <= 99)
            {
                string keyTj = $"{counter}Tj";
                string keyRu = $"{counter}Ru";
                string keyB = $"{counter}B";
                string keyOT = $"{counter}OT";
                string keyOR = $"{counter}OR";
                replacements.Add(keyTj, " ");
                replacements.Add(keyRu, " ");
                replacements.Add(keyB, " ");
                replacements.Add(keyOT, " ");
                replacements.Add(keyOR, " ");
                counter++;
            }

            return replacements;
        }
        private void CloseDiplomShow_Click(object sender, RoutedEventArgs e)
        {
            DiplomDoneGrid.Visibility = Visibility.Hidden;
        }

        private void ShowDiplom_Click(object sender, RoutedEventArgs e)
        {

            if (LevelBox.Text == "Бакалавр" )
            {
                DiplomSrollviewerBakalavr.Visibility = Visibility.Visible;
                DiplomSrollviewerMagistr.Visibility = Visibility.Hidden;
                BPhoto1.Visibility = Visibility.Visible;
                BPhoto2.Visibility = Visibility.Visible;
                Bphoto3.Visibility = Visibility.Visible;
                MuPhoto1.Visibility = Visibility.Hidden;
                MuPhoto2.Visibility = Visibility.Hidden;
                MuPhoto3.Visibility = Visibility.Hidden;
            }

            else if (LevelBox.Text == "Магистр")
            {
                DiplomSrollviewerBakalavr.Visibility = Visibility.Hidden;
                DiplomSrollviewerMagistr.Visibility = Visibility.Visible;
                BPhoto1.Visibility = Visibility.Hidden;
                BPhoto2.Visibility = Visibility.Hidden;
                Bphoto3.Visibility = Visibility.Hidden;
                MuPhoto1.Visibility = Visibility.Hidden;
                MuPhoto2.Visibility = Visibility.Hidden;
                MuPhoto3.Visibility = Visibility.Hidden;
            }
            else
            {
                DiplomSrollviewerBakalavr.Visibility = Visibility.Visible;
                DiplomSrollviewerMagistr.Visibility = Visibility.Hidden;
                BPhoto1.Visibility = Visibility.Hidden;
                BPhoto2.Visibility = Visibility.Hidden;
                Bphoto3.Visibility = Visibility.Hidden;
                MuPhoto1.Visibility = Visibility.Visible;
                MuPhoto2.Visibility = Visibility.Visible;
                MuPhoto3.Visibility = Visibility.Visible;
            }

            DiplomDoneGrid.Visibility = Visibility.Visible;
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private static readonly HttpClient client = new HttpClient();
        public static async Task<bool> AddDiplomAsync(UpdateDiplom update, string loginHeaderValue, string passwordHeaderValue, int Id)
        {
            try
            {
                string apiUrl = $"{Application.Current.Properties["UrlServer"].ToString()}/api/Authorize/UpdateDiplom?Login={loginHeaderValue}&Password={passwordHeaderValue}&Id={Id}";

                // Сериализация объекта в JSON
                string jsonData = JsonConvert.SerializeObject(update);

                // Создание контента для POST-запроса
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Отправка POST-запроса
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Проверка успешности запроса
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine($"Ошибка: {ex.Message}");
                return false;
            }
        }





        private async void ApproveDiplom_ClickAsync(object sender, RoutedEventArgs e)
        {

            var userData = new UpdateDiplom
            {
                Comment = " ",
                Status = "Accepted",

            };
            // Логин и пароль для URL
            string loginHeaderValue = (string)Application.Current.Properties["Login"];
            string passwordHeaderValue = (string)Application.Current.Properties["Password"];
            int idHeaderValue = Convert.ToInt32(Application.Current.Properties["SelectedDiplomId"]);

            bool isSuccess = await AddDiplomAsync(userData, loginHeaderValue, passwordHeaderValue, idHeaderValue);

            if (isSuccess)
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
                Console.WriteLine("Ошибка при изменние Диплома.");
            }
        }



        private async void RejectButtonPopup_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (CommentBoxPopUp.Text != "")
            {
                var userData = new UpdateDiplom
                {
                    Comment = CommentBoxPopUp.Text,
                    Status = "Rejected",

                };
                // Логин и пароль для URL
                string loginHeaderValue = (string)Application.Current.Properties["Login"];
                string passwordHeaderValue = (string)Application.Current.Properties["Password"];
                int idHeaderValue = Convert.ToInt32(Application.Current.Properties["SelectedDiplomId"]);

                bool isSuccess = await AddDiplomAsync(userData, loginHeaderValue, passwordHeaderValue, idHeaderValue);

                if (isSuccess)
                {
                    Desktop desktop = new Desktop();
                    desktop.Show();
                    GridComment.Visibility = Visibility.Hidden;
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
                    Console.WriteLine("Ошибка при изменние Диплома.");
                }
            }
        }

        private void CencelDiplom_Click(object sender, RoutedEventArgs e)
        {
            GridComment.Visibility = Visibility.Visible;
            CommentHeader1.Content = "Причина отклонения";
            CommentHeader2.Content = "Опишите проблему";
            RejectButtonPopup.Visibility = Visibility.Visible;
            RevisionButtonPopUp.Visibility = Visibility.Hidden;
        }

        private void ClosePopUpComment_Click(object sender, RoutedEventArgs e)
        {
            Desktop desktop = new Desktop();
            desktop.Show();
            GridComment.Visibility = Visibility.Hidden;
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOutAnimation.Completed += (animationSender, animationEventArgs) => { this.Close(); };

            this.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void RevisionDiplom_Click(object sender, RoutedEventArgs e)
        {
            GridComment.Visibility = Visibility.Visible;
            CommentHeader1.Content = "Причина";
            CommentHeader2.Content = "Опиcание ошибок";
            RejectButtonPopup.Visibility = Visibility.Hidden;
            RevisionButtonPopUp.Visibility = Visibility.Visible;
        }

        private async void RevisionButtonPopUp_ClickAsync(object sender, RoutedEventArgs e)
        {

            if (CommentBoxPopUp.Text != "")
            {
                var userData = new UpdateDiplom
                {
                    Comment = CommentBoxPopUp.Text,
                    Status = "ForFinishing",
                    StatusResponse = "ForFinishing"
                };
                // Логин и пароль для URL
                string loginHeaderValue = (string)Application.Current.Properties["Login"];
                string passwordHeaderValue = (string)Application.Current.Properties["Password"];
                int idHeaderValue = Convert.ToInt32(Application.Current.Properties["SelectedDiplomId"]);

                bool isSuccess = await AddDiplomAsync(userData, loginHeaderValue, passwordHeaderValue, idHeaderValue);

                if (isSuccess)
                {
                    Desktop desktop = new Desktop();
                    desktop.Show();
                    GridComment.Visibility = Visibility.Hidden;
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
                    Console.WriteLine("Ошибка при изменние Диплома.");
                }
            }
        }

        private void SurnameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LevelBox_TextChanged(object sender, TextChangedEventArgs e)
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

        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TajikButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PatronimicBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }




    
        

        private async void Yes_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["statusFieldsResponse"].ToString() != "Printed")
            {
                var userData = new UpdateDiplom
                {
                    Comment = " ",
                    StatusResponse = "Printed"

                };
                // Логин и пароль для URL
                string loginHeaderValue = (string)Application.Current.Properties["Login"];
                string passwordHeaderValue = (string)Application.Current.Properties["Password"];
                int idHeaderValue = Convert.ToInt32(Application.Current.Properties["SelectedDiplomId"]);

                bool isSuccess = await AddDiplomAsync(userData, loginHeaderValue, passwordHeaderValue, idHeaderValue);

                if (isSuccess)
                {
                    DiplomDoneGrid.Visibility = Visibility.Hidden;
                    PrintDiplom.Visibility = Visibility.Hidden;
                    AreYouShure.Visibility = Visibility.Hidden;
                    PrintDone.Visibility = Visibility.Hidden;

                }
                else
                {
                    Console.WriteLine("Ошибка при изменние Диплома.");
                }
            }
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            AreYouShure.Visibility = Visibility.Hidden;
        }

        private void PrintDone_Click_1(object sender, RoutedEventArgs e)
        {

            AreYouShure.Visibility = Visibility.Visible;
        }

        private void GiveDiplom_Click(object sender, RoutedEventArgs e)
        {
            



            GiveDiplomGrid.Visibility = Visibility.Visible;
        }

        private void PrintCertificate_Click(object sender, RoutedEventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // Обновление макета Grid
                CertificateGrid.UpdateLayout();

                // Создайте DrawingVisual для рисования содержимого Grid
                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext dc = visual.RenderOpen())
                {
                    VisualBrush visualBrush = new VisualBrush(CertificateGrid);
                    dc.DrawRectangle(visualBrush, null, new Rect(new Point(0, 0), new Size(CertificateGrid.ActualWidth, CertificateGrid.ActualHeight)));
                }

                // Печать
                printDialog.PrintVisual(visual, "Печать Grid");
            }

        }

        private async void CertificateDone_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["statusFieldsResponse"].ToString() != "Removed")
            {
                var userData = new UpdateDiplom
                {
                    Comment = " ",
                    StatusResponse = "Removed"

                };
                // Логин и пароль для URL
                string loginHeaderValue = (string)Application.Current.Properties["Login"];
                string passwordHeaderValue = (string)Application.Current.Properties["Password"];
                int idHeaderValue = Convert.ToInt32(Application.Current.Properties["SelectedDiplomId"]);

                bool isSuccess = await AddDiplomAsync(userData, loginHeaderValue, passwordHeaderValue, idHeaderValue);

                if (isSuccess)
                {
                    GiveDiplom.Visibility = Visibility.Hidden;
                    GiveDiplomGrid.Visibility = Visibility.Hidden;

                }
                else
                {
                    Console.WriteLine("Ошибка при изменние Диплома.");
                }
            }
        }

        private void Birthdatebox_TextChanged(object sender, TextChangedEventArgs e)
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
        private async void EditGrade_ClickAsync(object sender, RoutedEventArgs e)
        {
            string test = Application.Current.Properties["SelectedDiplomId"].ToString();
            RootObject1 StudentGradeList = await GetStudentGradeByIdAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentGrade1", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Application.Current.Properties["SelectedDiplomId"].ToString(), Application.Current.Properties["SelectedDiplomId"].ToString());


            DiplomGradeGrid.Visibility = Visibility.Visible;

            try
            {

                CreditsResponseBox.Text = await GetCreditsResponseAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentsGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Application.Current.Properties["SelectedDiplomId"].ToString());


                List<AddStudentGrade> GradeResponseData = await GetStudentGradeAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentsGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Application.Current.Properties["SelectedDiplomId"].ToString());
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

        private async void Done_Click(object sender, RoutedEventArgs e)
        {
            DiplomGradeGrid.Visibility = Visibility.Hidden;
        }

        private void Save_ClickAsync(object sender, RoutedEventArgs e)
        {
            GradeGrid.Visibility = Visibility.Visible;
            GradeId.Text = "";

        }

        private void StudentId_KeyDownAsync(object sender, KeyEventArgs e)
        {

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



        public static async Task<bool> UpdateStudentFieldsAsync(acceptDiplom studentFields, string loginHeaderValue, string passwordHeaderValue, int StudentId)
        {
            string UrlResp = Application.Current.Properties["UrlServer"].ToString();
            try
            {
                string apiUrl = $"{UrlResp}/api/Authorize/UpdateStudentFields?Login={loginHeaderValue}&Password={passwordHeaderValue}&Id={StudentId}";
                string jsonData = JsonConvert.SerializeObject(studentFields);
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


            else if (Convert.ToDecimal(GradeStudText.Text) >= 60 & Convert.ToDecimal(GradeStudText.Text) <= 64.99m)
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
                studentId = Application.Current.Properties["SelectedDiplomId"].ToString(),
            };

            // Логин и пароль для URL
            string loginHeaderValue = (string)Application.Current.Properties["Login"];
            string passwordHeaderValue = (string)Application.Current.Properties["Password"];

            // Вызов метода для отправки POST-запроса с параметрами в URL
            bool isSuccess = await AddGradeStudentAsync(userData, loginHeaderValue, passwordHeaderValue);




            try
            {
                CreditsResponseBox.Text = await GetCreditsResponseAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentsGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Application.Current.Properties["SelectedDiplomId"].ToString());
                List<AddStudentGrade> GradeResponseData = await GetStudentGradeAsync("" + Application.Current.Properties["UrlServer"].ToString() + "/api/Authorize/GetStudentsGrade", Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Application.Current.Properties["SelectedDiplomId"].ToString());
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

        private void StudentId_TextChangedAsync(object sender, TextChangedEventArgs e)
        {

        }

        private void Save_Copy_Click(object sender, RoutedEventArgs e)
        {
            GradeGrid.Visibility = Visibility.Hidden;
        }

        static async Task<string> GetApiResponseAsync( string Login, string Password, string StudentId)
        {

            string url = Application.Current.Properties["UrlServer"].ToString()+"/api/Authorize/GetDiplomById?Login="+ Login + "&Password="+ Password + "&id=" + StudentId;
            // Создание HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Отправка запроса и получение ответа
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Проверка успешности выполнения запроса
                    response.EnsureSuccessStatusCode();

                    // Чтение содержимого ответа как строку
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    // Обработка исключений
                    throw new Exception($"Request error: {e.Message}");
                }
            }
        }


        private async void EditStudentВetails_ClickAsync(object sender, RoutedEventArgs e)
        {
            DiplomStudentFields.Visibility = Visibility.Visible;

            try
            {
                // Вызов функции для получения ответа
                string responseBody = await GetApiResponseAsync(Application.Current.Properties["Login"].ToString(), Application.Current.Properties["Password"].ToString(), Application.Current.Properties["SelectedDiplomId"].ToString());

                var diplom = JsonConvert.DeserializeObject<acceptDiplom>(responseBody);              
                string firstNameTj = "";
                string middleNameTj = "";
                string lastNameTj = "";
                string firstNameRu = "";
                string middleNameRu = "";
                string lastNameRu = "";
                // Проверяем, что у нас достаточно слов
                string[] FioTj = diplom.FioStudentTj.Split(' ');
                string[] FioRu = diplom.FioStudentRu.Split(' ');
                if (FioTj.Length >= 3)
                {
                    // Присваиваем слова отдельным переменным
                    firstNameTj = FioTj[0];
                    middleNameTj = FioTj[1];
                    lastNameTj = FioTj[2];
                }
                else if (FioTj.Length >= 2)
                {
                    firstNameTj = FioTj[0];
                    middleNameTj = FioTj[1];
                }
                else
                {
                    Console.WriteLine("Строка не содержит достаточно слов.");
                }

                if (FioRu.Length >= 3)
                {
                    // Присваиваем слова отдельным переменным
                    firstNameRu = FioRu[0];
                    middleNameRu = FioRu[1];
                    lastNameRu = FioRu[2];
                }
                else if (FioRu.Length >= 2)
                {
                    firstNameRu = FioRu[0];
                    middleNameRu = FioRu[1];
                }
                else
                {
                    Console.WriteLine("Строка не содержит достаточно слов.");
                }

                SurnameBoxRus.Text =  firstNameRu;
                SurnameBoxTaj.Text = firstNameTj;
                NameBoxRus.Text = middleNameRu;
                NameBoxTaj.Text =  middleNameTj;
                PatronimicBoxRus.Text = lastNameRu;
                PatronimicBoxTaj.Text = lastNameRu;
                BirthdateboxStud.Text = diplom.Birthdate;
                INNBoxStud.Text = diplom.inn;
                LevelBoxRus.Text = diplom.StudyLevel;
                SpecialityBoxRus.Text = diplom.Speciality;
                SpecialityBoxTaj.Text = diplom.SpecialityTj;
                QualificationBoxRus.Text = diplom.QualificationRu;
                QualificationBoxTaj.Text = diplom.QualificationTj;
                DirectionBoxRus.Text = diplom.Direction;
                DirectionBoxTaj.Text = diplom.DirectionTj;
                StudyFormBoxRus.Text = diplom.TrainingForm;
                StudyFormBoxTaj.Text = diplom.TrainingFormTj;
                InstitutionsIssuedDiplomBoxRus.Text = diplom.Donishgoh;
                InstitutionsIssuedDiplomBoxTaj.Text = diplom.DonishgohTj;
                DiplomCreatedDateBoxStud.Text = diplom.CreatedDiplomDate.ToString();
                DocumentStudyBoxStud.Text = diplom.Document;
                StudyProgramLevelBoxRus.Text = diplom.TermStudy;
                StudyProgramLevelBoxTaj.Text = diplom.TermStudyTj;
                TypeDiplomBoxStud.Text = diplom.TypeDiplom;
                DescStudyBlockRus.Text = diplom.DescRu;
                DescStudyBlockTaj.Text = diplom.DescTj;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }



        }

        private void CencelStudent_Click(object sender, RoutedEventArgs e)
        {
            DiplomStudentFields.Visibility = Visibility.Hidden;
        }

        private async void SaveStudent_ClickAsync(object sender, RoutedEventArgs e)
        {
            var userData = new acceptDiplom
            {
                FioStudentRu = SurnameBoxRus.Text + " " + NameBoxRus.Text + " " + PatronimicBoxRus.Text,
                FioStudentTj = SurnameBoxTaj.Text + " " + NameBoxTaj.Text + " " + PatronimicBoxTaj.Text,
                Birthdate = BirthdateboxStud.Text,
                inn = INNBoxStud.Text,
                StudyLevel = LevelBoxRus.Text,
                Speciality = SpecialityBoxRus.Text,
                SpecialityTj = SpecialityBoxTaj.Text,
                QualificationRu = QualificationBoxRus.Text,
                QualificationTj = QualificationBoxTaj.Text,
                Direction = DirectionBoxRus.Text,
                DirectionTj = DirectionBoxTaj.Text,
                TrainingForm = StudyFormBoxRus.Text,
                TrainingFormTj = StudyFormBoxTaj.Text,
                Donishgoh = InstitutionsIssuedDiplomBoxRus.Text,
                DonishgohTj = InstitutionsIssuedDiplomBoxTaj.Text,
                Document = DocumentStudyBoxStud.Text,
                TermStudy = StudyProgramLevelBoxRus.Text,
                TermStudyTj = StudyProgramLevelBoxTaj.Text,
                TypeDiplom = TypeDiplomBoxStud.Text,
                DescRu = DescStudyBlockRus.Text,
                DescTj = DescStudyBlockTaj.Text
            };

            // Логин и пароль для URL
            string loginHeaderValue = (string)Application.Current.Properties["Login"];
            string passwordHeaderValue = (string)Application.Current.Properties["Password"];

            // Вызов метода для отправки POST-запроса с параметрами в URL
            bool isSuccess = await UpdateStudentFieldsAsync(userData, loginHeaderValue, passwordHeaderValue, Convert.ToInt32(Application.Current.Properties["SelectedDiplomId"]));

            if (isSuccess == true)
            {
                DiplomStudentFields.Visibility = Visibility.Hidden;
            }

        }
    }
}
