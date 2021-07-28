using HospitalDataAPI.Model.PatientModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Service
{
    public class PatientRepository : IPatient
    {
        readonly DataContext dataDb;
        public PatientRepository(DataContext dataDb)
        {
            this.dataDb = dataDb;
        }
        public IEnumerable<Patient> GetPatients
        {
            get
            {
                return dataDb.Patient.OrderBy(s => s.FirstName).ThenByDescending(s => s.LastName).AsNoTracking();
            }
        }

        public async Task<Patient> GetPatientById(Guid patientId)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));

                var patient = await dataDb.Patient.Where(s => s.PatientId == patientId).AsNoTracking().FirstOrDefaultAsync();
                //if (patient == null) throw new NullReferenceException(nameof(patient));
                return patient;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public IEnumerable<Patient> GetPatientByName(string patientName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(patientName)) throw new NullReferenceException(nameof(patientName));

                var patient = dataDb.Patient.Where(s => s.LastName.Contains(patientName)).OrderBy(s=>s.PatientId).AsNoTracking();
                //if (patient == null) throw new NullReferenceException(nameof(patient));
                return patient;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public async Task AddPatient(Patient newPatient)
        {
            try
            {
                if (newPatient == null) throw new NullReferenceException(nameof(newPatient));

                newPatient.PatientId = Guid.NewGuid();
                await dataDb.Patient.AddAsync(newPatient);
                await Save();
            }
            catch (Exception e)
            {

                throw e;
            }
             
        }
        public async Task<Patient> UpdatePatient(Patient updatePatient, Guid patientId)
        {
            try
            {
                if (updatePatient == null) throw new NullReferenceException(nameof(updatePatient));
                if (patientId == null) throw new NullReferenceException(nameof(patientId));

                var currentPatient = await GetPatientById(patientId);
                //if (currentPatient == null) throw new NullReferenceException(nameof(currentPatient));

                updatePatient.PatientId = patientId;
                dataDb.Entry(currentPatient).State = EntityState.Detached;
                dataDb.Entry(updatePatient).State = EntityState.Modified;
                await Save();

                return await GetPatientById(patientId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task Save()
        {
          await dataDb.SaveChangesAsync(); 
        }

       /* private void Update(Patient currentPatient,Patient updatePatient) 
        {
            if(updatePatient.)
        }*/
        
    }
}
