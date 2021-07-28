using System;


namespace HospitalDataAPI.Model.DTO.Patients
{
    public class PatientUpdate
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string AddressBox { get; set; }
        public string MaritalStatus { get; set; }
        public string Race { get; set; }
        public string BirthSex { get; set; }
    }
}
