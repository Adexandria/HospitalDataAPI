using HospitalDataAPI.Model.DTO.Patients;
using System;


namespace HospitalDataAPI.Model.DTO.Medications
{
    public class PrescribedMedsDTO
    {
        public Guid PrescribedId { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Prescriber { get; set; }
        public MedicationDTO Medication { get; set; }
        public PatientDTO Patient { get; set; }

    }
}
