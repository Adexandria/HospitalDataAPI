using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabTestCreate
    {
        [Required(ErrorMessage ="Enter Final/Pending")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Enter measured time (yyyy/mm/dd)")]
        public DateTime MeasuredTime { get; set; }
        [Required(ErrorMessage = "Enter reported time (yyyy/mm/dd")]
        public DateTime ReportedDate { get; set; }

    }
}
