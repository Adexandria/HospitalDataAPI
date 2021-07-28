using HospitalDataAPI.Model.PatientModel;
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
        [ForeignKey("TestCode")]
        public int CodeId { get; set; }
        public virtual Coding Code { get; set; }
        public DateTime MeasuredTime { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public DateTime ReportedDate { get; set; }
        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
    }

}