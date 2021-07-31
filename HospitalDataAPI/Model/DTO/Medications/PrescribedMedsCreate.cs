using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.Medications
{
    public class PrescribedMedsCreate
    {
        ///<example>0001-01-01</example>
        [Required(ErrorMessage ="Enter Date(yyyy/mm/dd)")]
        public DateTime Date { get; set; }
        ///<example>active</example>
        [Required(ErrorMessage = "Enter Active/InActive")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Enter Presciber")]
        public string Prescriber { get; set; }
        [Required(ErrorMessage = "Enter MedicationId")]
        public int MedicationId { get; set; }

    }
}
