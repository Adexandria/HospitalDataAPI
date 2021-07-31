using HospitalDataAPI.Model.LabModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Service
{
    public class TestRespository : ITest
    {
        readonly DataContext dataDb;
        public TestRespository(DataContext dataDb)
        {
            this.dataDb = dataDb;
        }

        //LabTest
        public async Task<LabTest> GetLabTestByCode(Guid patientId, string code)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (string.IsNullOrWhiteSpace(code)) throw new NullReferenceException(nameof(code));
                var labTest = await dataDb.LabTest.Where(s => s.PatientId == patientId).Where(s => s.Code.Code.Contains(code))
                    .Include(s=>s.Patient).Include(s=>s.Category).Include(s=>s.Code).AsNoTracking().FirstOrDefaultAsync();
               // if (labTest == null) throw new NullReferenceException(nameof(labTest));
                return labTest;
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }

        public async Task<LabTest> GetLabTestById(Guid patientId, Guid labTestId)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (labTestId == null) throw new NullReferenceException(nameof(labTestId));
                var labTest = await dataDb.LabTest.Where(s => s.PatientId == patientId).Where(s => s.TestId == labTestId)
                    .Include(s => s.Category).Include(s => s.Code).AsNoTracking().FirstOrDefaultAsync();
                return labTest;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public IEnumerable<LabTest> GetLabTestsById(Guid patientId)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                var labTests = dataDb.LabTest.Where(s => s.PatientId == patientId).OrderBy(s => s.TestId)
                    .Include(s => s.Category).Include(s => s.Code).AsNoTracking();
                return labTests;
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }
        public async Task AddLabTestById(Guid patientId,LabTest newLabTest)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (newLabTest == null) throw new NullReferenceException(nameof(newLabTest));
                newLabTest.TestId = Guid.NewGuid();
                await dataDb.LabTest.AddAsync(newLabTest);
                await Save();
            }
            catch (Exception e)
            {

                throw e;
            }
          

        }
        public async Task<LabTest> UpdateLabTestById(Guid patientId,LabTest updateLabTest)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (updateLabTest == null) throw new NullReferenceException(nameof(updateLabTest));

                var currentLabTest =await GetLabTestById(patientId, updateLabTest.TestId);

                Update(currentLabTest, updateLabTest);
                dataDb.Entry(currentLabTest).State = EntityState.Detached;
                dataDb.Entry(updateLabTest).State = EntityState.Modified;
                await Save();
                return await GetLabTestById(patientId, updateLabTest.TestId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //Coding
        public IEnumerable<Coding> GetCodings
        {
            get
            {
                return dataDb.Coding.OrderBy(s => s.TestCodeId).AsNoTracking();
            }
        }
        public IEnumerable<Coding> GetCodingsByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name)) throw new NullReferenceException(nameof(name));
                return dataDb.Coding.Where(s => s.Text.Contains(name)).AsNoTracking();
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        //Category
        public IEnumerable<Category> GetCategories
        {
            get
            {
                return dataDb.Category.OrderBy(s => s.CategoryId).AsNoTracking();
            }
        }
        public IEnumerable<Category> GetCategoryByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name)) throw new NullReferenceException(nameof(name));
                return dataDb.Category.Where(s => s.Code.Contains(name)).AsNoTracking();
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        private async Task Save() 
        {
            await dataDb.SaveChangesAsync();
        }

        private void Update(LabTest currentTest, LabTest updatedTest) 
        {
            updatedTest.PatientId = currentTest.PatientId;
            updatedTest.CategoryId = currentTest.CategoryId;
            updatedTest.CodeId = currentTest.CodeId;
            if(updatedTest.MeasuredTime.Date == DateTime.Now.Date)
            {
                updatedTest.MeasuredTime = currentTest.MeasuredTime;
            }
            if(updatedTest.ReportedDate.Date == DateTime.Now.Date) 
            {
                updatedTest.ReportedDate = currentTest.ReportedDate;
            }
            if(updatedTest.Status.ToString() == "Pending") 
            {
                updatedTest.Status = currentTest.Status;
            }
           
        }
    }
}
