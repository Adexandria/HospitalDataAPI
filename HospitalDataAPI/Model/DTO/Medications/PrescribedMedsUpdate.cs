using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.Medications
{
    public class PrescribedMedsUpdate
    {
        public Guid PrescribedId { get; set; }

        ///<example>0001-01-01</example>
        public DateTime Date { get; set; }
        ///<example>active</example>
        public string Status { get; set; }
        public string Prescriber { get; set; }
        public int MedicationId { get; set; }
    }
}
