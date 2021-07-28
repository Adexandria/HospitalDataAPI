using AutoMapper;
using HospitalDataAPI.Model.DTO.Medications;
using HospitalDataAPI.Model.MedicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}
