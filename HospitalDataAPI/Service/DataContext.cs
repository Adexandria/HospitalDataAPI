using HospitalDataAPI.Model.LabModel;
using HospitalDataAPI.Model.MedicationModel;
using HospitalDataAPI.Model.PatientModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDataAPI.Service
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
                
        }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<PrescribedMedication> PrescribedMedication { get; set; }
        public DbSet<LabTest> LabTest { get; set; }
        public DbSet<LabResult> LabResult { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Coding> Coding { get; set; }
        public DbSet<Medication> Medication { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
