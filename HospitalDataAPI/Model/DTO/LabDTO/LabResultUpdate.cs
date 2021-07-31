using System;


namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabResultUpdate
    {
        public Guid ResultId { get; set; }
        ///<example>pending</example>
        public string Status { get; set; }
        public string ResultValue { get; set; }
        public string ReferenceRange { get; set; }
    }

}
