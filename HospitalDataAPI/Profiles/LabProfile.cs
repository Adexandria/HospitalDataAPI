using AutoMapper;
using HospitalDataAPI.Model.DTO.LabDTO;
using HospitalDataAPI.Model.LabModel;

namespace HospitalDataAPI.Profiles
{
    public class LabProfile:Profile
    {
        public LabProfile()
        {
            CreateMap<LabTest, LabTestDTO>()
            .ForMember(s => s.Status, opt => opt.MapFrom(s => s.Status.ToString()));
            CreateMap<LabTestCreate, LabTest>();
            CreateMap<LabTestUpdate, LabTest>();

            CreateMap<LabResult, LabResultDTO>()
                .ForMember(s => s.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(s => s.ResultTime, opt => opt.MapFrom(s => s.ResultTime.Date.ToString()));
            CreateMap<LabResultCreate, LabResult>();
            CreateMap<LabResultUpdate, LabResult>();

            CreateMap<Coding, CodingDTO>();
            CreateMap<Category, CategoryDTO>();

        }
    }
}
