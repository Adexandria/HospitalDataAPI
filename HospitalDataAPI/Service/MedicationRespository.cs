using HospitalDataAPI.Model.MedicationModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Service
{
    public class MedicationRespository : IMedication
    {
        readonly DataContext dataDb;
        public MedicationRespository(DataContext dataDb)
        {
            this.dataDb = dataDb;
        }
        //Prescribed Medication

        public IEnumerable<PrescribedMedication> GetMedicationsById(Guid patientId)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));

                var prescribed = dataDb.PrescribedMedication.Where(s => s.PatientId == patientId);
                if (prescribed == null) throw new NullReferenceException(nameof(prescribed));
                return prescribed;
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }
        public async Task AddMedicationById(Guid patientId, PrescribedMedication newPrescribed)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (newPrescribed == null) throw new NullReferenceException(nameof(newPrescribed));

                var prescribed = dataDb.PrescribedMedication.Where(s => s.PatientId == patientId);
                if (prescribed == null) throw new NullReferenceException(nameof(prescribed));

                await dataDb.PrescribedMedication.AddAsync(newPrescribed);
                await Save();
            }
            catch (Exception e)
            {

                throw e;
            }
          
        }

        public async Task<PrescribedMedication> UpdateMedicationById(Guid patientId,PrescribedMedication updatePrescribed)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (updatePrescribed == null) throw new NullReferenceException(nameof(updatePrescribed));

                var currentPrescribed = GetMedicationById(patientId,updatePrescribed.PrescribedId);
                if (currentPrescribed == null) throw new NullReferenceException(nameof(currentPrescribed));

                dataDb.Entry(currentPrescribed).State = EntityState.Detached;
                dataDb.Entry(updatePrescribed).State = EntityState.Modified;
                return await GetMedicationById(patientId, updatePrescribed.PrescribedId);
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }

        private async Task<PrescribedMedication> GetMedicationById(Guid patientId,Guid prescribedId) 
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (prescribedId == null) throw new NullReferenceException(nameof(prescribedId));

                var currentPrescribed = await dataDb.PrescribedMedication.Where(s => s.PrescribedId == prescribedId).FirstOrDefaultAsync();
                if (currentPrescribed == null) throw new NullReferenceException(nameof(currentPrescribed));
                return currentPrescribed;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //Medication
        public IEnumerable<Medication> GetMedications
        {
            get
            {
                return dataDb.Medication.OrderBy(s => s.MedicationId);
            }
        }

        public IEnumerable<Medication> GetMedicationByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new NullReferenceException(nameof(name));

            var currentMedication = dataDb.Medication.Where(s => s.Display.Contains(name)).OrderBy(s=>s.MedicationId);
            if (currentMedication == null) throw new NullReferenceException(nameof(currentMedication));
            return currentMedication;
        }
        public async Task AddMedication(Medication newMedication)
        {
            if (newMedication == null) throw new NullReferenceException(nameof(newMedication));

            await dataDb.Medication.AddAsync(newMedication);
            await Save();
        }
        public async Task<Medication> UpdateMedication(Medication updateMedication,int medicationId)
        {
            if (medicationId == 0) throw new NullReferenceException(nameof(medicationId));
            if (updateMedication == null) throw new NullReferenceException(nameof(updateMedication));

            var currentMedication = await dataDb.Medication.Where(s => s.MedicationId == medicationId).FirstOrDefaultAsync();
            if (currentMedication == null) throw new NullReferenceException(nameof(currentMedication));

            dataDb.Entry(currentMedication).State = EntityState.Detached;
            dataDb.Entry(updateMedication).State = EntityState.Modified;
            return await dataDb.Medication.Where(s => s.MedicationId == medicationId).FirstOrDefaultAsync();
        }

        private async Task Save() 
        {
           await dataDb.SaveChangesAsync();
        }
    }
}
