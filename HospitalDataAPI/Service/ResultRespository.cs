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
                    .Include(s=>s.Patient)
                    .Include(s=>s.LabTest)
                    .Include(s => s.Code)
                    .AsNoTracking();
                //if (labResult == null) throw new NullReferenceException(nameof(labResult));
                return labResult;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public async Task<LabResult> GetLabResult(Guid patientId, Guid testId)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (testId == null) throw new NullReferenceException(nameof(testId));
                var labResult = await dataDb.LabResult.Where(s => s.PatientId == patientId).Where(s => s.TestId == testId).Include(s => s.Patient)
                    .Include(s => s.LabTest).Include(s => s.Code).AsNoTracking().FirstOrDefaultAsync();
               // if (labResult == null) throw new NullReferenceException(nameof(labResult));
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

                var currentLabResult = await GetLabResult(patientId, testId);
               
                Update(currentLabResult, updateLabResult);
                dataDb.Entry(currentLabResult).State = EntityState.Detached;
                dataDb.Entry(updateLabResult).State = EntityState.Detached;
                await Save();
                return await GetLabResult(patientId, testId);
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
