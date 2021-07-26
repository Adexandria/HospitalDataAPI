using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalDataAPI.Model.PatientModel
{
    public class Patient
    {
        [Key]
        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string AddressBox { get; set; }
        public MaritalStatus MaritalStatus { get; set; } = MaritalStatus.None;
        public Race Race { get; set; } = Race.None;
        public BirthSex BirthSex { get; set; } = BirthSex.None;
    }
}
