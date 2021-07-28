using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.Medications
{
    public class PrescribedMedsCreate
    {
        [Required(ErrorMessage ="Enter Date(yyyy/mm/dd)")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Enter Final/Pending")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Enter Presciber")]
        public string Prescriber { get; set; }
       
    }
}
