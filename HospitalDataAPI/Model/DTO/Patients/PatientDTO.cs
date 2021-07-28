using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.Patients
{
    public class PatientDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Phonenumber { get; set; }
        public string AddressBox { get; set; }
        public string MaritalStatus { get; set; } 
        public string Race { get; set; }
        public string BirthSex { get; set; }
    }
}
