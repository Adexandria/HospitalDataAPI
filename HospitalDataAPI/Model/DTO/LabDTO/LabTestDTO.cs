using HospitalDataAPI.Model.LabModel;
using HospitalDataAPI.Model.PatientModel;
using System;

namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabTestDTO
    {
        public Guid TestId { get; set; }
        public string Status { get; set; }
        public Coding Code { get; set; }
        public string MeasuredTime { get; set; }
        public Category Category { get; set; }
        public string ReportedDate { get; set; }
        public Patient Patient { get; set; }
    }
}
