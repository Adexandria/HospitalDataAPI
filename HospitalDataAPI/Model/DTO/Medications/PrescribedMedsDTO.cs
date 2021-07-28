using HospitalDataAPI.Model.MedicationModel;
using HospitalDataAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.Medications
{
    public class PrescribedMedsDTO
    {
        public Guid PrescribedId { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Prescriber { get; set; }
        public Medication Medication { get; set; }
        public Patient Patient { get; set; }

    }
}
