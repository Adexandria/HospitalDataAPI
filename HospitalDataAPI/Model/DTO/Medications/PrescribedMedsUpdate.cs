using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.Medications
{
    public class PrescribedMedsUpdate
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Prescriber { get; set; }
    }
}
