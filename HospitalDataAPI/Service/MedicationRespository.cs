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

                var prescribed = dataDb.PrescribedMedication.Where(s => s.PatientId == patientId).Include(s=>s.Patient).Include(s=>s.Medication);
                //if (prescribed == null) throw new NullReferenceException(nameof(prescribed));
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

                newPrescribed.PrescribedId = Guid.NewGuid();
                newPrescribed.PatientId = patientId;
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

                var currentPrescribed = await GetMedicationById(patientId,updatePrescribed.PrescribedId);
                // if (currentPrescribed == null) throw new NullReferenceException(nameof(currentPrescribed));
                UpdatePrescription(currentPrescribed, updatePrescribed);
                dataDb.Entry(currentPrescribed).State = EntityState.Detached;
                dataDb.Entry(updatePrescribed).State = EntityState.Modified;
                await Save();
                return await GetMedicationById(patientId, updatePrescribed.PrescribedId);
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }

        public async Task<PrescribedMedication> GetMedicationById(Guid patientId,Guid prescribedId) 
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (prescribedId == null) throw new NullReferenceException(nameof(prescribedId));

                var currentPrescribed = await dataDb.PrescribedMedication.Where(s => s.PrescribedId == prescribedId).Include(s=>s.Patient).Include(s=>s.Medication).FirstOrDefaultAsync();
                //if (currentPrescribed == null) throw new NullReferenceException(nameof(currentPrescribed));
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
            try
            {
                if (string.IsNullOrWhiteSpace(name)) throw new NullReferenceException(nameof(name));

                var currentMedication = dataDb.Medication.Where(s => s.Display.StartsWith(name)).OrderBy(s => s.MedicationId);
               // if (currentMedication == null) throw new NullReferenceException(nameof(currentMedication));
                return currentMedication;
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }
        public async Task AddMedication(Medication newMedication)
        {
            try
            {
                if (newMedication == null) throw new NullReferenceException(nameof(newMedication));

                await dataDb.Medication.AddAsync(newMedication);
                await Save();
            }
            catch (Exception e)
            {

                throw e;
            }
          
        }
        public async Task<Medication> UpdateMedication(Medication updateMedication,int medicationId)
        {
            try
            {
                if (medicationId == 0) throw new NullReferenceException(nameof(medicationId));
                if (updateMedication == null) throw new NullReferenceException(nameof(updateMedication));

                var currentMedication = await GetMedicationById(medicationId);
                // if (currentMedication == null) throw new NullReferenceException(nameof(currentMedication));
                Update(currentMedication, updateMedication);
                dataDb.Entry(currentMedication).State = EntityState.Detached;
                dataDb.Entry(updateMedication).State = EntityState.Modified;
                await Save();
                return await GetMedicationById(medicationId);
            }
            catch (Exception e)
            {

                throw e;
            }
          
        }
        public async Task<Medication> GetMedicationById(int medicationId)
        {
            if (medicationId == 0) throw new NullReferenceException(nameof(medicationId));
            return  await dataDb.Medication.Where(s => s.MedicationId == medicationId).FirstOrDefaultAsync();
        }

        public async Task DeleteMedication(int medicationId)
        {
            var currentMedication = await GetMedicationById(medicationId);
            dataDb.Medication.Remove(currentMedication);
            await Save();
        }
        private async Task Save() 
        {
           await dataDb.SaveChangesAsync();
        }

        private void Update(Medication currentmedication,Medication updateMedication)
        {
            updateMedication.MedicationId = currentmedication.MedicationId;
            if(string.IsNullOrWhiteSpace(updateMedication.Code)|| updateMedication.Code == "string") 
            {
                updateMedication.Code = currentmedication.Code;
            }
            if (string.IsNullOrWhiteSpace(updateMedication.Display) || updateMedication.Display == "string")
            {
                updateMedication.Display = currentmedication.Display;
            }

        }

        private void UpdatePrescription(PrescribedMedication currentMedication,PrescribedMedication updatemedication) 
        {
            updatemedication.PatientId = currentMedication.PatientId;
            if(updatemedication.MedicationId == 0) 
            {
                updatemedication.MedicationId = currentMedication.MedicationId;
            }
            if(updatemedication.Prescriber == "string" || string.IsNullOrWhiteSpace(updatemedication.Prescriber)) 
            {
                updatemedication.Prescriber = currentMedication.Prescriber;
            }
            if (updatemedication.Status.ToString() == "Active") 
            {
                updatemedication.Status = currentMedication.Status;
            }

        }
    }

}
