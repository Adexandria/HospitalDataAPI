using System;


namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabTestUpdate
    {
        public Guid TestId { get; set; }

        ///<example>pending</example>
        public string Status { get; set; }    
    }
}
