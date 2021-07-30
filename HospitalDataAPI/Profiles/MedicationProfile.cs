using AutoMapper;
using HospitalDataAPI.Model.DTO.Medications;
using HospitalDataAPI.Model.MedicationModel;

namespace HospitalDataAPI.Profiles
{
    public class MedicationProfile :Profile
    {
        public MedicationProfile()
        {
            CreateMap<PrescribedMedication, PrescribedMedsDTO>()
                .ForMember(s => s.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(s => s.Date, opt => opt.MapFrom(s => s.Date.Date.ToString()));
            CreateMap<PrescribedMedsUpdate, PrescribedMedication>();
            CreateMap<PrescribedMedsCreate, PrescribedMedication>();
            //CreateMap<PrescribedMedication,PrescribedMedication>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Medication, MedicationDTO>();
            CreateMap<MedicationCreate, Medication>();
            CreateMap<MedicationUpdate, Medication>();
        }
    }
}
