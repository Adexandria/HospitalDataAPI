using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.Medications
{
    public class MedicationUpdate
    {
        public int MedicationId { get; set; }
        public string Code { get; set; }
        public string Display { get; set; }
    }
}
