using HospitalDataAPI.Model.MedicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Service
{
    public interface IMedication
    {
        //PrescribedMedication
        IEnumerable<PrescribedMedication> GetMedicationsById(Guid patientId);
        Task<PrescribedMedication> AddMedicationById(Guid patientId);
        Task<PrescribedMedication> UpdateMedicationById(Guid patientId);

        //Medication
        IEnumerable<Medication> GetMedications { get; }
        Task<Medication> GetMedicationByName(string name);
        Task<Medication> AddMedication(Medication newMedication);
        Task<Medication> UpdateMedication(Medication updateMedication);

    }
}
