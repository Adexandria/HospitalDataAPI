using HospitalDataAPI.Model.LabModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalDataAPI.Service
{
   public interface IResult
    {
        IEnumerable<LabResult> GetLabResultsById(Guid patientId);
        IEnumerable<LabResult> GetLabResultsByTestId(Guid patientId, Guid testId);
        Task<LabResult> GetLabResultById(Guid patientId, Guid resultId);
        Task AddLabResultId(Guid patientId,LabResult newLabResult);
        Task<LabResult> UpdateLabResultId(Guid patientId,LabResult updateLabResult);
        Task DeleteLabResultById(Guid patientId, Guid testId);
    }
}
