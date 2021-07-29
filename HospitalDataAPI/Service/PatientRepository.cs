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
                Update(currentPatient, updatePatient);
                var query = dataDb.Patient.Attach(updatePatient);
                query.State = EntityState.Modified;
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

        private void Update(Patient currentPatient,Patient updatePatient) 
        {
            updatePatient.PatientId = currentPatient.PatientId;

            if(string.IsNullOrWhiteSpace(updatePatient.AddressBox) || updatePatient.AddressBox == "string") 
            {
                updatePatient.AddressBox = currentPatient.AddressBox;
            }
            if (string.IsNullOrWhiteSpace(updatePatient.FirstName) || updatePatient.FirstName == "string")
            {
                updatePatient.FirstName = currentPatient.FirstName;
            }
            if (string.IsNullOrWhiteSpace(updatePatient.LastName) || updatePatient.LastName == "string")
            {
                updatePatient.LastName = currentPatient.LastName;
            }
            if (string.IsNullOrWhiteSpace(updatePatient.MiddleName) || updatePatient.MiddleName == "string")
            {
                updatePatient.MiddleName = currentPatient.MiddleName;
            }
            if (string.IsNullOrWhiteSpace(updatePatient.Phonenumber) || updatePatient.Phonenumber == "string")
            {
                updatePatient.Phonenumber = currentPatient.Phonenumber;
            }
            if (updatePatient.Race.ToString() == "Black")
            {
                updatePatient.Race = currentPatient.Race;
            }
            if (updatePatient.MaritalStatus.ToString() == "Single")
            {
                updatePatient.MaritalStatus = currentPatient.MaritalStatus;
            }
            if (updatePatient.Gender.ToString() == "Male")
            {
                updatePatient.Gender = currentPatient.Gender;
            }
            if (updatePatient.BirthSex.ToString() == "Male")
            {
                updatePatient.BirthSex = currentPatient.BirthSex;
            }
            if(updatePatient.DateOfBirth.Date == new DateTime(0001, 1, 01)) 
            {
                updatePatient.DateOfBirth = currentPatient.DateOfBirth;
            }
        }
        
    }
}
