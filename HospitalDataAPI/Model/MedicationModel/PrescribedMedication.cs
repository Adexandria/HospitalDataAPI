using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalDataAPI.Model.PatientModel;


namespace HospitalDataAPI.Model.MedicationModel
{
    public class PrescribedMedication
    {
        [Key]
        public Guid PrescribedId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public MedicationStatus Status { get; set; } = MedicationStatus.Active;
        public string Prescriber { get; set; }
        [ForeignKey("Medication")]
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
        public  Patient Patient { get; set; }
    }
}
