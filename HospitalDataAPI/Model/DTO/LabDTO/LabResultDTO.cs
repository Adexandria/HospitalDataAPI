using HospitalDataAPI.Model.LabModel;
using HospitalDataAPI.Model.PatientModel;
using System;


namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabResultDTO
    {
        public Guid ResultId { get; set; }
        public string Status { get; set; }
        public  Coding Code { get; set; }
        public string ResultValue { get; set; }
        public string ReferenceRange { get; set; } 
        public string ResultTime { get; set; }
        public  Category Category { get; set; }
        public LabTest LabTest { get; set; }
        public Patient Patient { get; set; }
    }
}
