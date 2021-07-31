using HospitalDataAPI.Model.DTO.Patients;
using System;

namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabTestDTO
    {
        public Guid TestId { get; set; }
        public string Status { get; set; }
        public CodingDTO Code { get; set; }
        public CategoryDTO Category { get; set; }
        public string ReportedDate { get; set; }
        public PatientDTO Patient { get; set; }
    }
}
