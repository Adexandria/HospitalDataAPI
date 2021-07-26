using HospitalDataAPI.Model.PatientModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Model.LabModel
{
    public class LabResult
    {
        [Key]
        public Guid ResultId { get; set; }
        public LabStatus Status { get; set; }
        public ResultCode ResultCode { get; set; }
        public string ResultValue { get; set; }
        public string ReferenceRange { get; set; } = "5.0-8.0";
        public DateTime ResultTime { get; set; }
        public Category Category { get; set; }
        [ForeignKey("LabTest")]
        public Guid TestId { get; set; }
        public virtual LabTest LabTest { get; set; }
        [ForeignKey("Patient")]
        public  Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
