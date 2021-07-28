using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.Medications
{
    public class MedicationCreate
    {
        [Required(ErrorMessage = "Enter Medicine Code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Enter Medicine Display Name")]
        public string Display { get; set; }
    }
}
