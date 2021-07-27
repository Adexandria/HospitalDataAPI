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
        Task AddMedicationById(Guid patientId,PrescribedMedication newPrescribed);
        Task<PrescribedMedication> UpdateMedicationById(Guid patientId,PrescribedMedication updatePrescribed);

        //Medication
        IEnumerable<Medication> GetMedications { get; }
        IEnumerable<Medication> GetMedicationByName(string name);
        Task AddMedication(Medication newMedication);
        Task<Medication> UpdateMedication(Medication updateMedication,int medicationId);

    }
}
