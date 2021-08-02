using HospitalDataAPI.Model.LabModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Service
{
   public interface IResult
    {
        IEnumerable<LabResult> GetLabResultsById(Guid patientId);
        Task<LabResult> GetLabResult(Guid patientId, Guid testId);
        Task AddLabResultId(Guid patientId,LabResult newLabResult);
        Task<LabResult> UpdateLabResultId(Guid patientId, Guid testId, LabResult updateLabResult);
        Task DeleteLabResultById(Guid patientId, Guid testId);
    }
}
