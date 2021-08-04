using System;

namespace HospitalDataAPI.Model.DTO.Medications
{
    public class PrescribedMedsUpdate
    {
        public Guid PrescribedId { get; set; }
        ///<example>active</example>
        public string Status { get; set; }
        public string Prescriber { get; set; }
        public int MedicationId { get; set; }
    }
}
