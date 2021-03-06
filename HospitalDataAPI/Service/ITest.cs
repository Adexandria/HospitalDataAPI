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
        Task<LabTest> GetLabTestById(Guid patientId, Guid labTestId);
        IEnumerable<LabTest> GetLabTestByCode(Guid patientId, string code);
        Task AddLabTestById(Guid patientId,LabTest newLabTest);
        Task<LabTest> UpdateLabTestById(Guid patientId, LabTest updateLabTest);

        //Coding
        IEnumerable<Coding> GetCodings { get; }
        IEnumerable<Coding> GetCodingsByName(string name);
        Task<Coding> GetCodingById(int labCodeId);

        //Category
        IEnumerable<Category> GetCategories { get; }
        IEnumerable<Category> GetCategoryByName(string name);
        Task<Category> GetCategoryById(int labCategoryId);
    }
}
