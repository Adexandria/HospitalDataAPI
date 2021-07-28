using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabResultUpdate
    {
        public Guid ResultId { get; set; }
        public string Status { get; set; }
        public string ResultValue { get; set; }
        public string ReferenceRange { get; set; }
        public string ResultTime { get; set; }
    }

}
