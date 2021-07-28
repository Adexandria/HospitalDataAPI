using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabResultCreate
    {
        [Required(ErrorMessage ="Enter Final/Pending")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Enter result Value")]
        public string ResultValue { get; set; }
        [Required(ErrorMessage = "Enter reference range")]
        public string ReferenceRange { get; set; }
        [Required(ErrorMessage = "Enter result time (yyyy/mm/dd)")]
        public DateTime ResultTime { get; set; }
 
    }
}
