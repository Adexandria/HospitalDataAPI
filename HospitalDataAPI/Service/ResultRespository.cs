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
                var labResult = dataDb.LabResult.Where(s => s.PatientId == patientId).AsNoTracking();
                if (labResult == null) throw new NullReferenceException(nameof(labResult));
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
                var labResult = await dataDb.LabResult.Where(s => s.PatientId == patientId).Where(s => s.TestId == testId).AsNoTracking().FirstOrDefaultAsync();
                if (labResult == null) throw new NullReferenceException(nameof(labResult));
                return labResult;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task AddLabResultId(Guid patientId, LabResult newLabResult)
        {
            try
            {
                if (patientId == null) throw new NullReferenceException(nameof(patientId));
                if (newLabResult == null) throw new NullReferenceException(nameof(newLabResult));

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
                if (currentLabResult == null) throw new NullReferenceException(nameof(currentLabResult));

                dataDb.Entry(currentLabResult).State = EntityState.Detached;
                dataDb.Entry(updateLabResult).State = EntityState.Detached;
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
    }
}
