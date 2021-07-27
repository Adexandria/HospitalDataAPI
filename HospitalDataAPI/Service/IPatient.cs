﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using HospitalDataAPI.Model.PatientModel;

namespace HospitalDataAPI.Service
{
    public interface IPatient
    {
        IEnumerable<Patient> GetPatients { get; }
        Task<Patient> GetPatientById(Guid patientId);
        Task<Patient> GetPatientByName(string patientName);
        Task<Patient> AddPatient(Patient newPatient);
        Task<Patient> UpdatePatient(Patient updatePatient);
        Task Save(); 
    }
}
