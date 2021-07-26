using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalDataAPI.Model.LabModel
{
    public class LabTest
    {
        [Key]
        public Guid TestId { get; set; }
        public LabStatus Status { get; set; }
        public TestCode TestCode { get; set; }
        public DateTime MeasuredTime { get; set; }
        public Category Category { get; set; }
        public DateTime ReportedDate { get; set; }
        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
    }

}