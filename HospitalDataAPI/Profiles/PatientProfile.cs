using AutoMapper;
using HospitalDataAPI.Model.DTO.Patients;
using HospitalDataAPI.Model.PatientModel;
using HospitalDataAPI.Service;


namespace HospitalDataAPI.Profiles
{
    public class PatientProfile :Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientDTO>()
                .ForMember(s => s.Age, opt => opt.MapFrom(s => s.DateOfBirth.GetCurrentAge()))
                .ForMember(s => s.BirthSex, opt => opt.MapFrom(s => s.BirthSex.ToString()))
                .ForMember(s => s.Race, opt => opt.MapFrom(s => s.Race))
                .ForMember(s => s.MaritalStatus, opt => opt.MapFrom(s => s.MaritalStatus.ToString()))
                .ForMember(s=>s.Gender,opt=>opt.MapFrom(s=>s.Gender.ToString()));
            CreateMap<Patient, PatientsDTO>()
                .ForMember(s => s.Name, opt => opt.MapFrom(s => $"{s.FirstName } {s.MiddleName} {s.LastName}"));
            CreateMap<PatientCreate, Patient>();
            CreateMap<PatientUpdate, Patient>();
        }
    }
}
