using HospitalDataAPI.Model.LabModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Service
{
   public interface IResult
    {
        IEnumerable<LabResult> GetLabResultById(Guid patientId);
        Task<LabResult> GetLabResult(Guid patientId, int testId);
        Task<LabResult> AddLabResultId(Guid patientId);
        Task<LabResult> UpdateLabResultId(Guid patientId);
    }
}
