using System;
using System.Collections.Generic;
using System.Text;
using static Donishgoh.Desktop;

namespace Donishgoh.Models
{
    
        public class Authorize
        {
            public int Id { get; set; }
        public string Fio { get; set; }
        public string Roles { get; set; }
        public string Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Birthdate { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }


    public class DiplomResponseData
    {
        public int TotalCount { get; set; }
        public int TotalInProgress { get; set; }
        public int TotalAccepted { get; set; }
        public int TotalPrinted { get; set; }
        public int TotalForFinishing { get; set; }
        public int TotalRejected { get; set; }
        public List<DiplomList> diplomList { get; set; }
    }

    public class DiplomList
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public string EducationLevel { get; set; }
        public string Speciality { get; set; }
        public string Qualification { get; set; }
        public string Direction { get; set; }
        public string ApprovalDate { get; set; }
        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = StatusHelper.GetStatusText(value); }
        }
    }
    public class Person
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Position { get; set; }
        public int No_Approval { get; set; }
        public string Roles { get; set; }

    }

    public class Speciality
    {
        public string ksp { get; set; }
        public int kdn { get; set; }
        public string nsp { get; set; }
        public string nspt { get; set; }
    }

    public class Fakultet
    {
        public int kdn { get; set; }
        public string nfk { get; set; }
        public string nfkt { get; set; }
    }

    public class StudentViewModel
    {
        public int kdn { get; set; }
        public int kfk { get; set; }
        public int ksp { get; set; }
        public int kkr { get; set; }
        public int kgr { get; set; }
        public string nkart { get; set; }
        public string kzc { get; set; }
        public string nst { get; set; }
        public string nstt { get; set; }
        public string pol { get; set; }
        public string fin { get; set; }
        public Fakultet fakultet { get; set; }
        public Speciality speciality { get; set; }
        public string StudDuble { get; set; }
    }

    public class acceptDiplom
    {
        public int Id { get; set; }
        public string FioStudentTj { get; set; }
        public string FioStudentRu { get; set; }
        public int StudentId { get; set; }
        public string Birthdate { get; set; }
        public string StudyLevel { get; set; }
        public string Speciality { get; set; }
        public string SpecialityTj { get; set; }
        public string QualificationTj { get; set; }
        public string QualificationRu { get; set; }
        public string Direction { get; set; }
        public string DirectionTj { get; set; }
        public string TrainingForm { get; set; }
        public string TrainingFormTj { get; set; }
        public string Donishgoh { get; set; }
        public string DonishgohTj { get; set; }
        public string inn { get; set; }
        public DateTime CreatedDiplomDate { get; set; }
        public string TermStudy { get; set; }
        public string TermStudyTj { get; set; }
        public string Document { get; set; }
        public string Firstapp { get; set; }
        public string Secondapp { get; set; }
        public string Thirdapp { get; set; }
        public string Fourthapp { get; set; }
        public string Comment { get; set; }
        public string TypeDiplom { get; set; }
        public string DescRu { get; set; }
        public string DescTj { get; set; }
        public string CommissionDate { get; set; }
        public string FirstapprovalStatus { get; set; }
        public string SecondapprovalStatus { get; set; }
        public string ThirdapprovalStatus { get; set; }
        public string FourthapprovalStatus { get; set; }
        public string Created { get; set; }
        public string Status { get; set; }
        public string Fakulty { get; set; }
        public string DiplomCode { get; set; }
        public string FakultyCode { get; set; }
        public string Dublicate { get; set; }
        public string Language { get; set; }

    }


   

    public class UpdateDiplom
    {
        public string Comment { get; set; }
        public string Status { get; set; }
        public string StatusResponse { get; set; }

    }



    public class AddStudentGrade
    {
        public int id { get; set; }
        public string lessonName { get; set; }
        public string lessonNameTj { get; set; } 
        public string credits { get; set; }
        public string grades { get; set; }
        public string studentId { get; set; }

    }

    public class StudentGradesResponse
    {
        public string CreditsResponse { get; set; }
        public List<AddStudentGrade> Grades { get; set; }
    }


    public class Prk
    {
        public int Kdn { get; set; }
        public string Npr { get; set; }
        public string Nprt { get; set; }
        // Добавьте остальные свойства согласно вашему запросу
    }

    public class ReportItem
    {
        public int Id { get; set; }
        public string TargetSpeciality { get; set; }
        public int CountRedDiplomBakalavr { get; set; }
        public int CountBlueDiplomBakalavr { get; set; }
        public int CountRedDiplomMagistr { get; set; }
        public int CountBlueDiplomMagistr { get; set; }
        public int CountRedDiplomSpecialist { get; set; }
        public int CountBlueDiplomSpecialist { get; set; }
        public int Count { get; set; }
    }

   

    public class LogsDiplom
    {
        public string FIO { get; set; }
        public string EventDiplom { get; set; }
        public string CreatedDate { get; set; }
    }

    public class LogsResponseData
    {
        public int TotalCount { get; set; }
        public List<LogsDiplom> LogsResponse { get; set; }
    }

    public class AddSetting
    {
        public string SpecialityRu { get; set; }
        public string DirectionRu { get; set; }
        public string QualificationRu { get; set; }
        public string LevelofEducationRu { get; set; }
    }


    public class AddSettingResponse
    {
        public int Id { get; set; }
        public string SpecialityRu { get; set; }
        public string SpecialityTj { get; set; }
        public string DescEducProgRu { get; set; }
        public string DescEducProgTj { get; set; }
        public string DirectionRu { get; set; }
        public string DirectionTj { get; set; }
        public string QualificationRu { get; set; }
        public string QualificationTj { get; set; }
        public string LevelofEducationRu { get; set; }
        public string LevelofEducationTj { get; set; }
        public string FakultyRu { get; set; }
        public string FakultyTj { get; set; }
        public string StudyPeriodRu { get; set; }
        public string StudyPeriodTj { get; set; }
        public string TypeEducationRu { get; set; }
        public string TypeEducationTj { get; set; }
        public string Dekan { get; set; }
        public string Secretar { get; set; }
    }

    public class AddFakultyResponse
    {
    

        public int Id { get; set; }
        public string FakultyRu { get; set; }
        public string FakultyTj { get; set; }

    }
    public class AddOtherSettingResponse
    {
        public string ProfeccionalStatusRu { get; set; }
        public string ProfeccionalStatusTj { get; set; }
        public string InformEducSysRu { get; set; }
        public string InformEducSysTj { get; set; }
        public string AdditionalInformation { get; set; }
        public string OpportunityTrainingRu { get; set; }
        public string OpportunityTrainingTj { get; set; }
        public string Rektor { get; set; }
        public int DiplomeCode { get; set; }
    }


    public class VdStKrData
    {
        public int Kdn { get; set; }
        public int Kvd { get; set; }
        public int Kst { get; set; }
        public string KolKr { get; set; }
        public string chrUchGod { get; set; }
        public string itoceKr { get; set; }
        public decimal itoceBlKr { get; set; }
        public decimal itoceBl { get; set; }
        public string NameLessonRusian { get; set; }
        public string NameLessonTajik { get; set; }
        public int CountOccurrences { get; set; }

    }

    public class gpaData
    {
        public int Id { get; set; }
        public int KKr { get; set; }
        public int KGr { get; set; }
        public int Kst { get; set; }
        public string NSm { get; set; }
        public string chrUchGod { get; set; }
        public string language { get; set; }
        //public List<VdStKrData> VdStKrDataList { get; set; }
    }

    public class gpaDatalist
    {
        public gpaData gpaData { get; set; }
        public List<VdStKrData> VdStKrDataList { get; set; }
    }

    public class RootObject1
    {
        public int RowCount { get; set; }
        public int Result { get; set; }
        public int Audits { get; set; }
        public int Credits { get; set; }

        
        public List<AddStudentGrade> gpaDataList { get; set; }


        // public List<VdStKrData> vdStKrDataList { get; set; }
    }

    public class RootObject
    {
        public int Result { get; set; }
        public List<gpaDatalist> gpaDataList { get; set; }
       // public List<VdStKrData> vdStKrDataList { get; set; }
    }
}
