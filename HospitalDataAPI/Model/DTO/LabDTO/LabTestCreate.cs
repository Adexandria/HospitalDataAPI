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


    }
}
