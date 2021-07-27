using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using HospitalDataAPI.Model.LabModel;

namespace HospitalDataAPI.Service
{
   public interface ITest
    {
        //LabTest
        IEnumerable<LabTest> GetLabTestsById(Guid patientId);
        Task<LabTest> GetLabTestById(Guid patientId, Guid LabTestId);
        Task<LabTest> GetLabTestByCode(Guid patientId, string code);
        Task<LabTest> AddLabTestById(Guid patientId);
        Task<LabTest> UpdateLabTestById(Guid patientId);

        //Coding
        IEnumerable<Coding> GetCodings { get; }
        IEnumerable<Coding> GetCodingsByName(string name);

        //Category
        IEnumerable<Category> GetCategories { get; }
        IEnumerable<Category> GetCategoryByName(string name);
    }
}
