using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.DTO.LabDTO
{
    public class LabTestUpdate
    {
        public Guid TestId { get; set; }
        public string Status { get; set; }
        public DateTime MeasuredTime { get; set; }
        public DateTime ReportedDate { get; set; }
    }
}
