using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabResultCreate
    {
        ///<example>pending</example>
        [Required(ErrorMessage ="Enter Final/Pending")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Enter result Value")]
        public string ResultValue { get; set; }
        ///<example>5.0-8.0</example>
        [Required(ErrorMessage = "Enter reference range")]
        public string ReferenceRange { get; set; }
        [Required(ErrorMessage = "Enter Test Id")]
        public Guid TestId { get; set; }


    }
}
