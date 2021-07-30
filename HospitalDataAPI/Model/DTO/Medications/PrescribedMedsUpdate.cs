using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.Medications
{
    public class PrescribedMedsUpdate
    {
        public Guid PrescribedId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = "Active";
        public string Prescriber { get; set; }
        public int MedicationId { get; set; }
    }
}
