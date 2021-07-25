using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalDataAPI.Model.PatientModel;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.MedicationModel
{
    public class PrescribedMedication
    {
        [Key]
        public Guid MedationId { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public string Prescriber { get; set; }
        public Medication Medication { get; set; }
        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
