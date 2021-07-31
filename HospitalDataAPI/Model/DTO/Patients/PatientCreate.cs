using System;
using System.ComponentModel.DataAnnotations;


namespace HospitalDataAPI.Model.DTO.Patients
{
    public class PatientCreate
    {
        [Required(ErrorMessage ="Enter FirstName")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Enter LastName")]
        public string LastName { get; set; }
        ///<example>male</example>
        public string Gender { get; set; }
        [Required(ErrorMessage = "Enter Date of Birth (yyyy/mm/dd)")]
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string AddressBox { get; set; }
        ///<example>single</example>
        public string MaritalStatus { get; set; }
        ///<example>black</example>
        public string Race { get; set; }
        ///<example>male</example>
        public string BirthSex { get; set; }
    }
}
