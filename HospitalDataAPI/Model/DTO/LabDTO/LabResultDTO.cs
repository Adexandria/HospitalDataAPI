using System;


namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabResultDTO
    {
        public Guid ResultId { get; set; }
        public string Status { get; set; }
        public  CodingDTO Code { get; set; }
        public string ResultValue { get; set; }
        public string ReferenceRange { get; set; } 
        public string ResultTime { get; set; }
        public  CategoryDTO Category { get; set; }
        public LabTestDTO LabTest { get; set; }
    }
}
