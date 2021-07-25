using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.Patient
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string AddressBox { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Race Race { get; set; }
        public BirthSex BirthSex { get; set; }
    }
}
