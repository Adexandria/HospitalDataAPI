using HospitalDataAPI.Model.LabModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Service
{
    public class ResultRespository : IResult
    {
        readonly DataContext dataDb;
        public ResultRespository(DataContext dataDb)
        {
            this.dataDb = dataDb;
        }

        public IEnumerable<LabResult> GetLabResultsById(Guid patientId)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                var labResult = dataDb.LabResult.Where(s => s.PatientId == patientId)
                    .Include(s => s.Patient)
                    .Include(s => s.LabTest)
                    .Include(s => s.Code)
                    .Include(s => s.Category)
                    .Include(s => s.LabTest.Category)
                    .Include(s => s.LabTest.Code)
                    .Include(s => s.LabTest.Patient)
                    .AsNoTracking();
                //if (labResult == null) throw new NullReferenceException(nameof(labResult));
                return labResult;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public IEnumerable<LabResult> GetLabResultsByTestId(Guid patientId, Guid testId)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (testId == null) throw new NullReferenceException(nameof(testId));
                var labResult =  dataDb.LabResult.Where(s => s.PatientId == patientId).Where(s => s.TestId == testId).Include(s => s.Patient)
                    .Include(s => s.LabTest).Include(s => s.Code)
                    .Include(s => s.Category)
                    .Include(s => s.LabTest.Category)
                    .Include(s => s.LabTest.Code)
                    .Include(s => s.LabTest.Patient)
                    .AsNoTracking();
               
                return labResult;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<LabResult> GetLabResultById(Guid patientId, Guid resultId)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (resultId == null) throw new NullReferenceException(nameof(resultId));
                var labResult = await dataDb.LabResult.Where(s => s.PatientId == patientId).Where(s => s.ResultId == resultId).Include(s => s.Patient)
                    .Include(s => s.LabTest).Include(s => s.Code)
                    .Include(s => s.Category)
                    .Include(s => s.LabTest.Category)
                    .Include(s => s.LabTest.Code)
                    .Include(s => s.LabTest.Patient)
                    .AsNoTracking().FirstOrDefaultAsync();

                return labResult;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task AddLabResultId(Guid patientId,LabResult newLabResult)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (newLabResult == null) throw new NullReferenceException(nameof(newLabResult));
            
                newLabResult.ResultId = Guid.NewGuid();
                newLabResult.PatientId = patientId;
                await GetLabLibrary(newLabResult);
                await dataDb.LabResult.AddAsync(newLabResult);
                await Save();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }
        public async  Task<LabResult> UpdateLabResultId(Guid patientId, Guid testId, LabResult updateLabResult)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (updateLabResult == null) throw new NullReferenceException(nameof(updateLabResult));

                var currentLabResult = await GetLabResultById(patientId, testId);
               
                Update(currentLabResult, updateLabResult);
                dataDb.Entry(currentLabResult).State = EntityState.Detached;
                dataDb.Entry(updateLabResult).State = EntityState.Modified;
                await Save();
                return await GetLabResultById(patientId, testId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task DeleteLabResultById(Guid patientId,Guid resultId)
        {
            var currentLabResult = await GetLabResultById(patientId, resultId);
            dataDb.Entry(currentLabResult).State = EntityState.Deleted;
            await Save();
        }

        private async Task GetLabLibrary(LabResult labResult) 
        {
            var labTest =await dataDb.LabTest.Where(s => s.TestId == labResult.TestId).FirstOrDefaultAsync();
            labResult.CategoryId = labTest.CategoryId;
            labResult.CodeId = labTest.CodeId;

        }
        private async Task Save() 
        {
            await dataDb.SaveChangesAsync();
        }
        private void Update(LabResult currentResult, LabResult updateResult) 
        {
            updateResult.TestId = currentResult.TestId;
            updateResult.PatientId = currentResult.PatientId;
            updateResult.CategoryId = currentResult.CategoryId;
            updateResult.CodeId = currentResult.CodeId;
            if(updateResult.Status.ToString() == "Pending") 
            {
                updateResult.Status = currentResult.Status;
            }
            if(updateResult.ReferenceRange == "string" || string.IsNullOrWhiteSpace(updateResult.ReferenceRange))
            {
                updateResult.ReferenceRange = currentResult.ReferenceRange;
            }
            if (updateResult.ResultValue == "string" || string.IsNullOrWhiteSpace(updateResult.ResultValue))
            {
                updateResult.ReferenceRange = currentResult.ReferenceRange;
            }
            if(updateResult.ResultTime.Date == DateTime.Now.Date) 
            {
                updateResult.ResultTime = currentResult.ResultTime;
            }
        }

       
    }
}
