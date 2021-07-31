using System;


namespace HospitalDataAPI.Model.DTO.Patients
{
    public class PatientUpdate
    {
        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        ///<example>male</example>
        public string Gender { get; set; }
        ///<example>0001-01-01</example>
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string AddressBox { get; set; }
        /// <summary>
        /// Marital Status
        /// </summary>
        ///<example>single</example>
        public string MaritalStatus { get; set; }
        ///<example>black</example>
        public string Race { get; set; }
        ///<example>male</example>
        public string BirthSex { get; set; }
    }
}
